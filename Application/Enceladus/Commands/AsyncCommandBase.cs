using System;

namespace Enceladus
{
    abstract class AsyncCommandBase : CommandBase
    {
        public abstract IAsyncResult BeginExecute(AsyncCallback callback, object state);
        public abstract void EndGet(IAsyncResult result);
        protected abstract void GetAsyncWrapper(object argument);
    }
}
