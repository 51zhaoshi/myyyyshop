namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class DependencyParameter : KnownTypeParameter
    {
        private Type createType;
        private string name;
        private NotPresentBehavior notPresentBehavior;
        private SearchMode searchMode;

        public DependencyParameter(Type parameterType, string name, Type createType, NotPresentBehavior notPresentBehavior, SearchMode searchMode) : base(parameterType)
        {
            this.name = name;
            this.createType = createType;
            this.notPresentBehavior = notPresentBehavior;
            this.searchMode = searchMode;
        }

        public override object GetValue(IBuilderContext context)
        {
            return new DependencyResolver(context).Resolve(base.type, this.createType, this.name, this.notPresentBehavior, this.searchMode);
        }
    }
}

