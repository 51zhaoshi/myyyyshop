namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Properties;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class ManageableConfigurationSourceSingletonHelper : IDisposable
    {
        private IDictionary<ImplementationKey, ManageableConfigurationSourceImplementation> instances;
        private object lockObject;
        internal bool refresh;

        public ManageableConfigurationSourceSingletonHelper() : this(true)
        {
        }

        internal ManageableConfigurationSourceSingletonHelper(bool refresh)
        {
            this.lockObject = new object();
            this.refresh = refresh;
            this.instances = new Dictionary<ImplementationKey, ManageableConfigurationSourceImplementation>(new ImplementationKeyComparer());
        }

        public void Dispose()
        {
            foreach (ManageableConfigurationSourceImplementation implementation in this.instances.Values)
            {
                implementation.Dispose();
            }
        }

        public ManageableConfigurationSourceImplementation GetInstance(string configurationFilePath, IDictionary<string, ConfigurationSectionManageabilityProvider> manageabilityProviders, bool readGroupPolicies, bool generateWmiObjects, string applicationName)
        {
            ManageableConfigurationSourceImplementation implementation;
            if (string.IsNullOrEmpty(configurationFilePath))
            {
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "configurationFilePath");
            }
            string path = RootConfigurationFilePath(configurationFilePath);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Resources.Culture, Resources.ExceptionConfigurationLoadFileNotFound, new object[] { path }));
            }
            ImplementationKey key = new ImplementationKey(path, applicationName, readGroupPolicies);
            lock (this.lockObject)
            {
                this.instances.TryGetValue(key, out implementation);
                if (implementation == null)
                {
                    implementation = new ManageableConfigurationSourceImplementation(path, this.refresh, manageabilityProviders, readGroupPolicies, generateWmiObjects, applicationName);
                    this.instances.Add(key, implementation);
                }
            }
            return implementation;
        }

        private static string RootConfigurationFilePath(string configurationFilePath)
        {
            string path = configurationFilePath;
            if (!Path.IsPathRooted(path))
            {
                path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
            }
            return path;
        }
    }
}

