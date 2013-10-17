namespace Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    internal class installerdefault
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal installerdefault()
        {
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

        internal static string IDS_Button_Agree
        {
            get
            {
                return ResourceManager.GetString("IDS_Button_Agree", resourceCulture);
            }
        }

        internal static string IDS_Button_NO
        {
            get
            {
                return ResourceManager.GetString("IDS_Button_NO", resourceCulture);
            }
        }

        internal static string IDS_FormField_Area
        {
            get
            {
                return ResourceManager.GetString("IDS_FormField_Area", resourceCulture);
            }
        }

        internal static string IDS_FormField_Label1
        {
            get
            {
                return ResourceManager.GetString("IDS_FormField_Label1", resourceCulture);
            }
        }

        internal static string IDS_FormField_Label2
        {
            get
            {
                return ResourceManager.GetString("IDS_FormField_Label2", resourceCulture);
            }
        }

        internal static string IDS_FormField_Label3
        {
            get
            {
                return ResourceManager.GetString("IDS_FormField_Label3", resourceCulture);
            }
        }

        internal static string IDS_FormField_Label4
        {
            get
            {
                return ResourceManager.GetString("IDS_FormField_Label4", resourceCulture);
            }
        }

        internal static string IDS_PageTitle
        {
            get
            {
                return ResourceManager.GetString("IDS_PageTitle", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.installerdefault", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

