namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class ValueParameter<TValue> : KnownTypeParameter
    {
        private TValue value;

        public ValueParameter(TValue value) : base(typeof(TValue))
        {
            this.value = value;
        }

        public override object GetValue(IBuilderContext context)
        {
            return this.value;
        }
    }
}

