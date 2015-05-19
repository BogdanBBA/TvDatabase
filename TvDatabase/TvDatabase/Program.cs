using System;
using System.Windows.Forms;
using TvDatabase.Forms;

namespace TvDatabase
{
    static class Program
    {
        /// <summary>
        /// The main entry location for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FInitialize());
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
    }
}
