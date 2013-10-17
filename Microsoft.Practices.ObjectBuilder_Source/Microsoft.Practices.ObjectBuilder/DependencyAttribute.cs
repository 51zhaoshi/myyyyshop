namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public sealed class DependencyAttribute : ParameterAttribute
    {
        private Type createType;
        private string name;
        private Microsoft.Practices.ObjectBuilder.NotPresentBehavior notPresentBehavior;
        private Microsoft.Practices.ObjectBuilder.SearchMode searchMode;

        public override IParameter CreateParameter(Type annotatedMemberType)
        {
            return new DependencyParameter(annotatedMemberType, this.name, this.createType, this.notPresentBehavior, this.searchMode);
        }

        public Type CreateType
        {
            get
            {
                return this.createType;
            }
            set
            {
                this.createType = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public Microsoft.Practices.ObjectBuilder.NotPresentBehavior NotPresentBehavior
        {
            get
            {
                return this.notPresentBehavior;
            }
            set
            {
                this.notPresentBehavior = value;
            }
        }

        public Microsoft.Practices.ObjectBuilder.SearchMode SearchMode
        {
            get
            {
                return this.searchMode;
            }
            set
            {
                this.searchMode = value;
            }
        }
    }
}

