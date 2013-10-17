namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class ReflectionStrategy<TMemberInfo> : BuilderStrategy
    {
        protected ReflectionStrategy()
        {
        }

        protected abstract void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<TMemberInfo> member, IEnumerable<IParameter> parameters);
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            foreach (IReflectionMemberInfo<TMemberInfo> info in this.GetMembers(context, typeToBuild, existing, idToBuild))
            {
                if (this.MemberRequiresProcessing(info))
                {
                    IEnumerable<IParameter> parameters = this.GenerateIParametersFromParameterInfos(info.GetParameters());
                    this.AddParametersToPolicy(context, typeToBuild, idToBuild, info, parameters);
                }
            }
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        private IEnumerable<IParameter> GenerateIParametersFromParameterInfos(ParameterInfo[] parameterInfos)
        {
            List<IParameter> list = new List<IParameter>();
            foreach (ParameterInfo info in parameterInfos)
            {
                ParameterAttribute injectionAttribute = this.GetInjectionAttribute(info);
                list.Add(injectionAttribute.CreateParameter(info.ParameterType));
            }
            return list;
        }

        private ParameterAttribute GetInjectionAttribute(ParameterInfo parameterInfo)
        {
            ParameterAttribute[] customAttributes = (ParameterAttribute[]) parameterInfo.GetCustomAttributes(typeof(ParameterAttribute), true);
            switch (customAttributes.Length)
            {
                case 0:
                    return new DependencyAttribute();

                case 1:
                    return customAttributes[0];
            }
            throw new InvalidAttributeException();
        }

        protected abstract IEnumerable<IReflectionMemberInfo<TMemberInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild);
        protected abstract bool MemberRequiresProcessing(IReflectionMemberInfo<TMemberInfo> member);
    }
}

