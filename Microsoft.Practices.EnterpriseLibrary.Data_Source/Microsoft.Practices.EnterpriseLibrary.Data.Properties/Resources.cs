namespace Microsoft.Practices.EnterpriseLibrary.Data.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static string BlockName
        {
            get
            {
                return ResourceManager.GetString("BlockName", resourceCulture);
            }
        }

        internal static string CommandExecutedCounterHelpResource
        {
            get
            {
                return ResourceManager.GetString("CommandExecutedCounterHelpResource", resourceCulture);
            }
        }

        internal static string CommandFailedCounterHelpResource
        {
            get
            {
                return ResourceManager.GetString("CommandFailedCounterHelpResource", resourceCulture);
            }
        }

        internal static string ConfigurationFailureCreatingDatabase
        {
            get
            {
                return ResourceManager.GetString("ConfigurationFailureCreatingDatabase", resourceCulture);
            }
        }

        internal static string ConnectionFailedCounterHelpResource
        {
            get
            {
                return ResourceManager.GetString("ConnectionFailedCounterHelpResource", resourceCulture);
            }
        }

        internal static string ConnectionOpenedCounterHelpResource
        {
            get
            {
                return ResourceManager.GetString("ConnectionOpenedCounterHelpResource", resourceCulture);
            }
        }

        internal static string CounterCategoryHelpResourceName
        {
            get
            {
                return ResourceManager.GetString("CounterCategoryHelpResourceName", resourceCulture);
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

        internal static string ErrorConnectionFailedExtraInformation
        {
            get
            {
                return ResourceManager.GetString("ErrorConnectionFailedExtraInformation", resourceCulture);
            }
        }

        internal static string ErrorConnectionFailedMessage
        {
            get
            {
                return ResourceManager.GetString("ErrorConnectionFailedMessage", resourceCulture);
            }
        }

        internal static string ExceptionCommandNotSqlCommand
        {
            get
            {
                return ResourceManager.GetString("ExceptionCommandNotSqlCommand", resourceCulture);
            }
        }

        internal static string ExceptionDatabaseTypeDoesNotHaveAssemblerAttribute
        {
            get
            {
                return ResourceManager.GetString("ExceptionDatabaseTypeDoesNotHaveAssemblerAttribute", resourceCulture);
            }
        }

        internal static string ExceptionDatabaseTypeDoesNotHaveRequiredConfigurationTypeAttribute
        {
            get
            {
                return ResourceManager.GetString("ExceptionDatabaseTypeDoesNotHaveRequiredConfigurationTypeAttribute", resourceCulture);
            }
        }

        internal static string ExceptionMessageParameterMatchFailure
        {
            get
            {
                return ResourceManager.GetString("ExceptionMessageParameterMatchFailure", resourceCulture);
            }
        }

        internal static string ExceptionMessageUpdateDataSetArgumentFailure
        {
            get
            {
                return ResourceManager.GetString("ExceptionMessageUpdateDataSetArgumentFailure", resourceCulture);
            }
        }

        internal static string ExceptionMessageUpdateDataSetRowFailure
        {
            get
            {
                return ResourceManager.GetString("ExceptionMessageUpdateDataSetRowFailure", resourceCulture);
            }
        }

        internal static string ExceptionNoDatabaseDefined
        {
            get
            {
                return ResourceManager.GetString("ExceptionNoDatabaseDefined", resourceCulture);
            }
        }

        internal static string ExceptionNoProviderDefinedForConnectionString
        {
            get
            {
                return ResourceManager.GetString("ExceptionNoProviderDefinedForConnectionString", resourceCulture);
            }
        }

        internal static string ExceptionNullOrEmptyString
        {
            get
            {
                return ResourceManager.GetString("ExceptionNullOrEmptyString", resourceCulture);
            }
        }

        internal static string ExceptionParameterDiscoveryNotSupportedOnGenericDatabase
        {
            get
            {
                return ResourceManager.GetString("ExceptionParameterDiscoveryNotSupportedOnGenericDatabase", resourceCulture);
            }
        }

        internal static string ExceptionTableNameArrayEmpty
        {
            get
            {
                return ResourceManager.GetString("ExceptionTableNameArrayEmpty", resourceCulture);
            }
        }

        internal static string ExceptionTypeNotDatabaseAssembler
        {
            get
            {
                return ResourceManager.GetString("ExceptionTypeNotDatabaseAssembler", resourceCulture);
            }
        }

        internal static string Password
        {
            get
            {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Microsoft.Practices.EnterpriseLibrary.Data.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string UserName
        {
            get
            {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
        }
    }
}

