namespace Maticsoft.Web.Admin.SNS.PostsVideo
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
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
        protected int Act_DelData = 0x18;
        private Posts bll = new Posts();
        protected Button btnChecked;
        protected Button btnCheckedUnpass;
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl lblTip;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal6;
        protected DropDownList rdocheck;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtName;
        protected int type = -1;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[6].Visible = false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" type=3 ");
            if (Globals.SafeInt(this.rdocheck.SelectedValue, -1) > -1)
            {
                builder.AppendFormat("  and Status={0}", this.rdocheck.SelectedValue);
            }
            if (!string.IsNullOrEmpty(this.txtBeginTime.Text.Trim()))
            {
                builder.Append(" and convert(date,CreatedDate)>='" + this.txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
            {
                builder.Append(" and convert(date,CreatedDate)<='" + this.txtEndTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                builder.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
            }
            builder.Append("  order by CreatedDate Desc ");
            DataSet list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, 1))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新视频动态(PostID=" + selIDlist + ")为已审核状态成功", this);
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新视频动态(PostID=" + selIDlist + ")为已审核状态失败", this);
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnCheckedUnPass_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, 2))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新视频动态(PostID=" + selIDlist + ")为未通过状态成功", this);
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量更新视频动态(PostID=" + selIDlist + ")为未通过状态失败", this);
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (this.bll.DeleteListByNormalPost(selIDlist, true, base.CurrentUser.UserID))
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除视频动态(PostID=" + selIDlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除视频动态(PostID=" + selIDlist + ")失败", this);
                MessageBox.ShowSuccessTip(this, "删除失败！");
            }
            this.gridView.OnBind();
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

        private string GetSelIDTypelist(string type)
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if ((this.gridView.DataKeys[i].Values.Count > 1) && (this.gridView.DataKeys[i].Values[1].ToString() == type))
                    {
                        if (type == "0")
                        {
                            str = str + this.gridView.DataKeys[i].Values[0] + ",";
                        }
                        else
                        {
                            str = str + this.gridView.DataKeys[i].Values[2] + ",";
                        }
                    }
                }
            }
            if (flag && (str.Length > 0))
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), 0))
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

        public string GetTypeName(object target, object TargetId)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        return "文字 ";

                    case 1:
                        return (" <a  target='_blank' href='/Photo/Detail/" + TargetId.ToString() + "'>图片<a>");

                    case 2:
                        return (" <a  target='_blank' href='/Product/Detail/" + TargetId.ToString() + "'>商品 <a>");
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
            if (e.CommandArgument.Equals("Delete") && ((e.CommandName != null) && (e.CommandName.ToString() != "")))
            {
                if (this.bll.DeleteEx(Globals.SafeInt(e.CommandName, -1), true, base.CurrentUser.UserID))
                {
                    this.gridView.OnBind();
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "删除失败！");
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("linkbtnDel");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_ApproveList)) && (base.GetPermidByActID(base.Act_ApproveList) != -1))
                {
                    this.btnChecked.Visible = false;
                    this.btnCheckedUnpass.Visible = false;
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
                return 0x5c;
            }
        }
    }
}

