using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ivi.Visa;
using IviVisaExtended;
using System.IO;

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

        private ICommands rohdeSchwarzCommands = new RohdeSchwarzCommands();
        private ICommands keithleyCommands = new KeithleyCommands();
        private ICommands chromaCommands = new ChromaCommands();

        private DeviceConfig rohdeSchwarzConfig;
        private DeviceConfig keithleyConfig;
        private DeviceConfig chromaConfig;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetFullScreen();
            LoadDeviceConfigs();
            ConnectToAllDevices();
        }

        private void LoadDeviceConfigs()
        {
            rohdeSchwarzConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_NAME") ?? "Rohde & Schwarz",
                ConnectionString = Environment.GetEnvironmentVariable("ROHDE_SCHWARZ_CONN") ?? "TCPIP0::192.168.1.30::5200::SOCKET"
            };

            keithleyConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("KEITHLEY_NAME") ?? "Keithley",
                ConnectionString = Environment.GetEnvironmentVariable("KEITHLEY_CONN") ?? "GPIB0::16::INSTR"
            };

            chromaConfig = new DeviceConfig
            {
                DeviceName = Environment.GetEnvironmentVariable("CHROMA_NAME") ?? "Chroma",
                ConnectionString = Environment.GetEnvironmentVariable("CHROMA_CONN") ?? "GPIB0::7::INSTR"
            };
        }

        private async void ConnectToAllDevices()
        {
            await ConnectDeviceAsync(rohdeSchwarzConfig, rohdeSchwarzCommands);
            await ConnectDeviceAsync(keithleyConfig, keithleyCommands);
            await ConnectDeviceAsync(chromaConfig, chromaCommands);
        }

        private async Task ConnectDeviceAsync(DeviceConfig config, ICommands commands)
        {
            Label statusLabel = new Label
            {
                Text = $"Connecting to {config.DeviceName}...",
                Dock = DockStyle.Top,
                ForeColor = Color.Red
            };

            AddStatusLabelToGroupBox(config.DeviceName, statusLabel);

            try
            {
                _ConnectDrive = GlobalResourceManager.Open(config.ConnectionString) as IMessageBasedSession;
                _ConnectDrive.TimeoutMilliseconds = 3000;
                _ConnectDrive.SendEndEnabled = true;
                _ConnectDrive.TerminationCharacterEnabled = true;
                _ConnectDrive.Clear();
                _ConnectDrive.Write(commands.Identify() + "\n");
                var idnResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                statusLabel.Text = $"Connected to {config.DeviceName} successfully!";
                statusLabel.ForeColor = Color.Green;

                if (config.DeviceName == "Rohde & Schwarz")
                {
                    await ShowRohdeSchwarzDataAsync(commands);
                    // Create and display the "Set Parameters" button
                    Button setParametersButton = new Button
                    {
                        Text = "Set Parameters",
                        Location = new System.Drawing.Point(10, 20)
                    };
                    setParametersButton.Click += (s, e) =>
                    {
                        // Add logic to set parameters here
                        MessageBox.Show("Set Parameters clicked", "Set Parameters");
                    };
                    rohdeSchwarzGroupBox.Controls.Add(setParametersButton);
                }
                else if (config.DeviceName == "Keithley")
                {
                    // Measure and display DCV immediately
                    _ConnectDrive.Write(commands.MeasureVoltage() + "\n");
                    var dcvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                    Label dcvLabel = new Label
                    {
                        Text = "DC Voltage: " + dcvResponse,
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };
                    keithleyGroupBox.Controls.Add(dcvLabel);

                    // Measure and display ACV immediately
                    _ConnectDrive.Write(":MEAS:VOLT:AC?\n");
                    var acvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                    Label acvLabel = new Label
                    {
                        Text = "AC Voltage: " + acvResponse,
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Green
                    };
                    keithleyGroupBox.Controls.Add(acvLabel);
                }
                else if (config.DeviceName == "Chroma")
                {
                    _ConnectDrive.Write(commands.MeasureVoltage() + "\n");
                    var voltageResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());

                    Label voltageLabel = new Label
                    {
                        Text = "Voltage: " + voltageResponse,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };
                    chromaGroupBox.Controls.Add(voltageLabel);
                }
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                /*statusLabel.Text = $"Cannot connect to {config.DeviceName}: {e.Message}";*/
                statusLabel.Text = $"Cannot connect to {config.DeviceName}";
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
                function1Button.Click += async (s, e) => await ShowRohdeSchwarzDataAsync(rohdeSchwarzCommands);
                functionGroupBox.Controls.Add(function1Button);
            }
            else if (deviceName == "Keithley")
            {
                Button function1Button = new Button
                {
                    Text = "Show DCV",
                    Location = new System.Drawing.Point(10, 20)
                };
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write(keithleyCommands.MeasureVoltage() + "\n");
                    var dcvResponse = await Task.Run(() => _ConnectDrive.RawIO.ReadString());
                    MessageBox.Show("DC Voltage: " + dcvResponse, "DCV Measurement");
                };
                functionGroupBox.Controls.Add(function1Button);
            }
            else if (deviceName == "Chroma")
            {
                Button function1Button = new Button
                {
                    Text = "Show Voltage",
                    Location = new System.Drawing.Point(10, 20)
                };
                function1Button.Click += async (s, e) =>
                {
                    _ConnectDrive.Write(chromaCommands.MeasureVoltage() + "\n");
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
            this.BackColor = Color.White; // Set the background color to white

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

        private async Task ShowRohdeSchwarzDataAsync(ICommands commands)
        {
            try
            {
                if (commands is RohdeSchwarzCommands rohdeSchwarzCommands)
                {
                    _ConnectDrive.Write(rohdeSchwarzCommands.FetchData() + "\n");
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
                else
                {
                    MessageBox.Show("FetchData is not supported by this device.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching data: " + ex.Message, "Error");
            }
        }
    }
}