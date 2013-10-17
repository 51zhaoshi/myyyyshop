namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ProductsInStock : PageBaseAdmin
    {
        protected int Act_DelData = 0x1e4;
        protected int Act_UpdateData = 0x1e3;
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Button btnCheck;
        protected Button btnDelete;
        protected Button btnInverseApprove;
        protected Button btnInverseApprove2;
        protected Button btnSearch;
        protected Button Button1;
        protected DropDownList drpProductCategory;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Products.CategoryInfo manage = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductCategories productCategory = new Maticsoft.BLL.Shop.Products.ProductCategories();
        public string strTitle = string.Empty;
        protected TextBox txtKeyword;
        protected TextBox txtProductNum;

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
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            switch (this.SaleStatus)
            {
                case -1:
                    this.btnCheck.Visible = true;
                    this.strTitle = "您可以对未审核中的商品进行编辑、删除和审核操作";
                    this.btnInverseApprove.Visible = false;
                    this.btnInverseApprove2.Visible = false;
                    break;

                case 0:
                    this.strTitle = "您可以对仓库中的商品进行删除和上架功能";
                    this.btnInverseApprove.Text = "批量上架";
                    this.btnInverseApprove2.Text = "批量上架";
                    break;

                case 1:
                    this.strTitle = "您可以对出售中的商品进行编辑、删除和下架操作";
                    break;
            }
            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo {
                SaleStatus = this.SaleStatus
            };
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text.TrimEnd(new char[0])))
            {
                model.ProductName = InjectionFilter.SqlFilter(this.txtKeyword.Text);
            }
            if (!string.IsNullOrWhiteSpace(this.drpProductCategory.SelectedValue))
            {
                model.CategoryId = Globals.SafeInt(this.drpProductCategory.SelectedValue, 0);
            }
            if (!string.IsNullOrWhiteSpace(this.txtProductNum.Text))
            {
                model.ProductCode = InjectionFilter.SqlFilter(this.txtProductNum.Text);
            }
            this.gridView.DataSetSource = this.bll.GetProductInfo(model);
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.UpdateList(selIDlist, ProductSaleStatus.OnSale);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.GetSelIDlist()))
            {
                string selIDlist = this.GetSelIDlist();
                if (selIDlist.Trim().Length == 0)
                {
                    return;
                }
                this.bll.UpdateList(selIDlist, ProductSaleStatus.Deleted);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
            this.gridView.OnBind();
        }

        protected void btnFresh_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                ProductSaleStatus inStock;
                if (this.SaleStatus == 1)
                {
                    inStock = ProductSaleStatus.InStock;
                }
                else
                {
                    inStock = ProductSaleStatus.OnSale;
                }
                this.bll.UpdateList(selIDlist, inStock);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DoCallback()
        {
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "UpdateProductName"))
                {
                    if (str3 == "UpdateStockNum")
                    {
                        s = this.UpdateStockNum();
                    }
                    else if (str3 == "UpdateMarketPrice")
                    {
                        s = this.UpdateMarketPrice();
                    }
                    else if (str3 == "UpdateLowestSalePrice")
                    {
                        s = this.UpdateLowestSalePrice();
                    }
                }
                else
                {
                    s = this.UpdateProductName();
                }
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal literal = (Literal) e.Row.FindControl("litProductCate");
                object obj2 = DataBinder.Eval(e.Row.DataItem, "ProductId");
                if (obj2 != null)
                {
                    literal.Text = this.ProductCategories(Globals.SafeLong(obj2.ToString(), (long) 0L));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.BindCategories();
            }
        }

        private string ProductCategories(long productId)
        {
            List<Maticsoft.Model.Shop.Products.ProductCategories> modelList = this.productCategory.GetModelList(productId);
            StringBuilder builder = new StringBuilder();
            if ((modelList != null) && (modelList.Count > 0))
            {
                foreach (Maticsoft.Model.Shop.Products.ProductCategories categories in modelList)
                {
                    builder.Append(this.manage.GetFullNameByCache(categories.CategoryId));
                    builder.Append("</br>");
                }
            }
            return builder.ToString();
        }

        protected int StockNum(object obj)
        {
            if ((obj != null) && !string.IsNullOrWhiteSpace(obj.ToString()))
            {
                long productId = Globals.SafeLong(obj.ToString(), (long) 0L);
                return this.bll.StockNum(productId);
            }
            return 0;
        }

        private string UpdateLowestSalePrice()
        {
            JsonObject obj2 = new JsonObject();
            long productId = Globals.SafeLong(base.Request.Form["ProductId"], (long) 0L);
            decimal price = Globals.SafeDecimal(base.Request.Params["UpdateValue"], (decimal) 0M);
            if (price == 0M)
            {
                obj2.Put("STATUS", "FAILED");
            }
            else if (this.bll.UpdateLowestSalePrice(productId, price))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        private string UpdateMarketPrice()
        {
            JsonObject obj2 = new JsonObject();
            long productId = Globals.SafeLong(base.Request.Form["ProductId"], (long) 0L);
            decimal price = Globals.SafeDecimal(base.Request.Params["UpdateValue"], (decimal) 0M);
            if (price == 0M)
            {
                obj2.Put("STATUS", "FAILED");
            }
            else if (this.bll.UpdateMarketPrice(productId, price))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        private string UpdateProductName()
        {
            JsonObject obj2 = new JsonObject();
            long productId = Globals.SafeLong(base.Request.Form["ProductId"], (long) 0L);
            string str = base.Request.Params["UpdateValue"];
            if (string.IsNullOrWhiteSpace(str))
            {
                obj2.Put("STATUS", "FAILED");
            }
            else if (this.bll.UpdateProductName(productId, str))
            {
                obj2.Put("STATUS", "SUCCESS");
            }
            else
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        private string UpdateStockNum()
        {
            JsonObject obj2 = new JsonObject();
            Globals.SafeLong(base.Request.Form["ProductId"], (long) 0L);
            if (Globals.SafeInt(base.Request.Params["UpdateValue"], 0) == 0)
            {
                obj2.Put("STATUS", "FAILED");
            }
            return obj2.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1e2;
            }
        }

        protected int SaleStatus
        {
            get
            {
                int num = -1;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["SaleStatus"]))
                {
                    num = Globals.SafeInt(base.Request.Params["SaleStatus"], -1);
                }
                return num;
            }
        }
    }
}

