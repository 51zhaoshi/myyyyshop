namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public interface IParameter
    {
        Type GetParameterType(IBuilderContext context);
        object GetValue(IBuilderContext context);
    }
}

