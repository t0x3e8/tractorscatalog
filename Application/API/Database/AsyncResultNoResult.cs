using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Enceladus.Api
{
    internal class AsyncResultNoResult : IAsyncResult
    {
        #region Fields
        protected const Int32 StatePending = 0;
        protected const Int32 StateCompletedSynchronously = 1;
        protected const Int32 StateCompletedAsynchronously = 2;

        protected Int32 completedState = StatePending;
        protected readonly AsyncCallback asyncCallback;
        protected readonly Object asyncState;
        protected ManualResetEvent asyncWaitHandle;
        protected Exception exception;

        #region Implementation of IAsyncResult
        public Object AsyncState { get { return asyncState; } }

        public Boolean CompletedSynchronously
        {
            get { return Thread.VolatileRead(ref completedState) == StateCompletedSynchronously; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (asyncWaitHandle == null)
                {
                    Boolean done = IsCompleted;
                    ManualResetEvent mre = new ManualResetEvent(done);
                    if (Interlocked.CompareExchange(ref asyncWaitHandle,
                       mre, null) != null)
                    {
                        // Another thread created this object's event; dispose 
                        // the event we just created
                        mre.Close();
                    }
                    else
                    {
                        if (!done && IsCompleted)
                        {
                            // If the operation wasn't done when we created 
                            // the event but now it is done, set the event
                            asyncWaitHandle.Set();
                        }
                    }
                }
                return asyncWaitHandle;
            }
        }

        public Boolean IsCompleted
        {
            get { return Thread.VolatileRead(ref completedState) != StatePending; }
        }
        #endregion
        #endregion

        #region Constructors
        public AsyncResultNoResult(AsyncCallback asyncCallback, Object state)
        {
            this.asyncCallback = asyncCallback;
            this.asyncState = state;
        }
        #endregion

        #region Methods
        public void SetAsCompleted(Exception exception, Boolean completedSynchronously)
        {
            this.exception = exception;

            Int32 prevState = Interlocked.Exchange(ref completedState, completedSynchronously ? StateCompletedSynchronously : StateCompletedAsynchronously);
            if (prevState != StatePending)
                throw new InvalidOperationException("You can set a result only once");

            if (asyncWaitHandle != null)
                asyncWaitHandle.Set();

            if (this.asyncCallback != null)
                this.asyncCallback(this);
        }

        public void EndInvoke()
        {
            if (!IsCompleted)
            {
                AsyncWaitHandle.WaitOne();
                AsyncWaitHandle.Close();
                asyncWaitHandle = null;  // Allow early GC
            }

            if (exception != null)
                throw exception;
        }
        #endregion
    }
}
