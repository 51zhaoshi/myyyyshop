namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class ValueParameter : KnownTypeParameter
    {
        private object value;

        public ValueParameter(Type valueType, object value) : base(valueType)
        {
            this.value = value;
        }

        public override object GetValue(IBuilderContext context)
        {
            return this.value;
        }
    }
}

