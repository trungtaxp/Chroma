using System.Drawing;
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
                Text = $"Connecting to {_config.DeviceName}...",
                Dock = DockStyle.Top,
                ForeColor = Color.Red
            };
            _groupBox.Controls.Add(statusLabel);

            try
            {
                _connectDrive = GlobalResourceManager.Open(_config.ConnectionString) as IMessageBasedSession;
                _connectDrive.TimeoutMilliseconds = 3000;
                _connectDrive.SendEndEnabled = true;
                _connectDrive.TerminationCharacterEnabled = true;
                _connectDrive.Clear();
                var formattedIO = (IMessageBasedFormattedIO)_connectDrive;
                formattedIO.Write(_commands.Identify() + "\n");
                var idnResponse = await Task.Run(() => formattedIO.ReadString());

                statusLabel.Text = $"Connected to {_config.DeviceName} successfully!";
                statusLabel.ForeColor = Color.Green;

                formattedIO.Write(_commands.MeasureVoltage() + "\n");
                var dcvResponse = await Task.Run(() => formattedIO.ReadString());

                Label dcvLabel = new Label
                {
                    Text = "DC Voltage: " + dcvResponse,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Blue
                };
                _groupBox.Controls.Add(dcvLabel);

                formattedIO.Write(":MEAS:VOLT:AC?\n");
                var acvResponse = await Task.Run(() => formattedIO.ReadString());

                Label acvLabel = new Label
                {
                    Text = "AC Voltage: " + acvResponse,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Green
                };
                _groupBox.Controls.Add(acvLabel);
            }
            catch (Ivi.Visa.NativeVisaException e)
            {
                // statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
                MessageBox.Show("Cannot connect with the selected device:\n" + e.Message, "Error");
            }
        }
    }
}