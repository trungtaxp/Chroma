using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;
using Ivi.Visa;
using ScottPlot;
using ScottPlot.WinForms;
using Color = System.Drawing.Color;
using Label = System.Windows.Forms.Label;

namespace Chroma.Form1.DeviceConnection
{
    public class RohdeSchwarzConnection
    {
        private IMessageBasedSession _connectDrive;
        private DeviceConfig _config;
        private ICommands _commands;
        private GroupBox _groupBox;
        private FormsPlot _formsPlot;

        public RohdeSchwarzConnection(DeviceConfig config, ICommands commands, GroupBox groupBox)
        {
            _config = config;
            _commands = commands;
            _groupBox = groupBox;
            InitializePlot();
        }

        private void InitializePlot()
        {
            _formsPlot = new FormsPlot
            {
                Size = new System.Drawing.Size(_groupBox.Width / 2, _groupBox.Height / 2),
                Location = new System.Drawing.Point(_groupBox.Width / 2, 0)
            };
            _groupBox.Controls.Add(_formsPlot);
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
            
            double[] dataX = Generate.Sin(51);
            double[] dataY = Generate.Cos(51);
            _formsPlot.Plot.Add.Signal(dataX);
            _formsPlot.Plot.Add.Signal(dataY);
            _formsPlot.Plot.XLabel("Horizonal Axis");
            _formsPlot.Plot.YLabel("Vertical Axis");
            _formsPlot.Plot.Title("Plot Title");

            try
            {
                _connectDrive = GlobalResourceManager.Open(_config.ConnectionString) as IMessageBasedSession;
                _connectDrive.TimeoutMilliseconds = 3000;
                _connectDrive.SendEndEnabled = true;
                _connectDrive.TerminationCharacterEnabled = true;
                _connectDrive.Clear();
                _connectDrive.RawIO.Write(_commands.Identify() + "\n");
                var idnResponse = await Task.Run(() => _connectDrive.RawIO.ReadString());

                statusLabel.Text = $"Connected to {_config.DeviceName} successfully!";
                statusLabel.ForeColor = Color.Green;

                // Example data for the plot
                /*double[] dataX = { 1, 2, 3, 4, 5 };
                double[] dataY = { 1, 4, 9, 16, 25 };

                _formsPlot.Plot.Add.Scatter(dataX, dataY);
                _formsPlot.Plot.Save("demo.png", 400, 300);*/

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