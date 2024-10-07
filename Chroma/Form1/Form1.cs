using System.Windows.Forms;

namespace Chroma
{
    public partial class Form1 : Form
    {
        private ICommands rohdeSchwarzCommands = new RohdeSchwarzCommands();
        private ICommands keithleyCommands = new KeithleyCommands();
        private ICommands chromaCommands = new ChromaCommands();

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
            SetFullScreen();
            LoadDeviceConfigs();
            ConnectToAllDevices();
        }
    }
}