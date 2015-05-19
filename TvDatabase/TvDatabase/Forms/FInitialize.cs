using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using TvDatabase.Classes;

namespace TvDatabase.Forms
{
    public partial class FInitialize : Form
    {
        private MyThread initializeThread;

        public FInitialize()
        {
            InitializeComponent();
        }

        private void FInitialize_Load(object sender, EventArgs e)
        {
            this.initializeThread = new MyThread(true, false, InitializeThread_Work, InitializeThread_Update, InitializeThread_Done);
        }

        private void FInitialize_Shown(object sender, EventArgs e)
        {
            this.initializeThread.RunWorkerAsync();
        }

        private void InitializeThread_Work(object sender, DoWorkEventArgs args)
        {
            this.initializeThread.ReportProgress(15, "Initializing application");
            Thread.Sleep(200);

            this.initializeThread.ReportProgress(35, "Checking application files and folders");
            string checkResult = Paths.CheckFoldersAndFiles(true);
            if (!checkResult.Equals(""))
                throw new ApplicationException(checkResult);

            this.initializeThread.ReportProgress(55, "Loading static files");
            checkResult = Paths.LoadStaticImages();
            if (!checkResult.Equals(""))
                throw new ApplicationException(checkResult);

            this.initializeThread.ReportProgress(75, "Reading and processing database");
            Database db = new Database();
            checkResult = db.OpenDatabase();
            if (!checkResult.Equals(""))
                throw new ApplicationException(checkResult);

            args.Result = db;
        }

        private void InitializeThread_Update(object sender, ProgressChangedEventArgs args)
        {
            progressL.Text = args.ProgressPercentage + "%";
            statusL.Text = args.UserState as string + "...";
        }

        private void InitializeThread_Done(object sender, RunWorkerCompletedEventArgs args)
        {
            if (args.Error == null) // initialization completed successfully
            {
                FMain main = new FMain(args.Result as Database);
                main.Show();
                this.Hide();
            }
            else // an error occured during initialization
            {
                errorL.Text = args.Error.Message;
                errorL.Show();
                exitL.Show();
            }
        }

        private void errorL_Click(object sender, EventArgs e)
        {
            MessageBox.Show(errorL.Text, "Initialization ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitL_Click(object sender, EventArgs e)
        {
            int i = 0;
            while (this.initializeThread.IsBusy && ++i <= 5)
                Thread.Sleep(400);
            if (i <= 5)
                Application.Exit();
        }
    }
}
