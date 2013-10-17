namespace Maticsoft.Web
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Web.Security;
    using System.Web.UI;

    public abstract class PageBaseAdminAbs : Page
    {
        protected int Act_AddData = 15;
        protected int Act_ApproveList = 4;
        protected int Act_CloseList = 3;
        protected int Act_DelData = -1;
        protected int Act_DeleteList = 1;
        protected int Act_OpenList = 8;
        protected int Act_SetInvalid = 5;
        protected int Act_SetValid = 6;
        protected int Act_ShowInvalid = 2;
        protected int Act_UpdateData = -1;
        private static Hashtable ActHashtab;
        private User currentUser;
        protected readonly string DefaultLoginAdmin;
        protected static IPageBaseMessageTip PageBaseMessageTip;
        protected static IPageBaseOption PageBaseOption;
        public string ToolTipDelete;
        private AccountsPrincipal userPrincipal;

        public PageBaseAdminAbs(IPageBaseOption option, IPageBaseMessageTip message)
        {
            PageBaseOption = option;
            PageBaseMessageTip = message;
            this.DefaultLoginAdmin = PageBaseOption.DefaultLoginAdmin;
            this.ToolTipDelete = PageBaseMessageTip.TooltipDelConfirm;
        }

        public void ExportToExcel(Control ctr)
        {
            string s = @"<style> .text { mso-number-format:\@; } </script> ";
            base.Response.ClearContent();
            base.Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=utf-8>");
            base.Response.AddHeader("content-disposition", "attachment; filename=filename.xls");
            base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            base.Response.ContentType = "application/excel";
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            ctr.RenderControl(writer2);
            base.Response.Write(s);
            base.Response.Write(writer.ToString());
            base.Response.End();
        }

        protected void ExportToExcel(string tabHead, string tabData)
        {
            string str = "sheetName";
            string str2 = "fileName";
            if (tabData != null)
            {
                StringWriter writer = new StringWriter();
                writer.WriteLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                writer.WriteLine("<head>");
                writer.WriteLine("<!--[if gte mso 9]>");
                writer.WriteLine("<xml>");
                writer.WriteLine(" <x:ExcelWorkbook>");
                writer.WriteLine("  <x:ExcelWorksheets>");
                writer.WriteLine("   <x:ExcelWorksheet>");
                writer.WriteLine("    <x:Name>" + str + "</x:Name>");
                writer.WriteLine("    <x:WorksheetOptions>");
                writer.WriteLine("      <x:Print>");
                writer.WriteLine("       <x:ValidPrinterInfo />");
                writer.WriteLine("      </x:Print>");
                writer.WriteLine("    </x:WorksheetOptions>");
                writer.WriteLine("   </x:ExcelWorksheet>");
                writer.WriteLine("  </x:ExcelWorksheets>");
                writer.WriteLine("</x:ExcelWorkbook>");
                writer.WriteLine("</xml>");
                writer.WriteLine("<![endif]-->");
                writer.WriteLine("</head>");
                writer.WriteLine("<body>");
                writer.WriteLine("<table>");
                writer.WriteLine("<tr style=\"background-color: #e4ecf7; text-align: center; font-weight: bold\">");
                writer.WriteLine(tabHead);
                writer.WriteLine("</tr>");
                writer.WriteLine(tabData);
                writer.WriteLine("</table>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
                writer.Close();
                base.Response.Clear();
                base.Response.Buffer = true;
                base.Response.Charset = "UTF-8";
                this.EnableViewState = false;
                base.Response.AddHeader("Content-Disposition", "attachment; filename=" + str2 + ".xls");
                base.Response.ContentType = "application/ms-excel";
                base.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                base.Response.Write(writer);
                base.Response.End();
            }
        }

        public string GetApprovedText(object Value)
        {
            bool flag = false;
            if (((Value != null) && (Value.ToString().Length > 0)) && (Convert.ToInt32(Value) > 0))
            {
                flag = true;
            }
            if (!flag)
            {
                return ("<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>");
            }
            return ("<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>");
        }

        public string GetboolText(object boolValue)
        {
            StringBuilder builder = new StringBuilder();
            if (boolValue != null)
            {
                builder.Append((boolValue.ToString().Trim().ToLower() == "true") ? ("<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>") : ("<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>"));
            }
            return builder.ToString();
        }

        public int GetPermidByActID(int ActionID)
        {
            if (ActHashtab != null)
            {
                object obj2 = ActHashtab[ActionID.ToString()];
                if ((obj2 != null) && (obj2.ToString().Length > 0))
                {
                    return Convert.ToInt32(obj2);
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
            if (!MvcApplication.IsInstall)
            {
                base.Response.Write("<script language='javascript'>window.top.location='/Installer/Default.aspx'</script>");
                base.Response.End();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.DefaultLoginAdmin))
                {
                    throw new ArgumentNullException("SA_Config_System - KEY [DefaultLoginAdmin] IS NULL!");
                }
                if (!this.Context.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                    this.Session.Clear();
                    this.Session.Abandon();
                    base.Response.Clear();
                    base.Response.Write("<script defer>parent.location='" + this.DefaultLoginAdmin + "';</script>");
                    base.Response.End();
                }
                else
                {
                    this.userPrincipal = new AccountsPrincipal(this.Context.User.Identity.Name);
                    if ((this.GetPermidByActID(this.Act_PageLoad) != -1) && !this.userPrincipal.HasPermissionID(this.GetPermidByActID(this.Act_PageLoad)))
                    {
                        base.Response.Clear();
                        base.Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoPermission + "');history.back();</script>");
                        base.Response.End();
                    }
                    if (this.Session[Globals.SESSIONKEY_ADMIN] == null)
                    {
                        this.currentUser = new User(this.userPrincipal);
                        this.Session[Globals.SESSIONKEY_ADMIN] = this.currentUser;
                        this.Session["Style"] = this.currentUser.Style;
                    }
                    else
                    {
                        this.currentUser = (User) this.Session[Globals.SESSIONKEY_ADMIN];
                        this.Session["Style"] = this.currentUser.Style;
                        if (this.currentUser.UserType != "AA")
                        {
                            FormsAuthentication.SignOut();
                            this.Session.Clear();
                            this.Session.Abandon();
                            base.Response.Clear();
                            base.Response.Write("<script defer>parent.location='" + this.DefaultLoginAdmin + "';</script>");
                            base.Response.End();
                        }
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            base.Error += new EventHandler(this.PageBase_Error);
            SingleLogin login = new SingleLogin();
            if (login.ValidateForceLogin())
            {
                base.Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipForceLogin + "');parent.location='" + this.DefaultLoginAdmin + "';</script>");
                base.Response.End();
            }
            ActHashtab = new Actions().GetHashListByCache();
        }

        protected void PageBase_Error(object sender, EventArgs e)
        {
            this.PageError(sender, e);
        }

        protected abstract void PageError(object sender, EventArgs e);
        public string TranslateToPercent(string str)
        {
            return Convert.ToDecimal(str).ToString("#0.##%", CultureInfo.InvariantCulture);
        }

        protected virtual int Act_PageLoad
        {
            get
            {
                return -1;
            }
        }

        public User CurrentUser
        {
            get
            {
                return this.currentUser;
            }
        }

        public AccountsPrincipal UserPrincipal
        {
            get
            {
                return this.userPrincipal;
            }
        }
    }
}

