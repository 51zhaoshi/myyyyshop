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

    [DebuggerNonUserCode, CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0")]
    internal class MsEntryForm
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal MsEntryForm()
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

        internal static string ErrorNameNotNull
        {
            get
            {
                return ResourceManager.GetString("ErrorNameNotNull", resourceCulture);
            }
        }

        internal static string ErrorRemarkoverlength
        {
            get
            {
                return ResourceManager.GetString("ErrorRemarkoverlength", resourceCulture);
            }
        }

        internal static string lblCompanyAddress
        {
            get
            {
                return ResourceManager.GetString("lblCompanyAddress", resourceCulture);
            }
        }

        internal static string lblHouseAddress
        {
            get
            {
                return ResourceManager.GetString("lblHouseAddress", resourceCulture);
            }
        }

        internal static string lblMsEntryFormAdd
        {
            get
            {
                return ResourceManager.GetString("lblMsEntryFormAdd", resourceCulture);
            }
        }

        internal static string lblMsEntryFormList
        {
            get
            {
                return ResourceManager.GetString("lblMsEntryFormList", resourceCulture);
            }
        }

        internal static string lblMsEntryModify
        {
            get
            {
                return ResourceManager.GetString("lblMsEntryModify", resourceCulture);
            }
        }

        internal static string lblMsEntryShow
        {
            get
            {
                return ResourceManager.GetString("lblMsEntryShow", resourceCulture);
            }
        }

        internal static string lblUsername
        {
            get
            {
                return ResourceManager.GetString("lblUsername", resourceCulture);
            }
        }

        internal static string ptMsEntryFormAdd
        {
            get
            {
                return ResourceManager.GetString("ptMsEntryFormAdd", resourceCulture);
            }
        }

        internal static string ptMsEntryFormList
        {
            get
            {
                return ResourceManager.GetString("ptMsEntryFormList", resourceCulture);
            }
        }

        internal static string ptMsEntryModify
        {
            get
            {
                return ResourceManager.GetString("ptMsEntryModify", resourceCulture);
            }
        }

        internal static string ptMsEntryShow
        {
            get
            {
                return ResourceManager.GetString("ptMsEntryShow", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.MsEntryForm", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

