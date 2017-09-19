using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class MainCommand : LayoutCommandBase
    {
        private static MainLayout layout= null;

        public MainCommand(MainWindow window)
            : base(window) 
        {
            Logger.Instance.Log(LogType.Info, "MainCommand initialized");
        }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "MainCommand.Execute");

            if (layout == null)
                layout = new MainLayout(this.Window);

            this.Window.ActiveLayout = layout;
        }
    }
}
