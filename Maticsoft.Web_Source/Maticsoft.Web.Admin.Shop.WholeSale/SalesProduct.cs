namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class SalesProduct : PageBaseAdmin
    {
        protected HiddenField hfRelatedProducts;
        protected HiddenField hfSelectedAccessories;
        public int RuleId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !string.IsNullOrWhiteSpace(base.Request.QueryString["id"]))
            {
                this.RuleId = Globals.SafeInt(base.Request.QueryString["id"], 0);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x22f;
            }
        }
    }
}

