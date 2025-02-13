using System;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class Form1 : Form
    {
        // Instancia de ImageController como miembro de la clase
        private ImageController imageController;
        private VideoController videoController;
        private CameraController cameraController;

        public Form1()
        {
            InitializeComponent();

            // Aquí puedes inicializar los controladores
            imageController = new ImageController();
            videoController = new VideoController();
            cameraController = new CameraController();

            // Cargar el primer control (ImageController)
            CargarControl(imageController);
        }

        private void CargarControl(UserControl control)
        {
            panel1.Controls.Clear();  // Limpia el panel antes de agregar un nuevo control
            control.Dock = DockStyle.Fill;  // Ajusta el tamaño del control al panel
            panel1.Controls.Add(control);
        }

        private void Image_Click(object sender, EventArgs e)
        {
            // Usando la instancia previamente creada de ImageController
            CargarControl(imageController);
        }

        private void Video_Click(object sender, EventArgs e)
        {
            // Usando la instancia previamente creada de VideoController
            CargarControl(videoController);
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            // Usando la instancia previamente creada de CameraController
            CargarControl(cameraController);
        }

        private void BlancoYNegroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verificamos que ImageController tenga una imagen cargada
            if (imageController != null)
            {
                // Llamamos al método para aplicar el filtro blanco y negro
                imageController.AplicarFiltroBlancoYNegro();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void InvertirColoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroNegativo();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void AltoContrasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroAltoContraste();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void DesenfoqueGaussianoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarDesenfoqueGaussiano();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void ResaltarBordesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroBordes();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void UmbralToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Supón que tienes un formulario o cuadro de texto para ingresar el valor del umbral
            int umbral = 128; // Aquí podrías obtener este valor dinámicamente
            if (imageController != null)
            {
                imageController.AplicarFiltroUmbral(umbral);
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }
        private void PosterizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroPosterizar();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void ContrasteDinamicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroContrasteDinámico();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void LenteDeGloboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroLenteDeGlobo();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void ColoracionAleatoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroColoracionAleatoria();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void CristalizadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroCristalizado();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void PapelRaspadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroPapelRaspado();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }

        private void FiltroEspejoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imageController != null)
            {
                imageController.AplicarFiltroEspejo();
            }
            else
            {
                MessageBox.Show("No se ha cargado ninguna imagen.");
            }
        }
    }
}
