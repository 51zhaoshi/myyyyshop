namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PropertyReflectionStrategy : ReflectionStrategy<PropertyInfo>
    {
        protected override void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<PropertyInfo> member, IEnumerable<IParameter> parameters)
        {
            PropertySetterPolicy policy = context.Policies.Get<IPropertySetterPolicy>(typeToBuild, idToBuild) as PropertySetterPolicy;
            if (policy == null)
            {
                policy = new PropertySetterPolicy();
                context.Policies.Set<IPropertySetterPolicy>(policy, typeToBuild, idToBuild);
            }
            foreach (IParameter parameter in parameters)
            {
                if (!policy.Properties.ContainsKey(member.Name))
                {
                    policy.Properties.Add(member.Name, new PropertySetterInfo(member.MemberInfo, parameter));
                }
            }
        }

        protected override IEnumerable<IReflectionMemberInfo<PropertyInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            foreach (PropertyInfo iteratorVariable0 in typeToBuild.GetProperties())
            {
                yield return new PropertyReflectionMemberInfo(iteratorVariable0);
            }
        }

        protected override bool MemberRequiresProcessing(IReflectionMemberInfo<PropertyInfo> member)
        {
            return (member.GetCustomAttributes(typeof(ParameterAttribute), true).Length > 0);
        }

        [CompilerGenerated]
        private sealed class <GetMembers>d__0 : IEnumerable<IReflectionMemberInfo<PropertyInfo>>, IEnumerable, IEnumerator<IReflectionMemberInfo<PropertyInfo>>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private IReflectionMemberInfo<PropertyInfo> <>2__current;
            public Type <>3__typeToBuild;
            public PropertyReflectionStrategy <>4__this;
            public PropertyInfo[] <>7__wrap2;
            public int <>7__wrap3;
            public PropertyInfo <propInfo>5__1;
            public Type typeToBuild;

            [DebuggerHidden]
            public <GetMembers>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>1__state = 1;
                            this.<>7__wrap2 = this.typeToBuild.GetProperties();
                            this.<>7__wrap3 = 0;
                            goto Label_008A;

                        case 2:
                            this.<>1__state = 1;
                            this.<>7__wrap3++;
                            goto Label_008A;

                        default:
                            goto Label_00A1;
                    }
                Label_0046:
                    this.<propInfo>5__1 = this.<>7__wrap2[this.<>7__wrap3];
                    this.<>2__current = new PropertyReflectionStrategy.PropertyReflectionMemberInfo(this.<propInfo>5__1);
                    this.<>1__state = 2;
                    return true;
                Label_008A:
                    if (this.<>7__wrap3 < this.<>7__wrap2.Length)
                    {
                        goto Label_0046;
                    }
                    this.<>1__state = -1;
                Label_00A1:
                    flag = false;
                }
                fault
                {
                    ((IDisposable) this).Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<IReflectionMemberInfo<PropertyInfo>> IEnumerable<IReflectionMemberInfo<PropertyInfo>>.GetEnumerator()
            {
                PropertyReflectionStrategy.<GetMembers>d__0 d__;
                if (Interlocked.CompareExchange(ref this.<>1__state, 0, -2) == -2)
                {
                    d__ = this;
                }
                else
                {
                    d__ = new PropertyReflectionStrategy.<GetMembers>d__0(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.typeToBuild = this.<>3__typeToBuild;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<Microsoft.Practices.ObjectBuilder.IReflectionMemberInfo<System.Reflection.PropertyInfo>>.GetEnumerator();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.<>1__state)
                {
                    case 1:
                    case 2:
                        this.<>1__state = -1;
                        return;
                }
            }

            IReflectionMemberInfo<PropertyInfo> IEnumerator<IReflectionMemberInfo<PropertyInfo>>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }

        private class CustomPropertyParameterInfo : ParameterInfo
        {
            private PropertyInfo prop;

            public CustomPropertyParameterInfo(PropertyInfo prop)
            {
                this.prop = prop;
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return this.prop.GetCustomAttributes(attributeType, inherit);
            }

            public override Type ParameterType
            {
                get
                {
                    return this.prop.PropertyType;
                }
            }
        }

        private class PropertyReflectionMemberInfo : IReflectionMemberInfo<PropertyInfo>
        {
            private PropertyInfo prop;

            public PropertyReflectionMemberInfo(PropertyInfo prop)
            {
                this.prop = prop;
            }

            public object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return this.prop.GetCustomAttributes(attributeType, inherit);
            }

            public ParameterInfo[] GetParameters()
            {
                return new ParameterInfo[] { new PropertyReflectionStrategy.CustomPropertyParameterInfo(this.prop) };
            }

            public PropertyInfo MemberInfo
            {
                get
                {
                    return this.prop;
                }
            }

            public string Name
            {
                get
                {
                    return this.prop.Name;
                }
            }
        }
    }
}

