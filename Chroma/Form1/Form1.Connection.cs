using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa;
using IviVisaExtended;

namespace Chroma
{
    public partial class Form1
    {
        public static IMessageBasedSession _ConnectDrive = null;

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
                    Button setParametersButton = new Button
                    {
                        Text = "Set Parameters",
                        Location = new System.Drawing.Point(10, 20)
                    };
                    setParametersButton.Click += (s, _) =>
                    {
                        MessageBox.Show("Set Parameters clicked", "Set Parameters");
                    };
                    rohdeSchwarzGroupBox.Controls.Add(setParametersButton);
                }
                else if (config.DeviceName == "Keithley")
                {
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
            catch (Ivi.Visa.NativeVisaException)
            {
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
    }
}