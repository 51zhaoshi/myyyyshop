namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;

    public interface IBuilderStrategyChain
    {
        void Add(IBuilderStrategy strategy);
        void AddRange(IEnumerable strategies);
        IBuilderStrategy GetNext(IBuilderStrategy currentStrategy);

        IBuilderStrategy Head { get; }
    }
}

