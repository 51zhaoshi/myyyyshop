namespace Maticsoft.Web.Admin.SNS.Products
{
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class GetTaoDatas : PageBaseAdmin
    {
        protected AspNetPager AspNetPager1;
        protected Button btnGetData;
        protected Button btnMove;
        protected Button btnMove2;
        protected DataList DataListProduct;
        protected DropDownList ddlAlbumList;
        protected DropDownList ddlDiscount;
        protected DropDownList ddlShowcase;
        protected Literal Literal1;
        protected Literal Literal12;
        protected Literal Literal14;
        protected Literal Literal2;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected TaoBaoCategoryDropList TaoBaoCate;
        protected TextBox TextBox1;
        protected TextBox TextBox2;
        protected TextBox TopKeyWord;
        protected TextBox TopPageNo;
        protected TextBox TopPageSize;
        protected Literal txtProduct;
        protected TextBox txtUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

