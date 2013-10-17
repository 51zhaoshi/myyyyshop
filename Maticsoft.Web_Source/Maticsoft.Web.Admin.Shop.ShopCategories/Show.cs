namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Label lblAssociatedProductType;
        protected Label lblCategoryId;
        protected Label lblDepth;
        protected Label lblDescription;
        protected Label lblDisplaySequence;
        protected Label lblHasChildren;
        protected Label lblMeta_Description;
        protected Label lblMeta_Keywords;
        protected Label lblName;
        protected Label lblNotes1;
        protected Label lblNotes2;
        protected Label lblNotes3;
        protected Label lblNotes4;
        protected Label lblNotes5;
        protected Label lblParentCategoryId;
        protected Label lblPath;
        protected Label lblRewriteName;
        protected Label lblSKUPrefix;
        protected Label lblTheme;
        protected Literal Literal2;
        protected Literal Literal3;
        public string strid = "";

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int categoryId = Convert.ToInt32(this.strid);
                this.ShowInfo(categoryId);
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetModel(CategoryId);
            this.lblCategoryId.Text = model.CategoryId.ToString();
            this.lblName.Text = model.Name;
            this.lblDisplaySequence.Text = model.DisplaySequence.ToString();
            this.lblMeta_Description.Text = model.Meta_Description;
            this.lblMeta_Keywords.Text = model.Meta_Keywords;
            this.lblDescription.Text = model.Description;
            this.lblParentCategoryId.Text = model.ParentCategoryId.ToString();
            this.lblDepth.Text = model.Depth.ToString();
            this.lblPath.Text = model.Path;
            this.lblRewriteName.Text = model.RewriteName;
            this.lblSKUPrefix.Text = model.SKUPrefix;
            this.lblAssociatedProductType.Text = model.AssociatedProductType.ToString();
            this.lblNotes1.Text = model.Notes1;
            this.lblNotes2.Text = model.Notes2;
            this.lblNotes3.Text = model.Notes3;
            this.lblNotes4.Text = model.Notes4;
            this.lblNotes5.Text = model.Notes5;
            this.lblTheme.Text = model.Theme;
            this.lblHasChildren.Text = model.HasChildren ? "是" : "否";
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 530;
            }
        }
    }
}

