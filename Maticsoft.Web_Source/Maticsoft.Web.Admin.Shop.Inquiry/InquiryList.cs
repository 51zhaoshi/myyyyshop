namespace Maticsoft.Web.Admin.Shop.Inquiry
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Inquiry;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class InquiryList : PageBaseAdmin
    {
        protected Button Button1;
        protected GridViewEx gridView;
        private InquiryInfo infoBll = new InquiryInfo();
        protected Label Label1;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtKeyword;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            string text = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                builder.AppendFormat(" LeaveMsg Like '%{0}%'", text);
            }
            DataSet set = this.infoBll.GetList(-1, builder.ToString(), " CreatedDate desc");
            if (set != null)
            {
                this.gridView.DataSetSource = set;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GeStatusName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target, 0))
                {
                    case 1:
                        return "未处理";

                    case 2:
                        return "已处理";
                }
            }
            return str;
        }

        public string GetRegionName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int id = Globals.SafeInt(target, 0);
                str = new Regions().GetFullNameById4Cache(id);
            }
            return str;
        }

        public string GetUserName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                User user = new User(Globals.SafeInt(target, 0));
                str = (user == null) ? "" : user.NickName;
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
            int num = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.infoBll.DeleteEx((long) num);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)) && (base.GetPermidByActID(base.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}

