using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class BookmarkCommand : CommandBase
    {
        public BookmarkCommand() { Logger.Instance.Log(LogType.Info, "BookmarkCommand initialized"); }

        public override void Execute<T>(T state)
        {
            Logger.Instance.Log(LogType.Info, "BookmarkCommand.Execute");

            TractorBase tractor = state as TractorBase;

            if (!ApplicationState.MarkedTractorsCollection.Contains(tractor))
                ApplicationState.MarkedTractorsCollection.Add(tractor);
            else
                ApplicationState.MarkedTractorsCollection.Remove(tractor);
        }

        public override void Execute()
        {
            throw new NotSupportedException("This method is not supported");
        }
    }
}
