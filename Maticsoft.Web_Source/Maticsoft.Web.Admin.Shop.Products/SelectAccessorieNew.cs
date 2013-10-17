namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class SelectAccessorieNew : PageBaseAdmin
    {
        protected AspNetPager anpAddedProducts;
        protected AspNetPager anpSearchProducts;
        protected Button btnAddSearch;
        protected Button btnClear;
        protected Button btnSearch;
        protected DataList dlstAddedProducts;
        protected DataList dlstSearchProducts;
        protected DropDownList drpProductCategory;
        protected HiddenField hfCurrentAllData;
        protected HiddenField hfSelectedData;
        protected Literal LitProductCategories;
        protected Literal LitProductName;
        private Maticsoft.BLL.Shop.Products.SKUInfo manage = new Maticsoft.BLL.Shop.Products.SKUInfo();
        protected Panel Panel1;
        protected TextBox txtProductName;

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void BindAddProduct()
        {
            string str = this.hfSelectedData.Value;
            if (this.anpAddedProducts.RecordCount == 0)
            {
                this.anpAddedProducts.RecordCount = this.anpAddedProducts.PageSize;
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                this.anpAddedProducts.RecordCount = 0;
                this.dlstAddedProducts.DataSource = null;
                this.dlstAddedProducts.DataBind();
            }
            else
            {
                string[] source = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (source.Length >= 0)
                {
                    int num;
                    List<Maticsoft.Model.Shop.Products.SKUInfo> list = this.manage.GetSKU4AttrVal(source.Distinct<string>().ToArray<string>(), this.anpAddedProducts.StartRecordIndex, this.anpAddedProducts.EndRecordIndex, out num, this.ProductId);
                    this.anpAddedProducts.RecordCount = num;
                    this.dlstAddedProducts.DataSource = list;
                    this.dlstAddedProducts.DataBind();
                }
            }
        }

        private void BindCategories()
        {
            DataSet list = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetList("  Depth = 1 ");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.drpProductCategory.DataSource = list;
                this.drpProductCategory.DataTextField = "Name";
                this.drpProductCategory.DataValueField = "CategoryId";
                this.drpProductCategory.DataBind();
            }
            this.drpProductCategory.Items.Insert(0, new ListItem("请选择", string.Empty));
        }

        public void BindData()
        {
            this.BindSearchProduct();
            this.BindAddProduct();
        }

        private void BindExistAccessoriesValue()
        {
            List<Maticsoft.Model.Shop.Products.AccessoriesValue> list = new Maticsoft.BLL.Shop.Products.AccessoriesValue().AccessoriesByProductId(this.ProductId);
            if ((list != null) && (list.Count > 0))
            {
                StringBuilder strExistAcc = new StringBuilder();
                list.ForEach(delegate (Maticsoft.Model.Shop.Products.AccessoriesValue info) {
                    strExistAcc.Append(info.ProductAccessoriesSKU);
                    strExistAcc.Append(",");
                });
                this.hfSelectedData.Value = strExistAcc.ToString();
            }
        }

        private void BindSearchProduct()
        {
            int num;
            if (this.anpSearchProducts.RecordCount == 0)
            {
                this.anpSearchProducts.RecordCount = this.anpSearchProducts.PageSize;
            }
            List<Maticsoft.Model.Shop.Products.SKUInfo> list = this.manage.GetSKU4AttrVal(this.txtProductName.Text, this.drpProductCategory.SelectedValue, this.hfSelectedData.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), this.anpSearchProducts.StartRecordIndex, this.anpSearchProducts.EndRecordIndex, out num, this.ProductId);
            this.anpSearchProducts.RecordCount = num;
            if ((list != null) && (list.Count > 0))
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                list.ForEach(delegate (Maticsoft.Model.Shop.Products.SKUInfo info) {
                    tmpSkuIds.Append(info.SkuId);
                    tmpSkuIds.Append(",");
                });
                this.hfCurrentAllData.Value = tmpSkuIds.ToString();
            }
            else
            {
                this.hfCurrentAllData.Value = string.Empty;
            }
            this.dlstSearchProducts.DataSource = list;
            this.dlstSearchProducts.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.hfSelectedData.Value = "";
            this.BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.ProductId > 0L)
                {
                    this.BindExistAccessoriesValue();
                }
                this.BindCategories();
            }
        }

        protected void rptProducts_OnItemDataBound(object sender, DataListItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item))
            {
                Repeater repeater = e.Item.FindControl("rptSKUItems") as Repeater;
                repeater.DataSource = ((Maticsoft.Model.Shop.Products.SKUInfo) e.Item.DataItem).SkuItems;
                repeater.DataBind();
            }
        }

        public long ProductId
        {
            get
            {
                long num = 0L;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["pid"]))
                {
                    num = Globals.SafeLong(base.Request.Params["pid"], (long) 0L);
                }
                return num;
            }
        }
    }
}

