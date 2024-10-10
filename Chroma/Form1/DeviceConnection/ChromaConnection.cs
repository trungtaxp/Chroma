using System.Drawing;
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
                Text = $"Connecting to {_config.DeviceName}...",
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
                    
                    // _connectDrive.RawIO.Write(_commands.Identify() + "\n");
                    // var idnResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());

                    statusLabel.Text = $"Connected to {_config.DeviceName} successfully!";
                    statusLabel.ForeColor = Color.Green;

                    _connectDrive.RawIO.Write(_commands.MeasureVoltage() + "\n");
                    var voltageResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());

                    Label voltageLabel = new Label
                    {
                        Text = "Voltage: " + voltageResponse,
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Blue
                    };
                    _groupBox.Controls.Add(voltageLabel);
                }
            }
            catch (Ivi.Visa.NativeVisaException)
            {
                statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
            }
        }
    }
}