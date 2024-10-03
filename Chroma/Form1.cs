using System;
using System.Windows.Forms;
using Ivi.Visa; //This .NET assembly is installed with your NI VISA installation
using IviVisaExtended; //Custom extention functions for Ivi.Visa - all are defined in the IviVisaExtended Project


namespace Chroma
{
    public partial class Form1 : Form
    {
        public static IMessageBasedSession _Chroma63206A = null;
        private ComboBox deviceComboBox;
        private Button connectButton;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Initialize ComboBox
            deviceComboBox = new ComboBox();
            deviceComboBox.Items.AddRange(new string[] { "Rohde & Schwarz", "Keithley", "Chroma" });
            deviceComboBox.SelectedIndex = 0; // Default selection
            deviceComboBox.Location = new System.Drawing.Point(10, 10);
            this.Controls.Add(deviceComboBox);

            // Initialize Button
            connectButton = new Button();
            connectButton.Text = "Connect";
            connectButton.Location = new System.Drawing.Point(10, 40);
            connectButton.Click += new EventHandler(connectButton_Click);
            this.Controls.Add(connectButton);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            string selectedDevice = deviceComboBox.SelectedItem.ToString();
            string connectionString = "";

            switch (selectedDevice)
            {
                case "Rohde & Schwarz":
                    connectionString = "TCPIP0::192.168.1.10::INSTR"; // Example connection string
                    break;
                case "Keithley":
                    connectionString = "TCPIP0::192.168.1.20::INSTR"; // Example connection string
                    break;
                case "Chroma":
                    connectionString = "TCPIP0::192.168.1.30::INSTR"; // Example connection string
                    break;
            }

            Instrument_Connect(connectionString);
        }

        private void Instrument_Connect(string connectionString)
        {
            try
            {
                _Chroma63206A = GlobalResourceManager.Open(connectionString) as IMessageBasedSession;
                _Chroma63206A.TimeoutMilliseconds = 2000; //Timeout for VISA Read Operations
                _Chroma63206A.SendEndEnabled = true;
                _Chroma63206A.TerminationCharacterEnabled = true;
                _Chroma63206A.Clear();
                _Chroma63206A.Write("*IDN?\n");
                var idnResponse__Chroma63206A = _Chroma63206A.RawIO.ReadString();

                MessageBox.Show("Connected! \n" + idnResponse__Chroma63206A + "\n");
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                MessageBox.Show("Cannot connect with the selected device:\n" + e.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _Chroma63206A.Write(textBox1.Text.Trim() + "\n");
                var idnResponse__Chroma63206A = _Chroma63206A.RawIO.ReadString();
                label1.Text = idnResponse__Chroma63206A.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect with MS2090A:\n" + ex.Message, "Error");
            }
        }
    }
}
