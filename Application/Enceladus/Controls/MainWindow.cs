using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;

namespace Enceladus
{
    partial class MainWindow : Form
    {
        #region Properties
        private BaseLayout layout;
        public BaseLayout ActiveLayout
        {
            get { return layout; }
            set
            {
                this.SuspendLayout();

                value.Activate();
                if(this.layout != null)
                    this.layout.Deactivate();

                this.Controls.Add(value);
                this.Controls.Remove(this.layout);
                this.layout = value;

                this.UpdateLayout();
                this.ResumeLayout();
            }
        }

        private bool isClosingCancelled = false;
        #endregion

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            ApplicationState.OnApplicationCancelClosing += new EventHandler(ApplicationState_OnApplicationCancelClosing);
        }

        #endregion

        #region Methods
        private void UpdateLayout()
        {
            this.layout.Dock = DockStyle.Fill;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationState.ApplicationClosing();
            e.Cancel = this.isClosingCancelled;
            base.OnClosing(e);

            this.isClosingCancelled = false;
        }        
        #endregion

        #region Events

        private void ApplicationState_OnApplicationCancelClosing(object sender, EventArgs e)
        {
            this.isClosingCancelled = true;
        }
        #endregion
    }
}
