namespace Maticsoft.Web.Admin.SNS.Groups
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_ApproveList = 0x6b;
        protected int Act_DeleteList = 0x6a;
        private Maticsoft.BLL.SNS.Groups bll = new Maticsoft.BLL.SNS.Groups();
        protected Button btnChecked;
        protected Button btnCheckedUnpass;
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
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
        protected DropDownList rdorecommand;
        private int status = -1;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtName;

        public void BindData()
        {
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.Status >= 0)
            {
                builder.Append(" Status=" + this.status + " ");
            }
            if (Globals.SafeInt(this.rdorecommand.SelectedValue, -1) > -1)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("  IsRecommand={0}", this.rdorecommand.SelectedValue);
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
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, EnumHelper.GroupStatus.Checked))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核群组(GroupID=" + selIDlist + ")成功!", this);
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量审核群组(GroupID=" + selIDlist + ")失败!", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnCheckedUnPass_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateStatusList(selIDlist, EnumHelper.GroupStatus.CheckedUnPass))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量拒绝群组(GroupID=" + selIDlist + ")申请成功!", this);
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量拒绝群组(GroupID=" + selIDlist + ")申请失败!", this);
                }
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteListEx(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
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

        protected string GetThumb(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            string format = target.ToString();
            string path = string.Format(format, "T_");
            if (File.Exists(base.Server.MapPath(path)))
            {
                return path;
            }
            return format.Replace("{0}", "T80x80_");
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RecommendHome")
            {
                if (e.CommandArgument != null)
                {
                    int groupId = 0;
                    string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                    groupId = Globals.SafeInt(strArray[0], 0);
                    int recommand = (Globals.SafeInt(strArray[1], 0) == 1) ? 0 : 1;
                    if (this.bll.UpdateRecommand(groupId, recommand))
                    {
                        this.gridView.OnBind();
                    }
                }
            }
            else if ((e.CommandName == "RecommendPro") && (e.CommandArgument != null))
            {
                int num3 = 0;
                string[] strArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                num3 = Globals.SafeInt(strArray2[0], 0);
                int num4 = (Globals.SafeInt(strArray2[1], 0) == 2) ? 0 : 2;
                if (this.bll.UpdateRecommand(num3, num4))
                {
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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.bll.DeleteListEx(((int) this.gridView.DataKeys[e.RowIndex].Value).ToString());
            this.gridView.OnBind();
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
                    this.btnCheckedUnpass.Visible = false;
                    this.btnChecked.Visible = false;
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
                return 0x69;
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

