namespace Maticsoft.Web.Admin.SNS.GroupTopicReply
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_ApproveList = 0x239;
        protected int Act_DelData = 0x238;
        protected Button BtnBack;
        protected Button btnChecked;
        protected Button btnCheckedUnpass;
        protected Button btnDelete;
        protected Button btnForbidSpeak;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected int groupid;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        private Maticsoft.BLL.SNS.GroupTopicReply replyBll = new Maticsoft.BLL.SNS.GroupTopicReply();
        private int status = -1;
        protected int topicid;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            DataSet listEx = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.Status >= 0)
            {
                builder.Append("Status=" + this.status);
            }
            if (this.TopicID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("TopicID=" + this.TopicID);
            }
            if (this.GroupID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("GroupID=" + this.GroupID);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("ReplyNickName like '%{0}%' or Description like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            listEx = this.replyBll.GetListEx(builder.ToString());
            this.gridView.DataSetSource = listEx;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("/admin/SNS/Groups/Group.aspx?GroupId=" + this.GroupID);
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.replyBll.UpdateStatusList(selIDlist, EnumHelper.TopicStatus.Checked))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新主题状态(RePlyIds=" + selIDlist + ")成功!", this);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新主题状态(RePlyIds=" + selIDlist + ")失败!", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnCheckedUnPass_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.replyBll.UpdateStatusList(selIDlist, EnumHelper.TopicStatus.CheckedUnPass))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核主题(RePlyIds=" + selIDlist + ")成功!", this);
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核主题(RePlyIds=" + selIDlist + ")失败!", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.replyBll.DeleteListEx(selIDlist);
                MessageBox.ShowSuccessTip(this, "删除成功！");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除主题回复(RePlyIds=" + selIDlist + ")成功!", this);
                this.gridView.OnBind();
            }
        }

        protected void btnForbidSpeak_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                Maticsoft.BLL.SNS.GroupUsers users = new Maticsoft.BLL.SNS.GroupUsers();
                if (users.UpdateStatusByTopicReplyIds(selIDlist, 2))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量禁言用户成功!", this);
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量禁言用户失败!", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "未审核";

                    case 1:
                        return "已审核";

                    case 2:
                        return "审核未通过";
                }
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int replyID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.replyBll.Delete(replyID))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除主题回复(RePlyId=" + replyID + ")成功!", this);
            }
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.topicid = this.TopicID;
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ApproveList)) && (base.GetPermidByActID(this.Act_ApproveList) != -1))
                {
                    this.btnCheckedUnpass.Visible = false;
                    this.btnChecked.Visible = false;
                    this.btnForbidSpeak.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x237;
            }
        }

        public int GroupID
        {
            get
            {
                string str = base.Request.Params["groupid"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    this.groupid = Globals.SafeInt(str, 0);
                }
                return this.groupid;
            }
        }

        public int Status
        {
            get
            {
                string str = base.Request.Params["status"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    this.status = Globals.SafeInt(str, 0);
                }
                return this.status;
            }
        }

        public int TopicID
        {
            get
            {
                string str = base.Request.Params["topicid"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    this.topicid = Globals.SafeInt(str, 0);
                }
                return this.topicid;
            }
        }
    }
}

