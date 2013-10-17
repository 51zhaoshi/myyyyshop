namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Sales;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class ProductItems : PageBaseAdmin
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
        protected Literal LitProductCategories;
        protected Literal LitProductName;
        protected Literal litProductNum;
        protected Panel Panel1;
        private ProductInfo productManage = new ProductInfo();
        private SalesRuleProduct ruleProductBll = new SalesRuleProduct();
        protected TextBox txtProductName;
        protected TextBox txtProductNum;

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        private void BindAddProduct()
        {
            DataSet set = this.ruleProductBll.GetRuleProducts(this.RuleId, this.drpProductCategory.SelectedValue, this.txtProductName.Text);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    builder.Append(set.Tables[0].Rows[i]["ProductId"]);
                    builder.Append(",");
                }
                this.hfSelectedData.Value = builder.ToString().TrimEnd(new char[] { ',' });
            }
            else
            {
                this.hfSelectedData.Value = "";
            }
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
                    int endIndex = this.anpAddedProducts.StartRecordIndex + this.anpAddedProducts.PageSize;
                    List<ProductInfo> list = this.productManage.GetRuleProductList(source.Distinct<string>().ToArray<string>(), this.anpAddedProducts.StartRecordIndex, endIndex);
                    this.anpAddedProducts.RecordCount = this.productManage.GetRuleProductCount(source.Distinct<string>().ToArray<string>());
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

        public void BindData(bool isClear)
        {
            this.BindSearchProduct(isClear);
            this.BindAddProduct();
        }

        private void BindExistRuleProducts()
        {
            List<SalesRuleProduct> modelList = this.ruleProductBll.GetModelList(" RuleId=" + this.RuleId);
            if ((modelList != null) && (modelList.Count > 0))
            {
                StringBuilder strExistInfo = new StringBuilder();
                modelList.ForEach(delegate (SalesRuleProduct info) {
                    strExistInfo.Append(info.ProductId);
                    strExistInfo.Append(",");
                });
                this.hfSelectedData.Value = strExistInfo.ToString();
            }
        }

        private void BindSearchProduct(bool isClear)
        {
            if (this.anpSearchProducts.RecordCount == 0)
            {
                this.anpSearchProducts.RecordCount = this.anpSearchProducts.PageSize;
            }
            if (this.hfSelectedData.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length >= 0)
            {
                int endIndex = this.anpSearchProducts.StartRecordIndex + this.anpSearchProducts.PageSize;
                List<ProductInfo> list = this.productManage.GetNoRuleProductList(this.txtProductName.Text, this.drpProductCategory.SelectedValue, this.anpSearchProducts.StartRecordIndex, endIndex);
                this.anpSearchProducts.RecordCount = this.productManage.GetNoRuleProductCount(this.txtProductName.Text, this.drpProductCategory.SelectedValue);
                if ((list != null) && (list.Count > 0))
                {
                    StringBuilder tmpSkuIds = new StringBuilder();
                    list.ForEach(delegate (ProductInfo info) {
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
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.ruleProductBll.DeleteByRule(this.RuleId);
            this.hfSelectedData.Value = "";
            this.BindData(true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindExistRuleProducts();
                this.BindCategories();
                this.BindData(false);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x22f;
            }
        }

        public int RuleId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.QueryString["ruleId"]))
                {
                    num = Globals.SafeInt(base.Request.QueryString["ruleId"], 0);
                }
                return num;
            }
        }
    }
}

