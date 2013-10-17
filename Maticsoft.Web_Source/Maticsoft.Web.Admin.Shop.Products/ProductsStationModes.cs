namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class ProductsStationModes : PageBaseAdmin
    {
        protected HiddenField hfRelatedProducts;
        protected HiddenField hfSelectedAccessories;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1df;
            }
        }
    }
}

