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


    }
}
