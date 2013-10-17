namespace Maticsoft.Web.Admin.Settings.SEORelation
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Settings.SEORelation bll = new Maticsoft.BLL.Settings.SEORelation();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsActive;
        protected CheckBox chkIsAdd;
        protected CheckBox chkIsCMS;
        protected CheckBox chkIsComment;
        protected CheckBox chkIsShop;
        protected CheckBox chkIsSNS;
        protected TextBox txtKeyName;
        protected TextBox txtLinkURL;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtKeyName.Text))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "链接文字不能为空！");
            }
            else if (string.IsNullOrWhiteSpace(this.txtLinkURL.Text))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "链接地址不能为空！");
            }
            else
            {
                string text = this.txtKeyName.Text;
                if (this.bll.Exists(text))
                {
                    this.btnSave.Enabled = true;
                    this.btnCancle.Enabled = true;
                    MessageBox.ShowFailTip(this, "链接文字已经存在！");
                }
                else
                {
                    string str2 = this.txtLinkURL.Text;
                    bool flag = this.chkIsCMS.Checked;
                    bool flag2 = this.chkIsShop.Checked;
                    bool flag3 = this.chkIsSNS.Checked;
                    bool flag4 = this.chkIsComment.Checked;
                    bool flag5 = this.chkIsActive.Checked;
                    Maticsoft.Model.Settings.SEORelation model = new Maticsoft.Model.Settings.SEORelation {
                        KeyName = text,
                        LinkURL = str2,
                        IsCMS = flag,
                        IsShop = flag2,
                        IsSNS = flag3,
                        IsComment = flag4,
                        CreatedDate = new DateTime?(DateTime.Now),
                        IsActive = flag5
                    };
                    this.bll.Add(model);
                    if (this.chkIsAdd.Checked)
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功", "add.aspx");
                        this.btnSave.Enabled = true;
                        this.btnCancle.Enabled = true;
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...", "list.aspx");
                    }
                }
            }
        }

        private bool IsUrl(string s)
        {
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x189;
            }
        }
    }
}

