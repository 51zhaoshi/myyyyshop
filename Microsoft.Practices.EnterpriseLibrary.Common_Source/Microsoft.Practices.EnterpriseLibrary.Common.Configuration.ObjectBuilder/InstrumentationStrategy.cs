namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class InstrumentationStrategy : EnterpriseLibraryBuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            if (existing != null)
            {
                IConfigurationSource configurationSource = base.GetConfigurationSource(context);
                ConfigurationReflectionCache reflectionCache = base.GetReflectionCache(context);
                InstrumentationAttachmentStrategy strategy = new InstrumentationAttachmentStrategy();
                if (ConfigurationNameProvider.IsMadeUpName(id))
                {
                    strategy.AttachInstrumentation(existing, configurationSource, reflectionCache);
                }
                else
                {
                    strategy.AttachInstrumentation(id, existing, configurationSource, reflectionCache);
                }
            }
            return base.BuildUp(context, t, existing, id);
        }
    }
}

