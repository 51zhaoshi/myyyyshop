namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public class StrategyList<TStageEnum>
    {
        private object lockObject;
        private Dictionary<TStageEnum, List<IBuilderStrategy>> stages;
        private static readonly Array stageValues;

        static StrategyList()
        {
            StrategyList<TStageEnum>.stageValues = Enum.GetValues(typeof(TStageEnum));
        }

        public StrategyList()
        {
            this.lockObject = new object();
            this.stages = new Dictionary<TStageEnum, List<IBuilderStrategy>>();
            foreach (TStageEnum local in StrategyList<TStageEnum>.stageValues)
            {
                this.stages[local] = new List<IBuilderStrategy>();
            }
        }

        public void Add(IBuilderStrategy strategy, TStageEnum stage)
        {
            lock (this.lockObject)
            {
                this.stages[stage].Add(strategy);
            }
        }

        public void AddNew<TStrategy>(TStageEnum stage) where TStrategy: IBuilderStrategy, new()
        {
            lock (this.lockObject)
            {
                this.stages[stage].Add((default(TStrategy) == null) ? Activator.CreateInstance<TStrategy>() : default(TStrategy));
            }
        }

        public void Clear()
        {
            lock (this.lockObject)
            {
                foreach (TStageEnum local in StrategyList<TStageEnum>.stageValues)
                {
                    this.stages[local].Clear();
                }
            }
        }

        public IBuilderStrategyChain MakeReverseStrategyChain()
        {
            lock (this.lockObject)
            {
                List<IBuilderStrategy> strategies = new List<IBuilderStrategy>();
                foreach (TStageEnum local in StrategyList<TStageEnum>.stageValues)
                {
                    strategies.AddRange(this.stages[local]);
                }
                strategies.Reverse();
                BuilderStrategyChain chain = new BuilderStrategyChain();
                chain.AddRange(strategies);
                return chain;
            }
        }

        public IBuilderStrategyChain MakeStrategyChain()
        {
            lock (this.lockObject)
            {
                BuilderStrategyChain chain = new BuilderStrategyChain();
                foreach (TStageEnum local in StrategyList<TStageEnum>.stageValues)
                {
                    chain.AddRange(this.stages[local]);
                }
                return chain;
            }
        }
    }
}

