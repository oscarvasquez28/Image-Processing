namespace Image_Processing
{
    partial class VideoController
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.HistogramBoxRed = new System.Windows.Forms.PictureBox();
            this.HistogramBoxGreen = new System.Windows.Forms.PictureBox();
            this.HistogramBoxBlue = new System.Windows.Forms.PictureBox();
            this.VideoBox = new System.Windows.Forms.PictureBox();
            this.PausaBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.ResetVideo = new System.Windows.Forms.Button();
            this.CargarVideoBtn = new System.Windows.Forms.Button();
            this.HistogramBoxGeneral = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // HistogramBoxRed
            // 
            this.HistogramBoxRed.Location = new System.Drawing.Point(517, 0);
            this.HistogramBoxRed.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxRed.Name = "HistogramBoxRed";
            this.HistogramBoxRed.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxRed.TabIndex = 0;
            this.HistogramBoxRed.TabStop = false;
            // 
            // HistogramBoxGreen
            // 
            this.HistogramBoxGreen.Location = new System.Drawing.Point(725, 0);
            this.HistogramBoxGreen.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxGreen.Name = "HistogramBoxGreen";
            this.HistogramBoxGreen.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGreen.TabIndex = 1;
            this.HistogramBoxGreen.TabStop = false;
            // 
            // HistogramBoxBlue
            // 
            this.HistogramBoxBlue.Location = new System.Drawing.Point(725, 149);
            this.HistogramBoxBlue.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxBlue.Name = "HistogramBoxBlue";
            this.HistogramBoxBlue.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxBlue.TabIndex = 2;
            this.HistogramBoxBlue.TabStop = false;
            // 
            // VideoBox
            // 
            this.VideoBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VideoBox.BackgroundImage = global::Image_Processing.Properties.Resources.error_404;
            this.VideoBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VideoBox.Location = new System.Drawing.Point(0, 0);
            this.VideoBox.Margin = new System.Windows.Forms.Padding(4);
            this.VideoBox.Name = "VideoBox";
            this.VideoBox.Size = new System.Drawing.Size(468, 290);
            this.VideoBox.TabIndex = 3;
            this.VideoBox.TabStop = false;
            // 
            // PausaBtn
            // 
            this.PausaBtn.Location = new System.Drawing.Point(4, 325);
            this.PausaBtn.Margin = new System.Windows.Forms.Padding(4);
            this.PausaBtn.Name = "PausaBtn";
            this.PausaBtn.Size = new System.Drawing.Size(149, 50);
            this.PausaBtn.TabIndex = 4;
            this.PausaBtn.Text = "Pausa";
            this.PausaBtn.UseVisualStyleBackColor = true;
            this.PausaBtn.Click += new System.EventHandler(this.PausaBtn_Click);
            // 
            // PlayBtn
            // 
            this.PlayBtn.Location = new System.Drawing.Point(161, 325);
            this.PlayBtn.Margin = new System.Windows.Forms.Padding(4);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(149, 50);
            this.PlayBtn.TabIndex = 5;
            this.PlayBtn.Text = "Play";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // ResetVideo
            // 
            this.ResetVideo.Location = new System.Drawing.Point(543, 325);
            this.ResetVideo.Margin = new System.Windows.Forms.Padding(4);
            this.ResetVideo.Name = "ResetVideo";
            this.ResetVideo.Size = new System.Drawing.Size(149, 50);
            this.ResetVideo.TabIndex = 7;
            this.ResetVideo.Text = "Reiniciar Filtros";
            this.ResetVideo.UseVisualStyleBackColor = true;
            this.ResetVideo.Click += new System.EventHandler(this.ResetVideo_Click);
            // 
            // CargarVideoBtn
            // 
            this.CargarVideoBtn.Location = new System.Drawing.Point(319, 325);
            this.CargarVideoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.CargarVideoBtn.Name = "CargarVideoBtn";
            this.CargarVideoBtn.Size = new System.Drawing.Size(149, 50);
            this.CargarVideoBtn.TabIndex = 8;
            this.CargarVideoBtn.Text = "Cargar Video";
            this.CargarVideoBtn.UseVisualStyleBackColor = true;
            this.CargarVideoBtn.Click += new System.EventHandler(this.CargarVideoBtn_Click);
            // 
            // HistogramBoxGeneral
            // 
            this.HistogramBoxGeneral.Location = new System.Drawing.Point(517, 148);
            this.HistogramBoxGeneral.Name = "HistogramBoxGeneral";
            this.HistogramBoxGeneral.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGeneral.TabIndex = 9;
            this.HistogramBoxGeneral.TabStop = false;
            // 
            // VideoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.HistogramBoxGeneral);
            this.Controls.Add(this.CargarVideoBtn);
            this.Controls.Add(this.ResetVideo);
            this.Controls.Add(this.PlayBtn);
            this.Controls.Add(this.PausaBtn);
            this.Controls.Add(this.VideoBox);
            this.Controls.Add(this.HistogramBoxBlue);
            this.Controls.Add(this.HistogramBoxGreen);
            this.Controls.Add(this.HistogramBoxRed);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "VideoController";
            this.Size = new System.Drawing.Size(929, 414);
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox HistogramBoxRed;
        private System.Windows.Forms.PictureBox HistogramBoxGreen;
        private System.Windows.Forms.PictureBox HistogramBoxBlue;
        private System.Windows.Forms.PictureBox VideoBox;
        private System.Windows.Forms.Button PausaBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Button ResetVideo;
        private System.Windows.Forms.Button CargarVideoBtn;
        private System.Windows.Forms.PictureBox HistogramBoxGeneral;
    }
}
