using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class FreezeWindowCommand : CommandBase
    {
        public virtual MainWindow Window { get; private set; }
        protected delegate void ToggleWindowCallback(bool state);

        public FreezeWindowCommand(MainWindow window)
            : base(false)
        {
            this.Window = window;
            Logger.Instance.Log(LogType.Info, "FreezeWindowCommand initialized"); 
        }

        public override void Execute<T>(T state)
        {
            Logger.Instance.Log(LogType.Info, "FreezeWindowCommand.Execute", state.ToString());

            bool freezeStatus = bool.Parse(state.ToString());

            this.ToggleWindowAsync(freezeStatus);
            //this.Window.Enabled = freezeStatus;
        }

        protected void ToggleWindowAsync(bool freezeStatus)
        {
            if (this.Window.InvokeRequired)
                this.Window.Invoke(new ToggleWindowCallback(this.ToggleWindowAsync), new object[] { freezeStatus });
            else
            {
                if (this.Window.Enabled != freezeStatus)
                    this.Window.Enabled = freezeStatus;
            }
        }

        public override void Execute()
        {

        }
    }
}
