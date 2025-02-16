﻿namespace Image_Processing
{
    partial class CameraController
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
            this.OnOffCameraBtn = new System.Windows.Forms.Button();
            this.CameraBox = new System.Windows.Forms.PictureBox();
            this.HistogramBoxGreen = new System.Windows.Forms.PictureBox();
            this.HistogramBoxBlue = new System.Windows.Forms.PictureBox();
            this.HistogramBoxRed = new System.Windows.Forms.PictureBox();
            this.TakePhotoBtn = new System.Windows.Forms.Button();
            this.ResetFilters = new System.Windows.Forms.Button();
            this.HistogramBoxGeneral = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).BeginInit();
            this.SuspendLayout();
            // 
            // OnOffCameraBtn
            // 
            this.OnOffCameraBtn.Location = new System.Drawing.Point(64, 321);
            this.OnOffCameraBtn.Margin = new System.Windows.Forms.Padding(4);
            this.OnOffCameraBtn.Name = "OnOffCameraBtn";
            this.OnOffCameraBtn.Size = new System.Drawing.Size(149, 50);
            this.OnOffCameraBtn.TabIndex = 1;
            this.OnOffCameraBtn.Text = "Encender / Apagar";
            this.OnOffCameraBtn.UseVisualStyleBackColor = true;
            this.OnOffCameraBtn.Click += new System.EventHandler(this.OnOffCameraBtn_Click);
            // 
            // CameraBox
            // 
            this.CameraBox.BackgroundImage = global::Image_Processing.Properties.Resources.error_404;
            this.CameraBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CameraBox.Location = new System.Drawing.Point(0, 0);
            this.CameraBox.Margin = new System.Windows.Forms.Padding(4);
            this.CameraBox.Name = "CameraBox";
            this.CameraBox.Size = new System.Drawing.Size(468, 290);
            this.CameraBox.TabIndex = 2;
            this.CameraBox.TabStop = false;
            // 
            // HistogramBoxGreen
            // 
            this.HistogramBoxGreen.Location = new System.Drawing.Point(725, 0);
            this.HistogramBoxGreen.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxGreen.Name = "HistogramBoxGreen";
            this.HistogramBoxGreen.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGreen.TabIndex = 3;
            this.HistogramBoxGreen.TabStop = false;
            // 
            // HistogramBoxBlue
            // 
            this.HistogramBoxBlue.Location = new System.Drawing.Point(725, 149);
            this.HistogramBoxBlue.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxBlue.Name = "HistogramBoxBlue";
            this.HistogramBoxBlue.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxBlue.TabIndex = 4;
            this.HistogramBoxBlue.TabStop = false;
            // 
            // HistogramBoxRed
            // 
            this.HistogramBoxRed.Location = new System.Drawing.Point(517, 0);
            this.HistogramBoxRed.Margin = new System.Windows.Forms.Padding(4);
            this.HistogramBoxRed.Name = "HistogramBoxRed";
            this.HistogramBoxRed.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxRed.TabIndex = 5;
            this.HistogramBoxRed.TabStop = false;
            // 
            // TakePhotoBtn
            // 
            this.TakePhotoBtn.Location = new System.Drawing.Point(271, 321);
            this.TakePhotoBtn.Margin = new System.Windows.Forms.Padding(4);
            this.TakePhotoBtn.Name = "TakePhotoBtn";
            this.TakePhotoBtn.Size = new System.Drawing.Size(149, 50);
            this.TakePhotoBtn.TabIndex = 6;
            this.TakePhotoBtn.Text = "Tomar foto";
            this.TakePhotoBtn.UseVisualStyleBackColor = true;
            this.TakePhotoBtn.Click += new System.EventHandler(this.TakePhotoBtn_Click);
            // 
            // ResetFilters
            // 
            this.ResetFilters.Location = new System.Drawing.Point(634, 321);
            this.ResetFilters.Name = "ResetFilters";
            this.ResetFilters.Size = new System.Drawing.Size(149, 50);
            this.ResetFilters.TabIndex = 8;
            this.ResetFilters.Text = "Reiniciar Filtros";
            this.ResetFilters.UseVisualStyleBackColor = true;
            this.ResetFilters.Click += new System.EventHandler(this.ResetFilters_Click);
            // 
            // HistogramBoxGeneral
            // 
            this.HistogramBoxGeneral.Location = new System.Drawing.Point(517, 149);
            this.HistogramBoxGeneral.Name = "HistogramBoxGeneral";
            this.HistogramBoxGeneral.Size = new System.Drawing.Size(200, 142);
            this.HistogramBoxGeneral.TabIndex = 9;
            this.HistogramBoxGeneral.TabStop = false;
            // 
            // CameraController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HistogramBoxGeneral);
            this.Controls.Add(this.ResetFilters);
            this.Controls.Add(this.TakePhotoBtn);
            this.Controls.Add(this.HistogramBoxRed);
            this.Controls.Add(this.HistogramBoxBlue);
            this.Controls.Add(this.HistogramBoxGreen);
            this.Controls.Add(this.CameraBox);
            this.Controls.Add(this.OnOffCameraBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CameraController";
            this.Size = new System.Drawing.Size(929, 400);
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramBoxGeneral)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OnOffCameraBtn;
        private System.Windows.Forms.PictureBox CameraBox;
        private System.Windows.Forms.PictureBox HistogramBoxGreen;
        private System.Windows.Forms.PictureBox HistogramBoxBlue;
        private System.Windows.Forms.PictureBox HistogramBoxRed;
        private System.Windows.Forms.Button TakePhotoBtn;
        private System.Windows.Forms.Button ResetFilters;
        private System.Windows.Forms.PictureBox HistogramBoxGeneral;
    }
}
