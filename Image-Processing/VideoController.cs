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

        public enum TipoFiltro
        {
            Ninguno,
            BlancoYNegro,
            Negativo,
            AltoContraste,
            DesenfoqueGaussiano,
            ResaltarBordes,
            Umbral,
            Posterizar,
            ContrasteDinamico,
            LenteDeGlobo,
            ColoracionAleatoria,
            Cristalizado,
            PapelRaspado,
            Espejo
        }

        private int ActiveFilter = 0;

        public void setActiveFilter(int filter)
        {
            ActiveFilter = filter;
        }

        private void executeFilter()
        {
            switch (ActiveFilter)
            {
                case (int)TipoFiltro.BlancoYNegro:
                    AplicarFiltroBlancoYNegro();
                    break;
                case (int)TipoFiltro.Negativo:
                    AplicarFiltroNegativo();
                    break;
                case (int)TipoFiltro.AltoContraste:
                    AplicarFiltroAltoContraste();
                    break;
                case (int)TipoFiltro.DesenfoqueGaussiano:
                    AplicarFiltroDesenfoqueGaussiano();
                    break;
                case (int)TipoFiltro.ResaltarBordes:
                    AplicarFiltroResaltarBordes();
                    break;
                case (int)TipoFiltro.Umbral:
                    AplicarFiltroUmbral();
                    break;
                case (int)TipoFiltro.Posterizar:
                    AplicarFiltroPosterizar();
                    break;
                case (int)TipoFiltro.ContrasteDinamico:
                    AplicarFiltroContrasteDinamico();
                    break;
                case (int)TipoFiltro.LenteDeGlobo:
                    AplicarFiltroLenteDeGlobo();
                    break;
                case (int)TipoFiltro.ColoracionAleatoria:
                    AplicarFiltroColoracionAleatoria();
                    break;
                case (int)TipoFiltro.Cristalizado:
                    AplicarFiltroCristalizado();
                    break;
                case (int)TipoFiltro.PapelRaspado:
                    AplicarFiltroPapelRaspado();
                    break;
                case (int)TipoFiltro.Espejo:
                    AplicarFiltroEspejo();
                    break;
                default:
                    break;
            }
        }

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
                // Aplica el filtro blanco y negro aquí antes de mostrarlo
                executeFilter();

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
        // Método para aplicar filtro blanco y negro al video
        // Método para aplicar filtro blanco y negro al video
        public void AplicarFiltroBlancoYNegro()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            // Convertir el fotograma actual a Image<Bgr, byte>
            var img = _frame.ToImage<Bgr, byte>();

            // Convertir cada píxel a blanco y negro
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    int grayValue = (int)(color.Red * 0.3 + color.Green * 0.59 + color.Blue * 0.11); // Fórmula de escala de grises
                    var grayColor = new Bgr(grayValue, grayValue, grayValue); // Asignamos el valor en gris
                    img[y, x] = grayColor; // Asignamos el valor de color gris
                }
            }

            // Reemplazamos el fotograma original con la versión en blanco y negro
            _frame = img.Mat; // Actualizamos _frame con la versión modificada
        }


        // Método para aplicar filtro negativo al video
        public void AplicarFiltroNegativo()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            // Convertir el fotograma actual a Image<Bgr, byte>
            var img = _frame.ToImage<Bgr, byte>();

            // Convertir cada píxel a negativo
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    var invertedColor = new Bgr(255 - color.Red, 255 - color.Green, 255 - color.Blue);
                    img[y, x] = invertedColor; // Asignamos el color invertido
                }
            }

            // Reemplazamos el fotograma original con la versión en blanco y negro
            _frame = img.Mat; // Actualizamos _frame con la versión modificada
        }

        public void AplicarFiltroAltoContraste()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    int red = Math.Min(255, (int)(color.Red * 2.0)); // Aumentamos el contraste más fuerte
                    int green = Math.Min(255, (int)(color.Green * 2.0));
                    int blue = Math.Min(255, (int)(color.Blue * 2.0));
                    img[y, x] = new Bgr(red, green, blue);
                }
            }
            _frame = img.Mat;
        }

        public void AplicarFiltroDesenfoqueGaussiano()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            int kernelSize = 5; // Tamaño del kernel

            // Creamos un filtro gaussiano simple
            double[,] kernel = {
        { 1,  4,  6,  4, 1 },
        { 4, 16, 24, 16, 4 },
        { 6, 24, 36, 24, 6 },
        { 4, 16, 24, 16, 4 },
        { 1,  4,  6,  4, 1 }
    };

            // Normalizamos el kernel
            double kernelSum = kernel.Cast<double>().Sum();
            for (int i = 0; i < kernel.GetLength(0); i++)
            {
                for (int j = 0; j < kernel.GetLength(1); j++)
                {
                    kernel[i, j] /= kernelSum;
                }
            }

            var result = img.Clone();

            // Aplicamos el filtro gaussiano manualmente
            for (int y = kernelSize / 2; y < img.Height - kernelSize / 2; y++)
            {
                for (int x = kernelSize / 2; x < img.Width - kernelSize / 2; x++)
                {
                    double red = 0, green = 0, blue = 0;

                    for (int ky = -kernelSize / 2; ky <= kernelSize / 2; ky++)
                    {
                        for (int kx = -kernelSize / 2; kx <= kernelSize / 2; kx++)
                        {
                            var color = img[y + ky, x + kx];
                            red += color.Red * kernel[ky + kernelSize / 2, kx + kernelSize / 2];
                            green += color.Green * kernel[ky + kernelSize / 2, kx + kernelSize / 2];
                            blue += color.Blue * kernel[ky + kernelSize / 2, kx + kernelSize / 2];
                        }
                    }

                    result[y, x] = new Bgr(
                        Math.Min(255, Math.Max(0, (int)red)),
                        Math.Min(255, Math.Max(0, (int)green)),
                        Math.Min(255, Math.Max(0, (int)blue))
                    );
                }
            }

            _frame = result.Mat;
        }

        public void AplicarFiltroResaltarBordes()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var grayImg = img.Convert<Gray, byte>();

            int[,] sobelX = {
        { -1, 0, 1 },
        { -2, 0, 2 },
        { -1, 0, 1 }
    };

            int[,] sobelY = {
        { -1, -2, -1 },
        {  0,  0,  0 },
        {  1,  2,  1 }
    };

            var result = grayImg.Clone();

            for (int y = 1; y < grayImg.Height - 1; y++)
            {
                for (int x = 1; x < grayImg.Width - 1; x++)
                {
                    double gradientX = 0, gradientY = 0;

                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            gradientX += grayImg[y + ky, x + kx].Intensity * sobelX[ky + 1, kx + 1];
                            gradientY += grayImg[y + ky, x + kx].Intensity * sobelY[ky + 1, kx + 1];
                        }
                    }

                    double gradientMagnitude = Math.Sqrt(gradientX * gradientX + gradientY * gradientY);
                    result[y, x] = new Gray(Math.Min(255, Math.Max(0, gradientMagnitude)));
                }
            }

            _frame = result.Mat;
        }


        public void AplicarFiltroUmbral()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var grayImg = img.Convert<Gray, byte>();

            for (int y = 0; y < grayImg.Height; y++)
            {
                for (int x = 0; x < grayImg.Width; x++)
                {
                    double pixel = grayImg[y, x].Intensity;  // Obtener el valor de intensidad como double
                    byte pixelByte = (byte)Math.Min(255, Math.Max(0, pixel)); // Convertir a byte y asegurarse de que esté en el rango [0, 255]
                    grayImg[y, x] = (pixelByte > 100) ? new Gray(255) : new Gray(0);  // Asignar nuevo valor de intensidad
                }
            }

            _frame = grayImg.Mat;
        }




        public void AplicarFiltroPosterizar()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    int red = (int)((color.Red / 85) * 85);  // Convertir a int
                    int green = (int)((color.Green / 85) * 85);  // Convertir a int
                    int blue = (int)((color.Blue / 85) * 85);  // Convertir a int
                    img[y, x] = new Bgr(red, green, blue);
                }
            }
            _frame = img.Mat;
        }

        public void AplicarFiltroContrasteDinamico()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    int red = Math.Min(255, (int)(color.Red * 1.5));
                    int green = Math.Min(255, (int)(color.Green * 1.5));
                    int blue = Math.Min(255, (int)(color.Blue * 1.5));
                    img[y, x] = new Bgr(red, green, blue);
                }
            }
            _frame = img.Mat;
        }
        public void AplicarFiltroLenteDeGlobo()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>(); // Convertimos la imagen a un objeto Bgr de EmguCV
            var resized = new Image<Bgr, byte>(img.Width / 2, img.Height / 2); // Imagen reducida
            var expanded = new Image<Bgr, byte>(img.Width, img.Height); // Imagen expandida

            // Reducción de la imagen (tomamos cada segundo píxel)
            for (int y = 0; y < img.Height / 2; y++)
            {
                for (int x = 0; x < img.Width / 2; x++)
                {
                    resized[y, x] = img[y * 2, x * 2];
                }
            }

            // Expansión de la imagen (duplicamos los píxeles para rellenar el tamaño original)
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    // Interpolamos (simplemente copiamos el píxel más cercano)
                    int srcY = y / 2;
                    int srcX = x / 2;
                    expanded[y, x] = resized[srcY, srcX];
                }
            }

            _frame = expanded.Mat; // Asignamos la imagen expandida a _frame
        }


        public void AplicarFiltroColoracionAleatoria()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            Random rand = new Random();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    img[y, x] = new Bgr(rand.Next(256), rand.Next(256), rand.Next(256));
                }
            }
            _frame = img.Mat;
        }
        public void AplicarFiltroCristalizado()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>(); // Convertimos la imagen a un objeto Bgr de EmguCV.
            int newWidth = img.Width / 2;
            int newHeight = img.Height / 2;

            // Reducción de la imagen (PyrDown)
            var reducedImg = new Image<Bgr, byte>(newWidth, newHeight);
            CvInvoke.PyrDown(img, reducedImg); // Usamos PyrDown para reducir la imagen

            // Ampliación de la imagen (PyrUp)
            var expandedImg = new Image<Bgr, byte>(img.Width, img.Height);
            CvInvoke.PyrUp(reducedImg, expandedImg); // Usamos PyrUp para ampliar la imagen

            // Asignamos la imagen ampliada a _frame
            _frame = expandedImg.Mat;
        }




        public void AplicarFiltroPapelRaspado()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    img[y, x] = new Bgr(
                        Math.Min(255, color.Red + 7),
                        Math.Min(255, color.Green + 7),
                        Math.Min(255, color.Blue + 7)
                    );
                }
            }
            _frame = img.Mat;
        }
        public void AplicarFiltroEspejo()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width / 2; x++)
                {
                    var temp = img[y, x];
                    img[y, x] = img[y, img.Width - 1 - x];
                    img[y, img.Width - 1 - x] = temp;
                }
            }
            _frame = img.Mat;
        }

        public void ResetearFiltros()
        {
            // Restablece el valor de los filtros a Ninguno
            ActiveFilter = (int)TipoFiltro.Ninguno;

            // Vuelve a cargar el fotograma original (sin filtro) en el control VideoBox
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>(); // Obtener la imagen original del fotograma

            // Redimensionar el fotograma y mostrarlo sin aplicar ningún filtro
            var resizedFrame = img.Resize(VideoBox.Width, VideoBox.Height, Emgu.CV.CvEnum.Inter.Linear);

            // Verificar si ya existe una imagen y liberarla de manera segura
            if (VideoBox.Image != null)
            {
                VideoBox.Image.Dispose(); // Liberar la imagen anterior solo si es necesario
            }

            // Actualizar el PictureBox con la imagen sin filtro
            VideoBox.Image = resizedFrame.ToBitmap();

            // Limpiar histogramas o cualquier otro dato relacionado con los filtros
            LimpiarHistogramas();
        }

        private void LimpiarHistogramas()
        {
            // Limpia los histogramas en el PictureBox
            HistogramBoxRed.Image = null;
            HistogramBoxGreen.Image = null;
            HistogramBoxBlue.Image = null;
            HistogramBoxGeneral.Image = null;
        }


        private void ResetVideo_Click(object sender, EventArgs e)
        {
            ResetearFiltros();
        }
    }
}
