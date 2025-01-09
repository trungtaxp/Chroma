using System.ComponentModel;

namespace Chroma.Helper
{
    partial class Setting
    {
        private System.ComponentModel.IContainer components = null;
        
        private System.Windows.Forms.Label titleRohdeSchwarzLabel;
        private System.Windows.Forms.Label ipRohdeSchwarzLabel;
        private System.Windows.Forms.Label portRohdeSchwarzLabel;
        private System.Windows.Forms.TextBox ipRohdeSchwarzTextBox;
        private System.Windows.Forms.TextBox portRohdeSchwarzTextBox;
        
        private System.Windows.Forms.Label titleKeithleyLabel;
        private System.Windows.Forms.Label ipKeithleyLabel;
        private System.Windows.Forms.TextBox ipKeithleyTextBox;
        
        private System.Windows.Forms.Label titleChromaLabel;
        private System.Windows.Forms.Label ipChromaLabel;
        private System.Windows.Forms.TextBox ipChromaTextBox;
        
        private System.Windows.Forms.Button confirmButton;

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
            this.ClientSize = new System.Drawing.Size(350, 550);
            this.Text = "Setting";
            
            // Rohde Schwarz
            this.titleRohdeSchwarzLabel = new System.Windows.Forms.Label();
            this.titleRohdeSchwarzLabel.Text = "Rohde Schwarz";
            this.titleRohdeSchwarzLabel.Location = new System.Drawing.Point(125, 20);
            this.titleRohdeSchwarzLabel.Size = new System.Drawing.Size(200, 23);
            this.titleRohdeSchwarzLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            this.ipRohdeSchwarzLabel = new System.Windows.Forms.Label();
            this.ipRohdeSchwarzLabel.Text = "IP Address:";
            this.ipRohdeSchwarzLabel.Location = new System.Drawing.Point(20, 50);
            this.ipRohdeSchwarzLabel.Size = new System.Drawing.Size(100, 23);

            this.ipRohdeSchwarzTextBox = new System.Windows.Forms.TextBox();
            this.ipRohdeSchwarzTextBox.Location = new System.Drawing.Point(120, 50);
            this.ipRohdeSchwarzTextBox.Size = new System.Drawing.Size(200, 23);

            this.portRohdeSchwarzLabel = new System.Windows.Forms.Label();
            this.portRohdeSchwarzLabel.Text = "Port:";
            this.portRohdeSchwarzLabel.Location = new System.Drawing.Point(20, 85);
            this.portRohdeSchwarzLabel.Size = new System.Drawing.Size(100, 23);

            this.portRohdeSchwarzTextBox = new System.Windows.Forms.TextBox();
            this.portRohdeSchwarzTextBox.Location = new System.Drawing.Point(120, 85);
            this.portRohdeSchwarzTextBox.Size = new System.Drawing.Size(200, 23);
            
            // Keithley
            this.titleKeithleyLabel = new System.Windows.Forms.Label();
            this.titleKeithleyLabel.Text = "Keithley";
            this.titleKeithleyLabel.Location = new System.Drawing.Point(125, 115);
            this.titleKeithleyLabel.Size = new System.Drawing.Size(200, 23);
            this.titleKeithleyLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            this.ipKeithleyLabel = new System.Windows.Forms.Label();
            this.ipKeithleyLabel.Text = "IP Address:";
            this.ipKeithleyLabel.Location = new System.Drawing.Point(20, 145);
            this.ipKeithleyLabel.Size = new System.Drawing.Size(100, 23);
            
            this.ipKeithleyTextBox = new System.Windows.Forms.TextBox();
            this.ipKeithleyTextBox.Location = new System.Drawing.Point(120, 145);
            this.ipKeithleyTextBox.Size = new System.Drawing.Size(200, 23);
            
            // Chroma
            this.titleChromaLabel = new System.Windows.Forms.Label();
            this.titleChromaLabel.Text = "Chroma";
            this.titleChromaLabel.Location = new System.Drawing.Point(125, 175);
            this.titleChromaLabel.Size = new System.Drawing.Size(200, 23);
            this.titleChromaLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);

            this.ipChromaLabel = new System.Windows.Forms.Label();
            this.ipChromaLabel.Text = "IP Address:";
            this.ipChromaLabel.Location = new System.Drawing.Point(20, 205);
            this.ipChromaLabel.Size = new System.Drawing.Size(100, 23);

            this.ipChromaTextBox = new System.Windows.Forms.TextBox();
            this.ipChromaTextBox.Location = new System.Drawing.Point(120, 205);
            this.ipChromaTextBox.Size = new System.Drawing.Size(200, 23);
            
            // Confirm
            this.confirmButton = new System.Windows.Forms.Button();
            this.confirmButton.Text = "Confirm";
            this.confirmButton.Location = new System.Drawing.Point(120, 300);
            this.confirmButton.Size = new System.Drawing.Size(100, 30);
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);

            // RohdeSchwarz
            this.Controls.Add(this.ipRohdeSchwarzLabel);
            this.Controls.Add(this.ipRohdeSchwarzTextBox);
            this.Controls.Add(this.portRohdeSchwarzLabel);
            this.Controls.Add(this.portRohdeSchwarzTextBox);
            this.Controls.Add(this.titleRohdeSchwarzLabel);
            
            // Keithley
            this.Controls.Add(this.titleKeithleyLabel);
            this.Controls.Add(this.ipKeithleyLabel);
            this.Controls.Add(this.ipKeithleyTextBox);
            
            // Chroma
            this.Controls.Add(this.titleChromaLabel);
            this.Controls.Add(this.ipChromaLabel);
            this.Controls.Add(this.ipChromaTextBox);
            
            // Confirm
            this.Controls.Add(this.confirmButton);
        }
    }
}