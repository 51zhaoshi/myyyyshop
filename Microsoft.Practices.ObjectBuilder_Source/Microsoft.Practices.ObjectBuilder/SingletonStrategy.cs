namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class SingletonStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            DependencyResolutionLocatorKey key = new DependencyResolutionLocatorKey(typeToBuild, idToBuild);
            if ((context.Locator != null) && context.Locator.Contains(key, SearchMode.Local))
            {
                base.TraceBuildUp(context, typeToBuild, idToBuild, "", new object[0]);
                return context.Locator.Get(key);
            }
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }
    }
}

