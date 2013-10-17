namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class SelectRelatedProducts : PageBaseAdmin
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
        protected HiddenField HiddenField_SelectRelatedData;
        protected Literal LitProductCategories;
        protected Literal LitProductName;
        protected Literal litProductNum;
        private SKUInfo manage = new SKUInfo();
        protected Panel Panel1;
        private ProductInfo productManage = new ProductInfo();
        protected TextBox txtProductName;
        protected TextBox txtProductNum;

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
                this.dlstAddedProducts.DataSource = null;
                this.dlstAddedProducts.DataBind();
            }
            else
            {
                string[] source = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (source.Length >= 0)
                {
                    int num;
                    List<ProductInfo> list = this.productManage.GetProductsList(source.Distinct<string>().ToArray<string>(), this.anpAddedProducts.StartRecordIndex, this.anpAddedProducts.EndRecordIndex, out num, this.ProductId);
                    this.anpAddedProducts.RecordCount = num;
                    this.dlstAddedProducts.DataSource = list;
                    this.dlstAddedProducts.DataBind();
                }
            }
        }

        private void BindCategories()
        {
            DataSet list = new CategoryInfo().GetList("  Depth = 1 ");
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

        private void BindExistReleatedProducts()
        {
            DataSet set = new RelatedProduct().IsDoubleRelated(this.ProductId);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    builder.AppendFormat("{0}_{1}", set.Tables[0].Rows[i]["RelatedId"], set.Tables[0].Rows[i]["IsRelated"]);
                    builder.Append(",");
                    builder2.Append(set.Tables[0].Rows[i]["RelatedId"]);
                    builder2.Append(",");
                }
                this.hfSelectedData.Value = builder2.ToString();
                this.HiddenField_SelectRelatedData.Value = builder.ToString();
            }
        }

        private void BindSearchProduct()
        {
            int num;
            if (this.anpSearchProducts.RecordCount == 0)
            {
                this.anpSearchProducts.RecordCount = this.anpSearchProducts.PageSize;
            }
            string str = this.hfSelectedData.Value;
            string selectedPids = string.Empty;
            if (!string.IsNullOrWhiteSpace(str))
            {
                selectedPids = str.TrimEnd(new char[] { ',' });
            }
            List<ProductInfo> list = this.productManage.GetProductsList(selectedPids, this.txtProductName.Text, this.drpProductCategory.SelectedValue, this.anpSearchProducts.StartRecordIndex, this.anpSearchProducts.EndRecordIndex, out num, this.ProductId);
            this.anpSearchProducts.RecordCount = num;
            if ((list != null) && (list.Count > 0))
            {
                StringBuilder tmpSkuIds = new StringBuilder();
                list.ForEach(delegate (ProductInfo info) {
                    if (info.ProductId != this.ProductId)
                    {
                        tmpSkuIds.Append(info.ProductId);
                    }
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
                    this.BindExistReleatedProducts();
                }
                this.BindCategories();
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

