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
        private Bgr? selectedColor = null; // Color seleccionado, null por defecto
        private bool ColorSeleecionar = false; // Bandera para activar la selección de color
        private double colorTolerance = 100.0; // Tolerancia para la comparación de colores


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

                    // Aplicar el filtro de color seleccionado si hay un color seleccionado
                    if (selectedColor.HasValue && ColorSeleecionar)
                    {
                        AplicarFiltroColorSeleccionado(ref img);
                    }

                    // Redimensiona el cuadro del video para ajustarse al tamaño del PictureBox
                    var resizedFrame = img.Resize(CameraBox.Width, CameraBox.Height, Emgu.CV.CvEnum.Inter.Linear);

                    // Actualiza la imagen del PictureBox de la cámara
                    CameraBox.Image = resizedFrame.ToBitmap();

                    // Generar histogramas para el fotograma actual
                    GenerarHistogramaRGB(resizedFrame);
                }
            }
        }

        private void AplicarFiltroColorSeleccionado(ref Image<Bgr, byte> img)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    var color = img[y, x];
                    if (IsColorSimilar(color, selectedColor.Value, colorTolerance))
                    {
                        img[y, x] = selectedColor.Value;
                    }
                    else
                    {
                        img[y, x] = new Bgr(255, 255, 255); // Pintar de blanco
                    }
                }
            }
        }

        private bool IsColorSimilar(Bgr color1, Bgr color2, double tolerance)
        {
            double diff = Math.Sqrt(
                Math.Pow(color1.Blue - color2.Blue, 2) +
                Math.Pow(color1.Green - color2.Green, 2) +
                Math.Pow(color1.Red - color2.Red, 2)
            );
            return diff <= tolerance;
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
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void AplicarFiltroDesenfoqueGaussiano(ref Image<Bgr, byte> img)
        {
            // Crear una copia en blanco de la imagen de salida
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

            // Aplicar el filtro Gaussiano a cada píxel
            for (int y = 2; y < img.Height - 2; y++)
            {
                for (int x = 2; x < img.Width - 2; x++)
                {
                    double r = 0, g = 0, b = 0;

                    // Aplicar el kernel 5x5 a los píxeles alrededor
                    for (int ky = -2; ky <= 2; ky++)
                    {
                        for (int kx = -2; kx <= 2; kx++)
                        {
                            var pixel = img[y + ky, x + kx];

                            // Obtener el peso del kernel
                            double weight = kernel[ky + 2, kx + 2];

                            // Sumar los valores de los canales RGB ponderados
                            r += pixel.Red * weight;   // Red
                            g += pixel.Green * weight; // Green
                            b += pixel.Blue * weight;  // Blue
                        }
                    }

                    // Aplicar la normalización y clamping
                    int finalR = Clamp((int)(r / kernelSum), 0, 255);
                    int finalG = Clamp((int)(g / kernelSum), 0, 255);
                    int finalB = Clamp((int)(b / kernelSum), 0, 255);

                    // Asignar el píxel procesado a la imagen de salida
                    result[y, x] = new Bgr(finalB, finalG, finalR);
                }
            }

            // Actualizar la imagen original con el resultado
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
        public byte ClampToByte(double value)
        {
            return (byte)Math.Max(0, Math.Min(255, value));
        }

        public void AplicarFiltroResaltarBordes(ref Image<Bgr, byte> img)
        {
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

            // Aplicar el kernel para resaltar bordes
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    double blue = 0, green = 0, red = 0;

                    // Aplicar el filtro del kernel 3x3 a cada píxel
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

                    // Asignar el nuevo color al píxel en el resultado
                    result[y, x] = new Bgr(
                        ClampToByte(red),
                        ClampToByte(green),
                        ClampToByte(blue)
                    );
                }
            }

            // Actualizar la imagen original con el resultado
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
        public void AplicarFiltroLenteDeGlobo(ref Image<Bgr, byte> img)
        {
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);

            int centroX = width / 2;
            int centroY = height / 2;
            double radio = Math.Min(width, height) / 2.0;

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

                        // Asegurarse de que las nuevas coordenadas estén dentro de los límites de la imagen
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

            // Reemplazar la imagen original con el resultado modificado
            img = result;
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
        public void AplicarFiltroCristalizado(ref Image<Bgr, byte> img)
        {
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);
            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int offsetX = rand.Next(-5, 6); // Desplazamiento aleatorio horizontal
                    int offsetY = rand.Next(-5, 6); // Desplazamiento aleatorio vertical

                    // Calcular las nuevas coordenadas dentro de los límites de la imagen
                    int newX = Math.Max(0, Math.Min(x + offsetX, width - 1));
                    int newY = Math.Max(0, Math.Min(y + offsetY, height - 1));

                    result[y, x] = img[newY, newX]; // Asignar el pixel desplazado al resultado
                }
            }

            // Reemplazar la imagen original con el resultado modificado
            img = result;
        }

        public void AplicarFiltroPapelRaspado(ref Image<Bgr, byte> img)
        {
            var width = img.Width;
            var height = img.Height;

            var result = new Image<Bgr, byte>(width, height);
            Random rand = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int noise = rand.Next(-20, 21); // Generar ruido aleatorio entre -20 y 20
                    var color = img[y, x];
                    result[y, x] = new Bgr(
                        ClampToByte(color.Blue + noise),
                        ClampToByte(color.Green + noise),
                        ClampToByte(color.Red + noise)
                    );
                }
            }

            // Reemplazar la imagen original con el resultado modificado
            img = result;
        }


        public void AplicarFiltroAjusteGamma(ref Image<Bgr, byte> img)
        {
            // Definir el valor gamma dentro de la función
            double gamma = 0.5; // Puedes ajustar este valor para modificar el efecto

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
            ColorSeleecionar = false;
        }

        private void TakePhotoBtn_Click(object sender, EventArgs e)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                using (Mat frame = new Mat())
                {
                    _capture.Retrieve(frame);

                    // Convertir el fotograma a una imagen de Bgr
                    var img = frame.ToImage<Bgr, byte>();

                    // Aplicar el filtro seleccionado
                    executeFilter(ref img);

                    // Guardar la imagen capturada en un archivo
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    saveFileDialog.Title = "Guardar imagen capturada";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        img.Save(saveFileDialog.FileName);
                        MessageBox.Show("Imagen guardada exitosamente.");
                    }
                }
            }
            else
            {
                MessageBox.Show("La cámara no está encendida.");
            }
        }
        private void ColorPickerBtn_Click(object sender, EventArgs e)
        {
            // Crea una instancia de ColorDialog
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Abre el cuadro de diálogo para seleccionar un color
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener el color seleccionado
                    Color color = colorDialog.Color;
                    selectedColor = new Bgr(color.B, color.G, color.R); // Convertir a Bgr
                    ColorSeleecionar = true; // Activar la bandera de selección de color
                }
            }
        }

        private void CameraBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int mouseX = me.X;
            int mouseY = me.Y;

            // Asegúrate de que la imagen no sea nula
            if (CameraBox.Image != null)
            {
                // Convertir la imagen del PictureBox a Bitmap
                Bitmap bitmap = new Bitmap(CameraBox.Image);

                // Obtener el valor del píxel en la posición del mouse
                if (mouseX >= 0 && mouseX < bitmap.Width && mouseY >= 0 && mouseY < bitmap.Height)
                {
                    Color pixelColor = bitmap.GetPixel(mouseX, mouseY);

                    // Convertir el color del píxel a Bgr
                    Bgr bgrColor = new Bgr(pixelColor.B, pixelColor.G, pixelColor.R);

                    // Evaluar los valores de intensidad y cuadrantes L, A, B
                    EvaluarPixel(bgrColor);
                }
            }
        }

        private void RgbToLab(Bgr color, out double L, out double A, out double B)
        {
            // Convertir de BGR a XYZ
            double r = color.Red / 255.0;
            double g = color.Green / 255.0;
            double b = color.Blue / 255.0;

            r = (r > 0.04045) ? Math.Pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = (g > 0.04045) ? Math.Pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = (b > 0.04045) ? Math.Pow((b + 0.055) / 1.055, 2.4) : b / 12.92;

            r *= 100;
            g *= 100;
            b *= 100;

            // Convertir a XYZ
            double x = r * 0.4124 + g * 0.3576 + b * 0.1805;
            double y = r * 0.2126 + g * 0.7152 + b * 0.0722;
            double z = r * 0.0193 + g * 0.1192 + b * 0.9505;

            // Convertir de XYZ a CIELAB
            x /= 95.047;
            y /= 100.000;
            z /= 108.883;

            x = (x > 0.008856) ? Math.Pow(x, 1.0 / 3.0) : (7.787 * x) + (16.0 / 116.0);
            y = (y > 0.008856) ? Math.Pow(y, 1.0 / 3.0) : (7.787 * y) + (16.0 / 116.0);
            z = (z > 0.008856) ? Math.Pow(z, 1.0 / 3.0) : (7.787 * z) + (16.0 / 116.0);

            L = (116 * y) - 16;
            A = 500 * (x - y);
            B = 200 * (y - z);
        }

        private void EvaluarPixel(Bgr color)
        {
            // Calcular los valores de los cuadrantes L, A, B
            RgbToLab(color, out double L, out double A, out double B);

            // Convertir BGR a Color (revirtiendo el orden de los componentes BGR a RGB)
            Color colorMostrar = Color.FromArgb(
                ClampCuadrante(Convert.ToInt32(color.Red), 0, 255),
                ClampCuadrante(Convert.ToInt32(color.Green), 0, 255),
                ClampCuadrante(Convert.ToInt32(color.Blue), 0, 255)
            );

            // Determinar el cuadrante en el que se encuentra el color
            string cuadrante = ObtenerCuadrante(L, A, B);

            // Crear y mostrar el formulario flotante
            ColorValuesForm colorValuesForm = new ColorValuesForm();
            colorValuesForm.UpdateColorValues(
                Convert.ToInt32(Math.Round(L)),
                Convert.ToInt32(Math.Round(A)),
                Convert.ToInt32(Math.Round(B)),
                colorMostrar,
                cuadrante // Mostrar el cuadrante
            );
            colorValuesForm.Show(); // Muestra el formulario flotante
        }

        // Método para obtener el cuadrante basado en los valores de L, A, B
        private string ObtenerCuadrante(double L, double A, double B)
        {
            if (L >= 50 && A >= 0 && B >= 0)
                return "I: Cálido";
            else if (L >= 50 && A < 0 && B >= 0)
                return "II: Frío";
            else if (L >= 50 && A < 0 && B < 0)
                return "III: Frío";
            else if (L >= 50 && A >= 0 && B < 0)
                return "IV: Cálido";
            else if (L < 50 && A >= 0 && B >= 0)
                return "V: Cálido";
            else if (L < 50 && A < 0 && B >= 0)
                return "VI: Frío";
            else if (L < 50 && A < 0 && B < 0)
                return "VII: Frío";
            else if (L < 50 && A >= 0 && B < 0)
                return "VIII: Cálido";
            else
                return "Cuadrante Desconocido";
        }


        // Función Clamp para limitar un valor dentro de un rango específico
        private int ClampCuadrante(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}
