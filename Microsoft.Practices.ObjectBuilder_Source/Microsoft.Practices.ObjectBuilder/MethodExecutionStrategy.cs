namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Reflection;

    public class MethodExecutionStrategy : BuilderStrategy
    {
        private void ApplyPolicy(IBuilderContext context, object obj, string id)
        {
            if (obj != null)
            {
                Type typePolicyAppliesTo = obj.GetType();
                IMethodPolicy policy = context.Policies.Get<IMethodPolicy>(typePolicyAppliesTo, id);
                if (policy != null)
                {
                    foreach (IMethodCallInfo info in policy.Methods.Values)
                    {
                        MethodInfo method = info.SelectMethod(context, typePolicyAppliesTo, id);
                        if (method != null)
                        {
                            object[] parameters = info.GetParameters(context, typePolicyAppliesTo, id, method);
                            Guard.ValidateMethodParameters(method, parameters, obj.GetType());
                            if (base.TraceEnabled(context))
                            {
                                base.TraceBuildUp(context, typePolicyAppliesTo, id, Resources.CallingMethod, new object[] { method.Name, base.ParametersToTypeList(parameters) });
                            }
                            method.Invoke(obj, parameters);
                        }
                    }
                }
            }
        }

        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            this.ApplyPolicy(context, existing, idToBuild);
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }
    }
}

