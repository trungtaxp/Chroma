using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;
using ScottPlot.Colormaps;

namespace Chroma.Form1.DeviceConnection
{
    public class KeithleyConnection
    {
        private IMessageBasedSession _connectDrive;
        private readonly DeviceConfig _config;
        private readonly ICommands _commands;
        private readonly GroupBox _groupBox;

        public KeithleyConnection(DeviceConfig config, ICommands commands, GroupBox groupBox)
        {
            _config = config;
            _commands = commands;
            _groupBox = groupBox;
        }

        public async Task ConnectAsync()
        {
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
                            RowHeadersVisible = false,
                        };
                        DataGridView.Columns[0].Name = "Voltage";
                        _groupBox.Controls.Add(DataGridView);
                    }

                    Button dcvButton = new Button
                    {
                        Text = "Show DC Voltage",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };

                    dcvButton.Click += async (sender, e) =>
                    {
                        _connectDrive.RawIO.Write(_commands.MeasureVoltage() + "\n");
                        var dcvResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());
                        var value = FormatResponse(dcvResponse, false);
                        DataGridView.Rows.Add(value);
                    };
                    _groupBox.Controls.Add(dcvButton);

                    Button acvButton = new Button
                    {
                        Text = "Show AC Voltage",
                        Dock = DockStyle.Top,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Green
                    };
                    acvButton.Click += async (sender, e) =>
                    {
                        _connectDrive.RawIO.Write(new KeithleyCommands().MeasureVoltagAc() + "\n");
                        var acvResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());
                        var value = FormatResponse(acvResponse, true);
                        DataGridView.Rows.Add(value);
                    };
                    _groupBox.Controls.Add(acvButton);
                }
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                Label statusLabel = new Label
                {
                    Dock = DockStyle.Fill,
                    ForeColor = Color.Red
                };
                _groupBox.Controls.Add(statusLabel);
                statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
            }
        }
        private string FormatResponse(string response, bool isAC = false)
        {
            var values = response.Split(',');
            var voltageValue = values[0].Split('E')[0];
            var unit = isAC ? "mAVC" : "mVDC";

            return $"{voltageValue} {unit}";
        }
    }
}