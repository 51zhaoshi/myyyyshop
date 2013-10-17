namespace Microsoft.Practices.ObjectBuilder
{
    public interface IBuilderContext
    {
        IBuilderStrategy GetNextInChain(IBuilderStrategy currentStrategy);

        IBuilderStrategy HeadOfChain { get; }

        IReadWriteLocator Locator { get; }

        PolicyList Policies { get; }
    }
}

