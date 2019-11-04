using System;
using System.Threading;

namespace USC.GISResearchLab.Common.Threading
{

    // from http://msmvps.com/blogs/peterritchie/archive/2006/10/13/_2700_System.Threading.Thread.Suspend_280029002700_-is-obsolete_3A00_-_2700_Thread.Suspend-has-been-deprecated_2E002E002E00_.aspx

    public abstract class SuspendableThread
    {
        #region Data
        private ManualResetEvent suspendChangedEvent = new ManualResetEvent(false);
        private ManualResetEvent terminateEvent = new ManualResetEvent(false);
        private long suspended;
        private Thread _Thread;
        public Thread Thread
        {
            get { return _Thread; }
        }
        private System.Threading.ThreadState failsafeThreadState = System.Threading.ThreadState.Unstarted;
        #endregion Data

        public SuspendableThread()
        {
        }

        private void ThreadEntry()
        {
            failsafeThreadState = System.Threading.ThreadState.Stopped;
            OnDoWork();
        }

        protected abstract void OnDoWork();

        #region Protected methods

        protected Boolean SuspendIfNeeded()
        {
            Boolean suspendEventChanged = suspendChangedEvent.WaitOne(0, true);

            if (suspendEventChanged)
            {
                Boolean needToSuspend = Interlocked.Read(ref suspended) != 0;
                suspendChangedEvent.Reset();
                if (needToSuspend)
                {
                    /// Suspending...
                    if (1 == WaitHandle.WaitAny(new WaitHandle[] { suspendChangedEvent, terminateEvent }))
                    {
                        return true;
                    }
                    /// ...Waking
                }
            }

            return false;

        }

        protected bool HasTerminateRequest()
        {
            return terminateEvent.WaitOne(0, true);
        }
        #endregion Protected methods

        public void Start()
        {

            _Thread = new Thread(new ThreadStart(ThreadEntry));

            // make sure this thread won't be automaticaly
            // terminated by the runtime when the
            // application exits
            _Thread.IsBackground = false;
            _Thread.Start();
        }

        public void Join()
        {
            if (_Thread != null)
            {
                _Thread.Join();
            }
        }

        public Boolean Join(Int32 milliseconds)
        {
            if (_Thread != null)
            {
                return _Thread.Join(milliseconds);
            }
            return true;
        }

        /// <remarks>Not supported in .NET Compact Framework</remarks>
        public Boolean Join(TimeSpan timeSpan)
        {
            if (_Thread != null)
            {
                return _Thread.Join(timeSpan);
            }
            return true;
        }

        public void Terminate()
        {
            terminateEvent.Set();
        }

        public void TerminateAndWait()
        {
            terminateEvent.Set();
            _Thread.Join();
        }

        public void Suspend()
        {
            while (1 != Interlocked.Exchange(ref suspended, 1))
            {
            }
            suspendChangedEvent.Set();
        }

        public void Resume()
        {
            while (0 != Interlocked.Exchange(ref suspended, 0))
            {
            }
            suspendChangedEvent.Set();
        }

        public System.Threading.ThreadState ThreadState
        {
            get
            {
                if (null != _Thread)
                {
                    return _Thread.ThreadState;
                }
                return failsafeThreadState;
            }
        }
    }
}
