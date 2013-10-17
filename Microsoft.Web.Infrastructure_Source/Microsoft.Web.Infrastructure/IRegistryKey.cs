namespace Microsoft.Web.Infrastructure
{
    using System;
    using System.Security;

    [SecurityCritical]
    internal interface IRegistryKey : IDisposable
    {
        string[] GetSubKeyNames();
        object GetValue(string name);
        IRegistryKey OpenSubKey(string name);
    }
}

