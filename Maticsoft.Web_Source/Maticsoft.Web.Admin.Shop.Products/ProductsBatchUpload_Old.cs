namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ProductsBatchUpload_Old : PageBaseAdmin
    {
        protected Button ButUpload;
        protected CheckBox CheckBox1;
        protected CheckBox CheckBox2;
        protected CheckBox CheckBox3;
        protected CheckBox CheckBox4;
        protected CheckBox CheckBoxSaleStatus;
        protected HtmlGenericControl divCheckBox;
        protected DropDownList dropBrands;
        protected DropDownList dropEnterprise;
        private string dropItem0Value = "请选择";
        protected DropDownList dropProductTypes;
        protected FileUpload FileUploadProducts;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Maticsoft.Web.Controls.ProductsBatchUploadDropList ProductsBatchUploadDropList;

        private void BindBrands()
        {
            DataSet list = new Maticsoft.BLL.Shop.Products.BrandInfo().GetList(string.Empty);
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropBrands.DataSource = list;
                this.dropBrands.DataTextField = "BrandName";
                this.dropBrands.DataValueField = "BrandId";
                this.dropBrands.DataBind();
            }
            this.dropBrands.Items.Insert(0, this.dropItem0Value);
        }

        public void BindData()
        {
            this.BindProductTypes();
            this.BindBrands();
            this.BindEnterprise();
        }

        private void BindEnterprise()
        {
            DataSet list = new Enterprise().GetList(string.Empty);
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropEnterprise.DataSource = list;
                this.dropEnterprise.DataTextField = "Name";
                this.dropEnterprise.DataValueField = "EnterpriseID";
                this.dropEnterprise.DataBind();
            }
            this.dropEnterprise.Items.Insert(0, this.dropItem0Value);
        }

        private void BindProductTypes()
        {
            DataSet list = new Maticsoft.BLL.Shop.Products.ProductType().GetList(string.Empty);
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropProductTypes.DataSource = list;
                this.dropProductTypes.DataTextField = "TypeName";
                this.dropProductTypes.DataValueField = "TypeId";
                this.dropProductTypes.DataBind();
            }
        }

        protected void ButUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.FileUploadProducts.PostedFile != null) && (this.FileUploadProducts.PostedFile.ContentLength > 0))
                {
                    string str = Path.GetExtension(this.FileUploadProducts.PostedFile.FileName).ToLower();
                    ".csv".IndexOf(str);
                    if ((!this.CheckBox1.Checked && !this.CheckBox2.Checked) && (!this.CheckBox3.Checked && !this.CheckBox4.Checked))
                    {
                        MessageBox.ShowFailTip(this, "请至少选择一种商品推荐方式！");
                    }
                    else
                    {
                        int num = Convert.ToInt32((this.ProductsBatchUploadDropList.SelectedValue == this.dropItem0Value) ? "-1" : this.ProductsBatchUploadDropList.SelectedValue);
                        int num2 = Convert.ToInt32((this.dropProductTypes.SelectedValue == this.dropItem0Value) ? "-1" : this.dropProductTypes.SelectedValue);
                        int num3 = Convert.ToInt32((this.dropBrands.SelectedValue == this.dropItem0Value) ? "-1" : this.dropBrands.SelectedValue);
                        int num4 = Convert.ToInt32((this.dropEnterprise.SelectedValue == this.dropItem0Value) ? "-1" : this.dropEnterprise.SelectedValue);
                        string str2 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
                        OleDbConnection selectConnection = new OleDbConnection(str2 + this.FileUploadProducts.PostedFile.FileName.Replace(this.FileUploadProducts.FileName, string.Empty) + ";Extended Properties=\"text;HDR=Yes;FMT=Delimited\"");
                        DataSet dataSet = new DataSet();
                        new OleDbDataAdapter("select * from " + this.FileUploadProducts.FileName, selectConnection).Fill(dataSet);
                        selectConnection.Close();
                        int num5 = 0;
                        Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo();
                        Maticsoft.BLL.Shop.Products.ProductInfo info2 = new Maticsoft.BLL.Shop.Products.ProductInfo();
                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            model.CategoryId = num;
                            model.TypeId = new int?(num2);
                            model.BrandId = num3;
                            model.SupplierId = num4;
                            model.SaleStatus = this.CheckBoxSaleStatus.Checked ? 1 : 0;
                            model.ProductName = dataSet.Tables[0].Rows[i]["商品名称"].ToString();
                            model.Description = dataSet.Tables[0].Rows[i]["简单介绍"].ToString();
                            model.AddedDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["添加时间"]);
                            model.ImageUrl = dataSet.Tables[0].Rows[i]["清晰图"].ToString();
                            model.MarketPrice = new decimal?(Convert.ToDecimal(dataSet.Tables[0].Rows[i]["市场价"].ToString()));
                            model.LineId = 1;
                            int num7 = Convert.ToInt32(info2.Add(model));
                            if (num7 != 0)
                            {
                                Maticsoft.Model.Shop.Products.ProductStationMode mode = new Maticsoft.Model.Shop.Products.ProductStationMode();
                                Maticsoft.BLL.Shop.Products.ProductStationMode mode2 = new Maticsoft.BLL.Shop.Products.ProductStationMode();
                                int num8 = 0;
                                for (int j = 1; j <= 4; j++)
                                {
                                    CheckBox box = (CheckBox) this.divCheckBox.FindControl("CheckBox" + j);
                                    if (box.Checked)
                                    {
                                        mode.ProductId = num7;
                                        mode.DisplaySequence = mode2.GetRecordCount(" ProductId = " + num7) + 1;
                                        mode.Type = Convert.ToInt32(box.SkinID);
                                        if (mode2.Add(mode) != 0)
                                        {
                                            num8 = 1;
                                        }
                                    }
                                }
                                if (num8 == 1)
                                {
                                    num8 = 0;
                                    num5++;
                                }
                            }
                        }
                        if (num5 > 0)
                        {
                            MessageBox.ShowAndBack(this, "已成功导入 " + num5.ToString() + " 个商品！");
                        }
                        else
                        {
                            MessageBox.ShowFailTip(this, "导入失败！");
                        }
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, "请选择上传文件！");
                }
            }
            catch (Exception exception)
            {
                MessageBox.ShowFailTip(this, exception.Message);
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
                return 470;
            }
        }
    }
}

