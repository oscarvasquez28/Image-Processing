namespace Image_Processing
{
    partial class ImageController
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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.HistogramBoxRed = new System.Windows.Forms.PictureBox();
            this.UploadImage = new System.Windows.Forms.Button();
            this.ResetImage = new System.Windows.Forms.Button();
            this.HistogramBoxGreen = new System.Windows.Forms.PictureBox();
            this.HistogramBoxBlue = new System.Windows.Forms.PictureBox();
            this.SaveImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.BackgroundImage = global::Image_Processing.Properties.Resources.error_404;
            this.ImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImageBox.Location = new System.Drawing.Point(0, 0);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(351, 236);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // HistogramBoxRed
            // 
            this.HistogramBoxRed.Location = new System.Drawing.Point(362, 0);
            this.HistogramBoxRed.Margin = new System.Windows.Forms.Padding(2);
            this.HistogramBoxRed.Name = "HistogramBoxRed";
            this.HistogramBoxRed.Size = new System.Drawing.Size(150, 236);
            this.HistogramBoxRed.TabIndex = 1;
            this.HistogramBoxRed.TabStop = false;
            // 
            // UploadImage
            // 
            this.UploadImage.Location = new System.Drawing.Point(88, 266);
            this.UploadImage.Margin = new System.Windows.Forms.Padding(2);
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.Size = new System.Drawing.Size(112, 41);
            this.UploadImage.TabIndex = 2;
            this.UploadImage.Text = "Cargar Imagen";
            this.UploadImage.UseVisualStyleBackColor = true;
            this.UploadImage.Click += new System.EventHandler(this.UploadImage_Click);
            // 
            // ResetImage
            // 
            this.ResetImage.Location = new System.Drawing.Point(447, 266);
            this.ResetImage.Margin = new System.Windows.Forms.Padding(2);
            this.ResetImage.Name = "ResetImage";
            this.ResetImage.Size = new System.Drawing.Size(112, 41);
            this.ResetImage.TabIndex = 3;
            this.ResetImage.Text = "Reiniciar Imagen";
            this.ResetImage.UseVisualStyleBackColor = true;
            this.ResetImage.Click += new System.EventHandler(this.ResetImage_Click);
            // 
            // HistogramBoxGreen
            // 
            this.HistogramBoxGreen.Location = new System.Drawing.Point(517, 0);
            this.HistogramBoxGreen.Name = "HistogramBoxGreen";
            this.HistogramBoxGreen.Size = new System.Drawing.Size(150, 115);
            this.HistogramBoxGreen.TabIndex = 4;
            this.HistogramBoxGreen.TabStop = false;
            // 
            // HistogramBoxBlue
            // 
            this.HistogramBoxBlue.Location = new System.Drawing.Point(517, 121);
            this.HistogramBoxBlue.Name = "HistogramBoxBlue";
            this.HistogramBoxBlue.Size = new System.Drawing.Size(150, 115);
            this.HistogramBoxBlue.TabIndex = 5;
            this.HistogramBoxBlue.TabStop = false;
            // 
            // SaveImage
            // 
            this.SaveImage.Location = new System.Drawing.Point(268, 266);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(112, 41);
            this.SaveImage.TabIndex = 6;
            this.SaveImage.Text = "Guardar Imagen";
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // ImageController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.HistogramBoxBlue);
            this.Controls.Add(this.HistogramBoxGreen);
            this.Controls.Add(this.ResetImage);
            this.Controls.Add(this.UploadImage);
            this.Controls.Add(this.HistogramBoxRed);
            this.Controls.Add(this.ImageBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ImageController";
            this.Size = new System.Drawing.Size(670, 325);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.PictureBox HistogramBoxRed;
        private System.Windows.Forms.Button UploadImage;
        private System.Windows.Forms.Button ResetImage;
        private System.Windows.Forms.PictureBox HistogramBoxGreen;
        private System.Windows.Forms.PictureBox HistogramBoxBlue;
        private System.Windows.Forms.Button SaveImage;
    }
}
