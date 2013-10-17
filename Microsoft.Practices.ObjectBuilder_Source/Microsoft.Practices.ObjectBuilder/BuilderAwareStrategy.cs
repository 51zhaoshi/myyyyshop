namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;

    public class BuilderAwareStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            IBuilderAware aware = existing as IBuilderAware;
            if (aware != null)
            {
                base.TraceBuildUp(context, t, id, Resources.CallingOnBuiltUp, new object[0]);
                aware.OnBuiltUp(id);
            }
            return base.BuildUp(context, t, existing, id);
        }

        public override object TearDown(IBuilderContext context, object item)
        {
            IBuilderAware aware = item as IBuilderAware;
            if (aware != null)
            {
                base.TraceTearDown(context, item, Resources.CallingOnTearingDown, new object[0]);
                aware.OnTearingDown();
            }
            return base.TearDown(context, item);
        }
    }
}

