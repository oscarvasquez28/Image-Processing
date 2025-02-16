using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Image_Processing
{
    public partial class CameraController : UserControl
    {
        private VideoCapture _capture;
        private bool _cameraOn = false;
        private Image<Bgr, byte> imagenOriginal; // Para guardar la imagen original
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
            Espejo,
            Gamma,
            Termico
        }

        private int ActiveFilter = 0;

        public void setActiveFilter(int filter)
        {
            ActiveFilter = filter;
        }

        private void executeFilter(ref Image<Bgr, byte> img)
        {
            switch (ActiveFilter)
            {
                case (int)TipoFiltro.BlancoYNegro:
                    AplicarFiltroBlancoYNegro(ref img);
                    break;
                case (int)TipoFiltro.Negativo:
                    AplicarFiltroNegativo(ref img);
                    break;
                case (int)TipoFiltro.AltoContraste:
                    AplicarFiltroAltoContraste(ref img);
                    break;
                case (int)TipoFiltro.DesenfoqueGaussiano:
                    AplicarFiltroDesenfoqueGaussiano(ref img);
                    break;
                case (int)TipoFiltro.ResaltarBordes:
                    AplicarFiltroResaltarBordes(ref img);
                    break;
                case (int)TipoFiltro.Umbral:
                    AplicarFiltroUmbral(ref img);
                    break;
                case (int)TipoFiltro.Posterizar:
                    AplicarFiltroPosterizar(ref img);
                    break;
                case (int)TipoFiltro.ContrasteDinamico:
                    AplicarFiltroContrasteDinamico(ref img);
                    break;
                case (int)TipoFiltro.LenteDeGlobo:
                    AplicarFiltroLenteDeGlobo(ref img);
                    break;
                case (int)TipoFiltro.ColoracionAleatoria:
                    AplicarFiltroColoracionAleatoria(ref img);
                    break;
                case (int)TipoFiltro.Cristalizado:
                    AplicarFiltroCristalizado(ref img);
                    break;
                case (int)TipoFiltro.PapelRaspado:
                    AplicarFiltroPapelRaspado(ref img);
                    break;
                case (int)TipoFiltro.Espejo:
                    AplicarFiltroEspejo(ref img);
                    break;
                case (int)TipoFiltro.Gamma:
                    AplicarFiltroAjusteGamma(ref img);
                    break;
                case (int)TipoFiltro.Termico:
                    AplicarFiltroTermico(ref img);
                    break;
                default:
                    break;
            }
        }

        public CameraController()
        {
            InitializeComponent();
        }

        private void OnOffCameraBtn_Click(object sender, EventArgs e)
        {
            if (_cameraOn)
            {
                // Apagar cámara
                _capture.Stop();
                _capture.Dispose();
                CameraBox.Image = null;
                _cameraOn = false;
                OnOffCameraBtn.Text = "Encender Cámara";
            }
            else
            {
                // Encender cámara
                _capture = new VideoCapture(0); // 0 para cámara predeterminada
                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
                _cameraOn = true;
                OnOffCameraBtn.Text = "Apagar Cámara";
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                using (Mat frame = new Mat())
                {
                    _capture.Retrieve(frame);

                    // Convertir el fotograma a una imagen de Bgr
                    var img = frame.ToImage<Bgr, byte>();

                    // Guardar una copia de la imagen original solo la primera vez que se recibe un fotograma
                    if (imagenOriginal == null)
                    {
                        imagenOriginal = img.Copy(); // Guardar la imagen original
                    }

                    // Aplicar el filtro seleccionado
                    executeFilter(ref img);

                    // Redimensiona el cuadro del video para ajustarse al tamaño del PictureBox
                    var resizedFrame = img.Resize(CameraBox.Width, CameraBox.Height, Emgu.CV.CvEnum.Inter.Linear);

                    // Actualiza la imagen del PictureBox de la cámara
                    CameraBox.Image = resizedFrame.ToBitmap();

                    // Generar histogramas para el fotograma actual
                    GenerarHistogramaRGB(resizedFrame);
                }
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

            // Dibujar los histogramas en los PictureBox correspondientes
            DibujarHistograma(HistogramBoxRed, histR, Color.Red);
            DibujarHistograma(HistogramBoxGreen, histG, Color.Green);
            DibujarHistograma(HistogramBoxBlue, histB, Color.Blue);
            DibujarHistogramaGeneral(HistogramBoxGeneral, histR, histG, histB);
        }

        // Método para liberar manualmente los recursos
        public void ReleaseResources()
        {
            _capture?.Stop(); // Detiene la captura
            _capture?.Dispose(); // Libera el objeto VideoCapture
            CameraBox.Image?.Dispose(); // Libera la imagen de la cámara
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


        private void CameraController_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.Dispose();
            }
        }

        // Ejemplo de filtro Blanco y Negro
        private void AplicarFiltroBlancoYNegro(ref Image<Bgr, byte> img)
        {
            img = img.Convert<Gray, byte>().Convert<Bgr, byte>();
        }

        // Ejemplo de filtro negativo
        private void AplicarFiltroNegativo(ref Image<Bgr, byte> img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    img[y, x] = new Bgr(255 - color.Red, 255 - color.Green, 255 - color.Blue);
                }
            }
        }

        public void AplicarFiltroAltoContraste(ref Image<Bgr, byte> img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    int factor = 2;
                    img[y, x] = new Bgr(
                        Math.Min(255, color.Red * factor),
                        Math.Min(255, color.Green * factor),
                        Math.Min(255, color.Blue * factor)
                    );
                }
            }
        }

        // Función para limitar los valores RGB entre 0 y 255
        private int Clamp(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public void AplicarFiltroDesenfoqueGaussiano(ref Image<Bgr, byte> img)
        {
            var result = img.CopyBlank();
            double[,] kernel = {
        { 1, 4, 7, 4, 1 },
        { 4, 16, 26, 16, 4 },
        { 7, 26, 41, 26, 7 },
        { 4, 16, 26, 16, 4 },
        { 1, 4, 7, 4, 1 }
    };
            double kernelSum = 273.0;

            for (int y = 2; y < img.Height - 2; y++)
            {
                for (int x = 2; x < img.Width - 2; x++)
                {
                    double r = 0, g = 0, b = 0;

                    for (int ky = -2; ky <= 2; ky++)
                    {
                        for (int kx = -2; kx <= 2; kx++)
                        {
                            var color = img[y + ky, x + kx];
                            double weight = kernel[ky + 2, kx + 2];

                            r += color.Red * weight;
                            g += color.Green * weight;
                            b += color.Blue * weight;
                        }
                    }

                    result[y, x] = new Bgr(
                        Clamp((int)(r / kernelSum), 0, 255),
                        Clamp((int)(g / kernelSum), 0, 255),
                        Clamp((int)(b / kernelSum), 0, 255)
                    );
                }
            }

            img = result;
        }
        // Función para limitar valores térmicos
        private double ClampTermico(double value, double min, double max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public void AplicarFiltroTermico(ref Image<Bgr, byte> img)
        {
            var result = img.CopyBlank();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    double intensidad = 0.299 * color.Red + 0.587 * color.Green + 0.114 * color.Blue;
                    intensidad = ClampTermico(intensidad, 0, 255);
                    result[y, x] = ObtenerColorTermico(intensidad);
                }
            }

            img = result;
        }
        // Función para obtener el color térmico basado en la intensidad
        private Bgr ObtenerColorTermico(double intensidad)
        {
            if (intensidad < 64) return new Bgr(255, 0, 0);
            if (intensidad < 128) return new Bgr(255, 165, 0);
            if (intensidad < 192) return new Bgr(255, 255, 0);
            return new Bgr(0, 0, 255);
        }

        private void AplicarFiltroEspejo(ref Image<Bgr, byte> img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width / 2; x++)
                {
                    var color = img[y, x];
                    var mirrorColor = img[y, img.Width - x - 1];
                    img[y, x] = mirrorColor;
                    img[y, img.Width - x - 1] = color;
                }
            }
        }

        // Función para limitar los valores entre 0 y 255 (para resaltar bordes)
        private byte ClampToByte(double value)
        {
            return (byte)Math.Min(Math.Max(value, 0), 255);
        }
        public void AplicarFiltroResaltarBordes(ref Image<Bgr, byte> img)
        {
            var width = img.Width;
            var height = img.Height;
            var result = new Image<Bgr, byte>(width, height);

            int[,] kernel = new int[3, 3] {
        { -1, -1, -1 },
        { -1,  8, -1 },
        { -1, -1, -1 }
    };

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double blue = 0, green = 0, red = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            var color = img[y + ky, x + kx];
                            blue += color.Blue * kernel[ky + 1, kx + 1];
                            green += color.Green * kernel[ky + 1, kx + 1];
                            red += color.Red * kernel[ky + 1, kx + 1];
                        }
                    }

                    result[y, x] = new Bgr(
                        ClampToByte(red),
                        ClampToByte(green),
                        ClampToByte(blue)
                    );
                }
            }

            img = result;
        }

        private void AplicarFiltroUmbral(ref Image<Bgr, byte> img)
        {
            byte umbral = 128; // Ajusta el valor del umbral
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    byte gray = (byte)((color.Red + color.Green + color.Blue) / 3);
                    byte valor = gray > umbral ? (byte)255 : (byte)0;
                    img[y, x] = new Bgr(valor, valor, valor);
                }
            }
        }
        public void AplicarFiltroPosterizar(ref Image<Bgr, byte> img)
        {
            int niveles = 4;

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    img[y, x] = new Bgr(
                        (byte)((color.Red / (256 / niveles)) * (256 / niveles)),
                        (byte)((color.Green / (256 / niveles)) * (256 / niveles)),
                        (byte)((color.Blue / (256 / niveles)) * (256 / niveles))
                    );
                }
            }
        }
        private void AplicarFiltroContrasteDinamico(ref Image<Bgr, byte> img)
        {
            byte max = 255;
            byte min = 0;
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    byte r = (byte)Math.Min(max, Math.Max(min, color.Red * 1.5));
                    byte g = (byte)Math.Min(max, Math.Max(min, color.Green * 1.5));
                    byte b = (byte)Math.Min(max, Math.Max(min, color.Blue * 1.5));
                    img[y, x] = new Bgr(r, g, b);
                }
            }
        }
        private void AplicarFiltroLenteDeGlobo(ref Image<Bgr, byte> img)
        {
            int radio = 100; // Ajusta el radio de la lente
            int centroX = img.Width / 2;
            int centroY = img.Height / 2;
            double factorDeIntensidad = 1.2;

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    double distancia = Math.Sqrt(Math.Pow(x - centroX, 2) + Math.Pow(y - centroY, 2));
                    double factor = Math.Min(1, factorDeIntensidad / (1 + distancia / radio));

                    var color = img[y, x];
                    byte r = (byte)Math.Min(255, color.Red * factor);
                    byte g = (byte)Math.Min(255, color.Green * factor);
                    byte b = (byte)Math.Min(255, color.Blue * factor);
                    img[y, x] = new Bgr(r, g, b);
                }
            }
        }
        private void AplicarFiltroColoracionAleatoria(ref Image<Bgr, byte> img)
        {
            Random rand = new Random();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    byte r = (byte)rand.Next(0, 256);
                    byte g = (byte)rand.Next(0, 256);
                    byte b = (byte)rand.Next(0, 256);
                    img[y, x] = new Bgr(r, g, b);
                }
            }
        }
        private void AplicarFiltroCristalizado(ref Image<Bgr, byte> img)
        {
            Random rand = new Random();
            int step = 5; // Tamaño del bloque de cristal
            for (int y = 0; y < img.Height; y += step)
            {
                for (int x = 0; x < img.Width; x += step)
                {
                    int offsetX = rand.Next(-2, 3);
                    int offsetY = rand.Next(-2, 3);
                    int newX = Math.Min(img.Width - 1, Math.Max(0, x + offsetX));
                    int newY = Math.Min(img.Height - 1, Math.Max(0, y + offsetY));
                    img[y, x] = img[newY, newX];
                }
            }
        }
        private void AplicarFiltroPapelRaspado(ref Image<Bgr, byte> img)
        {
            Random rand = new Random();
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    // Convertimos explícitamente a int para evitar problemas de tipo
                    int r = (int)img[y, x].Red + rand.Next(-50, 50);  // Ajusta el rango de la aleatoriedad
                    int g = (int)img[y, x].Green + rand.Next(-50, 50);
                    int b = (int)img[y, x].Blue + rand.Next(-50, 50);

                    // Aseguramos que los valores estén dentro del rango de 0 a 255
                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));

                    // Asignamos el color final a la imagen
                    img[y, x] = new Bgr((byte)r, (byte)g, (byte)b);
                }
            }
        }

        public void AplicarFiltroAjusteGamma(ref Image<Bgr, byte> img)
        {
            double gamma = 0.5;

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    img[y, x] = new Bgr(
                        (byte)(255 * Math.Pow(color.Red / 255.0, gamma)),
                        (byte)(255 * Math.Pow(color.Green / 255.0, gamma)),
                        (byte)(255 * Math.Pow(color.Blue / 255.0, gamma))
                    );
                }
            }
        }

        public void ResetearFiltros()
        {
            // Restablecer el valor de los filtros a Ninguno
            ActiveFilter = (int)TipoFiltro.Ninguno;

            // Verificar si el frame está vacío
            if (_capture == null || _capture.Ptr == IntPtr.Zero)
            {
                MessageBox.Show("No hay video o imagen cargada.");
                return;
            }

            using (Mat frame = new Mat())
            {
                // Captura el fotograma actual
                _capture.Retrieve(frame);

                // Convertir el fotograma a una imagen de Bgr
                var img = frame.ToImage<Bgr, byte>();

                // Si no hay una imagen original guardada, lo guardamos ahora
                if (imagenOriginal == null)
                {
                    imagenOriginal = img.Copy(); // Guardar la imagen original
                }

                // Redimensiona el fotograma y mostrarlo sin aplicar ningún filtro
                var resizedFrame = img.Resize(CameraBox.Width, CameraBox.Height, Emgu.CV.CvEnum.Inter.Linear);

                // Verificar si ya existe una imagen y liberarla de manera segura
                if (CameraBox.Image != null)
                {
                    CameraBox.Image.Dispose(); // Liberar la imagen anterior solo si es necesario
                }

                // Actualizar el PictureBox con la imagen sin filtro
                CameraBox.Image = resizedFrame.ToBitmap();
            }
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

        private void ResetFilters_Click(object sender, EventArgs e)
        {
            ResetearFiltros();
        }
    }
}
