namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Reflection;

    public class DefaultCreationPolicy : ICreationPolicy, IBuilderPolicy
    {
        public object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] objArray = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                objArray[i] = context.HeadOfChain.BuildUp(context, parameters[i].ParameterType, null, id);
            }
            return objArray;
        }

        public ConstructorInfo SelectConstructor(IBuilderContext context, Type typeToBuild, string idToBuild)
        {
            ConstructorInfo[] constructors = typeToBuild.GetConstructors();
            if (constructors.Length > 0)
            {
                return constructors[0];
            }
            return null;
        }
    }
}

