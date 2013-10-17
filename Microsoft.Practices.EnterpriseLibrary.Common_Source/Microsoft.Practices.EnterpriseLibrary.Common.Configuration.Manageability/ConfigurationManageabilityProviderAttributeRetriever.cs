namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class ConfigurationManageabilityProviderAttributeRetriever
    {
        private ICollection<ConfigurationElementManageabilityProviderAttribute> elementManageabilityProviderAttributes;
        private ICollection<ConfigurationSectionManageabilityProviderAttribute> sectionManageabilityProviderAttributes;

        public ConfigurationManageabilityProviderAttributeRetriever() : this(AppDomain.CurrentDomain.BaseDirectory)
        {
        }

        public ConfigurationManageabilityProviderAttributeRetriever(IEnumerable<string> fileNames)
        {
            this.sectionManageabilityProviderAttributes = new List<ConfigurationSectionManageabilityProviderAttribute>();
            this.elementManageabilityProviderAttributes = new List<ConfigurationElementManageabilityProviderAttribute>();
            LoadRegisteredManageabilityProviders(fileNames, this.sectionManageabilityProviderAttributes, this.elementManageabilityProviderAttributes);
        }

        public ConfigurationManageabilityProviderAttributeRetriever(string baseDirectory) : this(GetAssemblyNames(baseDirectory))
        {
        }

        private static IEnumerable<string> GetAssemblyNames(string baseDirectory)
        {
            return Directory.GetFiles(baseDirectory, "*.dll");
        }

        private static void LoadAttributes<T>(Assembly assembly, ICollection<T> manageabilityProviderAttributes)
        {
            foreach (T local in assembly.GetCustomAttributes(typeof(T), false))
            {
                manageabilityProviderAttributes.Add(local);
            }
        }

        private static void LoadRegisteredManageabilityProviders(IEnumerable<string> fileNames, ICollection<ConfigurationSectionManageabilityProviderAttribute> sectionManageabilityProviderAttributes, ICollection<ConfigurationElementManageabilityProviderAttribute> elementManageabilityProviderAttributes)
        {
            foreach (string str in fileNames)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(AssemblyName.GetAssemblyName(str));
                }
                catch (BadImageFormatException)
                {
                }
                if (assembly != null)
                {
                    LoadAttributes<ConfigurationSectionManageabilityProviderAttribute>(assembly, sectionManageabilityProviderAttributes);
                    LoadAttributes<ConfigurationElementManageabilityProviderAttribute>(assembly, elementManageabilityProviderAttributes);
                }
            }
        }

        public IEnumerable<ConfigurationElementManageabilityProviderAttribute> ElementManageabilityProviderAttributes
        {
            get
            {
                return this.elementManageabilityProviderAttributes;
            }
        }

        public IEnumerable<ConfigurationSectionManageabilityProviderAttribute> SectionManageabilityProviderAttributes
        {
            get
            {
                return this.sectionManageabilityProviderAttributes;
            }
        }
    }
}

