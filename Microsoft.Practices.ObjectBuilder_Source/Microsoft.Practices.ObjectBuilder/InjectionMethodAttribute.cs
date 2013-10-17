namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public sealed class InjectionMethodAttribute : Attribute
    {
    }
}

