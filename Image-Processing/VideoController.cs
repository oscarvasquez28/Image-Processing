using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Lee el siguiente fotograma
            if (_videoCapture.Read(_frame) && !_frame.IsEmpty)
            {
                // Redimensiona la imagen para que se ajuste al tamaño del PictureBox
                Bitmap resizedImage = new Bitmap(_frame.ToBitmap(), VideoBox.Size);

                // Libera la imagen anterior para evitar fugas de memoria
                VideoBox.Image?.Dispose();

                // Asigna la nueva imagen redimensionada
                VideoBox.Image = resizedImage;
            }
            else
            {
                _timer.Stop(); // Detiene el temporizador cuando el video se haya terminado
            }
        }


        // Método para liberar manualmente los recursos
        public void ReleaseResources()
        {
            _timer?.Stop(); // Detiene el temporizador
            _videoCapture?.Dispose(); // Libera el objeto VideoCapture
            _frame?.Dispose(); // Libera el objeto Mat
        }
    }
}
