namespace Microsoft.Practices.ObjectBuilder
{
    public interface ITypeMappingPolicy : IBuilderPolicy
    {
        DependencyResolutionLocatorKey Map(DependencyResolutionLocatorKey incomingTypeIDPair);
    }
}

