namespace Maticsoft.Web.Admin.Members.SiteMessages
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList ddlUserType;
        private List<Maticsoft.Model.Members.Users> list = new List<Maticsoft.Model.Members.Users>();
        protected Literal Literal1;
        protected RadioButton rbUser;
        protected RadioButton rbUserType;
        private Maticsoft.Model.Members.SiteMessage SitemMsgModel = new Maticsoft.Model.Members.SiteMessage();
        private Maticsoft.BLL.Members.SiteMessage SiteMsgBll = new Maticsoft.BLL.Members.SiteMessage();
        public int SystemUserId = -1;
        protected TextBox txtContent;
        protected TextBox txtTitle;
        protected TextBox txtUser;
        private Maticsoft.BLL.Members.Users UserBll = new Maticsoft.BLL.Members.Users();
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();

        private void BindToChkList()
        {
            this.ddlUserType.DataSource = this.UserType.GetAllList();
            this.ddlUserType.DataTextField = "Description";
            this.ddlUserType.DataValueField = "UserType";
            this.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtTitle.Text;
            string str2 = this.txtContent.Text;
            DateTime now = DateTime.Now;
            this.SitemMsgModel.Title = text;
            this.SitemMsgModel.Content = str2;
            this.SitemMsgModel.SenderID = new int?(this.SystemUserId);
            this.SitemMsgModel.SendTime = new DateTime?(now);
            this.SitemMsgModel.ReaderIsDel = false;
            this.SitemMsgModel.ReceiverIsRead = false;
            this.SitemMsgModel.SenderIsDel = false;
            if (this.txtContent.Text.Length <= 0)
            {
                MessageBox.ShowFailTip(this, "请输入内容");
            }
            else
            {
                if (this.rbUserType.Checked)
                {
                    List<Maticsoft.Model.Members.Users> modelList = this.UserBll.GetModelList("UserType= '" + this.ddlUserType.SelectedValue + "'");
                    if ((modelList != null) && (modelList.Count > 0))
                    {
                        foreach (Maticsoft.Model.Members.Users users in modelList)
                        {
                            this.SitemMsgModel.ReceiverID = new int?(users.UserID);
                            this.SitemMsgModel.MsgType = users.UserType;
                            this.SiteMsgBll.Add(this.SitemMsgModel);
                        }
                    }
                }
                if (this.rbUser.Checked)
                {
                    if (string.IsNullOrWhiteSpace(this.txtUser.Text))
                    {
                        MessageBox.ShowFailTip(this, "请输入制定用户昵称或者邮箱");
                        return;
                    }
                    foreach (string str4 in this.txtUser.Text.Split(new char[] { ';' }))
                    {
                        List<Maticsoft.Model.Members.Users> list2 = this.UserBll.GetModelList("NickName='" + str4 + "'");
                        List<Maticsoft.Model.Members.Users> list3 = this.UserBll.GetModelList("Email='" + str4 + "'");
                        if (list2.Count > 0)
                        {
                            this.list = list2;
                        }
                        if (list3.Count > 0)
                        {
                            this.list = list3;
                        }
                        if (this.list.Count > 0)
                        {
                            this.SitemMsgModel.ReceiverID = new int?(this.list[0].UserID);
                            this.SiteMsgBll.Add(this.SitemMsgModel);
                        }
                    }
                }
                MessageBox.ShowSuccessTip(this, "发送成功！", "add.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
                else
                {
                    this.BindToChkList();
                }
            }
        }

        public void UserType_Changed(object sender, EventArgs e)
        {
            if (this.rbUser.Checked)
            {
                this.txtUser.Enabled = true;
                this.ddlUserType.Enabled = false;
            }
            else
            {
                this.txtUser.Enabled = false;
                this.ddlUserType.Enabled = true;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x12e;
            }
        }
    }
}

