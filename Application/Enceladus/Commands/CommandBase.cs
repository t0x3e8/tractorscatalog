using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    abstract class CommandBase: ICommand
    {
        protected bool autoCommandExecution = false;
        public virtual bool AutoCommandExecution
        {
            get { return this.autoCommandExecution; }
        }

        public CommandBase() { }

        public CommandBase(bool autoCommandExecution) { this.autoCommandExecution = autoCommandExecution; }

        public abstract void Execute<T>(T state);

        public abstract void Execute();
    }
}
