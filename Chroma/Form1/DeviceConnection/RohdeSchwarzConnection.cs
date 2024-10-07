using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa;

namespace Chroma
{
    public class RohdeSchwarzConnection
    {
        private IMessageBasedSession _connectDrive;
        private DeviceConfig _config;
        private ICommands _commands;
        private GroupBox _groupBox;

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

                Button setParametersButton = new Button
                {
                    Text = "Set Parameters",
                    Location = new System.Drawing.Point(10, 20)
                };
                setParametersButton.Click += (s, _) =>
                {
                    MessageBox.Show("Set Parameters clicked", "Set Parameters");
                };
                _groupBox.Controls.Add(setParametersButton);
            }
            catch (Ivi.Visa.NativeVisaException)
            {
                statusLabel.Text = $"Cannot connect to {_config.DeviceName}";
            }
        }
    }
}