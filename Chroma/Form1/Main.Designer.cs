﻿using System.Windows.Forms;

namespace Chroma.Form1
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Main
            // 
            // this.AutoScaleDimensions = new System.Drawing.SizeF(this.ClientSize.Width / 96F, this.ClientSize.Height / 96F);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = Screen.PrimaryScreen.Bounds.Size;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "Voltage Controller Test";
            this.ResumeLayout(false);

        }

        #endregion
    }
}

