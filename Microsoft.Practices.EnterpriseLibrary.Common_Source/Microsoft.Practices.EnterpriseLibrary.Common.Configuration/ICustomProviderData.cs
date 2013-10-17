namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections.Specialized;

    public interface ICustomProviderData
    {
        NameValueCollection Attributes { get; }

        string Name { get; }
    }
}

