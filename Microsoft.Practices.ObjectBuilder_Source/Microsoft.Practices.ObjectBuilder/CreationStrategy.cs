namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    public class CreationStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            if (existing != null)
            {
                this.BuildUpExistingObject(context, typeToBuild, existing, idToBuild);
            }
            else
            {
                existing = this.BuildUpNewObject(context, typeToBuild, existing, idToBuild);
            }
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        private void BuildUpExistingObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            this.RegisterObject(context, typeToBuild, existing, idToBuild);
        }

        [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        private object BuildUpNewObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            ICreationPolicy policy = context.Policies.Get<ICreationPolicy>(typeToBuild, idToBuild);
            if (policy == null)
            {
                if (idToBuild == null)
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.MissingPolicyUnnamed, new object[] { typeToBuild }));
                }
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.MissingPolicyNamed, new object[] { typeToBuild, idToBuild }));
            }
            try
            {
                existing = FormatterServices.GetSafeUninitializedObject(typeToBuild);
            }
            catch (MemberAccessException exception)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.CannotCreateInstanceOfType, new object[] { typeToBuild }), exception);
            }
            this.RegisterObject(context, typeToBuild, existing, idToBuild);
            this.InitializeObject(context, existing, idToBuild, policy);
            return existing;
        }

        private void InitializeObject(IBuilderContext context, object existing, string id, ICreationPolicy policy)
        {
            Type type = existing.GetType();
            ConstructorInfo constructor = policy.SelectConstructor(context, type, id);
            if (constructor == null)
            {
                if (!type.IsValueType)
                {
                    throw new ArgumentException(Resources.NoAppropriateConstructor);
                }
            }
            else
            {
                object[] parameters = policy.GetParameters(context, type, id, constructor);
                MethodBase methodInfo = constructor;
                Guard.ValidateMethodParameters(methodInfo, parameters, existing.GetType());
                if (base.TraceEnabled(context))
                {
                    base.TraceBuildUp(context, type, id, Resources.CallingConstructor, new object[] { base.ParametersToTypeList(parameters) });
                }
                methodInfo.Invoke(existing, parameters);
            }
        }

        private void RegisterObject(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            if (context.Locator != null)
            {
                ILifetimeContainer container = context.Locator.Get<ILifetimeContainer>(typeof(ILifetimeContainer), SearchMode.Local);
                if (container != null)
                {
                    ISingletonPolicy policy = context.Policies.Get<ISingletonPolicy>(typeToBuild, idToBuild);
                    if ((policy != null) && policy.IsSingleton)
                    {
                        context.Locator.Add(new DependencyResolutionLocatorKey(typeToBuild, idToBuild), existing);
                        container.Add(existing);
                        if (base.TraceEnabled(context))
                        {
                            base.TraceBuildUp(context, typeToBuild, idToBuild, Resources.SingletonRegistered, new object[0]);
                        }
                    }
                }
            }
        }

        private void ValidateCtorParameters(MethodBase methodInfo, object[] parameters, Type typeBeingBuilt)
        {
            ParameterInfo[] infoArray = methodInfo.GetParameters();
            for (int i = 0; i < infoArray.Length; i++)
            {
                Guard.TypeIsAssignableFromType(infoArray[i].ParameterType, parameters[i].GetType(), typeBeingBuilt);
            }
        }
    }
}

