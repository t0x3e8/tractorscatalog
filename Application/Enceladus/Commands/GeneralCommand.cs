using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class GeneralCommand : LayoutCommandBase
    {
        public GeneralCommand(MainWindow window)
            : base(window) { Logger.Instance.Log(LogType.Info, "SearchGeneralCommand initialized"); }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "SearchGeneralCommand.Execute");

            SearchLayout searchLayout = this.Window.ActiveLayout as SearchLayout;

            if (searchLayout != null)
            {
                searchLayout.ActivePageIndex = 0;
            }
        }
    }
}
