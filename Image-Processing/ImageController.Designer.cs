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
            this.HistogramBoxGeneral = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.BackgroundImage = global::Image_Processing.Properties.Resources.SubeArchivo;
            this.ImageBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImageBox.Location = new System.Drawing.Point(0, 0);
            this.ImageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(468, 290);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // HistogramBoxRed
            // 
            this.HistogramBoxRed.Location = new System.Drawing.Point(483, 0);
            this.HistogramBoxRed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HistogramBoxRed.Name = "HistogramBoxRed";
            this.HistogramBoxRed.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxRed.TabIndex = 1;
            this.HistogramBoxRed.TabStop = false;
            // 
            // UploadImage
            // 
            this.UploadImage.BackgroundImage = global::Image_Processing.Properties.Resources.CargarBtn;
            this.UploadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UploadImage.Location = new System.Drawing.Point(117, 327);
            this.UploadImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.Size = new System.Drawing.Size(149, 50);
            this.UploadImage.TabIndex = 2;
            this.UploadImage.UseVisualStyleBackColor = true;
            this.UploadImage.Click += new System.EventHandler(this.UploadImage_Click);
            // 
            // ResetImage
            // 
            this.ResetImage.BackgroundImage = global::Image_Processing.Properties.Resources.ReiniciarBtn;
            this.ResetImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ResetImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetImage.Location = new System.Drawing.Point(596, 327);
            this.ResetImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResetImage.Name = "ResetImage";
            this.ResetImage.Size = new System.Drawing.Size(149, 50);
            this.ResetImage.TabIndex = 3;
            this.ResetImage.UseVisualStyleBackColor = true;
            this.ResetImage.Click += new System.EventHandler(this.ResetImage_Click);
            // 
            // HistogramBoxGreen
            // 
            this.HistogramBoxGreen.Location = new System.Drawing.Point(690, 0);
            this.HistogramBoxGreen.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxGreen.Name = "HistogramBoxGreen";
            this.HistogramBoxGreen.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGreen.TabIndex = 4;
            this.HistogramBoxGreen.TabStop = false;
            // 
            // HistogramBoxBlue
            // 
            this.HistogramBoxBlue.Location = new System.Drawing.Point(690, 149);
            this.HistogramBoxBlue.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxBlue.Name = "HistogramBoxBlue";
            this.HistogramBoxBlue.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxBlue.TabIndex = 5;
            this.HistogramBoxBlue.TabStop = false;
            // 
            // SaveImage
            // 
            this.SaveImage.BackgroundImage = global::Image_Processing.Properties.Resources.GuardarBtn;
            this.SaveImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveImage.Location = new System.Drawing.Point(357, 327);
            this.SaveImage.Margin = new System.Windows.Forms.Padding(4);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(149, 50);
            this.SaveImage.TabIndex = 6;
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // HistogramBoxGeneral
            // 
            this.HistogramBoxGeneral.Location = new System.Drawing.Point(484, 149);
            this.HistogramBoxGeneral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HistogramBoxGeneral.Name = "HistogramBoxGeneral";
            this.HistogramBoxGeneral.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGeneral.TabIndex = 7;
            this.HistogramBoxGeneral.TabStop = false;
            // 
            // ImageController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.HistogramBoxGeneral);
            this.Controls.Add(this.SaveImage);
            this.Controls.Add(this.HistogramBoxBlue);
            this.Controls.Add(this.HistogramBoxGreen);
            this.Controls.Add(this.ResetImage);
            this.Controls.Add(this.UploadImage);
            this.Controls.Add(this.HistogramBoxRed);
            this.Controls.Add(this.ImageBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ImageController";
            this.Size = new System.Drawing.Size(893, 400);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).EndInit();
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
        private System.Windows.Forms.PictureBox HistogramBoxGeneral;
    }
}
