namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;

    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple=false)]
    public sealed class HasInstallableResourcesAttribute : Attribute
    {
    }
}

