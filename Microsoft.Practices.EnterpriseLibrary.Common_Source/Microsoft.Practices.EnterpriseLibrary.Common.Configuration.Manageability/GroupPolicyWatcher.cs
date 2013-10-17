namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    internal sealed class GroupPolicyWatcher : IGroupPolicyWatcher, IDisposable
    {
        private AutoResetEvent currentThreadExitEvent;
        private object lockObject;
        private GroupPolicyNotificationRegistrationBuilder registrationBuilder;

        public event GroupPolicyUpdateDelegate GroupPolicyUpdated;

        public GroupPolicyWatcher() : this(new GroupPolicyNotificationRegistrationBuilder())
        {
        }

        public GroupPolicyWatcher(GroupPolicyNotificationRegistrationBuilder registrationBuilder)
        {
            this.lockObject = new object();
            this.registrationBuilder = registrationBuilder;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.StopWatching();
            }
        }

        private void DoWatch(object parameter)
        {
            AutoResetEvent event2 = (AutoResetEvent) parameter;
            try
            {
                using (GroupPolicyNotificationRegistration registration = this.registrationBuilder.CreateRegistration())
                {
                    AutoResetEvent[] waitHandles = new AutoResetEvent[] { event2, registration.MachinePolicyEvent, registration.UserPolicyEvent };
                    bool flag = true;
                    while (flag)
                    {
                        int num = WaitHandle.WaitAny(waitHandles);
                        if (num != 0)
                        {
                            if (this.GroupPolicyUpdated != null)
                            {
                                this.GroupPolicyUpdated(num == 1);
                            }
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }
            finally
            {
                event2.Close();
            }
        }

        ~GroupPolicyWatcher()
        {
            this.Dispose(false);
        }

        public void StartWatching()
        {
            lock (this.lockObject)
            {
                if (this.currentThreadExitEvent == null)
                {
                    this.currentThreadExitEvent = new AutoResetEvent(false);
                    new Thread(new ParameterizedThreadStart(this.DoWatch)) { IsBackground = true, Name = Resources.GroupPolicyWatcherThread }.Start(this.currentThreadExitEvent);
                }
            }
        }

        public void StopWatching()
        {
            lock (this.lockObject)
            {
                if (this.currentThreadExitEvent != null)
                {
                    this.currentThreadExitEvent.Set();
                    this.currentThreadExitEvent = null;
                }
            }
        }

        internal class GroupPolicyNotificationRegistration : IDisposable
        {
            private AutoResetEvent machinePolicyEvent = new AutoResetEvent(false);
            private AutoResetEvent userPolicyEvent = new AutoResetEvent(false);

            public GroupPolicyNotificationRegistration()
            {
                this.CheckReturnValue(Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.RegisterGPNotification(this.machinePolicyEvent.SafeWaitHandle, true));
                this.CheckReturnValue(Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.RegisterGPNotification(this.userPolicyEvent.SafeWaitHandle, false));
            }

            private void CheckReturnValue(bool returnValue)
            {
                if (!returnValue)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
            }

            public virtual void Dispose()
            {
                try
                {
                    this.CheckReturnValue(Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.UnregisterGPNotification(this.machinePolicyEvent.SafeWaitHandle));
                    this.CheckReturnValue(Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.NativeMethods.UnregisterGPNotification(this.userPolicyEvent.SafeWaitHandle));
                }
                finally
                {
                    this.machinePolicyEvent.Close();
                    this.userPolicyEvent.Close();
                }
            }

            public AutoResetEvent MachinePolicyEvent
            {
                get
                {
                    return this.machinePolicyEvent;
                }
            }

            public AutoResetEvent UserPolicyEvent
            {
                get
                {
                    return this.userPolicyEvent;
                }
            }
        }

        internal class GroupPolicyNotificationRegistrationBuilder
        {
            public virtual GroupPolicyWatcher.GroupPolicyNotificationRegistration CreateRegistration()
            {
                return new GroupPolicyWatcher.GroupPolicyNotificationRegistration();
            }
        }
    }
}

