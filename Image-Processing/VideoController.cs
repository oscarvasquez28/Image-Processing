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
            Espejo,
            Gamma,
            Termico
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
                case (int)TipoFiltro.Gamma:
                    AplicarFiltroAjusteGamma();
                    break;
                case (int)TipoFiltro.Termico:
                    AplicarFiltroTermico();
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

            // Recorrer cada píxel y ajustar el contraste
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    // Aumentar contraste multiplicando los valores de los canales por un factor
                    int factor = 2; // Puedes ajustar este valor para cambiar el nivel de contraste
                    var highContrastColor = new Bgr(
                        Math.Min(255, color.Red * factor),
                        Math.Min(255, color.Green * factor),
                        Math.Min(255, color.Blue * factor)
                    );
                    img[y, x] = highContrastColor; // Asignamos el color con alto contraste
                }
            }

            // Actualizamos el fotograma con la imagen de alto contraste
            _frame = img.Mat;
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public void AplicarFiltroDesenfoqueGaussiano()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var result = img.CopyBlank();

            // Kernel Gaussiano 5x5
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
                            var pixel = img[y + ky, x + kx];
                            double weight = kernel[ky + 2, kx + 2];

                            r += pixel.Red * weight;
                            g += pixel.Green * weight;
                            b += pixel.Blue * weight;
                        }
                    }

                    int finalR = Clamp((int)(r / kernelSum), 0, 255);
                    int finalG = Clamp((int)(g / kernelSum), 0, 255);
                    int finalB = Clamp((int)(b / kernelSum), 0, 255);

                    result[y, x] = new Bgr(finalB, finalG, finalR);
                }
            }

            _frame = result.Mat;
        }




        public byte ClampToByte(double value)
        {
            return (byte)Math.Max(0, Math.Min(255, value));
        }

        public void AplicarFiltroResaltarBordes()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            // Kernel para detección de bordes
            int[,] kernel = new int[3, 3]
            {
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

            _frame = result.Mat;
        }

        public void AplicarFiltroUmbral()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            int umbral = 128; // Valor fijo del umbral

            // Convertir el fotograma actual a imagen en escala de grises
            var img = _frame.ToImage<Bgr, byte>().Convert<Gray, byte>();

            // Recorrer cada píxel de la imagen
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    // Obtener el valor de intensidad del píxel (escala de grises)
                    byte pixelIntensity = (byte)img[y, x].Intensity;  // Conversión explícita

                    // Convertir el valor de intensidad a blanco (255) o negro (0) según el umbral
                    byte result = pixelIntensity >= umbral ? (byte)255 : (byte)0;

                    // Asignamos el valor resultante al píxel
                    img[y, x] = new Gray(result);
                }
            }

            // Actualizar el fotograma con el resultado del filtro de umbral
            _frame = img.Mat;
        }


        public void AplicarFiltroAjusteGamma()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            // Definir el valor gamma dentro de la función
            double gamma = 0.5; // Puedes ajustar este valor para modificar el efecto

            // Convertir el fotograma actual a imagen Bgr
            var img = _frame.ToImage<Bgr, byte>();

            // Recorrer cada píxel y aplicar la corrección gamma
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];

                    // Aplicar la fórmula de corrección gamma a cada componente de color
                    color.Red = (byte)(255 * Math.Pow(color.Red / 255.0, gamma));
                    color.Green = (byte)(255 * Math.Pow(color.Green / 255.0, gamma));
                    color.Blue = (byte)(255 * Math.Pow(color.Blue / 255.0, gamma));

                    img[y, x] = color; // Asignamos el nuevo color
                }
            }

            // Reemplazamos el fotograma original con el resultado modificado
            _frame = img.Mat;
        }
        private double ClampTermico(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public void AplicarFiltroTermico()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var result = img.CopyBlank();

            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];

                    // Convertir a escala de grises (intensidad)
                    double intensidad = 0.299 * color.Red + 0.587 * color.Green + 0.114 * color.Blue;
                    intensidad = ClampTermico(intensidad, 0, 255);

                    Bgr thermalColor = ObtenerColorTermico(intensidad);
                    result[y, x] = thermalColor;
                }
            }

            _frame = result.Mat;
        }

        // Función para asignar un color térmico basado en la intensidad
        private Bgr ObtenerColorTermico(double intensidad)
        {
            if (intensidad < 64) return new Bgr(255, 0, 0);         // Rojo intenso
            if (intensidad < 128) return new Bgr(255, 165, 0);      // Naranja
            if (intensidad < 192) return new Bgr(255, 255, 0);      // Amarillo
            return new Bgr(0, 0, 255);                              // Azul frío
        }



        public void AplicarFiltroPosterizar()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            int niveles = 4; // Número de niveles de colores

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = img[y, x];
                    result[y, x] = new Bgr(
                        (byte)(color.Blue / niveles * niveles),
                        (byte)(color.Green / niveles * niveles),
                        (byte)(color.Red / niveles * niveles)
                    );
                }
            }

            _frame = result.Mat;
        }


        public void AplicarFiltroContrasteDinamico()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            double min = 255, max = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = img[y, x];
                    double gray = 0.299 * color.Red + 0.587 * color.Green + 0.114 * color.Blue;
                    min = Math.Min(min, gray);
                    max = Math.Max(max, gray);
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var color = img[y, x];
                    double gray = 0.299 * color.Red + 0.587 * color.Green + 0.114 * color.Blue;
                    double normalized = (gray - min) / (max - min) * 255;
                    result[y, x] = new Bgr(normalized, normalized, normalized);
                }
            }

            _frame = result.Mat;
        }

        public void AplicarFiltroLenteDeGlobo()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            int centroX = width / 2;
            int centroY = height / 2;
            double radio = Math.Min(width, height) / 4;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double distancia = Math.Sqrt(Math.Pow(x - centroX, 2) + Math.Pow(y - centroY, 2));
                    if (distancia < radio)
                    {
                        double factor = 1 - (distancia / radio);
                        int newX = centroX + (int)((x - centroX) * factor);
                        int newY = centroY + (int)((y - centroY) * factor);

                        newX = Math.Max(0, Math.Min(newX, width - 1));
                        newY = Math.Max(0, Math.Min(newY, height - 1));

                        result[y, x] = img[newY, newX];
                    }
                    else
                    {
                        result[y, x] = img[y, x];
                    }
                }
            }

            _frame = result.Mat;
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

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);
            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int offsetX = rand.Next(-5, 6);
                    int offsetY = rand.Next(-5, 6);

                    int newX = Math.Max(0, Math.Min(x + offsetX, width - 1));
                    int newY = Math.Max(0, Math.Min(y + offsetY, height - 1));

                    result[y, x] = img[newY, newX];
                }
            }

            _frame = result.Mat;
        }





        public void AplicarFiltroPapelRaspado()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);
            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int noise = rand.Next(-20, 21);
                    var color = img[y, x];
                    result[y, x] = new Bgr(
                        ClampToByte(color.Blue + noise),
                        ClampToByte(color.Green + noise),
                        ClampToByte(color.Red + noise)
                    );
                }
            }

            _frame = result.Mat;
        }

        public void AplicarFiltroEspejo()
        {
            if (_frame.IsEmpty)
            {
                MessageBox.Show("No hay video cargado.");
                return;
            }

            var img = _frame.ToImage<Bgr, byte>();
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[y, width - x - 1] = img[y, x];
                }
            }

            _frame = result.Mat;
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
