namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;

    public interface IObjectWithNameAndType : IObjectWithName
    {
        System.Type Type { get; }
    }
}

