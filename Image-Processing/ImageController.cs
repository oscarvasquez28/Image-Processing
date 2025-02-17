using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class ImageController : UserControl
    {
        // Variable para almacenar la imagen original
        private Image imagenOriginal;

        public ImageController()
        {
            InitializeComponent();
        }

        // Evento para cargar la imagen
        private void UploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar imagen";
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Cargar la imagen
                        ImageBox.Image = new Bitmap(openFileDialog.FileName);

                        // Guardar una copia de la imagen original
                        imagenOriginal = new Bitmap(ImageBox.Image);

                        // Generar histograma de la imagen cargada
                        GenerarHistogramaRGB();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Método para generar el histograma RGB
        private void GenerarHistogramaRGB()
        {
            if (ImageBox.Image == null) return;

            Bitmap imagen = new Bitmap(ImageBox.Image);
            int[] histR = new int[256];
            int[] histG = new int[256];
            int[] histB = new int[256];

            // Recorremos la imagen y contamos la frecuencia de cada nivel de color
            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);
                    histR[pixel.R]++;
                    histG[pixel.G]++;
                    histB[pixel.B]++;
                }
            }

            // Dibujar los histogramas
            DibujarHistograma(HistogramBoxRed, histR, Color.Red);
            DibujarHistograma(HistogramBoxGreen, histG, Color.Green);
            DibujarHistograma(HistogramBoxBlue, histB, Color.Blue);

            DibujarHistogramaGeneral(HistogramBoxGeneral, histR, histG, histB);
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
                // Normalizar la altura para cada canal
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

        // Método para dibujar el histograma
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
                // Normalizar la altura
                int barHeight = (int)((histograma[i] / (float)maxFrecuencia) * (height - 5));
                int xPos = (int)(i * barWidth);

                // Dibujar barra con color específico
                g.FillRectangle(new SolidBrush(color), xPos, height - barHeight, barWidth, barHeight);
            }

            pictureBox.Image = histImage;
        }

        // Método para aplicar el filtro Blanco y Negro
        public void AplicarFiltroBlancoYNegro()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            // Convertir la imagen a escala de grises
            Bitmap original = new Bitmap(ImageBox.Image);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11); // Fórmula de escala de grises
                    Color grayColor = Color.FromArgb(grayValue, grayValue, grayValue); // Asignamos el valor en gris

                    original.SetPixel(x, y, grayColor);
                }
            }

            // Actualizamos la imagen del PictureBox
            ImageBox.Image = original;

            // Actualizamos el histograma con la nueva imagen en blanco y negro
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroNegativo()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);
                    // Invertir los colores
                    Color invertedColor = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    imagen.SetPixel(x, y, invertedColor);
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroAltoContraste()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);
                    int R = Math.Min(255, pixel.R * 2);
                    int G = Math.Min(255, pixel.G * 2);
                    int B = Math.Min(255, pixel.B * 2);
                    Color contrastColor = Color.FromArgb(R, G, B);
                    imagen.SetPixel(x, y, contrastColor);
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        public void AplicarDesenfoqueGaussiano()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            int radius = 3; // Radio del filtro gaussiano
            Bitmap blurredImage = new Bitmap(original.Width, original.Height);

            for (int y = radius; y < original.Height - radius; y++)
            {
                for (int x = radius; x < original.Width - radius; x++)
                {
                    int r = 0, g = 0, b = 0;
                    int kernelSize = (2 * radius + 1) * (2 * radius + 1);

                    for (int ky = -radius; ky <= radius; ky++)
                    {
                        for (int kx = -radius; kx <= radius; kx++)
                        {
                            Color pixel = original.GetPixel(x + kx, y + ky);
                            r += pixel.R;
                            g += pixel.G;
                            b += pixel.B;
                        }
                    }

                    r /= kernelSize;
                    g /= kernelSize;
                    b /= kernelSize;

                    blurredImage.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            ImageBox.Image = blurredImage;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroBordes()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);
            Bitmap resultado = new Bitmap(imagen.Width, imagen.Height);

            int[] kernel = new int[] { -1, -1, -1, -1, 8, -1, -1, -1, -1 }; // Kernel de detección de bordes

            for (int y = 1; y < imagen.Height - 1; y++)
            {
                for (int x = 1; x < imagen.Width - 1; x++)
                {
                    int r = 0, g = 0, b = 0;
                    int index = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            Color pixel = imagen.GetPixel(x + kx, y + ky);
                            r += pixel.R * kernel[index];
                            g += pixel.G * kernel[index];
                            b += pixel.B * kernel[index];
                            index++;
                        }
                    }

                    r = Math.Min(255, Math.Max(0, r));
                    g = Math.Min(255, Math.Max(0, g));
                    b = Math.Min(255, Math.Max(0, b));
                    resultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            ImageBox.Image = resultado;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroUmbral(int umbral)
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);
                    int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);

                    // Si el valor en gris es mayor que el umbral, lo ponemos blanco, de lo contrario negro
                    if (grayValue >= umbral)
                        imagen.SetPixel(x, y, Color.White);
                    else
                        imagen.SetPixel(x, y, Color.Black);
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroPosterizar()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);
                    int R = (pixel.R / 64) * 64;
                    int G = (pixel.G / 64) * 64;
                    int B = (pixel.B / 64) * 64;

                    imagen.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroContrasteDinámico()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);

                    int R = (int)(pixel.R * 1.2);
                    int G = (int)(pixel.G * 1.2);
                    int B = (int)(pixel.B * 1.2);

                    // Limitar los valores a 255 para evitar desbordamientos
                    R = Math.Min(255, R);
                    G = Math.Min(255, G);
                    B = Math.Min(255, B);

                    imagen.SetPixel(x, y, Color.FromArgb(R, G, B));
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroLenteDeGlobo()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            Bitmap nuevaImagen = new Bitmap(original.Width, original.Height);

            int centroX = original.Width / 2;
            int centroY = original.Height / 2;
            double maxDistancia = Math.Sqrt(Math.Pow(centroX, 2) + Math.Pow(centroY, 2));

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    double distancia = Math.Sqrt(Math.Pow(x - centroX, 2) + Math.Pow(y - centroY, 2));
                    double factor = Math.Min(1, distancia / maxDistancia);

                    int nuevoX = (int)(centroX + factor * (x - centroX));
                    int nuevoY = (int)(centroY + factor * (y - centroY));

                    // Asegurarse de no salir de los límites de la imagen
                    nuevoX = Math.Min(Math.Max(nuevoX, 0), original.Width - 1);
                    nuevoY = Math.Min(Math.Max(nuevoY, 0), original.Height - 1);

                    Color pixelColor = original.GetPixel(nuevoX, nuevoY);
                    nuevaImagen.SetPixel(x, y, pixelColor);
                }
            }

            ImageBox.Image = nuevaImagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroColoracionAleatoria()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            Bitmap nuevaImagen = new Bitmap(original.Width, original.Height);
            Random rand = new Random();

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);

                    // Generar una variación aleatoria para cada canal de color
                    int r = Math.Min(255, Math.Max(0, pixelColor.R + rand.Next(-50, 51)));
                    int g = Math.Min(255, Math.Max(0, pixelColor.G + rand.Next(-50, 51)));
                    int b = Math.Min(255, Math.Max(0, pixelColor.B + rand.Next(-50, 51)));

                    Color nuevoPixelColor = Color.FromArgb(r, g, b);
                    nuevaImagen.SetPixel(x, y, nuevoPixelColor);
                }
            }

            ImageBox.Image = nuevaImagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroCristalizado()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            Bitmap nuevaImagen = new Bitmap(original.Width, original.Height);

            int tamañoBloque = 10; // Tamaño de cada bloque en el mosaico (puedes ajustar este valor)

            for (int y = 0; y < original.Height; y += tamañoBloque)
            {
                for (int x = 0; x < original.Width; x += tamañoBloque)
                {
                    // Obtener el color promedio del bloque
                    int rPromedio = 0, gPromedio = 0, bPromedio = 0;
                    int contador = 0;

                    // Recorrer el bloque de píxeles
                    for (int dy = 0; dy < tamañoBloque && (y + dy) < original.Height; dy++)
                    {
                        for (int dx = 0; dx < tamañoBloque && (x + dx) < original.Width; dx++)
                        {
                            Color pixel = original.GetPixel(x + dx, y + dy);
                            rPromedio += pixel.R;
                            gPromedio += pixel.G;
                            bPromedio += pixel.B;
                            contador++;
                        }
                    }

                    // Calcular el color promedio del bloque
                    rPromedio /= contador;
                    gPromedio /= contador;
                    bPromedio /= contador;

                    // Asignar el color promedio al bloque en la nueva imagen
                    for (int dy = 0; dy < tamañoBloque && (y + dy) < original.Height; dy++)
                    {
                        for (int dx = 0; dx < tamañoBloque && (x + dx) < original.Width; dx++)
                        {
                            nuevaImagen.SetPixel(x + dx, y + dy, Color.FromArgb(rPromedio, gPromedio, bPromedio));
                        }
                    }
                }
            }

            // Actualizamos la imagen en el PictureBox
            ImageBox.Image = nuevaImagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroPapelRaspado()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            Bitmap nuevaImagen = new Bitmap(original.Width, original.Height);

            Random rand = new Random();

            // Crear el efecto de raspado añadiendo "rasguños" en la imagen
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // Tomamos el color del píxel original
                    Color pixelOriginal = original.GetPixel(x, y);

                    // Creamos un "rasguño" aleatorio basado en ruido
                    int ruido = rand.Next(-30, 30); // Este valor controla la intensidad del rasguño

                    // Aplicamos el ruido al color original para crear un efecto de raspado
                    int r = Math.Min(Math.Max(pixelOriginal.R + ruido, 0), 255);
                    int g = Math.Min(Math.Max(pixelOriginal.G + ruido, 0), 255);
                    int b = Math.Min(Math.Max(pixelOriginal.B + ruido, 0), 255);

                    // Establecer el color final con la textura raspada
                    nuevaImagen.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            // Añadir una textura de rayas (simulando papel raspado)
            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    // Introducimos una variación en las líneas horizontales
                    if (rand.Next(0, 100) < 5) // 5% de probabilidad de agregar una línea de rayas
                    {
                        // Modificar píxeles de manera aleatoria para agregar líneas
                        nuevaImagen.SetPixel(x, y, Color.FromArgb(rand.Next(200, 255), rand.Next(200, 255), rand.Next(200, 255)));
                    }
                }
            }

            // Actualizamos la imagen en el PictureBox
            ImageBox.Image = nuevaImagen;
            GenerarHistogramaRGB();
        }

        public void AplicarFiltroEspejo(bool horizontal = true)
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap original = new Bitmap(ImageBox.Image);
            Bitmap resultado = new Bitmap(original.Width, original.Height);

            int width = original.Width;
            int height = original.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = original.GetPixel(x, y);

                    // Reflexión horizontal
                    if (horizontal)
                    {
                        resultado.SetPixel(width - x - 1, y, pixel);
                    }
                    // Reflexión vertical
                    else
                    {
                        resultado.SetPixel(x, height - y - 1, pixel);
                    }

                    // Mantener los píxeles originales
                    resultado.SetPixel(x, y, pixel);
                }
            }

            // Actualizamos la imagen con el filtro de espejo
            ImageBox.Image = resultado;
            GenerarHistogramaRGB();
        }


        public void AplicarFiltroTermico()
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada.");
                return;
            }

            Bitmap imagen = new Bitmap(ImageBox.Image);

            for (int y = 0; y < imagen.Height; y++)
            {
                for (int x = 0; x < imagen.Width; x++)
                {
                    Color pixel = imagen.GetPixel(x, y);

                    // Convertir a escala de grises (intensidad)
                    int intensidad = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);

                    // Asignar color térmico según la intensidad
                    Color colorTermico = ObtenerColorTermico(intensidad);
                    imagen.SetPixel(x, y, colorTermico);
                }
            }

            ImageBox.Image = imagen;
            GenerarHistogramaRGB();
        }

        /// <summary>
        /// Asigna un color térmico basado en la intensidad (0-255).
        /// Gradiente: Azul → Cian → Verde → Amarillo → Rojo
        /// </summary>
        private Color ObtenerColorTermico(int intensidad)
        {
            if (intensidad < 64) // Azul a Cian
            {
                return Color.FromArgb(0, intensidad * 4, 255);
            }
            else if (intensidad < 128) // Cian a Verde
            {
                return Color.FromArgb(0, 255, 255 - ((intensidad - 64) * 4));
            }
            else if (intensidad < 192) // Verde a Amarillo
            {
                return Color.FromArgb((intensidad - 128) * 4, 255, 0);
            }
            else // Amarillo a Rojo
            {
                return Color.FromArgb(255, 255 - ((intensidad - 192) * 4), 0);
            }
        }



        // Evento para restaurar la imagen al estado original
        private void ResetImage_Click(object sender, EventArgs e)
        {
            if (imagenOriginal != null)
            {
                // Restaurar la imagen original
                ImageBox.Image = new Bitmap(imagenOriginal);

                // Restaurar el histograma de la imagen original
                GenerarHistogramaRGB();
            }
            else
            {
                MessageBox.Show("No hay imagen cargada para restaurar.");
            }
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            if (ImageBox.Image == null)
            {
                MessageBox.Show("No hay imagen cargada para guardar.");
                return;
            }

            // Crear un cuadro de diálogo para elegir la ubicación y el nombre del archivo
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Establecer filtros para los tipos de archivo que se pueden guardar
                saveFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                saveFileDialog.Title = "Guardar imagen como";

                // Mostrar el cuadro de diálogo y verificar si el usuario seleccionó un archivo
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Obtener el nombre del archivo seleccionado
                        string filePath = saveFileDialog.FileName;

                        // Guardar la imagen en el archivo seleccionado
                        ImageBox.Image.Save(filePath);
                        MessageBox.Show("Imagen guardada exitosamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar la imagen: " + ex.Message);
                    }
                }
            }
        }
    }
}
