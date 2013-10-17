namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Globalization;
    using System.Reflection;

    public class PropertySetterStrategy : BuilderStrategy
    {
        public override object BuildUp(IBuilderContext context, Type typeToBuild, object existing, string idToBuild)
        {
            if (existing != null)
            {
                this.InjectProperties(context, existing, idToBuild);
            }
            return base.BuildUp(context, typeToBuild, existing, idToBuild);
        }

        private void InjectProperties(IBuilderContext context, object obj, string id)
        {
            if (obj != null)
            {
                Type typePolicyAppliesTo = obj.GetType();
                IPropertySetterPolicy policy = context.Policies.Get<IPropertySetterPolicy>(typePolicyAppliesTo, id);
                if (policy != null)
                {
                    foreach (IPropertySetterInfo info in policy.Properties.Values)
                    {
                        PropertyInfo propInfo = info.SelectProperty(context, typePolicyAppliesTo, id);
                        if (propInfo != null)
                        {
                            if (!propInfo.CanWrite)
                            {
                                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.CannotInjectReadOnlyProperty, new object[] { typePolicyAppliesTo, propInfo.Name }));
                            }
                            object obj2 = info.GetValue(context, typePolicyAppliesTo, id, propInfo);
                            if (obj2 != null)
                            {
                                Guard.TypeIsAssignableFromType(propInfo.PropertyType, obj2.GetType(), obj.GetType());
                            }
                            if (base.TraceEnabled(context))
                            {
                                base.TraceBuildUp(context, typePolicyAppliesTo, id, Resources.CallingProperty, new object[] { propInfo.Name, propInfo.PropertyType.Name });
                            }
                            propInfo.SetValue(obj, obj2, null);
                        }
                    }
                }
            }
        }
    }
}

