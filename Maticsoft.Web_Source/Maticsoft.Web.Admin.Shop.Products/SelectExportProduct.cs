namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class SelectExportProduct : Page
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
        protected Literal litDesc;
        protected Panel Panel1;
        private Maticsoft.BLL.Shop.Products.ProductInfo productManage = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected TextBox txtProductName;

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        private void BindAddProduct()
        {
            new Maticsoft.BLL.Shop.Products.ProductStationMode();
            string str = this.hfSelectedData.Value;
            int num = this.productManage.GetRecordCountEx(string.IsNullOrEmpty(str) ? "0" : str, this.txtProductName.Text, "", 0);
            DataSet set = this.productManage.GetListByPage(this.productManage.GetListExSql(string.IsNullOrEmpty(str) ? "0" : str, this.txtProductName.Text, "", 0), "", this.anpAddedProducts.StartRecordIndex, this.anpAddedProducts.EndRecordIndex);
            this.anpAddedProducts.RecordCount = num;
            this.dlstAddedProducts.DataSource = set;
            this.dlstAddedProducts.DataBind();
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

        private void BindSearchProduct()
        {
            if (this.anpSearchProducts.RecordCount == 0)
            {
                this.anpSearchProducts.RecordCount = this.anpSearchProducts.PageSize;
            }
            string outIds = this.hfSelectedData.Value;
            outIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<Maticsoft.Model.Shop.Products.ProductInfo> list = this.productManage.DataTableToList(this.productManage.GetListByPage(this.productManage.GetListExSql("", this.txtProductName.Text, outIds, Globals.SafeInt(this.drpProductCategory.SelectedValue, 0)), "", this.anpSearchProducts.StartRecordIndex, this.anpSearchProducts.EndRecordIndex).Tables[0]);
            this.anpSearchProducts.RecordCount = this.productManage.GetRecordCount(this.productManage.GetListExSql("", this.txtProductName.Text, outIds, Globals.SafeInt(this.drpProductCategory.SelectedValue, 0)));
            long[] values = (from item in list select item.ProductId).Distinct<long>().ToArray<long>();
            string str2 = string.Join<long>(",", values);
            this.hfCurrentAllData.Value = string.IsNullOrEmpty(str2) ? str2.TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' }) : "";
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
                this.BindCategories();
                this.litDesc.Text = "选择需要导出的商品";
            }
        }
    }
}

