using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa; //This .NET assembly is installed with your NI VISA installation
using IviVisaExtended; //Custom extention functions for Ivi.Visa - all are defined in the IviVisaExtended Project

namespace Chroma
{
    public partial class Form1 : Form
    {
        public static IMessageBasedSession _ConnectDrive = null;
        private ComboBox deviceComboBox;
        private Button connectButton;
        private GroupBox functionGroupBox;
        private ProgressBar loadingProgressBar;

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

            // Initialize GroupBox for functions
            functionGroupBox = new GroupBox();
            functionGroupBox.Text = "Device Functions";
            functionGroupBox.Location = new System.Drawing.Point(10, 70);
            functionGroupBox.Size = new System.Drawing.Size(200, 200);
            functionGroupBox.Visible = false; // Initially hidden
            this.Controls.Add(functionGroupBox);

            // Initialize ProgressBar for loading
            loadingProgressBar = new ProgressBar();
            loadingProgressBar.Style = ProgressBarStyle.Marquee;
            loadingProgressBar.Location = new System.Drawing.Point(connectButton.Right + 10, connectButton.Top);
            loadingProgressBar.Size = new System.Drawing.Size(100, 23);
            loadingProgressBar.Visible = false; // Initially hidden
            this.Controls.Add(loadingProgressBar);
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            string selectedDevice = deviceComboBox.SelectedItem.ToString();
            string connectionString = "";

            switch (selectedDevice)
            {
                case "Rohde & Schwarz":
                    connectionString = "GPIB0::7::INSTR"; // Rohde & Schwarz connection string
                    break;
                case "Keithley":
                    connectionString = "GPIB0::16::INSTR"; // Keithley connection string
                    break;
                case "Chroma":
                    connectionString = "TCPIP0::192.168.1.30::5200::SOCKET"; // Chroma connection string
                    break;
            }

            // Show loading progress bar
            loadingProgressBar.Visible = true;

            await Instrument_ConnectAsync(connectionString);

            // Hide loading progress bar
            loadingProgressBar.Visible = false;
        }

        private async Task Instrument_ConnectAsync(string connectionString)
        {
            try
            {
                _ConnectDrive = GlobalResourceManager.Open(connectionString) as IMessageBasedSession;
                _ConnectDrive.TimeoutMilliseconds = 3000; //Timeout for VISA Read Operations
                _ConnectDrive.SendEndEnabled = true;
                _ConnectDrive.TerminationCharacterEnabled = true;
                _ConnectDrive.Clear();
                _ConnectDrive.Write("*IDN?\n");
                var idnResponse__ConnectDrive = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                MessageBox.Show("Connected! \n" + idnResponse__ConnectDrive + "\n");

                // Show function options based on the connected device
                ShowFunctionOptions();
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                MessageBox.Show("Cannot connect with the selected device:\n" + e.Message, "Error");
            }
        }

        private void ShowFunctionOptions()
        {
            functionGroupBox.Controls.Clear(); // Clear previous controls

            string selectedDevice = deviceComboBox.SelectedItem.ToString();

            if (selectedDevice == "Rohde & Schwarz")
            {
                // Add buttons for Rohde & Schwarz functions
                Button function1Button = new Button();
                function1Button.Text = "Function 1";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += (s, e) => { /* Add function 1 code here */ };
                functionGroupBox.Controls.Add(function1Button);

                Button function2Button = new Button();
                function2Button.Text = "Function 2";
                function2Button.Location = new System.Drawing.Point(10, 50);
                function2Button.Click += (s, e) => { /* Add function 2 code here */ };
                functionGroupBox.Controls.Add(function2Button);
            }
            else if (selectedDevice == "Keithley")
            {
                // Add buttons for Keithley functions
                Button function1Button = new Button();
                function1Button.Text = "Function 1";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += (s, e) => { /* Add function 1 code here */ };
                functionGroupBox.Controls.Add(function1Button);

                Button function2Button = new Button();
                function2Button.Text = "Function 2";
                function2Button.Location = new System.Drawing.Point(10, 50);
                function2Button.Click += (s, e) => { /* Add function 2 code here */ };
                functionGroupBox.Controls.Add(function2Button);
            }
            else if (selectedDevice == "Chroma")
            {
                // Add buttons for Chroma functions
                Button function1Button = new Button();
                function1Button.Text = "Function 1";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += (s, e) => { /* Add function 1 code here */ };
                functionGroupBox.Controls.Add(function1Button);

                Button function2Button = new Button();
                function2Button.Text = "Function 2";
                function2Button.Location = new System.Drawing.Point(10, 50);
                function2Button.Click += (s, e) => { /* Add function 2 code here */ };
                functionGroupBox.Controls.Add(function2Button);
            }

            functionGroupBox.Visible = true; // Show the GroupBox
        }
    }
}