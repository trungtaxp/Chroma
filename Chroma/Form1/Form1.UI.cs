using System.Drawing;
using System.Windows.Forms;

namespace Chroma.Form1
{
    public partial class Main
    {
        private SplitContainer _mainSplitContainer;
        private SplitContainer _secondarySplitContainer;
        private GroupBox _rohdeSchwarzGroupBox;
        private GroupBox _keithleyGroupBox;
        private GroupBox _chromaGroupBox;
        private GroupBox _functionGroupBox;
        private ComboBox _controlComboBox;

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.White;

            // Create MenuStrip
            var menuStrip = new MenuStrip { Dock = DockStyle.Top };

            // Create File menu
            var fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => { this.Close(); });

            // Create Edit menu
            var editMenu = new ToolStripMenuItem("Options");
            editMenu.DropDownItems.Add("Settings", null, (s, e) => {ShowSettingsDialog(); });

            // Create View menu
            var viewMenu = new ToolStripMenuItem("View");
            viewMenu.DropDownItems.Add("Zoom In", null, (s, e) => { /* Zoom In action */ });
            viewMenu.DropDownItems.Add("Zoom Out", null, (s, e) => { /* Zoom Out action */ });

            // Add menus to MenuStrip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(viewMenu);

            // Add MenuStrip to the form
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Existing SplitContainer code
            _mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(this.ClientSize.Width * 4 / 6.0),
                IsSplitterFixed = true // Prevent moving the splitter
            };
            this.Controls.Add(_mainSplitContainer);
            this.Controls.SetChildIndex(menuStrip, 15); // Ensure MenuStrip is on top

            _secondarySplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = _mainSplitContainer.ClientSize.Height / 2,
                IsSplitterFixed = true // Prevent moving the splitter
            };
            _mainSplitContainer.Panel2.Controls.Add(_secondarySplitContainer);

            _rohdeSchwarzGroupBox = new GroupBox
            {
                Text = "Rohde & Schwarz",
                Dock = DockStyle.Fill
            };
            _mainSplitContainer.Panel1.Controls.Add(_rohdeSchwarzGroupBox);

            _keithleyGroupBox = new GroupBox
            {
                Text = "Keithley",
                Dock = DockStyle.Fill
            };
            _secondarySplitContainer.Panel1.Controls.Add(_keithleyGroupBox);

            _chromaGroupBox = new GroupBox
            {
                Text = "Chroma",
                Dock = DockStyle.Fill
            };
            _secondarySplitContainer.Panel2.Controls.Add(_chromaGroupBox);

            _functionGroupBox = new GroupBox(); // Initialize functionGroupBox

            _secondarySplitContainer.SplitterDistance = _secondarySplitContainer.ClientSize.Height / 2;
        }

        private void SetFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
        }

        private void ShowSettingsDialog()
        {
            _controlComboBox = new ComboBox
            {
                Name = "Setting Device",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Top,
                Location = new Point(10, 10) // Adjust the location as needed
            };

            var comboBoxForm = new Form
            {
                Text = "Setting",
                Size = new Size(855, 550)
            };

            comboBoxForm.Controls.Add(_controlComboBox);
            comboBoxForm.ShowDialog();
        }
        
    }
}