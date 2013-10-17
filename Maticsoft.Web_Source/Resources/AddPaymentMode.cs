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

    [GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class AddPaymentMode
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal AddPaymentMode()
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

        internal static string ErrorEmailLength
        {
            get
            {
                return ResourceManager.GetString("ErrorEmailLength", resourceCulture);
            }
        }

        internal static string ErrorHandlingCharge
        {
            get
            {
                return ResourceManager.GetString("ErrorHandlingCharge", resourceCulture);
            }
        }

        internal static string ErrorPayFee
        {
            get
            {
                return ResourceManager.GetString("ErrorPayFee", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_Create_UnKnowError
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_Create_UnKnowError", resourceCulture);
            }
        }

        internal static string IDS_ErrorMessage_GatewayExists
        {
            get
            {
                return ResourceManager.GetString("IDS_ErrorMessage_GatewayExists", resourceCulture);
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

        internal static string IDS_FromField_lblReturn
        {
            get
            {
                return ResourceManager.GetString("IDS_FromField_lblReturn", resourceCulture);
            }
        }

        internal static string IDS_Message_Create_Success
        {
            get
            {
                return ResourceManager.GetString("IDS_Message_Create_Success", resourceCulture);
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

        internal static string lblAllowRecharge
        {
            get
            {
                return ResourceManager.GetString("lblAllowRecharge", resourceCulture);
            }
        }

        internal static string lblOnlineRecharge
        {
            get
            {
                return ResourceManager.GetString("lblOnlineRecharge", resourceCulture);
            }
        }

        internal static string lblPaymentModes
        {
            get
            {
                return ResourceManager.GetString("lblPaymentModes", resourceCulture);
            }
        }

        internal static string ptEditPaymentMode
        {
            get
            {
                return ResourceManager.GetString("ptEditPaymentMode", resourceCulture);
            }
        }

        internal static string ptPaymentModes
        {
            get
            {
                return ResourceManager.GetString("ptPaymentModes", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.AddPaymentMode", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string TooltipEmialAddressLength
        {
            get
            {
                return ResourceManager.GetString("TooltipEmialAddressLength", resourceCulture);
            }
        }
    }
}

