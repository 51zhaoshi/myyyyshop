namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;

    public class ProductsExport_Old : PageBaseAdmin
    {
        protected Button ButExport;
        protected DropDownList dropBrands;
        protected DropDownList dropCategory;
        protected DropDownList dropSaleStatus;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox tbProdName;
        protected TextBox tbSKU;

        private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
        {
            return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
        }

        private void BindBrands()
        {
            DataSet list = new BrandInfo().GetList(string.Empty);
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropBrands.DataSource = list;
                this.dropBrands.DataTextField = "BrandName";
                this.dropBrands.DataValueField = "BrandId";
                this.dropBrands.DataBind();
            }
            this.dropBrands.Items.Insert(0, string.Empty);
        }

        private void BindCategories()
        {
            DataSet list = new CategoryInfo().GetList("  Depth = 1 ");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropCategory.DataSource = list;
                this.dropCategory.DataTextField = "Name";
                this.dropCategory.DataValueField = "CategoryId";
                this.dropCategory.DataBind();
            }
            this.dropCategory.Items.Insert(0, string.Empty);
        }

        public void BindData()
        {
            this.BindCategories();
            this.BindBrands();
        }

        protected void ButExport_Click(object sender, EventArgs e)
        {
            try
            {
                int saleStatus = 1;
                string productName = string.Empty;
                int categoryId = -1;
                string sKU = string.Empty;
                int brandId = -1;
                if (this.dropSaleStatus.SelectedValue == "1")
                {
                    saleStatus = 1;
                }
                else
                {
                    saleStatus = 0;
                }
                string str4 = this.tbProdName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(str4))
                {
                    productName = str4;
                }
                categoryId = Convert.ToInt32((this.dropCategory.SelectedValue == string.Empty) ? "-1" : this.dropCategory.SelectedValue);
                string str5 = this.tbSKU.Text.Trim();
                if (!string.IsNullOrWhiteSpace(str5))
                {
                    sKU = str5;
                }
                brandId = Convert.ToInt32((this.dropBrands.SelectedValue == string.Empty) ? "-1" : this.dropBrands.SelectedValue);
                DataSet ds = new ProductInfo().GetListByExport(saleStatus, productName, categoryId, sKU, brandId);
                if (!DataSetTools.DataSetIsNull(ds))
                {
                    DataTable table = ds.Tables[0];
                    StringWriter writer = new StringWriter();
                    writer.WriteLine("商品名称,简单介绍,添加时间,清晰图,市场价");
                    foreach (DataRow row in table.Rows)
                    {
                        StringBuilder argSource = new StringBuilder();
                        argSource = this.AppendCSVFields(argSource, row["ProductName"].ToString());
                        argSource = this.AppendCSVFields(argSource, row["Description"].ToString());
                        argSource = this.AppendCSVFields(argSource, row["AddedDate"].ToString());
                        argSource = this.AppendCSVFields(argSource, row["ImageUrl"].ToString());
                        argSource = this.AppendCSVFields(argSource, row["MarketPrice"].ToString());
                        argSource.Remove(argSource.Length - 1, 1);
                        writer.WriteLine(argSource.ToString());
                    }
                    this.DownloadFile(base.Response, writer.GetStringBuilder(), "MaticsoftProducts");
                    writer.Close();
                    base.Response.End();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "根据条件查询到0条记录！");
                }
            }
            catch (Exception exception)
            {
                MessageBox.ShowFailTip(this, exception.Message);
            }
        }

        public void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
        {
            try
            {
                string str = string.Empty;
                if (!string.IsNullOrWhiteSpace(strFileName))
                {
                    str = "inline; filename=" + strFileName + ".csv";
                }
                else
                {
                    str = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
                }
                argResp.AppendHeader("Content-Disposition", str);
                argResp.ContentType = "application/ms-excel";
                argResp.ContentEncoding = Encoding.GetEncoding("GB2312");
                argResp.Write(argFileStream);
            }
            catch (Exception exception)
            {
                MessageBox.ShowFailTip(this.Page, exception.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1d5;
            }
        }
    }
}

