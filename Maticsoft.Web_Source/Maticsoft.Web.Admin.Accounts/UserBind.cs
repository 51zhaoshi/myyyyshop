namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class UserBind : PageBaseAdmin
    {
        protected GridViewEx gridView;
        protected Literal Literal2;
        protected Literal txtTitle;
        private Maticsoft.BLL.Members.UserBind userBindBll = new Maticsoft.BLL.Members.UserBind();

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            builder.AppendFormat(" UserId=-1", new object[0]);
            if (!string.IsNullOrWhiteSpace(str))
            {
                builder.AppendFormat(" and Status={0}", str);
            }
            this.gridView.DataSetSource = this.userBindBll.GetList(-1, builder.ToString(), " BindId desc");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.userBindBll.DeleteList(selIDlist))
                {
                    MessageBox.ShowAndRedirect(this, "操作成功！", "UserBind.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
        }

        protected string GetImage(object target)
        {
            string str = "";
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (Globals.SafeInt(target.ToString(), 0))
            {
                case 3:
                    return "<img alt='Sina' src='/Admin/images/sina.png' />";

                case 13:
                    return "<img alt='QZone' src='/Admin/images/QQ.png' />";
            }
            return "未知";
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
            if (this.userBindBll.Delete((int) this.gridView.DataKeys[e.RowIndex].Value))
            {
                this.gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.userBindBll.GetWeiBoList(base.CurrentUser.UserID);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xcf;
            }
        }
    }
}

