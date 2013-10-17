namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BuilderStrategyChain : IBuilderStrategyChain
    {
        private List<IBuilderStrategy> strategies = new List<IBuilderStrategy>();

        public void Add(IBuilderStrategy strategy)
        {
            this.strategies.Add(strategy);
        }

        public void AddRange(IEnumerable strategies)
        {
            foreach (IBuilderStrategy strategy in strategies)
            {
                this.Add(strategy);
            }
        }

        public IBuilderStrategy GetNext(IBuilderStrategy currentStrategy)
        {
            for (int i = 0; i < (this.strategies.Count - 1); i++)
            {
                if (object.ReferenceEquals(currentStrategy, this.strategies[i]))
                {
                    return this.strategies[i + 1];
                }
            }
            return null;
        }

        public IBuilderStrategy Head
        {
            get
            {
                if (this.strategies.Count > 0)
                {
                    return this.strategies[0];
                }
                return null;
            }
        }
    }
}

