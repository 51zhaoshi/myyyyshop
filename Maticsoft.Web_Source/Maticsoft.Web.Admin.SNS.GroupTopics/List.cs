namespace Maticsoft.Web.Admin.SNS.GroupTopics
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_ApproveList = 0x6f;
        protected int Act_DelData = 0x6d;
        protected int Act_DeleteList = 110;
        private Maticsoft.BLL.SNS.Groups bllGroup = new Maticsoft.BLL.SNS.Groups();
        private Maticsoft.BLL.SNS.GroupTopics bllTopic = new Maticsoft.BLL.SNS.GroupTopics();
        protected Button BtnBack;
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList ddlBatch;
        protected DropDownList ddtRecommand;
        protected GridViewEx gridView;
        protected int groupid;
        protected HtmlGenericControl liDel;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected Literal ltlTitle;
        private int status = -1;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtName;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[9].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.Status >= 0)
            {
                builder.Append(" Status=" + this.status + " ");
            }
            if (this.GroupID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" GroupID=" + this.GroupID + " ");
            }
            if (Globals.SafeInt(this.ddtRecommand.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  IsAdminRecommend={0}", this.ddtRecommand.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)>='" + this.txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("  convert(date,CreatedDate)<='" + this.txtEndTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrWhiteSpace(builder.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    builder.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else if (!string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                builder.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("GroupName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            if (builder.Length > 0)
            {
                builder.Append(" and ");
            }
            builder.Append(" 1=1  order by CreatedDate Desc ");
            list = this.bllTopic.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("/admin/SNS/Groups/Group.aspx?GroupId=" + this.GroupID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bllTopic.DeleteListEx(selIDlist);
                MessageBox.ShowSuccessTip(this, "删除成功！");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除群组主题（id=" + selIDlist + "）成功!", this);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void ddlBatch_Changed(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "请选择主题");
            }
            else if (string.IsNullOrWhiteSpace(this.ddlBatch.SelectedValue))
            {
                MessageBox.ShowFailTip(this, "请选择操作类型");
            }
            else
            {
                switch (Globals.SafeInt(this.ddlBatch.SelectedValue, 0))
                {
                    case 1:
                        if (!this.bllTopic.UpdateStatusList(selIDlist, EnumHelper.TopicStatus.Checked))
                        {
                            MessageBox.ShowFailTip(this, "操作失败！");
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新群主主题（id=" + selIDlist + "）状态为未通过失败!", this);
                            break;
                        }
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新群组主题（id=" + selIDlist + "）状态成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        break;

                    case 2:
                        if (!this.bllTopic.UpdateStatusList(selIDlist, EnumHelper.TopicStatus.CheckedUnPass))
                        {
                            MessageBox.ShowFailTip(this, "操作失败！");
                            break;
                        }
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新群主主题（id=" + selIDlist + "）状态为未通过成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        break;

                    case 3:
                    {
                        Maticsoft.BLL.SNS.GroupUsers users = new Maticsoft.BLL.SNS.GroupUsers();
                        if (!users.UpdateStatusByTopicIds(selIDlist, 2))
                        {
                            MessageBox.ShowFailTip(this, "操作失败！");
                            break;
                        }
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "对发表主题（id=" + selIDlist + "）的用户禁言成功!", this);
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        break;
                    }
                }
                this.gridView.OnBind();
            }
        }

        public string GetGroupName(int GroupId)
        {
            if (GroupId > 0)
            {
                Maticsoft.Model.SNS.Groups model = this.bllGroup.GetModel(GroupId);
                if (model != null)
                {
                    return ("[" + model.GroupName + "]");
                }
            }
            return "";
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
                        return "未通过";
                }
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if ((e.CommandName == "RecommendIndex") && (e.CommandArgument != null))
            {
                int topicId = 0;
                string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                topicId = Globals.SafeInt(strArray[0], 0);
                bool flag = Globals.SafeBool(strArray[1], false);
                if (this.bllTopic.UpdateAdminRecommand(topicId, !flag))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "推荐群组主题（id=" + topicId + "）成功!", this);
                    this.gridView.OnBind();
                }
            }
            if ((e.CommandName == "RecommendChannal") && (e.CommandArgument != null))
            {
                int num2 = 0;
                int num3 = 1;
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                num2 = Globals.SafeInt(strArray2[0], 0);
                num3 = Globals.SafeInt(strArray2[1], 1);
                if (this.bllTopic.UpdateRecommand(num2, (num3 == 0) ? 1 : 0))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "推荐群组主题（id=" + num2 + "）成功!", this);
                    this.gridView.OnBind();
                }
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                LinkButton button = (LinkButton) e.Row.FindControl("lbtnDelete");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bllTopic.DeleteListEx(num.ToString()))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除群组主题（id=" + num + "）成功!", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
                this.gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, "删除失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_ApproveList)) && (base.GetPermidByActID(this.Act_ApproveList) != -1))
                {
                    this.ddlBatch.Visible = false;
                }
                this.ltlTitle.Text = this.ltlTitle.Text + this.GetGroupName(this.GroupID);
                if (this.GroupID <= 0)
                {
                    this.BtnBack.Visible = false;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x6c;
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
    }
}

