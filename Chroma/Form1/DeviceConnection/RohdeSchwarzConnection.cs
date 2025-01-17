using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;
using CefSharp.WinForms;

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
            try
            {
                // Add ChromiumWebBrowser control to display the iframe with the web remote control
                var browser = new ChromiumWebBrowser("data:text/html,<iframe src='http://169.168.1.16/instrumentctrl/vnc/vnc_control.php' width='100%' height='100%' frameborder='0'></iframe>") // Replace with the actual URL of the web remote control
                {
                    Dock = DockStyle.Fill
                };
                _groupBox.Controls.Add(browser);
                //_connectDrive = GlobalResourceManager.Open(_config.ConnectionString) as IMessageBasedSession;
                //if (_connectDrive != null)
                //{
                //    _connectDrive.TimeoutMilliseconds = 3000;
                //    _connectDrive.SendEndEnabled = true;
                //    _connectDrive.TerminationCharacterEnabled = true;
                //    _connectDrive.Clear();

                    
                //}
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
    }
}