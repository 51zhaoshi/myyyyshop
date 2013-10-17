namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    public interface IEventLogEntryFormatter
    {
        string GetEntryText(string message, params string[] extraInformation);
        string GetEntryText(string message, Exception exception, params string[] extraInformation);
    }
}

