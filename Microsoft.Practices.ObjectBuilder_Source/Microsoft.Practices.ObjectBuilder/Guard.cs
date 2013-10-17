namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Globalization;
    using System.Reflection;

    internal static class Guard
    {
        public static void TypeIsAssignableFromType(Type assignee, Type providedType, Type classBeingBuilt)
        {
            if (!assignee.IsAssignableFrom(providedType))
            {
                throw new IncompatibleTypesException(string.Format(CultureInfo.CurrentCulture, Resources.TypeNotCompatible, new object[] { assignee, providedType, classBeingBuilt }));
            }
        }

        public static void ValidateMethodParameters(MethodBase methodInfo, object[] parameters, Type typeBeingBuilt)
        {
            ParameterInfo[] infoArray = methodInfo.GetParameters();
            for (int i = 0; i < infoArray.Length; i++)
            {
                if (parameters[i] != null)
                {
                    TypeIsAssignableFromType(infoArray[i].ParameterType, parameters[i].GetType(), typeBeingBuilt);
                }
            }
        }
    }
}

