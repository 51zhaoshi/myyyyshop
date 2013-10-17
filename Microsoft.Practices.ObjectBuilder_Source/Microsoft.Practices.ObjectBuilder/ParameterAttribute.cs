namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public abstract class ParameterAttribute : Attribute
    {
        protected ParameterAttribute()
        {
        }

        public abstract IParameter CreateParameter(Type memberType);
    }
}

