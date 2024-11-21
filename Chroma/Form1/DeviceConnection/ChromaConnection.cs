using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;

namespace Chroma.Form1.DeviceConnection
{
    public class ChromaConnection
    {
        private IMessageBasedSession _connectDrive;
        private readonly DeviceConfig _config;
        private readonly ICommands _commands;
        private readonly GroupBox _groupBox;
        private Timer _timer;

        public ChromaConnection(DeviceConfig config, ICommands commands, GroupBox groupBox)
        {
            _config = config;
            _commands = commands;
            _groupBox = groupBox;
        }

        public async Task ConnectAsync()
        {
            Label statusLabel = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.Red
            };
            _groupBox.Controls.Add(statusLabel);

            try
            {
                _connectDrive = GlobalResourceManager.Open(_config.ConnectionString) as IMessageBasedSession;
                if (_connectDrive != null)
                {
                    _connectDrive.TimeoutMilliseconds = 3000;
                    _connectDrive.SendEndEnabled = true;
                    _connectDrive.TerminationCharacterEnabled = true;
                    _connectDrive.Clear();

                    // Table to display the data
                    var dataGridView = _groupBox.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (dataGridView == null)
                    {
                        dataGridView = new DataGridView
                        {
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                            BackgroundColor = Color.White,
                            ColumnCount = 2,
                            RowHeadersVisible = false
                        };
                        dataGridView.Columns[0].Name = "Time";
                        dataGridView.Columns[1].Name = "Voltage";
                        _groupBox.Controls.Add(dataGridView);
                    }

                    Button realTimeVoltageButton = new Button
                    {
                        Text = "Show Real-Time Voltage",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };

                    realTimeVoltageButton.Click += (sender, e) =>
                    {
                        if (_timer == null)
                        {
                            _timer = new Timer { Interval = 1000 }; // 1 second interval
                            _timer.Tick += async (s, args) =>
                            {
                                var voltage = await GetVoltageAsync();
                                var time = DateTime.Now.ToString("HH:mm:ss");
                                dataGridView.Rows.Add(time, voltage);
                            };
                        }

                        if (_timer.Enabled)
                        {
                            _timer.Stop();
                            realTimeVoltageButton.Text = "Show Real-Time Voltage";
                        }
                        else
                        {
                            _timer.Start();
                            realTimeVoltageButton.Text = "Stop Real-Time Voltage";
                        }
                    };
                    _groupBox.Controls.Add(realTimeVoltageButton);
                }
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
            }
        }

        private async Task<string> GetVoltageAsync()
        {
            if (_connectDrive == null)
            {
                throw new InvalidOperationException("Device is not connected.");
            }

            _connectDrive.RawIO.Write("MEAS:VOLT?\n");
            return await Task.Run(() => _connectDrive.RawIO.ReadString());
        }
    }
}