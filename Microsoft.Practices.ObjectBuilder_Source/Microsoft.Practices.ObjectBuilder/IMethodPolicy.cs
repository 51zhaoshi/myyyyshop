namespace Microsoft.Practices.ObjectBuilder
{
    using System.Collections.Generic;

    public interface IMethodPolicy : IBuilderPolicy
    {
        Dictionary<string, IMethodCallInfo> Methods { get; }
    }
}

