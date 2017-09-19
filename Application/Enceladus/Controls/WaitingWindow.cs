using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;
using Enceladus.StringLibrary;
using System.Threading;

namespace Enceladus
{
    partial class WaitingWindow : Form, IChangeLanguage
    {
        #region Fields Properties
        #endregion

        #region Constructors
        public WaitingWindow()
        {
            InitializeComponent();

            this.ChangeLanguage();

            this.controlLabel1.ForeColor = Defines.CarrotColor;
            this.controlLabel1.Font = Defines.NormalFont;
        }
        #endregion

        #region Methods
        public void ChangeLanguage()
        {
            this.controlLabel1.Text = ResourceReader.GetString("Msg_PleaseWait");
        }

        public void HideForm()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(this.HideForm), new object[] { });
            else
            {
                this.Hide();
            }
        }
        #endregion

        internal void UpdateState()
        {
            this.waitingBar1.UpdateProgress();
        }
    }
}