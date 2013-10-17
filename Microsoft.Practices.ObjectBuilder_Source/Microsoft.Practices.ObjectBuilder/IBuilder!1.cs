namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IBuilder<TStageEnum>
    {
        TTypeToBuild BuildUp<TTypeToBuild>(IReadWriteLocator locator, string idToBuild, object existing, params PolicyList[] transientPolicies);
        object BuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, params PolicyList[] transientPolicies);
        TItem TearDown<TItem>(IReadWriteLocator locator, TItem item);

        PolicyList Policies { get; }

        StrategyList<TStageEnum> Strategies { get; }
    }
}

