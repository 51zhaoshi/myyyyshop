namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ConstructorPolicy : ICreationPolicy, IBuilderPolicy
    {
        private ConstructorInfo constructor;
        private List<IParameter> parameters;

        public ConstructorPolicy()
        {
            this.parameters = new List<IParameter>();
        }

        public ConstructorPolicy(params IParameter[] parameters)
        {
            this.parameters = new List<IParameter>();
            foreach (IParameter parameter in parameters)
            {
                this.AddParameter(parameter);
            }
        }

        public ConstructorPolicy(ConstructorInfo constructor, params IParameter[] parameters) : this(parameters)
        {
            this.constructor = constructor;
        }

        public void AddParameter(IParameter parameter)
        {
            this.parameters.Add(parameter);
        }

        public object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor)
        {
            List<object> list = new List<object>();
            foreach (IParameter parameter in this.parameters)
            {
                list.Add(parameter.GetValue(context));
            }
            return list.ToArray();
        }

        public ConstructorInfo SelectConstructor(IBuilderContext context, Type type, string id)
        {
            if (this.constructor != null)
            {
                return this.constructor;
            }
            List<Type> list = new List<Type>();
            foreach (IParameter parameter in this.parameters)
            {
                list.Add(parameter.GetParameterType(context));
            }
            return type.GetConstructor(list.ToArray());
        }
    }
}

