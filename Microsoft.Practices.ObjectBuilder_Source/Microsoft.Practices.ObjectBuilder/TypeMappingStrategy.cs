namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;

    public class TypeMappingStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type t, object existing, string id)
        {
            DependencyResolutionLocatorKey incomingTypeIDPair = new DependencyResolutionLocatorKey(t, id);
            ITypeMappingPolicy policy = context.Policies.Get<ITypeMappingPolicy>(t, id);
            if (policy != null)
            {
                incomingTypeIDPair = policy.Map(incomingTypeIDPair);
                object[] args = new object[] { incomingTypeIDPair.Type, incomingTypeIDPair.ID ?? "(null)" };
                base.TraceBuildUp(context, t, id, Resources.TypeMapped, args);
                Guard.TypeIsAssignableFromType(t, incomingTypeIDPair.Type, t);
            }
            return base.BuildUp(context, incomingTypeIDPair.Type, existing, incomingTypeIDPair.ID);
        }
    }
}

