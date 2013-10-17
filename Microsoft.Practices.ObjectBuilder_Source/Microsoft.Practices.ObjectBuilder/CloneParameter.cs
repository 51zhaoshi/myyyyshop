namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class CloneParameter : IParameter
    {
        private IParameter param;

        public CloneParameter(IParameter param)
        {
            this.param = param;
        }

        public Type GetParameterType(IBuilderContext context)
        {
            return this.param.GetParameterType(context);
        }

        public object GetValue(IBuilderContext context)
        {
            object obj2 = this.param.GetValue(context);
            if (obj2 is ICloneable)
            {
                obj2 = ((ICloneable) obj2).Clone();
            }
            return obj2;
        }
    }
}

