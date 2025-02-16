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
        private bool isVideoControlVisible = false;
        private bool isImageControlVisible = false;
        private bool isCameraControlVisible = false;

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
            panel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);

            // Establece el estado de visibilidad explícitamente
            if (control == imageController)
            {
                isImageControlVisible = true;
                isVideoControlVisible = false;
                isCameraControlVisible = false;
            }
            else if (control == videoController)
            {
                isImageControlVisible = false;
                isVideoControlVisible = true;
                isCameraControlVisible = false;
            }
            else if (control == cameraController)
            {
                isImageControlVisible = false;
                isVideoControlVisible = false;
                isCameraControlVisible = true;
            }
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
        private void AplicarFiltroAEtiqueta(Func<ImageController, Action> filtroImagen, Func<VideoController, Action> filtroVideo, Func<CameraController, Action> filtroCamara)
        {
            if (isImageControlVisible)  // Si ImageController está visible
            {
                filtroImagen(imageController)();  // Aplicamos el filtro a la imagen
            }
            else if (isVideoControlVisible)  // Si VideoController está visible
            {
                filtroVideo(videoController)();  // Aplicamos el filtro al video
            }
            else if (isCameraControlVisible)  // Si CameraController está visible
            {
                filtroCamara(cameraController)();  // Aplicamos el filtro a la cámara
            }
            else
            {
                MessageBox.Show("No hay imagen ni video cargados.");
            }
        }



        private void BlancoYNegroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroBlancoYNegro,
                videoController => () => videoController.setActiveFilter(1),
                cameraController => () => cameraController.setActiveFilter(1)
            );
        }

        private void InvertirColoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroNegativo,
                videoController => () => videoController.setActiveFilter(2),
                cameraController => () => cameraController.setActiveFilter(2)
            );
        }


        private void AltoContrasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroAltoContraste,
                videoController => () => videoController.setActiveFilter(3),
                cameraController => () => cameraController.setActiveFilter(3)
            );
        }

        private void DesenfoqueGaussianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarDesenfoqueGaussiano,
                videoController => () => videoController.setActiveFilter(4),
                cameraController => () => cameraController.setActiveFilter(4)
            );
        }

        private void ResaltarBordesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroBordes,
                videoController => () => videoController.setActiveFilter(5),
                cameraController => () => cameraController.setActiveFilter(5)
            );
        }

        private void UmbralToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int umbral = 150;
            AplicarFiltroAEtiqueta(
                imageController => () => imageController.AplicarFiltroUmbral(umbral),
                videoController => () => videoController.setActiveFilter(6),
                cameraController => () => cameraController.setActiveFilter(6)
            );
        }

        private void LenteDeGloboToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroLenteDeGlobo,
                videoController => () => videoController.setActiveFilter(9),
                cameraController => () => cameraController.setActiveFilter(9)
            );
        }

        private void ColoracionAleatoriaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroColoracionAleatoria,
                videoController => () => videoController.setActiveFilter(10),
                cameraController => () => cameraController.setActiveFilter(10)
            );
        }

        private void CristalizadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroCristalizado,
                videoController => () => videoController.setActiveFilter(11),
                cameraController => () => cameraController.setActiveFilter(1)
            );
        }

        private void PapelRaspadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroPapelRaspado,
                videoController => () => videoController.setActiveFilter(12),
                cameraController => () => cameraController.setActiveFilter(12)
            );
        }
        private void FiltroEspejoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                 imageController => () => imageController.AplicarFiltroEspejo(true),
                videoController => () => videoController.setActiveFilter(12),
                cameraController => () => cameraController.setActiveFilter(12)
            );
        }

        private void ContrasteDinamicoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroContrasteDinámico,
                videoController => () => videoController.setActiveFilter(8),
                cameraController => () => cameraController.setActiveFilter(8)
            );
        }

        private void PosterizarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroPosterizar,
                videoController => () => videoController.setActiveFilter(7),
                cameraController => () => cameraController.setActiveFilter(1)
            );
        }

        private void AjustarGammaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroPosterizar,
                videoController => () => videoController.setActiveFilter(14),
                cameraController => () => cameraController.setActiveFilter(14)
            );
        }

        private void FiltroTermicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AplicarFiltroAEtiqueta(
                imageController => imageController.AplicarFiltroPosterizar,
                videoController => () => videoController.setActiveFilter(15),
                cameraController => () => cameraController.setActiveFilter(15)
            );
        }
    }
}
