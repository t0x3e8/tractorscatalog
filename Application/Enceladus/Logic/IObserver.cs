using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus
{
    interface IObserver
    {
        void Update(bool state);
    }
}
