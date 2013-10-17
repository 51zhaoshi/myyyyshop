namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public interface ICreationPolicy : IBuilderPolicy
    {
        object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor);
        ConstructorInfo SelectConstructor(IBuilderContext context, Type type, string id);
    }
}

