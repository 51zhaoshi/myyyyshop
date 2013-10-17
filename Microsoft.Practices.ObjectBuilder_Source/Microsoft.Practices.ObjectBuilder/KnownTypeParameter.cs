namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public abstract class KnownTypeParameter : IParameter
    {
        protected Type type;

        protected KnownTypeParameter(Type type)
        {
            this.type = type;
        }

        public Type GetParameterType(IBuilderContext context)
        {
            return this.type;
        }

        public abstract object GetValue(IBuilderContext context);
    }
}

