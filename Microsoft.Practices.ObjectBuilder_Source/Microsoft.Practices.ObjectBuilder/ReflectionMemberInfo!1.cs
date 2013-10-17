namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public class ReflectionMemberInfo<TMemberInfo> : IReflectionMemberInfo<TMemberInfo> where TMemberInfo: MethodBase
    {
        private TMemberInfo memberInfo;

        public ReflectionMemberInfo(TMemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.memberInfo.GetCustomAttributes(attributeType, inherit);
        }

        public ParameterInfo[] GetParameters()
        {
            return this.memberInfo.GetParameters();
        }

        public TMemberInfo MemberInfo
        {
            get
            {
                return this.memberInfo;
            }
        }

        public string Name
        {
            get
            {
                return this.memberInfo.Name;
            }
        }
    }
}

