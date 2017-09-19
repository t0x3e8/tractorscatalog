using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using Enceladus.Api;

namespace Enceladus
{
    class WaitingWindowDispatcher
    {
        #region Fields & Properties
        protected static WaitingWindowDispatcher instance;
        protected readonly WaitingWindow waitingWindow;
        protected readonly BackgroundWorker backgroundWorker;
        protected int currentState = 0;
        protected readonly static int OpenState = 1;
        protected readonly static int OpeningState = 2;
        protected readonly static int CloseState = 3;
        protected readonly static int ClosingState = 4;
        #endregion

        #region Constructor
        public WaitingWindowDispatcher()
        {
            this.waitingWindow = new WaitingWindow();
            Logger.Instance.Log(LogType.Info, "WaitingWindowDispatcher.ctor");
            
            this.backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            //this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            this.backgroundWorker.WorkerSupportsCancellation = true;
            //this.backgroundWorker.WorkerReportsProgress = true;

            this.currentState = CloseState;
        }

        public static WaitingWindowDispatcher Instance
        {
            get
            {
                if (instance == null)
                    instance = new WaitingWindowDispatcher();
                return instance;
            }
        }
        #endregion

        #region Methods
        public void Show(Point location)
        {
            if (this.currentState == CloseState)
            {
                Logger.Instance.Log(LogType.Info, "WaitingWindow.Show");

                this.waitingWindow.Location = location;

                Interlocked.Exchange(ref this.currentState, OpeningState);
                this.waitingWindow.Show();
                this.waitingWindow.BringToFront();

                this.backgroundWorker.RunWorkerAsync();
            }
        }

        public void Hide()
        {
            // State cannot be Opening since the opening is in the main thread
            if (this.currentState == OpenState || this.currentState == OpeningState)
            {
                Logger.Instance.Log(LogType.Info, "WaitingWindow.Hide");
             
                this.backgroundWorker.CancelAsync();
            }
        }

        protected void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bg = (sender as BackgroundWorker);

            Interlocked.Exchange(ref this.currentState, OpenState);

            while (!bg.CancellationPending)
            {
                //(sender as BackgroundWorker).ReportProgress(0);
                Thread.Sleep(200);
            }
        }

        protected void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.waitingWindow.UpdateState();
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Interlocked.Exchange(ref this.currentState, CloseState);
            this.waitingWindow.SendToBack();
            this.waitingWindow.Hide();
        }

        #endregion
    }
}
