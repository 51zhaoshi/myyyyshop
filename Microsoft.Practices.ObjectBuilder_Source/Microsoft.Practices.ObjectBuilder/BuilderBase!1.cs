namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Collections.Generic;

    public class BuilderBase<TStageEnum> : IBuilder<TStageEnum>
    {
        private Dictionary<object, object> lockObjects;
        private PolicyList policies;
        private StrategyList<TStageEnum> strategies;

        public BuilderBase()
        {
            this.policies = new PolicyList(new PolicyList[0]);
            this.strategies = new StrategyList<TStageEnum>();
            this.lockObjects = new Dictionary<object, object>();
        }

        public BuilderBase(IBuilderConfigurator<TStageEnum> configurator)
        {
            this.policies = new PolicyList(new PolicyList[0]);
            this.strategies = new StrategyList<TStageEnum>();
            this.lockObjects = new Dictionary<object, object>();
            configurator.ApplyConfiguration(this);
        }

        public TTypeToBuild BuildUp<TTypeToBuild>(IReadWriteLocator locator, string idToBuild, object existing, params PolicyList[] transientPolicies)
        {
            return (TTypeToBuild) this.BuildUp(locator, typeof(TTypeToBuild), idToBuild, existing, transientPolicies);
        }

        public virtual object BuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, params PolicyList[] transientPolicies)
        {
            if (locator != null)
            {
                lock (this.GetLock(locator))
                {
                    return this.DoBuildUp(locator, typeToBuild, idToBuild, existing, transientPolicies);
                }
            }
            return this.DoBuildUp(locator, typeToBuild, idToBuild, existing, transientPolicies);
        }

        private object DoBuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, PolicyList[] transientPolicies)
        {
            IBuilderStrategyChain chain = this.strategies.MakeStrategyChain();
            BuilderBase<TStageEnum>.ThrowIfNoStrategiesInChain(chain);
            IBuilderContext context = this.MakeContext(chain, locator, transientPolicies);
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);
            if (policy != null)
            {
                object[] args = new object[] { typeToBuild, idToBuild ?? "(null)" };
                policy.Trace(Resources.BuildUpStarting, args);
            }
            object obj2 = chain.Head.BuildUp(context, typeToBuild, existing, idToBuild);
            if (policy != null)
            {
                object[] objArray2 = new object[] { typeToBuild, idToBuild ?? "(null)" };
                policy.Trace(Resources.BuildUpFinished, objArray2);
            }
            return obj2;
        }

        private TItem DoTearDown<TItem>(IReadWriteLocator locator, TItem item)
        {
            IBuilderStrategyChain chain = this.strategies.MakeReverseStrategyChain();
            BuilderBase<TStageEnum>.ThrowIfNoStrategiesInChain(chain);
            Type type = item.GetType();
            IBuilderContext context = this.MakeContext(chain, locator, new PolicyList[0]);
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);
            if (policy != null)
            {
                policy.Trace(Resources.TearDownStarting, new object[] { type });
            }
            TItem local = (TItem) chain.Head.TearDown(context, item);
            if (policy != null)
            {
                policy.Trace(Resources.TearDownFinished, new object[] { type });
            }
            return local;
        }

        private object GetLock(object locator)
        {
            lock (this.lockObjects)
            {
                if (this.lockObjects.ContainsKey(locator))
                {
                    return this.lockObjects[locator];
                }
                object obj2 = new object();
                this.lockObjects[locator] = obj2;
                return obj2;
            }
        }

        private IBuilderContext MakeContext(IBuilderStrategyChain chain, IReadWriteLocator locator, params PolicyList[] transientPolicies)
        {
            PolicyList policies = new PolicyList(new PolicyList[] { this.policies });
            foreach (PolicyList list2 in transientPolicies)
            {
                policies.AddPolicies(list2);
            }
            return new BuilderContext(chain, locator, policies);
        }

        public TItem TearDown<TItem>(IReadWriteLocator locator, TItem item)
        {
            if (!typeof(TItem).IsValueType && (item == null))
            {
                throw new ArgumentNullException("item");
            }
            if (locator != null)
            {
                lock (this.GetLock(locator))
                {
                    return this.DoTearDown<TItem>(locator, item);
                }
            }
            return this.DoTearDown<TItem>(locator, item);
        }

        private static void ThrowIfNoStrategiesInChain(IBuilderStrategyChain chain)
        {
            if (chain.Head == null)
            {
                throw new InvalidOperationException(Resources.BuilderHasNoStrategies);
            }
        }

        public PolicyList Policies
        {
            get
            {
                return this.policies;
            }
        }

        public StrategyList<TStageEnum> Strategies
        {
            get
            {
                return this.strategies;
            }
        }
    }
}

