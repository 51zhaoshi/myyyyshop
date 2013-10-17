namespace Maticsoft.Web.Admin
{
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class ErrorPage : PageBaseAdmin
    {
        protected Button btnCancle;
        public string ErrorMessage = "";
        protected Literal Literal1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["ErrorMsg"] != null)
            {
                this.ErrorMessage = this.Session["ErrorMsg"].ToString();
                this.Session["ErrorMsg"] = null;
                base.Server.ClearError();
            }
        }
    }
}

