using System;

namespace Enceladus.Api
{
    public class CommandEventArgs : EventArgs
    {
        public virtual object CommandArgument { get; set; }
        public virtual bool Cancel { get; set; }

        public CommandEventArgs()
        {
            this.CommandArgument = null;
            this.Cancel = false;
        }
    }
}
