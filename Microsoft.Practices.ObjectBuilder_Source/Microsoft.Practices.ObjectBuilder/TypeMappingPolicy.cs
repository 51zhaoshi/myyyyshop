namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class TypeMappingPolicy : ITypeMappingPolicy, IBuilderPolicy
    {
        private DependencyResolutionLocatorKey pair;

        public TypeMappingPolicy(Type type, string id)
        {
            this.pair = new DependencyResolutionLocatorKey(type, id);
        }

        public DependencyResolutionLocatorKey Map(DependencyResolutionLocatorKey incomingTypeIDPair)
        {
            return this.pair;
        }
    }
}

