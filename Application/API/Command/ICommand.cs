using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api
{
    public interface ICommand
    {
        void Execute<T>(T state);
        void Execute();
        /// <summary>
        /// The value determines if the command should be executed right away when the button is clicked. There is Execute method which
        /// takes an argument, which sometimes needs to be set manually.
        /// </summary>
        bool AutoCommandExecution { get; }
    }
}