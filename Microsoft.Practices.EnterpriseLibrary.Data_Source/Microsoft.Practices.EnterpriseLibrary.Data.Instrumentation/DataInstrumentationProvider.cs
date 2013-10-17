namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;
    using System.Runtime.CompilerServices;

    [InstrumentationListener(typeof(DataInstrumentationListener), typeof(DataInstrumentationListenerBinder))]
    public class DataInstrumentationProvider
    {
        [InstrumentationProvider("CommandExecuted")]
        public event EventHandler<CommandExecutedEventArgs> commandExecuted;

        [InstrumentationProvider("CommandFailed")]
        public event EventHandler<CommandFailedEventArgs> commandFailed;

        [InstrumentationProvider("ConnectionFailed")]
        public event EventHandler<ConnectionFailedEventArgs> connectionFailed;

        [InstrumentationProvider("ConnectionOpened")]
        public event EventHandler<EventArgs> connectionOpened;

        public void FireCommandExecutedEvent(DateTime startTime)
        {
            if (this.commandExecuted != null)
            {
                this.commandExecuted(this, new CommandExecutedEventArgs(startTime));
            }
        }

        public void FireCommandFailedEvent(string commandText, string connectionString, Exception exception)
        {
            if (this.commandFailed != null)
            {
                this.commandFailed(this, new CommandFailedEventArgs(commandText, connectionString, exception));
            }
        }

        public void FireConnectionFailedEvent(string connectionString, Exception exception)
        {
            if (this.connectionFailed != null)
            {
                this.connectionFailed(this, new ConnectionFailedEventArgs(connectionString, exception));
            }
        }

        public void FireConnectionOpenedEvent()
        {
            if (this.connectionOpened != null)
            {
                this.connectionOpened(this, new EventArgs());
            }
        }
    }
}

