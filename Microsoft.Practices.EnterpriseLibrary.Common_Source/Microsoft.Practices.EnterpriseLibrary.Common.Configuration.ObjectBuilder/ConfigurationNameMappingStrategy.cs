namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder;
    using System;

    public class ConfigurationNameMappingStrategy : EnterpriseLibraryBuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            if (id == null)
            {
                IConfigurationNameMapper configurationNameMapper = base.GetReflectionCache(context).GetConfigurationNameMapper(t);
                if (configurationNameMapper != null)
                {
                    id = configurationNameMapper.MapName(id, base.GetConfigurationSource(context));
                }
            }
            return base.BuildUp(context, t, existing, id);
        }
    }
}

