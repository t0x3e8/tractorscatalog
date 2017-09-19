using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api
{
    internal class AsyncResult<T> : AsyncResultNoResult
    {
        #region Fields
        private T result = default(T);
        #endregion

        #region Constructors
        public AsyncResult(AsyncCallback asyncCallback, Object state) :
            base(asyncCallback, state) { }
        #endregion

        #region Methods
        public void SetAsCompleted(T result, bool completedSynchronously)
        {
            this.result = result;
            base.SetAsCompleted(null, completedSynchronously);
        }

        new public T EndInvoke()
        {
            base.EndInvoke();
            return this.result;
        }
        #endregion
    }
}
