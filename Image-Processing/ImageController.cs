using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class ImageController : UserControl
    {
        public ImageController()
        {
            InitializeComponent();
        }

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
                        ImageBox.Image = new Bitmap(openFileDialog.FileName);
                        GenerarHistogramaRGB();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

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
                // Normalizar la altura
                int barHeight = (int)((histograma[i] / (float)maxFrecuencia) * (height - 5));
                int xPos = (int)(i * barWidth);

                // Dibujar barra con color específico
                g.FillRectangle(new SolidBrush(color), xPos, height - barHeight, barWidth, barHeight);
            }

            pictureBox.Image = histImage;
        }
    }
}
