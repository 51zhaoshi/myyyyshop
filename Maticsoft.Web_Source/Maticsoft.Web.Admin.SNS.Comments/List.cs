namespace Maticsoft.Web.Admin.SNS.Comments
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x1a;
        private Maticsoft.BLL.SNS.Comments bll = new Maticsoft.BLL.SNS.Comments();
        protected Button btnBack;
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        private int targetid;
        protected TextBox txtBeginTime;
        protected TextBox txtEndTime;
        protected TextBox txtKeyword;
        protected TextBox txtPoster;
        private int type = -1;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.Type >= 0)
            {
                builder.Append(" Type=" + this.Type + " ");
            }
            if (this.TargetID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.Append(" TargetId=" + this.TargetID + " ");
            }
            if (!string.IsNullOrEmpty(this.txtPoster.Text.Trim()))
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("CreatedNickName like '%{0}%' ", this.txtPoster.Text.Trim());
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
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat("Description like '%{0}%' ", this.txtKeyword.Text.Trim());
            }
            if (builder.Length > 0)
            {
                builder.Append(" and");
            }
            builder.Append("  1=1 order by CreatedDate desc");
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Type == 3)
            {
                base.Response.Redirect("/admin/SNS/UserAlbums/UserAlbums.aspx");
            }
            if (this.Type == 4)
            {
                base.Response.Redirect("/admin/SNS/Posts/PostsVideo.aspx");
            }
            if ((this.Type >= 0) && (this.Type < 3))
            {
                base.Response.Redirect("/admin/SNS/Posts/Posts.aspx");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteListEx(selIDlist))
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
                    case 0:
                        return "动态";

                    case 1:
                        return "图片";

                    case 2:
                        return "商品";

                    case 3:
                        return "专辑";
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
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.DeleteListEx(num.ToString());
            this.gridView.OnBind();
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "删除评论(CommentID=" + num + ")成功!", this);
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
                if (this.Type == -1)
                {
                    this.btnBack.Visible = false;
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
                return 0x17;
            }
        }

        public int TargetID
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.Request.Params["targetid"]))
                {
                    this.targetid = Globals.SafeInt(base.Request.Params["targetid"], 0);
                    if (((this.targetid > 0) && (this.Type > 0)) && (this.Type != 3))
                    {
                        Maticsoft.Model.SNS.Posts model = new Maticsoft.BLL.SNS.Posts().GetModel(this.targetid);
                        this.targetid = model.TargetId;
                    }
                }
                return this.targetid;
            }
        }

        public int Type
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    this.type = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return this.type;
            }
        }
    }
}

