using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Processing
{
    public partial class ColorValuesForm : Form
    {
        public ColorValuesForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Con bordes, ya que se quiere un botón de cierre
            this.MaximizeBox = false; // Evitar que se maximice
            this.StartPosition = FormStartPosition.CenterScreen; // Centrado en la pantalla
            this.TopMost = true; // Asegurar que esté siempre encima de otros formularios
        }

        public void UpdateColorValues(int L, int A, int B, Color colorMostrar, string cuadrante)
        {
            // Actualizar los valores en los Labels correspondientes
            labelL.Text = $"L: {L}";
            labelA.Text = $"A: {A}";
            labelB.Text = $"B: {B}";

            // Mostrar el color en un Label o Panel
            panelColor.BackColor = colorMostrar; // Suponiendo que tienes un Panel llamado 'panelColor'

            // Mostrar el cuadrante en el Label correspondiente
            labelCuadrante.Text = $"Cuadrante: {cuadrante}";
        }

    }

}
