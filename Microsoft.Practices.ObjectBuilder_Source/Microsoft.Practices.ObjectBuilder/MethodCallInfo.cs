namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class MethodCallInfo : IMethodCallInfo
    {
        private MethodInfo method;
        private string methodName;
        private List<IParameter> parameters;

        public MethodCallInfo(MethodInfo method) : this(null, method, null)
        {
        }

        public MethodCallInfo(string methodName) : this(methodName, null, null)
        {
        }

        public MethodCallInfo(MethodInfo method, params IParameter[] parameters) : this(null, method, parameters)
        {
        }

        public MethodCallInfo(MethodInfo method, IEnumerable<IParameter> parameters) : this(null, method, parameters)
        {
        }

        public MethodCallInfo(string methodName, params IParameter[] parameters) : this(methodName, null, parameters)
        {
        }

        public MethodCallInfo(string methodName, params object[] parameters) : this(methodName, null, ObjectsToIParameters(parameters))
        {
        }

        public MethodCallInfo(string methodName, IEnumerable<IParameter> parameters) : this(methodName, null, parameters)
        {
        }

        private MethodCallInfo(string methodName, MethodInfo method, IEnumerable<IParameter> parameters)
        {
            this.methodName = methodName;
            this.method = method;
            this.parameters = new List<IParameter>();
            if (parameters != null)
            {
                foreach (IParameter parameter in parameters)
                {
                    this.parameters.Add(parameter);
                }
            }
        }

        public object[] GetParameters(IBuilderContext context, Type type, string id, MethodInfo method)
        {
            List<object> list = new List<object>();
            foreach (IParameter parameter in this.parameters)
            {
                list.Add(parameter.GetValue(context));
            }
            return list.ToArray();
        }

        private static IEnumerable<IParameter> ObjectsToIParameters(object[] parameters)
        {
            List<IParameter> list = new List<IParameter>();
            if (parameters != null)
            {
                foreach (object obj2 in parameters)
                {
                    list.Add(new ValueParameter(obj2.GetType(), obj2));
                }
            }
            return list.ToArray();
        }

        public MethodInfo SelectMethod(IBuilderContext context, Type type, string id)
        {
            if (this.method != null)
            {
                return this.method;
            }
            List<Type> list = new List<Type>();
            foreach (IParameter parameter in this.parameters)
            {
                list.Add(parameter.GetParameterType(context));
            }
            return type.GetMethod(this.methodName, list.ToArray());
        }
    }
}

