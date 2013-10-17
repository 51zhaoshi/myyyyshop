namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public class MethodPolicy : IMethodPolicy, IBuilderPolicy
    {
        private Dictionary<string, IMethodCallInfo> methods = new Dictionary<string, IMethodCallInfo>();

        public Dictionary<string, IMethodCallInfo> Methods
        {
            get
            {
                return this.methods;
            }
        }
    }
}

