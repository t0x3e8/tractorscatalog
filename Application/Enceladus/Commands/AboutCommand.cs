﻿using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class AboutCommand : LayoutCommandBase
    {
        private static AboutLayout layout = null;

        public AboutCommand(MainWindow window) : base(window) { Logger.Instance.Log(LogType.Info, "AboutCommand initialized"); }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "AboutCommand.Execute");

            if (layout == null)
                layout = new AboutLayout(this.Window);

            this.Window.ActiveLayout = layout;
        }
    }
}