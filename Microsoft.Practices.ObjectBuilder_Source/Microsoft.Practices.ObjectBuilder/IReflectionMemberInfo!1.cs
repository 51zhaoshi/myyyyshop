namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public interface IReflectionMemberInfo<TMemberInfo>
    {
        object[] GetCustomAttributes(Type attributeType, bool inherit);
        ParameterInfo[] GetParameters();

        TMemberInfo MemberInfo { get; }

        string Name { get; }
    }
}

