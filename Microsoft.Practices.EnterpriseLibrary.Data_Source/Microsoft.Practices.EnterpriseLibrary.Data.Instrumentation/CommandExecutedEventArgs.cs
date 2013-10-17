namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using System;

    public class CommandExecutedEventArgs : EventArgs
    {
        private DateTime startTime;

        public CommandExecutedEventArgs(DateTime startTime)
        {
            this.startTime = startTime;
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
        }
    }
}

