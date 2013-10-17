namespace Microsoft.Practices.ObjectBuilder.Properties
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

        internal static string BuilderHasNoStrategies
        {
            get
            {
                return ResourceManager.GetString("BuilderHasNoStrategies", resourceCulture);
            }
        }

        internal static string BuilderStrategyTraceBuildUp
        {
            get
            {
                return ResourceManager.GetString("BuilderStrategyTraceBuildUp", resourceCulture);
            }
        }

        internal static string BuilderStrategyTraceTearDown
        {
            get
            {
                return ResourceManager.GetString("BuilderStrategyTraceTearDown", resourceCulture);
            }
        }

        internal static string BuildUpFinished
        {
            get
            {
                return ResourceManager.GetString("BuildUpFinished", resourceCulture);
            }
        }

        internal static string BuildUpStarting
        {
            get
            {
                return ResourceManager.GetString("BuildUpStarting", resourceCulture);
            }
        }

        internal static string CallingConstructor
        {
            get
            {
                return ResourceManager.GetString("CallingConstructor", resourceCulture);
            }
        }

        internal static string CallingMethod
        {
            get
            {
                return ResourceManager.GetString("CallingMethod", resourceCulture);
            }
        }

        internal static string CallingOnBuiltUp
        {
            get
            {
                return ResourceManager.GetString("CallingOnBuiltUp", resourceCulture);
            }
        }

        internal static string CallingOnTearingDown
        {
            get
            {
                return ResourceManager.GetString("CallingOnTearingDown", resourceCulture);
            }
        }

        internal static string CallingProperty
        {
            get
            {
                return ResourceManager.GetString("CallingProperty", resourceCulture);
            }
        }

        internal static string CannotCreateInstanceOfType
        {
            get
            {
                return ResourceManager.GetString("CannotCreateInstanceOfType", resourceCulture);
            }
        }

        internal static string CannotInjectReadOnlyProperty
        {
            get
            {
                return ResourceManager.GetString("CannotInjectReadOnlyProperty", resourceCulture);
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

        internal static string DependencyMissing
        {
            get
            {
                return ResourceManager.GetString("DependencyMissing", resourceCulture);
            }
        }

        internal static string InvalidAttributeCombination
        {
            get
            {
                return ResourceManager.GetString("InvalidAttributeCombination", resourceCulture);
            }
        }

        internal static string InvalidEnumerationValue
        {
            get
            {
                return ResourceManager.GetString("InvalidEnumerationValue", resourceCulture);
            }
        }

        internal static string KeyAlreadyPresentInDictionary
        {
            get
            {
                return ResourceManager.GetString("KeyAlreadyPresentInDictionary", resourceCulture);
            }
        }

        internal static string MissingPolicyNamed
        {
            get
            {
                return ResourceManager.GetString("MissingPolicyNamed", resourceCulture);
            }
        }

        internal static string MissingPolicyUnnamed
        {
            get
            {
                return ResourceManager.GetString("MissingPolicyUnnamed", resourceCulture);
            }
        }

        internal static string NoAppropriateConstructor
        {
            get
            {
                return ResourceManager.GetString("NoAppropriateConstructor", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Microsoft.Practices.ObjectBuilder.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string SingletonRegistered
        {
            get
            {
                return ResourceManager.GetString("SingletonRegistered", resourceCulture);
            }
        }

        internal static string SingletonReturned
        {
            get
            {
                return ResourceManager.GetString("SingletonReturned", resourceCulture);
            }
        }

        internal static string TearDownFinished
        {
            get
            {
                return ResourceManager.GetString("TearDownFinished", resourceCulture);
            }
        }

        internal static string TearDownStarting
        {
            get
            {
                return ResourceManager.GetString("TearDownStarting", resourceCulture);
            }
        }

        internal static string TypeMapped
        {
            get
            {
                return ResourceManager.GetString("TypeMapped", resourceCulture);
            }
        }

        internal static string TypeNotCompatible
        {
            get
            {
                return ResourceManager.GetString("TypeNotCompatible", resourceCulture);
            }
        }
    }
}

