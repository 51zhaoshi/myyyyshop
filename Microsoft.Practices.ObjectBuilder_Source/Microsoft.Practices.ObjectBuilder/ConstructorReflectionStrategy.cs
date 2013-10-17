namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ConstructorReflectionStrategy : ReflectionStrategy<ConstructorInfo>
    {
        protected override void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<ConstructorInfo> member, IEnumerable<IParameter> parameters)
        {
            ConstructorPolicy policy = new ConstructorPolicy();
            foreach (IParameter parameter in parameters)
            {
                policy.AddParameter(parameter);
            }
            context.Policies.Set<ICreationPolicy>(policy, typeToBuild, idToBuild);
        }

        protected override IEnumerable<IReflectionMemberInfo<ConstructorInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            List<IReflectionMemberInfo<ConstructorInfo>> list = new List<IReflectionMemberInfo<ConstructorInfo>>();
            ICreationPolicy policy = context.Policies.Get<ICreationPolicy>(typeToBuild, idToBuild);
            if ((existing == null) && ((policy == null) || (policy is DefaultCreationPolicy)))
            {
                ConstructorInfo memberInfo = null;
                ConstructorInfo[] constructors = typeToBuild.GetConstructors();
                if (constructors.Length == 1)
                {
                    memberInfo = constructors[0];
                }
                else
                {
                    foreach (ConstructorInfo info2 in constructors)
                    {
                        if (Attribute.IsDefined(info2, typeof(InjectionConstructorAttribute)))
                        {
                            if (memberInfo != null)
                            {
                                throw new InvalidAttributeException();
                            }
                            memberInfo = info2;
                        }
                    }
                }
                if (memberInfo != null)
                {
                    list.Add(new ReflectionMemberInfo<ConstructorInfo>(memberInfo));
                }
            }
            return list;
        }

        protected override bool MemberRequiresProcessing(IReflectionMemberInfo<ConstructorInfo> member)
        {
            return true;
        }
    }
}

