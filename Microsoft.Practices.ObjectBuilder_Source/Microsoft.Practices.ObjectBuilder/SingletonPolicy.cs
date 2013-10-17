namespace Microsoft.Practices.ObjectBuilder
{
    using System;

    public class SingletonPolicy : ISingletonPolicy, IBuilderPolicy
    {
        private bool isSingleton;

        public SingletonPolicy(bool isSingleton)
        {
            this.isSingleton = isSingleton;
        }

        public bool IsSingleton
        {
            get
            {
                return this.isSingleton;
            }
        }
    }
}

