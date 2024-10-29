using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;

namespace Chroma.Form1.DeviceConnection
{
    public class RohdeSchwarzConnection
    {
        private IMessageBasedSession _connectDrive;
        private readonly DeviceConfig _config;
        private readonly ICommands _commands;
        private readonly GroupBox _groupBox;

        public RohdeSchwarzConnection(DeviceConfig config, ICommands commands, GroupBox groupBox)
        {
            _config = config;
            _commands = commands;
            _groupBox = groupBox;
        }

        public async Task ConnectAsync()
        {
            Label statusLabel = new Label
            {
                Dock = DockStyle.Top,
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

                    // table to display the data
                    var DataGridView = _groupBox.Controls.OfType<DataGridView>().FirstOrDefault();
                    if (DataGridView == null)
                    {
                        DataGridView = new DataGridView
                        {
                            Dock = DockStyle.Fill,
                            ReadOnly = true,
                            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                            BackgroundColor = Color.White,
                            ColumnCount = 1,
                            RowHeadersVisible = false
                            // GridColor = Color.Gray
                            // Margin = new Padding(30)
                        };
                        DataGridView.Columns[0].Name = "NVDC";/*
                        DataGridView.Columns[1].Name = "SECS";
                        DataGridView.Columns[2].Name = "RDNG";
                        DataGridView.Columns[3].Name = "EXTCHAN";*/
                        _groupBox.Controls.Add(DataGridView);
                    }

                    Button dcvButton = new Button
                    {
                        Text = "Show Voltage",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };

                    dcvButton.Click += async (sender, e) =>
                    {
                        _connectDrive.RawIO.Write(new ChromaCommands().MeasureVoltage() + "\n");
                        var dcvResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());
                        DataGridView.Rows.Add(dcvResponse);
                        // MessageBox.Show("Data:\n" + dcvResponse, "Show Voltage");
                        /*var values = FormatResponse(dcvResponse).Split(',');
                        DataGridView.Rows.Add(values);*/
                    };
                    _groupBox.Controls.Add(dcvButton);
                }
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
            }
        }

        private string FormatResponse(string response)
        {
            var values = response.Split(',');
            return $"{values[0]}, {values[1]}, {values[2]}, {values[3]}";
        }
    }
}