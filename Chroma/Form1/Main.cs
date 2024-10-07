using System.Windows.Forms;
using Chroma.Commands;

namespace Chroma.Form1
{
    public partial class Main : Form
    {
        private ICommands _rohdeSchwarzCommands = new RohdeSchwarzCommands();
        private ICommands _keithleyCommands = new KeithleyCommands();
        private ICommands _chromaCommands = new ChromaCommands();

        public Main()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetFullScreen();
            LoadDeviceConfigs();
            ConnectToAllDevices();
        }
    }
}