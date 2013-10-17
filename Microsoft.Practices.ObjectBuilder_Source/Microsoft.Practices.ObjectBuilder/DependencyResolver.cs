namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Globalization;

    public class DependencyResolver
    {
        private IBuilderContext context;

        public DependencyResolver(IBuilderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
        }

        public object Resolve(Type typeToResolve, Type typeToCreate, string id, NotPresentBehavior notPresent, SearchMode searchMode)
        {
            if (typeToResolve == null)
            {
                throw new ArgumentNullException("typeToResolve");
            }
            if (!Enum.IsDefined(typeof(NotPresentBehavior), notPresent))
            {
                throw new ArgumentException(Resources.InvalidEnumerationValue, "notPresent");
            }
            if (typeToCreate == null)
            {
                typeToCreate = typeToResolve;
            }
            DependencyResolutionLocatorKey key = new DependencyResolutionLocatorKey(typeToResolve, id);
            if (this.context.Locator.Contains(key, searchMode))
            {
                return this.context.Locator.Get(key, searchMode);
            }
            switch (notPresent)
            {
                case NotPresentBehavior.CreateNew:
                    return this.context.HeadOfChain.BuildUp(this.context, typeToCreate, null, key.ID);

                case NotPresentBehavior.ReturnNull:
                    return null;
            }
            throw new DependencyMissingException(string.Format(CultureInfo.CurrentCulture, Resources.DependencyMissing, new object[] { typeToResolve.ToString() }));
        }
    }
}

