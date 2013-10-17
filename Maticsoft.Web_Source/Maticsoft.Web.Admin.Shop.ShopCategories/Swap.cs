namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Swap : PageBaseAdmin
    {
        private CategoryInfo bll = new CategoryInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected CategoriesDropList CategoriesDropList1;
        protected CategoriesDropList CategoriesDropList2;
        protected Literal Literal2;
        protected Literal Literal3;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int fromCategoryId = Globals.SafeInt(this.CategoriesDropList2.SelectedValue, 0);
            int toCategoryId = Globals.SafeInt(this.CategoriesDropList1.SelectedValue, 0);
            if (this.bll.DisplaceCategory(fromCategoryId, toCategoryId))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, "商品转移成功，正在跳转列表页...", "list.aspx");
            }
            else
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowFailTip(this, "商品转移失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !string.IsNullOrWhiteSpace(base.Request.Params["id"].Trim()))
            {
                this.CategoriesDropList2.SelectedValue = base.Request.Params["id"];
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x213;
            }
        }
    }
}

