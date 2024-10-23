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

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.White;

            _mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(this.ClientSize.Width * 4 / 6.0),
                IsSplitterFixed = true // Prevent moving the splitter
            };
            this.Controls.Add(_mainSplitContainer);

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
    }
}