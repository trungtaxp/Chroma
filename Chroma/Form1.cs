using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
            SetFullScreen();
        }
        
        private void SetFullScreen()
        {
            // Lấy thông tin về màn hình hiện tại
            Screen screen = Screen.FromControl(this);
    
            // Đặt kích thước form để phù hợp với kích thước màn hình
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = screen.Bounds;
            this.TopMost = true;
        }

        private void InitializeCustomComponents()
        {
            // Initialize ComboBox
            deviceComboBox = new ComboBox();
            deviceComboBox.Items.AddRange(new string[] { "Rohde & Schwarz", "Keithley", "Chroma" });
            deviceComboBox.SelectedIndex = 0; // Default selection
            deviceComboBox.Location = new System.Drawing.Point(10, 10);
            deviceComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(deviceComboBox);

            // Initialize Button
            connectButton = new Button();
            connectButton.Text = "Connect";
            connectButton.Location = new System.Drawing.Point(10, 40);
            connectButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            connectButton.Click += new EventHandler(connectButton_Click);
            this.Controls.Add(connectButton);

            // Initialize GroupBox for functions
            functionGroupBox = new GroupBox();
            functionGroupBox.Text = "Device Functions";
            functionGroupBox.Location = new System.Drawing.Point(10, 70);
            functionGroupBox.Size = new System.Drawing.Size(250, 250);
            functionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            functionGroupBox.Visible = false; // Initially hidden
            this.Controls.Add(functionGroupBox);

            // Initialize ProgressBar for loading
            loadingProgressBar = new ProgressBar();
            loadingProgressBar.Style = ProgressBarStyle.Marquee;
            loadingProgressBar.Location = new System.Drawing.Point(connectButton.Right + 10, connectButton.Top);
            loadingProgressBar.Size = new System.Drawing.Size(100, 23);
            loadingProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
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
                    connectionString = "TCPIP0::192.168.1.30::5200::SOCKET"; // Rohde & Schwarz connection string
                    break;
                case "Keithley":
                    connectionString = "GPIB0::16::INSTR"; // Keithley connection string
                    break;
                case "Chroma":
                    connectionString = "GPIB0::7::INSTR"; // Chroma connection string
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

                /*MessageBox.Show("Connected! \n" + idnResponse__ConnectDrive + "\n");*/
                MessageBox.Show("Connected successfully!", "Success");

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
                function1Button.Text = "Show data";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += async (s, e) =>
                {
                    await ShowRohdeSchwarzDataAsync();
                };
                
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
                function1Button.Text = "Show DCV";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write(":MEAS:VOLT:DC?\n");
                    var dcvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("DC Voltage: " + dcvResponse, "DCV Measurement");
                };
                functionGroupBox.Controls.Add(function1Button);

                Button function2Button = new Button();
                function2Button.Text = "Show ACV";
                function2Button.Location = new System.Drawing.Point(10, 50);
                function2Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write(":MEAS:VOLT:AC?\n");
                    var dcvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("AC Voltage: " + dcvResponse, "ACV Measurement");
                };
                functionGroupBox.Controls.Add(function2Button);
            }
            else if (selectedDevice == "Chroma")
            {
                // Add buttons for Chroma functions
                Button function1Button = new Button();
                function1Button.Text = "Show Voltage";
                function1Button.Location = new System.Drawing.Point(10, 20);
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write("MEAS:VOLT?\n");
                    var voltageResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("Voltage: " + voltageResponse, "Voltage Measurement");
                };
                functionGroupBox.Controls.Add(function1Button);

                Button function2Button = new Button();
                function2Button.Text = "Show Current";
                function2Button.Location = new System.Drawing.Point(10, 50);
                function2Button.Click += async (s, e) => 
                {
                    _ConnectDrive.Write("MEAS:VOLT?\n");
                    var voltageResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("Current: " + voltageResponse, "Current Measurement");
                };
                functionGroupBox.Controls.Add(function2Button);
                
                Button function3Button = new Button();
                function3Button.Text = "Show Power";
                function3Button.Location = new System.Drawing.Point(10, 80);
                function3Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write("MEAS:VOLT?\n");
                    var voltageResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("Power: " + voltageResponse, "Power Measurement");
                };
                functionGroupBox.Controls.Add(function3Button);
            }

            functionGroupBox.Visible = true; // Show the GroupBox
        }
        
        private async Task ShowRohdeSchwarzDataAsync()
        {
            try
            {
                _ConnectDrive.Write("FETCH:DATA?\n");
                var dataResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                // Assuming dataResponse is a comma-separated string of data points
                var dataPoints = dataResponse.Split(',').Select(double.Parse).ToArray();

                // Show data in a chart (you need to add a Chart control to your form)
                Chart dataChart = new Chart();
                dataChart.Location = new System.Drawing.Point(10, 110);
                dataChart.Size = new System.Drawing.Size(400, 300);
                functionGroupBox.Controls.Add(dataChart);

                var series = new Series
                {
                    Name = "Data",
                    Color = Color.Blue,
                    ChartType = SeriesChartType.Line
                };

                dataChart.Series.Add(series);

                for (int i = 0; i < dataPoints.Length; i++)
                {
                    series.Points.AddXY(i, dataPoints[i]);
                }

                dataChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error");
            }
        }
        
    }
}