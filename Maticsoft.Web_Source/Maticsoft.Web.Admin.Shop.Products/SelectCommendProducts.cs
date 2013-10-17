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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class SelectCommendProducts : PageBaseAdmin
    {
        protected int Act_AddData = 480;
        protected int Act_DelData = 0x1e1;
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
        protected HtmlGenericControl liDelAll;
        protected Literal litDesc;
        protected Literal LitProductCategories;
        protected Literal LitProductName;
        protected Literal litProductNum;
        private SKUInfo manage = new SKUInfo();
        protected Panel Panel1;
        private ProductInfo productManage = new ProductInfo();
        private ProductStationMode stationModeManage = new ProductStationMode();
        protected TextBox txtProductName;
        protected TextBox txtProductNum;

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        private void BindAddProduct()
        {
            int categoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue, 0);
            DataSet set = this.stationModeManage.GetStationMode(this.SelectType, categoryId, this.txtProductName.Text);
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
                    int productRecListCount = this.productManage.GetProductRecListCount(source.Distinct<string>().ToArray<string>());
                    int endIndex = this.anpAddedProducts.StartRecordIndex + this.anpAddedProducts.PageSize;
                    List<ProductInfo> list = this.productManage.GetProductRecListByPage(source.Distinct<string>().ToArray<string>(), this.anpAddedProducts.StartRecordIndex, endIndex);
                    this.anpAddedProducts.RecordCount = productRecListCount;
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

        private void BindExistReleatedProducts()
        {
            List<RelatedProduct> modelList = new RelatedProduct().GetModelList(this.ProductId);
            if ((modelList != null) && (modelList.Count > 0))
            {
                StringBuilder strExistInfo = new StringBuilder();
                modelList.ForEach(delegate (RelatedProduct info) {
                    strExistInfo.Append(info.RelatedId);
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
            int endIndex = this.anpSearchProducts.StartRecordIndex + this.anpSearchProducts.PageSize;
            int categoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue, 0);
            string pName = this.txtProductName.Text.Trim();
            int num3 = this.productManage.GetProductNoRecCount(categoryId, pName, this.SelectType);
            List<ProductInfo> list = new List<ProductInfo>();
            if (num3 > 0)
            {
                list = this.productManage.GetProductNoRecList(categoryId, pName, this.SelectType, this.anpSearchProducts.StartRecordIndex, endIndex);
            }
            this.anpSearchProducts.RecordCount = num3;
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
            int categoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue, 0);
            this.stationModeManage.DeleteByType(this.SelectType, categoryId);
            this.hfSelectedData.Value = "";
            this.BindData(true);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData(false);
        }

        protected void dlstAddedProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("lbtnDel");
                control.Visible = false;
            }
        }

        protected void dlstSearchProducts_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("lbtnAdd");
                control.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.liDelAll.Visible = false;
            }
            if (!this.Page.IsPostBack)
            {
                if (this.ProductId > 0L)
                {
                    this.BindExistReleatedProducts();
                }
                this.BindCategories();
                this.BindData(false);
                if (this.SelectType == 0)
                {
                    this.litDesc.Text = "需要推荐的商品";
                }
                if (this.SelectType == 1)
                {
                    this.litDesc.Text = "需要热卖的商品";
                }
                if (this.SelectType == 2)
                {
                    this.litDesc.Text = "需要特价的商品";
                }
                if (this.SelectType == 3)
                {
                    this.litDesc.Text = "最新商品推荐";
                }
                if (this.SelectType == -1)
                {
                    this.litDesc.Text = "选择要导出的商品";
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1df;
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

        public int SelectType
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return num;
            }
        }
    }
}

