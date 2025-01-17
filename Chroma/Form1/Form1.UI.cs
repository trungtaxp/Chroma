using System.Drawing;
using System.Windows.Forms;
using Chroma.Helper;

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
            var aboutMenu = new ToolStripMenuItem("About", null, (s, e) => { ShowAboutDialog(); });

          
            // Add menus to MenuStrip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(aboutMenu);

            // Add MenuStrip to the form
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Existing SplitContainer code
            _mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(this.ClientSize.Width * 4.0 / 6.0),
                IsSplitterFixed = true, // Prevent moving the splitter
                SplitterWidth = 15, // Set splitter width to 15
                BackColor = Color.White // Match the background color of the form
            };
            this.Controls.Add(_mainSplitContainer);
            this.Controls.SetChildIndex(menuStrip, 15); // Ensure MenuStrip is on top

            _secondarySplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = _mainSplitContainer.ClientSize.Height / 2,
                IsSplitterFixed = true, // Prevent moving the splitter
                SplitterWidth = 10, // Set splitter width to 10
                BackColor = Color.White // Match the background color of the form
            };
            _mainSplitContainer.Panel2.Controls.Add(_secondarySplitContainer);

            _rohdeSchwarzGroupBox = new GroupBox
            {
                Text = "Rohde & Schwarz",
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10, FontStyle.Bold),
            };
            _mainSplitContainer.Panel1.Controls.Add(_rohdeSchwarzGroupBox);

            _keithleyGroupBox = new GroupBox
            {
                Text = "Keithley",
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10, FontStyle.Bold),
            };
            _secondarySplitContainer.Panel1.Controls.Add(_keithleyGroupBox);

            _chromaGroupBox = new GroupBox
            {
                Text = "Chroma",
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10, FontStyle.Bold),
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

        private void ShowAboutDialog()
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

    }
}