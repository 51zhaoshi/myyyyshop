namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public sealed class AssemblerAttribute : Attribute
    {
        private Type assemblerType;

        public AssemblerAttribute(Type assemblerType)
        {
            if (assemblerType == null)
            {
                throw new ArgumentNullException("assemblerType");
            }
            this.assemblerType = assemblerType;
        }

        public Type AssemblerType
        {
            get
            {
                return this.assemblerType;
            }
        }
    }
}

