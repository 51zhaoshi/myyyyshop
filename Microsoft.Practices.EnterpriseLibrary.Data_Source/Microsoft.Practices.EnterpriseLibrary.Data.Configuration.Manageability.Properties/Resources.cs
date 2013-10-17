namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static string ConnectionStringConnectionStringPartName
        {
            get
            {
                return ResourceManager.GetString("ConnectionStringConnectionStringPartName", resourceCulture);
            }
        }

        internal static string ConnectionStringPolicyNameTemplate
        {
            get
            {
                return ResourceManager.GetString("ConnectionStringPolicyNameTemplate", resourceCulture);
            }
        }

        internal static string ConnectionStringProviderNamePartName
        {
            get
            {
                return ResourceManager.GetString("ConnectionStringProviderNamePartName", resourceCulture);
            }
        }

        internal static string ConnectionStringsCategoryName
        {
            get
            {
                return ResourceManager.GetString("ConnectionStringsCategoryName", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string DatabaseCategoryName
        {
            get
            {
                return ResourceManager.GetString("DatabaseCategoryName", resourceCulture);
            }
        }

        internal static string DatabaseSettingsDefaultDatabasePartName
        {
            get
            {
                return ResourceManager.GetString("DatabaseSettingsDefaultDatabasePartName", resourceCulture);
            }
        }

        internal static string DatabaseSettingsPolicyName
        {
            get
            {
                return ResourceManager.GetString("DatabaseSettingsPolicyName", resourceCulture);
            }
        }

        internal static string OracleConnectionPackagesPartName
        {
            get
            {
                return ResourceManager.GetString("OracleConnectionPackagesPartName", resourceCulture);
            }
        }

        internal static string OracleConnectionPolicyNameTemplate
        {
            get
            {
                return ResourceManager.GetString("OracleConnectionPolicyNameTemplate", resourceCulture);
            }
        }

        internal static string OracleConnectionsCategoryName
        {
            get
            {
                return ResourceManager.GetString("OracleConnectionsCategoryName", resourceCulture);
            }
        }

        internal static string ProviderMappingDatabaseTypePartName
        {
            get
            {
                return ResourceManager.GetString("ProviderMappingDatabaseTypePartName", resourceCulture);
            }
        }

        internal static string ProviderMappingPolicyNameTemplate
        {
            get
            {
                return ResourceManager.GetString("ProviderMappingPolicyNameTemplate", resourceCulture);
            }
        }

        internal static string ProviderMappingsCategoryName
        {
            get
            {
                return ResourceManager.GetString("ProviderMappingsCategoryName", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Manageability.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

