using System;
using System.Windows.Forms;

namespace Chroma.Helper
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }
        
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Handle the confirm button click event
            string ipAddress = ipRohdeSchwarzTextBox.Text;
            string port = portRohdeSchwarzTextBox.Text;

            // Perform validation or other actions here

            MessageBox.Show($"IP Address: {ipAddress}\nPort: {port}", "Confirmation");
        }
        
    }
}