using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class VideoController : UserControl
    {
        private VideoCapture _videoCapture;
        private Timer _timer;
        private Mat _frame;

        public VideoController()
        {
            InitializeComponent();
            _frame = new Mat();
            _timer = new Timer();
            _timer.Interval = 33; // Aproximadamente 30 FPS
            _timer.Tick += Timer_Tick;
        }

        private void CargarVideoBtn_Click(object sender, EventArgs e)
        {
            // Abre el archivo de video
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de video|*.mp4;*.avi;*.mov;*.mkv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _videoCapture = new VideoCapture(openFileDialog.FileName);
                    _timer.Start(); // Comienza a reproducir el video
                }
            }
        }

        private void PausaBtn_Click(object sender, EventArgs e)
        {
            // Detiene el temporizador para pausar la reproducción del video
            _timer.Stop();
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            // Reinicia el temporizador para continuar la reproducción desde donde se pausó
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Lee el siguiente fotograma
            if (_videoCapture.Read(_frame) && !_frame.IsEmpty)
            {
                // Redimensiona el fotograma al tamaño del PictureBox
                var resizedFrame = _frame.ToImage<Bgr, byte>().Resize(VideoBox.Width, VideoBox.Height, Emgu.CV.CvEnum.Inter.Linear);

                // Convierte el fotograma redimensionado a Bitmap y lo muestra en el PictureBox
                VideoBox.Image?.Dispose(); // Libera la imagen anterior para evitar fugas de memoria
                VideoBox.Image = resizedFrame.ToBitmap(); // Asigna la imagen redimensionada al PictureBox

                // Generar histogramas para el fotograma actual
                GenerarHistogramaRGB(resizedFrame);
            }
            else
            {
                // Reinicia el video al primer fotograma
                _videoCapture.Set(Emgu.CV.CvEnum.CapProp.PosFrames, 0);

                // Continúa la reproducción
                _timer.Start(); // Reinicia el temporizador para que continúe el ciclo
            }
        }

        private void GenerarHistogramaRGB(Image<Bgr, byte> img)
        {
            // Obtener los datos de los tres canales
            var histR = new int[256];
            var histG = new int[256];
            var histB = new int[256];

            // Obtener la imagen de cada canal
            var imgR = img[0]; // Canal Rojo
            var imgG = img[1]; // Canal Verde
            var imgB = img[2]; // Canal Azul

            // Recorremos cada canal y contamos las frecuencias
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    histR[imgR.Data[y, x, 0]]++;
                    histG[imgG.Data[y, x, 0]]++;
                    histB[imgB.Data[y, x, 0]]++;
                }
            }

            // Dibujar los histogramas
            DibujarHistograma(HistogramBoxRed, histR, Color.Red);
            DibujarHistograma(HistogramBoxGreen, histG, Color.Green);
            DibujarHistograma(HistogramBoxBlue, histB, Color.Blue);
            DibujarHistogramaGeneral(HistogramBoxGeneral, histR, histG, histB);
        }

        // Método para liberar manualmente los recursos
        public void ReleaseResources()
        {
            _timer?.Stop(); // Detiene el temporizador
            _videoCapture?.Dispose(); // Libera el objeto VideoCapture
            _frame?.Dispose(); // Libera el objeto Mat
        }

        private void DibujarHistogramaGeneral(PictureBox pictureBox, int[] histR, int[] histG, int[] histB)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            Bitmap histImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(histImage);
            g.Clear(Color.White);

            // Encontrar la frecuencia máxima de los tres histogramas para normalizar
            int maxFrecuencia = Math.Max(Math.Max(histR.Max(), histG.Max()), histB.Max());
            if (maxFrecuencia == 0) return;

            // Escalar el ancho de cada barra al tamaño del PictureBox
            float barWidth = width / 256f;

            for (int i = 0; i < 256; i++)
            {
                // Normalizar la altura para cada canal y convertir a int
                int barHeightR = (int)((histR[i] / (float)maxFrecuencia) * (height - 5));
                int barHeightG = (int)((histG[i] / (float)maxFrecuencia) * (height - 5));
                int barHeightB = (int)((histB[i] / (float)maxFrecuencia) * (height - 5));

                int xPos = (int)(i * barWidth);

                // Dibujar barras con colores diferentes para cada canal
                g.FillRectangle(new SolidBrush(Color.Red), xPos, height - barHeightR, barWidth, barHeightR);
                g.FillRectangle(new SolidBrush(Color.Green), xPos, height - barHeightG, barWidth, barHeightG);
                g.FillRectangle(new SolidBrush(Color.Blue), xPos, height - barHeightB, barWidth, barHeightB);
            }

            pictureBox.Image = histImage;
        }

        private void DibujarHistograma(PictureBox pictureBox, int[] histograma, Color color)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            Bitmap histImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(histImage);
            g.Clear(Color.White);

            // Encontrar la frecuencia máxima para normalizar
            int maxFrecuencia = histograma.Max();
            if (maxFrecuencia == 0) return;

            // Escalar el ancho de cada barra al tamaño del PictureBox
            float barWidth = width / 256f;

            for (int i = 0; i < 256; i++)
            {
                // Normalizar la altura y convertir a int
                int barHeight = (int)((histograma[i] / (float)maxFrecuencia) * (height - 5));
                int xPos = (int)(i * barWidth);

                // Dibujar barra con color específico
                g.FillRectangle(new SolidBrush(color), xPos, height - barHeight, barWidth, barHeight);
            }

            pictureBox.Image = histImage;
        }


    }
}
