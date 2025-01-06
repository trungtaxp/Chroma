using System.ComponentModel;

namespace Chroma.Helper
{
    partial class Setting
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Setting";

            this.ipLabel = new System.Windows.Forms.Label();
            this.ipLabel.Text = "IP Address:";
            this.ipLabel.Location = new System.Drawing.Point(50, 50);
            this.ipLabel.Size = new System.Drawing.Size(100, 23);

            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.ipTextBox.Location = new System.Drawing.Point(150, 50);
            this.ipTextBox.Size = new System.Drawing.Size(200, 23);

            this.portLabel = new System.Windows.Forms.Label();
            this.portLabel.Text = "Port:";
            this.portLabel.Location = new System.Drawing.Point(50, 100);
            this.portLabel.Size = new System.Drawing.Size(100, 23);

            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox.Location = new System.Drawing.Point(150, 100);
            this.portTextBox.Size = new System.Drawing.Size(200, 23);

            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portTextBox);
        }
    }
}