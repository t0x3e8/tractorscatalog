using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class SearchCommand : LayoutCommandBase
    {
        private static SearchLayout layout = null;

        public SearchCommand(MainWindow window)
            : base(window)
        {
            Logger.Instance.Log(LogType.Info, "SearchCommand initialized"); 
        }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "SearchCommand.Execute"); 

            if (layout == null)
                layout = new SearchLayout(this.Window);

            this.Window.ActiveLayout = layout;
        }
    }
}
