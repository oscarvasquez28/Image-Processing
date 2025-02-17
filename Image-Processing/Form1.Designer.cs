namespace Image_Processing
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlancoYNegroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InvertirColoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AltoContrasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DesenfoqueGaussianoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResaltarBordesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UmbralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LenteDeGloboToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ColoracionAleatoriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CristalizadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PapelRaspadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FiltroEspejoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContrasteDinamicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PosterizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AjustarGammaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FiltroTermicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Image = new System.Windows.Forms.Button();
            this.Camera = new System.Windows.Forms.Button();
            this.Video = new System.Windows.Forms.Button();
            this.FiltroBlancoNegro = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(25, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(929, 400);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem,
            this.filtrosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(981, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // filtrosToolStripMenuItem
            // 
            this.filtrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BlancoYNegroToolStripMenuItem,
            this.InvertirColoresToolStripMenuItem,
            this.AltoContrasteToolStripMenuItem,
            this.DesenfoqueGaussianoToolStripMenuItem,
            this.ResaltarBordesToolStripMenuItem,
            this.UmbralToolStripMenuItem,
            this.LenteDeGloboToolStripMenuItem,
            this.ColoracionAleatoriaToolStripMenuItem,
            this.CristalizadoToolStripMenuItem,
            this.PapelRaspadoToolStripMenuItem,
            this.FiltroEspejoToolStripMenuItem,
            this.ContrasteDinamicoToolStripMenuItem,
            this.PosterizarToolStripMenuItem,
            this.AjustarGammaToolStripMenuItem,
            this.FiltroTermicoToolStripMenuItem});
            this.filtrosToolStripMenuItem.Name = "filtrosToolStripMenuItem";
            this.filtrosToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.filtrosToolStripMenuItem.Text = "Filtros";
            // 
            // BlancoYNegroToolStripMenuItem
            // 
            this.BlancoYNegroToolStripMenuItem.Name = "BlancoYNegroToolStripMenuItem";
            this.BlancoYNegroToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.BlancoYNegroToolStripMenuItem.Text = "Blanco y Negro";
            this.BlancoYNegroToolStripMenuItem.Click += new System.EventHandler(this.BlancoYNegroToolStripMenuItem_Click);
            // 
            // InvertirColoresToolStripMenuItem
            // 
            this.InvertirColoresToolStripMenuItem.Name = "InvertirColoresToolStripMenuItem";
            this.InvertirColoresToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.InvertirColoresToolStripMenuItem.Text = "Invertir Colores";
            this.InvertirColoresToolStripMenuItem.Click += new System.EventHandler(this.InvertirColoresToolStripMenuItem_Click_1);
            // 
            // AltoContrasteToolStripMenuItem
            // 
            this.AltoContrasteToolStripMenuItem.Name = "AltoContrasteToolStripMenuItem";
            this.AltoContrasteToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.AltoContrasteToolStripMenuItem.Text = "Alto Contraste";
            this.AltoContrasteToolStripMenuItem.Click += new System.EventHandler(this.AltoContrasteToolStripMenuItem_Click_1);
            // 
            // DesenfoqueGaussianoToolStripMenuItem
            // 
            this.DesenfoqueGaussianoToolStripMenuItem.Name = "DesenfoqueGaussianoToolStripMenuItem";
            this.DesenfoqueGaussianoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.DesenfoqueGaussianoToolStripMenuItem.Text = "Desenfoque Gaussiano";
            this.DesenfoqueGaussianoToolStripMenuItem.Click += new System.EventHandler(this.DesenfoqueGaussianoToolStripMenuItem_Click);
            // 
            // ResaltarBordesToolStripMenuItem
            // 
            this.ResaltarBordesToolStripMenuItem.Name = "ResaltarBordesToolStripMenuItem";
            this.ResaltarBordesToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.ResaltarBordesToolStripMenuItem.Text = "Resaltar Bordes";
            this.ResaltarBordesToolStripMenuItem.Click += new System.EventHandler(this.ResaltarBordesToolStripMenuItem_Click_1);
            // 
            // UmbralToolStripMenuItem
            // 
            this.UmbralToolStripMenuItem.Name = "UmbralToolStripMenuItem";
            this.UmbralToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.UmbralToolStripMenuItem.Text = "Umbral";
            this.UmbralToolStripMenuItem.Click += new System.EventHandler(this.UmbralToolStripMenuItem_Click_1);
            // 
            // LenteDeGloboToolStripMenuItem
            // 
            this.LenteDeGloboToolStripMenuItem.Name = "LenteDeGloboToolStripMenuItem";
            this.LenteDeGloboToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.LenteDeGloboToolStripMenuItem.Text = "Lente de Globo";
            this.LenteDeGloboToolStripMenuItem.Click += new System.EventHandler(this.LenteDeGloboToolStripMenuItem_Click_1);
            // 
            // ColoracionAleatoriaToolStripMenuItem
            // 
            this.ColoracionAleatoriaToolStripMenuItem.Name = "ColoracionAleatoriaToolStripMenuItem";
            this.ColoracionAleatoriaToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.ColoracionAleatoriaToolStripMenuItem.Text = "Coloración Aleatoria";
            this.ColoracionAleatoriaToolStripMenuItem.Click += new System.EventHandler(this.ColoracionAleatoriaToolStripMenuItem_Click_1);
            // 
            // CristalizadoToolStripMenuItem
            // 
            this.CristalizadoToolStripMenuItem.Name = "CristalizadoToolStripMenuItem";
            this.CristalizadoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.CristalizadoToolStripMenuItem.Text = "Cristalizado";
            this.CristalizadoToolStripMenuItem.Click += new System.EventHandler(this.CristalizadoToolStripMenuItem_Click_1);
            // 
            // PapelRaspadoToolStripMenuItem
            // 
            this.PapelRaspadoToolStripMenuItem.Name = "PapelRaspadoToolStripMenuItem";
            this.PapelRaspadoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.PapelRaspadoToolStripMenuItem.Text = "Papel Raspado";
            this.PapelRaspadoToolStripMenuItem.Click += new System.EventHandler(this.PapelRaspadoToolStripMenuItem_Click_1);
            // 
            // FiltroEspejoToolStripMenuItem
            // 
            this.FiltroEspejoToolStripMenuItem.Name = "FiltroEspejoToolStripMenuItem";
            this.FiltroEspejoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.FiltroEspejoToolStripMenuItem.Text = "Filtro Espejo";
            this.FiltroEspejoToolStripMenuItem.Click += new System.EventHandler(this.FiltroEspejoToolStripMenuItem_Click_1);
            // 
            // ContrasteDinamicoToolStripMenuItem
            // 
            this.ContrasteDinamicoToolStripMenuItem.Name = "ContrasteDinamicoToolStripMenuItem";
            this.ContrasteDinamicoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.ContrasteDinamicoToolStripMenuItem.Text = "Contraste Dinámico";
            this.ContrasteDinamicoToolStripMenuItem.Click += new System.EventHandler(this.ContrasteDinamicoToolStripMenuItem_Click_1);
            // 
            // PosterizarToolStripMenuItem
            // 
            this.PosterizarToolStripMenuItem.Name = "PosterizarToolStripMenuItem";
            this.PosterizarToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.PosterizarToolStripMenuItem.Text = "Posterizar";
            this.PosterizarToolStripMenuItem.Click += new System.EventHandler(this.PosterizarToolStripMenuItem_Click_1);
            // 
            // AjustarGammaToolStripMenuItem
            // 
            this.AjustarGammaToolStripMenuItem.Name = "AjustarGammaToolStripMenuItem";
            this.AjustarGammaToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.AjustarGammaToolStripMenuItem.Text = "Ajustar Gamma";
            this.AjustarGammaToolStripMenuItem.Click += new System.EventHandler(this.AjustarGammaToolStripMenuItem_Click);
            // 
            // FiltroTermicoToolStripMenuItem
            // 
            this.FiltroTermicoToolStripMenuItem.Name = "FiltroTermicoToolStripMenuItem";
            this.FiltroTermicoToolStripMenuItem.Size = new System.Drawing.Size(243, 26);
            this.FiltroTermicoToolStripMenuItem.Text = "Filtro Termico";
            this.FiltroTermicoToolStripMenuItem.Click += new System.EventHandler(this.FiltroTermicoToolStripMenuItem_Click);
            // 
            // Image
            // 
            this.Image.BackColor = System.Drawing.Color.Transparent;
            this.Image.BackgroundImage = global::Image_Processing.Properties.Resources.BtnCamara;
            this.Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Image.Location = new System.Drawing.Point(29, 478);
            this.Image.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(149, 50);
            this.Image.TabIndex = 2;
            this.Image.UseVisualStyleBackColor = false;
            this.Image.Click += new System.EventHandler(this.Image_Click);
            // 
            // Camera
            // 
            this.Camera.BackColor = System.Drawing.Color.Transparent;
            this.Camera.BackgroundImage = global::Image_Processing.Properties.Resources.BtnCamara;
            this.Camera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Camera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Camera.Location = new System.Drawing.Point(729, 478);
            this.Camera.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Camera.Name = "Camera";
            this.Camera.Size = new System.Drawing.Size(149, 50);
            this.Camera.TabIndex = 4;
            this.Camera.UseVisualStyleBackColor = false;
            this.Camera.Click += new System.EventHandler(this.Camera_Click);
            // 
            // Video
            // 
            this.Video.BackColor = System.Drawing.Color.Transparent;
            this.Video.BackgroundImage = global::Image_Processing.Properties.Resources.BtnVideo;
            this.Video.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Video.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Video.Location = new System.Drawing.Point(404, 478);
            this.Video.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Video.Name = "Video";
            this.Video.Size = new System.Drawing.Size(149, 50);
            this.Video.TabIndex = 3;
            this.Video.UseVisualStyleBackColor = false;
            this.Video.Click += new System.EventHandler(this.Video_Click);
            // 
            // FiltroBlancoNegro
            // 
            this.FiltroBlancoNegro.Location = new System.Drawing.Point(637, 86);
            this.FiltroBlancoNegro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FiltroBlancoNegro.Name = "FiltroBlancoNegro";
            this.FiltroBlancoNegro.Size = new System.Drawing.Size(0, 0);
            this.FiltroBlancoNegro.TabIndex = 5;
            this.FiltroBlancoNegro.Text = "Blanco y Negro";
            this.FiltroBlancoNegro.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 139);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 0);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(820, 86);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(820, 139);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(0, 0);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(637, 194);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(0, 0);
            this.button4.TabIndex = 9;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(820, 194);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(0, 0);
            this.button5.TabIndex = 10;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(637, 254);
            this.button6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(0, 0);
            this.button6.TabIndex = 11;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(820, 254);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(0, 0);
            this.button7.TabIndex = 12;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(637, 310);
            this.button8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(0, 0);
            this.button8.TabIndex = 13;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(713, 271);
            this.button9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(0, 0);
            this.button9.TabIndex = 14;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Image_Processing.Properties.Resources.BackgroundProcesamiento;
            this.ClientSize = new System.Drawing.Size(981, 553);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FiltroBlancoNegro);
            this.Controls.Add(this.Camera);
            this.Controls.Add(this.Video);
            this.Controls.Add(this.Image);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrosToolStripMenuItem;
        private System.Windows.Forms.Button Image;
        private System.Windows.Forms.Button Camera;
        private System.Windows.Forms.Button Video;
        private System.Windows.Forms.Button FiltroBlancoNegro;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.ToolStripMenuItem BlancoYNegroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InvertirColoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AltoContrasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DesenfoqueGaussianoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResaltarBordesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UmbralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LenteDeGloboToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ColoracionAleatoriaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CristalizadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PapelRaspadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FiltroEspejoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ContrasteDinamicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PosterizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AjustarGammaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FiltroTermicoToolStripMenuItem;
    }
}

