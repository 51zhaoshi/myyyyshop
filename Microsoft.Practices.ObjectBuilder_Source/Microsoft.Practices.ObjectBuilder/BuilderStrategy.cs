namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class BuilderStrategy : IBuilderStrategy
    {
        protected BuilderStrategy()
        {
        }

        public TItem BuildUp<TItem>(IBuilderContext context, TItem existing, string idToBuild)
        {
            return (TItem) this.BuildUp(context, typeof(TItem), existing, idToBuild);
        }

        public virtual object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            IBuilderStrategy nextInChain = context.GetNextInChain(this);
            if (nextInChain != null)
            {
                return nextInChain.BuildUp(context, typeToBuild, existing, idToBuild);
            }
            return existing;
        }

        protected string ParametersToTypeList(params object[] parameters)
        {
            List<string> list = new List<string>();
            foreach (object obj2 in parameters)
            {
                list.Add(obj2.GetType().Name);
            }
            return string.Join(", ", list.ToArray());
        }

        public virtual object TearDown(IBuilderContext context, object item)
        {
            IBuilderStrategy nextInChain = context.GetNextInChain(this);
            if (nextInChain != null)
            {
                return nextInChain.TearDown(context, item);
            }
            return item;
        }

        protected void TraceBuildUp(IBuilderContext context, Type typeToBuild, string idToBuild, string format, params object[] args)
        {
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);
            if (policy != null)
            {
                string str = string.Format(CultureInfo.CurrentCulture, format, args);
                object[] objArray = new object[] { base.GetType().Name, typeToBuild.Name, idToBuild ?? "(null)", str };
                policy.Trace(Resources.BuilderStrategyTraceBuildUp, objArray);
            }
        }

        protected bool TraceEnabled(IBuilderContext context)
        {
            return (context.Policies.Get<IBuilderTracePolicy>(null, null) != null);
        }

        protected void TraceTearDown(IBuilderContext context, object item, string format, params object[] args)
        {
            IBuilderTracePolicy policy = context.Policies.Get<IBuilderTracePolicy>(null, null);
            if (policy != null)
            {
                string str = string.Format(CultureInfo.CurrentCulture, format, args);
                policy.Trace(Resources.BuilderStrategyTraceTearDown, new object[] { base.GetType().Name, item.GetType().Name, str });
            }
        }
    }
}

