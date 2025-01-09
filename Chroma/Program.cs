/*using System;
using System.Windows.Forms;
using Chroma.Helper;

namespace Chroma
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Setting());
        }
    }
}*/

using System;
using System.Windows.Forms;

namespace Chroma
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1.Main());
        }
    }
}
