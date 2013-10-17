namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MethodReflectionStrategy : ReflectionStrategy<MethodInfo>
    {
        protected override void AddParametersToPolicy(IBuilderContext context, Type typeToBuild, string idToBuild, IReflectionMemberInfo<MethodInfo> member, IEnumerable<IParameter> parameters)
        {
            MethodPolicy policy = context.Policies.Get<IMethodPolicy>(typeToBuild, idToBuild) as MethodPolicy;
            if (policy == null)
            {
                policy = new MethodPolicy();
                context.Policies.Set<IMethodPolicy>(policy, typeToBuild, idToBuild);
            }
            policy.Methods.Add(member.Name, new MethodCallInfo(member.MemberInfo, parameters));
        }

        protected override IEnumerable<IReflectionMemberInfo<MethodInfo>> GetMembers(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            foreach (MethodInfo iteratorVariable0 in typeToBuild.GetMethods())
            {
                yield return new ReflectionMemberInfo<MethodInfo>(iteratorVariable0);
            }
        }

        protected override bool MemberRequiresProcessing(IReflectionMemberInfo<MethodInfo> member)
        {
            return (member.GetCustomAttributes(typeof(InjectionMethodAttribute), true).Length > 0);
        }

        [CompilerGenerated]
        private sealed class <GetMembers>d__0 : IEnumerable<IReflectionMemberInfo<MethodInfo>>, IEnumerable, IEnumerator<IReflectionMemberInfo<MethodInfo>>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private IReflectionMemberInfo<MethodInfo> <>2__current;
            public Type <>3__typeToBuild;
            public MethodReflectionStrategy <>4__this;
            public MethodInfo[] <>7__wrap2;
            public int <>7__wrap3;
            public MethodInfo <method>5__1;
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
                            this.<>7__wrap2 = this.typeToBuild.GetMethods();
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
                    this.<method>5__1 = this.<>7__wrap2[this.<>7__wrap3];
                    this.<>2__current = new ReflectionMemberInfo<MethodInfo>(this.<method>5__1);
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
            IEnumerator<IReflectionMemberInfo<MethodInfo>> IEnumerable<IReflectionMemberInfo<MethodInfo>>.GetEnumerator()
            {
                MethodReflectionStrategy.<GetMembers>d__0 d__;
                if (Interlocked.CompareExchange(ref this.<>1__state, 0, -2) == -2)
                {
                    d__ = this;
                }
                else
                {
                    d__ = new MethodReflectionStrategy.<GetMembers>d__0(0) {
                        <>4__this = this.<>4__this
                    };
                }
                d__.typeToBuild = this.<>3__typeToBuild;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<Microsoft.Practices.ObjectBuilder.IReflectionMemberInfo<System.Reflection.MethodInfo>>.GetEnumerator();
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

            IReflectionMemberInfo<MethodInfo> IEnumerator<IReflectionMemberInfo<MethodInfo>>.Current
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
    }
}

