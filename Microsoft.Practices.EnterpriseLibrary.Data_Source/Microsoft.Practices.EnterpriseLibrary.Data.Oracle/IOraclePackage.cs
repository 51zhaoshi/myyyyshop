namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    using System;

    public interface IOraclePackage
    {
        string Name { get; }

        string Prefix { get; }
    }
}

