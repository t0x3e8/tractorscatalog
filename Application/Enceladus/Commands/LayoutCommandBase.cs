using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    abstract class LayoutCommandBase : ICommand
    {
        private MainWindow window;

        public MainWindow Window
        {
            get { return this.window; }
        }

        public LayoutCommandBase() { }
        
        public LayoutCommandBase(MainWindow window)
        {
            this.window = window;
        }

        public abstract void Execute<T>(T state);

        public abstract void Execute();

        public virtual bool AutoCommandExecution { get { return true; } }
    }
}
