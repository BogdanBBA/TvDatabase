using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvDatabase.Classes
{
    public class MyThread : BackgroundWorker
    {
        public MyThread(bool doUpdate, bool canCancel, DoWorkEventHandler workEH, ProgressChangedEventHandler updateEH, RunWorkerCompletedEventHandler doneEH)
            : base()
        {
            this.WorkerReportsProgress = doUpdate;
            this.WorkerSupportsCancellation = canCancel;
            this.DoWork += workEH;
            this.ProgressChanged += updateEH;
            this.RunWorkerCompleted += doneEH;
        }
    }
}
