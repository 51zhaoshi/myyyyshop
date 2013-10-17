namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Install;

    public abstract class AbstractInstallerBuilder
    {
        private IList<Type> instrumentedTypes;

        protected AbstractInstallerBuilder(Type[] availableTypes, Type instrumentationAttributeType)
        {
            this.instrumentedTypes = this.FindInstrumentedTypes(availableTypes, instrumentationAttributeType);
        }

        protected bool ConfirmAttributeExists(Type instrumentedType, Type attributeType)
        {
            return (instrumentedType.GetCustomAttributes(attributeType, false).Length != 0);
        }

        protected abstract ICollection<Installer> CreateInstallers(ICollection<Type> instrumentedTypes);
        public void Fill(Installer installer)
        {
            foreach (Installer installer2 in this.CreateInstallers(this.InstrumentedTypes))
            {
                installer.Installers.Add(installer2);
            }
        }

        private Type[] FindInstrumentedTypes(Type[] reflectableTypes, Type instrumentedAttributeType)
        {
            List<Type> list = new List<Type>();
            foreach (Type type in reflectableTypes)
            {
                if (this.IsInstrumented(type, instrumentedAttributeType))
                {
                    list.Add(type);
                }
            }
            return list.ToArray();
        }

        protected bool IsInstrumented(Type instrumentedType, Type instrumentedAttributeType)
        {
            if (instrumentedType == null)
            {
                return false;
            }
            return (this.ConfirmAttributeExists(instrumentedType, typeof(HasInstallableResourcesAttribute)) && this.ConfirmAttributeExists(instrumentedType, instrumentedAttributeType));
        }

        protected IList<Type> InstrumentedTypes
        {
            get
            {
                return this.instrumentedTypes;
            }
            set
            {
                this.instrumentedTypes = value;
            }
        }
    }
}

