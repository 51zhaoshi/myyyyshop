namespace Maticsoft.Web.Admin.Ms.SMS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Settings : PageBaseAdmin
    {
        protected int Act_UpdateData = -1;
        protected Button btnSave;
        protected CheckBox chkOpen;
        protected HiddenField thumbList;
        protected TextBox txtKey;
        protected TextBox txtPassword;
        protected TextBox txtSerialNo;
        protected TextBox txtSMSContent;

        private void BoundData()
        {
            this.txtSerialNo.Text = ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            this.txtKey.Text = ConfigSystem.GetValueByCache("Emay_SMS_Key");
            this.txtPassword.Text = ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            this.chkOpen.Checked = ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");
            this.txtSMSContent.Text = ConfigSystem.GetValueByCache("Emay_SMS_Content");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string text = this.txtSerialNo.Text;
                string str2 = this.txtKey.Text;
                string str3 = this.txtSMSContent.Text;
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.ShowFailTip(this, "请填写软件序列号");
                }
                else if (string.IsNullOrWhiteSpace(str2))
                {
                    MessageBox.ShowFailTip(this, "请填写自定义关键字");
                }
                else if (string.IsNullOrWhiteSpace(str3))
                {
                    MessageBox.ShowFailTip(this, "请填写短信发送内容");
                }
                else
                {
                    ConfigSystem.Modify("Emay_SMS_SerialNo", text, "亿美短信接口 软件序列号", ApplicationKeyType.OpenAPI);
                    ConfigSystem.Modify("Emay_SMS_Key", str2, "亿美短信接口 自定义关键字", ApplicationKeyType.OpenAPI);
                    if (!string.IsNullOrWhiteSpace(this.txtPassword.Text))
                    {
                        ConfigSystem.Modify("Emay_SMS_Pwd", this.txtPassword.Text, "亿美短信接口序列号密码", ApplicationKeyType.OpenAPI);
                    }
                    bool flag = this.chkOpen.Checked;
                    ConfigSystem.Modify("Emay_SMS_IsOpen", flag.ToString(), "是否启用短信机制", ApplicationKeyType.OpenAPI);
                    ConfigSystem.Modify("Emay_SMS_Content", str3, "短信发送内容", ApplicationKeyType.OpenAPI);
                    base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.OpenAPI);
                    base.Cache.Remove("ConfigSystemHashList");
                    if (flag)
                    {
                        SMSHelper.RegistEx();
                    }
                    else
                    {
                        SMSHelper.Logout();
                    }
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "Settings.aspx");
                }
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, "操作失败", "Settings.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.btnSave.Visible = false;
            }
            if (!base.IsPostBack)
            {
                this.BoundData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x70;
            }
        }
    }
}

