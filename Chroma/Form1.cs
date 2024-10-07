using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ivi.Visa;
using IviVisaExtended;

namespace Chroma
{
    public partial class Form1 : Form
    {
        public static IMessageBasedSession _ConnectDrive = null;
        private SplitContainer mainSplitContainer;
        private SplitContainer secondarySplitContainer;
        private GroupBox rohdeSchwarzGroupBox;
        private GroupBox keithleyGroupBox;
        private GroupBox chromaGroupBox;
        private GroupBox functionGroupBox; // Define as class-level field

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetFullScreen();
            ConnectToAllDevices();
        }

        private async void ConnectToAllDevices()
        {
            await ConnectDeviceAsync("Rohde & Schwarz", "TCPIP0::192.168.1.30::5200::SOCKET");
            await ConnectDeviceAsync("Keithley", "GPIB0::16::INSTR");
            await ConnectDeviceAsync("Chroma", "GPIB0::7::INSTR");
        }

        private async Task ConnectDeviceAsync(string deviceName, string connectionString)
        {
            Label statusLabel = new Label
            {
                Text = $"Connecting to {deviceName}...",
                Dock = DockStyle.Top,
                ForeColor = Color.Red
            };

            AddStatusLabelToGroupBox(deviceName, statusLabel);

            try
            {
                _ConnectDrive = GlobalResourceManager.Open(connectionString) as IMessageBasedSession;
                _ConnectDrive.TimeoutMilliseconds = 3000;
                _ConnectDrive.SendEndEnabled = true;
                _ConnectDrive.TerminationCharacterEnabled = true;
                _ConnectDrive.Clear();
                _ConnectDrive.Write("*IDN?\n");
                var idnResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                statusLabel.Text = $"Connected to {deviceName} successfully!";
                statusLabel.ForeColor = Color.Green;

                ShowFunctionOptions(deviceName);

                if (deviceName == "Rohde & Schwarz")
                {
                    await ShowRohdeSchwarzDataAsync();
                }
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                statusLabel.Text = $"Cannot connect to {deviceName}: {e.Message}";
            }
        }

        private void AddStatusLabelToGroupBox(string deviceName, Label statusLabel)
        {
            if (deviceName == "Rohde & Schwarz")
            {
                rohdeSchwarzGroupBox.Controls.Add(statusLabel);
            }
            else if (deviceName == "Keithley")
            {
                keithleyGroupBox.Controls.Add(statusLabel);
            }
            else if (deviceName == "Chroma")
            {
                chromaGroupBox.Controls.Add(statusLabel);
            }
        }

        private void ShowFunctionOptions(string deviceName)
        {
            functionGroupBox = new GroupBox
            {
                Text = deviceName,
                Dock = DockStyle.Fill
            };

            if (deviceName == "Rohde & Schwarz")
            {
                // Add buttons for Rohde & Schwarz functions
                Button function1Button = new Button
                {
                    Text = "Show data",
                    Location = new System.Drawing.Point(10, 20)
                };
                function1Button.Click += async (s, e) => await ShowRohdeSchwarzDataAsync();
                functionGroupBox.Controls.Add(function1Button);
            }
            else if (deviceName == "Keithley")
            {
                // Add buttons for Keithley functions
                Button function1Button = new Button
                {
                    Text = "Show DCV",
                    Location = new System.Drawing.Point(10, 20)
                };
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write(":MEAS:VOLT:DC?\n");
                    var dcvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("DC Voltage: " + dcvResponse, "DCV Measurement");
                };
                functionGroupBox.Controls.Add(function1Button);
            }
            else if (deviceName == "Chroma")
            {
                // Add buttons for Chroma functions
                Button function1Button = new Button
                {
                    Text = "Show Voltage",
                    Location = new System.Drawing.Point(10, 20)
                };
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write("MEAS:VOLT?\n");
                    var voltageResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("Voltage: " + voltageResponse, "Voltage Measurement");
                };
                functionGroupBox.Controls.Add(function1Button);
            }

            if (deviceName == "Rohde & Schwarz")
            {
                rohdeSchwarzGroupBox.Controls.Add(functionGroupBox);
            }
            else if (deviceName == "Keithley")
            {
                keithleyGroupBox.Controls.Add(functionGroupBox);
            }
            else if (deviceName == "Chroma")
            {
                chromaGroupBox.Controls.Add(functionGroupBox);
            }
        }

        private void SetFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeCustomComponents()
        {
            mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(this.ClientSize.Width * 4 / 6.0)
            };
            this.Controls.Add(mainSplitContainer);

            secondarySplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = mainSplitContainer.ClientSize.Height / 2 // Corrected initialization
            };
            mainSplitContainer.Panel2.Controls.Add(secondarySplitContainer);

            rohdeSchwarzGroupBox = new GroupBox
            {
                Text = "Rohde & Schwarz",
                Dock = DockStyle.Fill
            };
            mainSplitContainer.Panel1.Controls.Add(rohdeSchwarzGroupBox);

            keithleyGroupBox = new GroupBox
            {
                Text = "Keithley",
                Dock = DockStyle.Fill
            };
            secondarySplitContainer.Panel1.Controls.Add(keithleyGroupBox);

            chromaGroupBox = new GroupBox
            {
                Text = "Chroma",
                Dock = DockStyle.Fill
            };
            secondarySplitContainer.Panel2.Controls.Add(chromaGroupBox);

            // Adjust the SplitterDistance to divide the remaining 1/3 between Keithley and Chroma
            secondarySplitContainer.SplitterDistance = secondarySplitContainer.ClientSize.Height / 2;
        }

        private async Task ShowRohdeSchwarzDataAsync()
        {
            try
            {
                _ConnectDrive.Write("FETCH:DATA?\n");
                var dataResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                var dataPoints = dataResponse.Split(',').Select(double.Parse).ToArray();

                Chart dataChart = new Chart
                {
                    Location = new System.Drawing.Point(10, 110),
                    Size = new System.Drawing.Size(400, 300)
                };
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