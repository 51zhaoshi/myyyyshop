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
    internal class Messages
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Messages()
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

        internal static string drpListUU
        {
            get
            {
                return ResourceManager.GetString("drpListUU", resourceCulture);
            }
        }

        internal static string ErrorContentNotNull
        {
            get
            {
                return ResourceManager.GetString("ErrorContentNotNull", resourceCulture);
            }
        }

        internal static string ErrorLastDateForm
        {
            get
            {
                return ResourceManager.GetString("ErrorLastDateForm", resourceCulture);
            }
        }

        internal static string ErrorReciveID
        {
            get
            {
                return ResourceManager.GetString("ErrorReciveID", resourceCulture);
            }
        }

        internal static string ErrorSendDateForm
        {
            get
            {
                return ResourceManager.GetString("ErrorSendDateForm", resourceCulture);
            }
        }

        internal static string ErrorSendID
        {
            get
            {
                return ResourceManager.GetString("ErrorSendID", resourceCulture);
            }
        }

        internal static string ErrorTitleNotNull
        {
            get
            {
                return ResourceManager.GetString("ErrorTitleNotNull", resourceCulture);
            }
        }

        internal static string fieldAddresserId
        {
            get
            {
                return ResourceManager.GetString("fieldAddresserId", resourceCulture);
            }
        }

        internal static string fieldEmployeeID
        {
            get
            {
                return ResourceManager.GetString("fieldEmployeeID", resourceCulture);
            }
        }

        internal static string fieldIsRead
        {
            get
            {
                return ResourceManager.GetString("fieldIsRead", resourceCulture);
            }
        }

        internal static string fieldLastTime
        {
            get
            {
                return ResourceManager.GetString("fieldLastTime", resourceCulture);
            }
        }

        internal static string fieldPublishContent
        {
            get
            {
                return ResourceManager.GetString("fieldPublishContent", resourceCulture);
            }
        }

        internal static string fieldPublishDate
        {
            get
            {
                return ResourceManager.GetString("fieldPublishDate", resourceCulture);
            }
        }

        internal static string fieldReceiveMessageId
        {
            get
            {
                return ResourceManager.GetString("fieldReceiveMessageId", resourceCulture);
            }
        }

        internal static string fieldRecipient
        {
            get
            {
                return ResourceManager.GetString("fieldRecipient", resourceCulture);
            }
        }

        internal static string fieldSendMessageId
        {
            get
            {
                return ResourceManager.GetString("fieldSendMessageId", resourceCulture);
            }
        }

        internal static string fieldTitle
        {
            get
            {
                return ResourceManager.GetString("fieldTitle", resourceCulture);
            }
        }

        internal static string lblChoseUser
        {
            get
            {
                return ResourceManager.GetString("lblChoseUser", resourceCulture);
            }
        }

        internal static string lblInboxList
        {
            get
            {
                return ResourceManager.GetString("lblInboxList", resourceCulture);
            }
        }

        internal static string lblMessageShow
        {
            get
            {
                return ResourceManager.GetString("lblMessageShow", resourceCulture);
            }
        }

        internal static string lblOutboxList
        {
            get
            {
                return ResourceManager.GetString("lblOutboxList", resourceCulture);
            }
        }

        internal static string lblSendMessage
        {
            get
            {
                return ResourceManager.GetString("lblSendMessage", resourceCulture);
            }
        }

        internal static string lblSendMessageSelectUser
        {
            get
            {
                return ResourceManager.GetString("lblSendMessageSelectUser", resourceCulture);
            }
        }

        internal static string lblSendMessageToUser
        {
            get
            {
                return ResourceManager.GetString("lblSendMessageToUser", resourceCulture);
            }
        }

        internal static string ptEditInformation
        {
            get
            {
                return ResourceManager.GetString("ptEditInformation", resourceCulture);
            }
        }

        internal static string ptInboxList
        {
            get
            {
                return ResourceManager.GetString("ptInboxList", resourceCulture);
            }
        }

        internal static string ptMessageShow
        {
            get
            {
                return ResourceManager.GetString("ptMessageShow", resourceCulture);
            }
        }

        internal static string ptOutboxList
        {
            get
            {
                return ResourceManager.GetString("ptOutboxList", resourceCulture);
            }
        }

        internal static string ptSendMessage
        {
            get
            {
                return ResourceManager.GetString("ptSendMessage", resourceCulture);
            }
        }

        internal static string ptSendMessageSelectUser
        {
            get
            {
                return ResourceManager.GetString("ptSendMessageSelectUser", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Resources.Messages", Assembly.Load("App_GlobalResources"));
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static string String1
        {
            get
            {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }

        internal static string TooltipSendFail
        {
            get
            {
                return ResourceManager.GetString("TooltipSendFail", resourceCulture);
            }
        }

        internal static string TooltipSendSucceed
        {
            get
            {
                return ResourceManager.GetString("TooltipSendSucceed", resourceCulture);
            }
        }
    }
}

