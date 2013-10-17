namespace Maticsoft.Web.Admin.Shop.Brands
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected ProductTypesCheckBoxList chkProductTpyes;
        protected Image imgLogo;
        protected Label lblBrandId;
        protected Label lblBrandName;
        protected Label lblBrandSpell;
        protected Label lblCompanyUrl;
        protected Label lblDescription;
        protected Label lblDisplaySequence;
        protected Label lblMeta_Description;
        protected Label lblMeta_Keywords;
        protected Label lblTheme;
        protected Literal Literal2;
        protected Literal Literal3;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Alist.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.chkProductTpyes.DataBind();
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
                {
                    this.strid = base.Request.Params["id"];
                    int brandId = Convert.ToInt32(this.strid);
                    this.ShowInfo(brandId);
                }
            }
        }

        private void ShowInfo(int BrandId)
        {
            Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
            Maticsoft.Model.Shop.Products.BrandInfo model = info.GetModel(BrandId);
            this.lblBrandId.Text = model.BrandId.ToString();
            this.lblBrandName.Text = model.BrandName;
            this.lblBrandSpell.Text = model.BrandSpell;
            this.lblMeta_Description.Text = model.Meta_Description;
            this.lblMeta_Keywords.Text = model.Meta_Keywords;
            this.imgLogo.ImageUrl = model.Logo;
            this.lblCompanyUrl.Text = model.CompanyUrl;
            Maticsoft.Model.Shop.Products.BrandInfo relatedProduct = info.GetRelatedProduct(new int?(BrandId), null);
            foreach (ListItem item in this.chkProductTpyes.Items)
            {
                if (relatedProduct.ProductTypeIdOrBrandsId.Contains(int.Parse(item.Value)))
                {
                    item.Selected = true;
                }
            }
            this.lblDescription.Text = model.Description;
            this.lblDisplaySequence.Text = model.DisplaySequence.ToString();
            this.lblTheme.Text = model.Theme;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 400;
            }
        }
    }
}

