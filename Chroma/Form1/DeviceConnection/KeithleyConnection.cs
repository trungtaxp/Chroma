using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;

namespace Chroma.Form1.DeviceConnection
{
    public class KeithleyConnection
    {
        private IMessageBasedSession _connectDrive;
        private DeviceConfig _config;
        private ICommands _commands;
        private GroupBox _groupBox;

        public KeithleyConnection(DeviceConfig config, ICommands commands, GroupBox groupBox)
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

                        var dcvDataGridView = _groupBox.Controls.OfType<DataGridView>().FirstOrDefault();
                        if (dcvDataGridView == null)
                        {
                            dcvDataGridView = new DataGridView
                            {
                                Dock = DockStyle.Bottom,
                                ReadOnly = true,
                                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                                ColumnCount = 4
                            };
                            dcvDataGridView.Columns[0].Name = "NVDC";
                            dcvDataGridView.Columns[1].Name = "SECS";
                            dcvDataGridView.Columns[2].Name = "RDNG#";
                            dcvDataGridView.Columns[3].Name = "EXTCHAN";
                            _groupBox.Controls.Add(dcvDataGridView);
                        }

                        var values = FormatResponse(dcvResponse).Split(',');
                        dcvDataGridView.Rows.Add(values);
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

                        var acvDataGridView = _groupBox.Controls.OfType<DataGridView>().FirstOrDefault();
                        if (acvDataGridView == null)
                        {
                            acvDataGridView = new DataGridView
                            {
                                Dock = DockStyle.Bottom,
                                ReadOnly = true,
                                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                                ColumnCount = 4
                            };
                            acvDataGridView.Columns[0].Name = "NVAC";
                            acvDataGridView.Columns[1].Name = "SECS";
                            acvDataGridView.Columns[2].Name = "RDNG#";
                            acvDataGridView.Columns[3].Name = "EXTCHAN";
                            _groupBox.Controls.Add(acvDataGridView);
                        }

                        var values = FormatResponse(acvResponse).Split(',');
                        acvDataGridView.Rows.Add(values);
                    };
                    _groupBox.Controls.Add(acvButton);
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