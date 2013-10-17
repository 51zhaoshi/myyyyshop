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

    [DebuggerNonUserCode, GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "10.0.0.0"), CompilerGenerated]
    internal class EmailSettings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal EmailSettings()
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

        internal static string IDM_btnChangeEmailSettings
        {
            get
            {
                return ResourceManager.GetString("IDM_btnChangeEmailSettings", resourceCulture);
            }
        }

        internal static string IDM_btnTestEmailSettings
        {
            get
            {
                return ResourceManager.GetString("IDM_btnTestEmailSettings", resourceCulture);
            }
        }

        internal static string IDM_btnTestEmailSettings_Click_Msg_NO
        {
            get
            {
                return ResourceManager.GetString("IDM_btnTestEmailSettings_Click_Msg_NO", resourceCulture);
            }
        }

        internal static string IDM_btnTestEmailSettings_Click_Msg_OK
        {
            get
            {
                return ResourceManager.GetString("IDM_btnTestEmailSettings_Click_Msg_OK", resourceCulture);
            }
        }

        internal static string IDM_EmailGeneralSettings
        {
            get
            {
                return ResourceManager.GetString("IDM_EmailGeneralSettings", resourceCulture);
            }
        }

        internal static string IDM_help03
        {
            get
            {
                return ResourceManager.GetString("IDM_help03", resourceCulture);
            }
        }

        internal static string IDM_help04
        {
            get
            {
                return ResourceManager.GetString("IDM_help04", resourceCulture);
            }
        }

        internal static string IDM_help07
        {
            get
            {
                return ResourceManager.GetString("IDM_help07", resourceCulture);
            }
        }

        internal static string IDM_help08
        {
            get
            {
                return ResourceManager.GetString("IDM_help08", resourceCulture);
            }
        }

        internal static string IDM_help082
        {
            get
            {
                return ResourceManager.GetString("IDM_help082", resourceCulture);
            }
        }

        internal static string IDM_help09
        {
            get
            {
                return ResourceManager.GetString("IDM_help09", resourceCulture);
            }
        }

        internal static string IDM_help10
        {
            get
            {
                return ResourceManager.GetString("IDM_help10", resourceCulture);
            }
        }

        internal static string IDM_lblAdminEmailAddress
        {
            get
            {
                return ResourceManager.GetString("IDM_lblAdminEmailAddress", resourceCulture);
            }
        }

        internal static string IDM_lblEmailEncoding
        {
            get
            {
                return ResourceManager.GetString("IDM_lblEmailEncoding", resourceCulture);
            }
        }

        internal static string IDM_lblEmailThrottle
        {
            get
            {
                return ResourceManager.GetString("IDM_lblEmailThrottle", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpEnableSsl
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpEnableSsl", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpPortNumber
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpPortNumber", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpServer
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpServer", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpServerPassword
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpServerPassword", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpServerPassword2
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpServerPassword2", resourceCulture);
            }
        }

        internal static string IDM_lblSmtpServerUserName
        {
            get
            {
                return ResourceManager.GetString("IDM_lblSmtpServerUserName", resourceCulture);
            }
        }

        internal static string IDM_SMTPLoginInfo
        {
            get
            {
                return ResourceManager.GetString("IDM_SMTPLoginInfo", resourceCulture);
            }
        }

        internal static string IDS_btnChangeEmailSettings_Click_Msg_OK
        {
            get
            {
                return ResourceManager.GetString("IDS_btnChangeEmailSettings_Click_Msg_OK", resourceCulture);
            }
        }

        internal static string IDS_EmailAddress_Error
        {
            get
            {
                return ResourceManager.GetString("IDS_EmailAddress_Error", resourceCulture);
            }
        }

        internal static string IDS_EmailThrottle_Error
        {
            get
            {
                return ResourceManager.GetString("IDS_EmailThrottle_Error", resourceCulture);
            }
        }

        internal static string IDS_help01
        {
            get
            {
                return ResourceManager.GetString("IDS_help01", resourceCulture);
            }
        }

        internal static string IDS_Invalid_EmailThrottle_NotNumber
        {
            get
            {
                return ResourceManager.GetString("IDS_Invalid_EmailThrottle_NotNumber", resourceCulture);
            }
        }

        internal static string IDS_Invalid_SmtpPortNumber_NotNumber
        {
            get
            {
                return ResourceManager.GetString("IDS_Invalid_SmtpPortNumber_NotNumber", resourceCulture);
            }
        }

        internal static string IDS_Invalid_SmtpServerPassword_NotComp
        {
            get
            {
                return ResourceManager.GetString("IDS_Invalid_SmtpServerPassword_NotComp", resourceCulture);
            }
        }

        internal static string IDS_Invalid_SmtpServerUserName_FormatError
        {
            get
            {
                return ResourceManager.GetString("IDS_Invalid_SmtpServerUserName_FormatError", resourceCulture);
            }
        }

        internal static string IDS_PageDesc
        {
            get
            {
                return ResourceManager.GetString("IDS_PageDesc", resourceCulture);
            }
        }

        internal static string IDS_PageTiTle
        {
            get
            {
                return ResourceManager.GetString("IDS_PageTiTle", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.EmailSettings", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

