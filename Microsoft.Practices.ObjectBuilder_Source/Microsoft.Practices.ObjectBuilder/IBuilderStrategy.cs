namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IBuilderStrategy
    {
        object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild);
        object TearDown(IBuilderContext context, object item);
    }
}

