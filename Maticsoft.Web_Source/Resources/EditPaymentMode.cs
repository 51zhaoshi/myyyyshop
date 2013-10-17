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
    internal class EditPaymentMode
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal EditPaymentMode()
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

        internal static string IDS_Button_Update
        {
            get
            {
                return ResourceManager.GetString("IDS_Button_Update", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_OutofNumber
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_OutofNumber", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_PaymentNameExitis
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_PaymentNameExitis", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_Update_UnKnowError
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_Update_UnKnowError", resourceCulture);
            }
        }

        internal static string IDS_FromField_lblReturn
        {
            get
            {
                return ResourceManager.GetString("IDS_FromField_lblReturn", resourceCulture);
            }
        }

        internal static string IDS_Message_Update_Success
        {
            get
            {
                return ResourceManager.GetString("IDS_Message_Update_Success", resourceCulture);
            }
        }

        internal static string IDS_PageDesc
        {
            get
            {
                return ResourceManager.GetString("IDS_PageDesc", resourceCulture);
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
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.EditPaymentMode", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

