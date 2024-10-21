using System.Threading.Tasks;
using System.Windows.Forms;
using Chroma.Commands;

namespace Chroma.Form1
{
    public partial class Main : Form
    {
        private ICommands _rohdeSchwarzCommands = new RohdeSchwarzCommands();
        private ICommands _keithleyCommands = new KeithleyCommands();
        private ICommands _chromaCommands = new ChromaCommands();
        private Timer _connectionCheckTimer;

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetFullScreen();
            LoadDeviceConfigs();
            ConnectToAllDevices();
            InitializeConnectionCheckTimer();
        }

        private void InitializeConnectionCheckTimer()
        {
            _connectionCheckTimer = new Timer();
            _connectionCheckTimer.Interval = 5000; // Check every 5 seconds
            _connectionCheckTimer.Tick += async (sender, e) => await CheckAndReconnectDevices();
            _connectionCheckTimer.Start();
        }

        private async Task CheckAndReconnectDevices()
        {
            if (!_rohdeSchwarzConnection.IsConnected)
            {
                await _rohdeSchwarzConnection.ConnectAsync();
            }

            if (!_keithleyConnection.IsConnected)
            {
                await _keithleyConnection.ConnectAsync();
            }

            if (!_chromaConnection.IsConnected)
            {
                await _chromaConnection.ConnectAsync();
            }
        }
    }
}