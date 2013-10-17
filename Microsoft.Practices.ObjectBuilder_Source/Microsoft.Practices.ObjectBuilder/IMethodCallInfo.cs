namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public interface IMethodCallInfo
    {
        object[] GetParameters(IBuilderContext context, Type type, string id, MethodInfo method);
        MethodInfo SelectMethod(IBuilderContext context, Type type, string id);
    }
}

