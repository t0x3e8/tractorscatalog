using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class CleanBookmarkListCommand : CommandBase
    {
        public CleanBookmarkListCommand() : base(true) { Logger.Instance.Log(LogType.Info, "CleanBookmarkListCommand initialized"); }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "CleanBookmarkListCommand.Execute");

            ApplicationState.MarkedTractorsCollection.Clear();
        }
    } 
}
