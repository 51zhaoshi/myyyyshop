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

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0"), DebuggerNonUserCode]
    internal class PaymentModes
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal PaymentModes()
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

        internal static string IDS_Delete_Message
        {
            get
            {
                return ResourceManager.GetString("IDS_Delete_Message", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_No_Check
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_No_Check", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_UnKownError
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_UnKownError", resourceCulture);
            }
        }

        internal static string IDS_FromField_lblAddPaymentMode
        {
            get
            {
                return ResourceManager.GetString("IDS_FromField_lblAddPaymentMode", resourceCulture);
            }
        }

        internal static string IDS_Header_DisplaySequence
        {
            get
            {
                return ResourceManager.GetString("IDS_Header_DisplaySequence", resourceCulture);
            }
        }

        internal static string IDS_Header_Gateway
        {
            get
            {
                return ResourceManager.GetString("IDS_Header_Gateway", resourceCulture);
            }
        }

        internal static string IDS_Header_merchantCode
        {
            get
            {
                return ResourceManager.GetString("IDS_Header_merchantCode", resourceCulture);
            }
        }

        internal static string IDS_Header_Name
        {
            get
            {
                return ResourceManager.GetString("IDS_Header_Name", resourceCulture);
            }
        }

        internal static string IDS_Message_Delete_Number
        {
            get
            {
                return ResourceManager.GetString("IDS_Message_Delete_Number", resourceCulture);
            }
        }

        internal static string IDS_Message_Delete_Success
        {
            get
            {
                return ResourceManager.GetString("IDS_Message_Delete_Success", resourceCulture);
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

        internal static string IDS_PaymentNameExitis
        {
            get
            {
                return ResourceManager.GetString("IDS_PaymentNameExitis", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.PaymentModes", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

