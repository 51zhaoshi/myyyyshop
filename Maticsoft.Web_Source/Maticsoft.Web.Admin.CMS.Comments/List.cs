namespace Maticsoft.Web.Admin.CMS.Comments
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x24d;
        private Comment bll = new Comment();
        protected Button btnApprove;
        protected Button btnDelete;
        protected Button btnSearch;
        protected Button btnUpdateState;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            DataSet set = new DataSet();
            StringBuilder builder = new StringBuilder();
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
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("Description like '%{0}%' ", this.txtKeyword.Text.Trim());
            }
            set = this.bll.GetList(0, builder.ToString(), "  CreatedDate desc ");
            this.gridView.DataSetSource = set;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateList(selIDlist, 1))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除评论(CommentID=" + selIDlist + ")成功!", this);
                    this.gridView.OnBind();
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除评论(CommentID=" + selIDlist + ")失败!", this);
                    MessageBox.ShowFailTip(this, "删除失败！");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.UpdateList(selIDlist, 0))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
            }
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

        public string GetType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 1:
                        return "图片评论";

                    case 2:
                        return "视频评论";

                    case 3:
                        return "内容评论";

                    case 4:
                        return "帖子评论";
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
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            if (this.bll.Delete(iD))
            {
                this.gridView.OnBind();
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除评论(CommentID=" + iD + ")成功!", this);
                MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除评论(CommentID=" + iD + ")失败!", this);
                MessageBox.ShowFailTip(this, "删除失败！");
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
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                this.txtBeginTime.Attributes["readonly"] = "true";
                this.txtEndTime.Attributes["readonly"] = "true";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x24c;
            }
        }
    }
}

