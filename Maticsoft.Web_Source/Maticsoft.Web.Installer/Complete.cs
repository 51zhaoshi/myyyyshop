namespace Maticsoft.Web.Installer
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class Complete : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(base.Request.Params["type"]) || (base.Request.Params["type"] != "complete"))
            {
                base.Response.Redirect("/Installer/Step.aspx");
            }
        }
    }
}

