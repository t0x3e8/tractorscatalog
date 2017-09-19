using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus
{
    abstract class StateObserver
    {
        public abstract void Attach(IObserver observer);
        public abstract void Deattach(IObserver observer);
        public abstract void Notify();
    }
}
