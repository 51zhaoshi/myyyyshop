namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface ISingletonPolicy : IBuilderPolicy
    {
        bool IsSingleton { get; }
    }
}

