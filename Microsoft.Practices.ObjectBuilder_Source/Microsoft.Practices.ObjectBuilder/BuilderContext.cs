namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class BuilderContext : IBuilderContext
    {
        private IBuilderStrategyChain chain;
        private IReadWriteLocator locator;
        private PolicyList policies;

        protected BuilderContext()
        {
        }

        public BuilderContext(IBuilderStrategyChain chain, IReadWriteLocator locator, PolicyList policies)
        {
            this.chain = chain;
            this.locator = locator;
            this.policies = new PolicyList(new PolicyList[] { policies });
        }

        public IBuilderStrategy GetNextInChain(IBuilderStrategy currentStrategy)
        {
            return this.chain.GetNext(currentStrategy);
        }

        protected void SetLocator(IReadWriteLocator locator)
        {
            this.locator = locator;
        }

        protected void SetPolicies(PolicyList policies)
        {
            this.policies = policies;
        }

        public IBuilderStrategy HeadOfChain
        {
            get
            {
                return this.chain.Head;
            }
        }

        public IReadWriteLocator Locator
        {
            get
            {
                return this.locator;
            }
        }

        public PolicyList Policies
        {
            get
            {
                return this.policies;
            }
        }

        protected IBuilderStrategyChain StrategyChain
        {
            get
            {
                return this.chain;
            }
            set
            {
                this.chain = value;
            }
        }
    }
}

