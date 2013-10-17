namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class LookupParameter : IParameter
    {
        private object key;

        public LookupParameter(object key)
        {
            this.key = key;
        }

        public Type GetParameterType(IBuilderContext context)
        {
            return this.GetValue(context).GetType();
        }

        public object GetValue(IBuilderContext context)
        {
            return context.Locator.Get(this.key);
        }
    }
}

