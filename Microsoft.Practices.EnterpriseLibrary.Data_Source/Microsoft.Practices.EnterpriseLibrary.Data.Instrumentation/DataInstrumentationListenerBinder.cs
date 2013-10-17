namespace Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
    using System;

    public class DataInstrumentationListenerBinder : IExplicitInstrumentationBinder
    {
        public void Bind(object source, object listener)
        {
            DataInstrumentationListener listener2 = (DataInstrumentationListener) listener;
            DataInstrumentationProvider provider = (DataInstrumentationProvider) source;
            provider.commandExecuted += new EventHandler<CommandExecutedEventArgs>(listener2.CommandExecuted);
            provider.commandFailed += new EventHandler<CommandFailedEventArgs>(listener2.CommandFailed);
            provider.connectionFailed += new EventHandler<ConnectionFailedEventArgs>(listener2.ConnectionFailed);
            provider.connectionOpened += new EventHandler<EventArgs>(listener2.ConnectionOpened);
        }
    }
}

