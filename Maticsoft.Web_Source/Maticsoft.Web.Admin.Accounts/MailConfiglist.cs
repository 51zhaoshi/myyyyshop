namespace Maticsoft.Web.Admin.Accounts
{
    using Maticsoft.BLL;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class MailConfiglist : PageBaseAdmin
    {
        private MailConfig bll = new MailConfig();
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;

        public void BindData()
        {
            DataSet listByUser = new DataSet();
            if (base.CurrentUser != null)
            {
                listByUser = this.bll.GetListByUser(base.CurrentUser.UserID);
                this.gridView.DataSetSource = listByUser;
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            DataControlRowType rowType = e.Row.RowType;
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int iD = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(iD);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (this.Session["Style"] != null)) && (this.Session["Style"].ToString() != ""))
            {
                string str = this.Session["Style"] + "xtable_bordercolorlight";
                if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                {
                    this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
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
                return 0xcd;
            }
        }
    }
}

