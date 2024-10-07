using System.Drawing;
using System.Windows.Forms;

namespace Chroma
{
    public partial class Form1
    {
        private SplitContainer mainSplitContainer;
        private SplitContainer secondarySplitContainer;
        private GroupBox rohdeSchwarzGroupBox;
        private GroupBox keithleyGroupBox;
        private GroupBox chromaGroupBox;
        private GroupBox functionGroupBox;

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.White;

            mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(this.ClientSize.Width * 4 / 6.0)
            };
            this.Controls.Add(mainSplitContainer);

            secondarySplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = mainSplitContainer.ClientSize.Height / 2
            };
            mainSplitContainer.Panel2.Controls.Add(secondarySplitContainer);

            rohdeSchwarzGroupBox = new GroupBox
            {
                Text = "Rohde & Schwarz",
                Dock = DockStyle.Fill
            };
            mainSplitContainer.Panel1.Controls.Add(rohdeSchwarzGroupBox);

            keithleyGroupBox = new GroupBox
            {
                Text = "Keithley",
                Dock = DockStyle.Fill
            };
            secondarySplitContainer.Panel1.Controls.Add(keithleyGroupBox);

            chromaGroupBox = new GroupBox
            {
                Text = "Chroma",
                Dock = DockStyle.Fill
            };
            secondarySplitContainer.Panel2.Controls.Add(chromaGroupBox);

            functionGroupBox = new GroupBox(); // Initialize functionGroupBox

            secondarySplitContainer.SplitterDistance = secondarySplitContainer.ClientSize.Height / 2;
        }

        private void SetFullScreen()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}