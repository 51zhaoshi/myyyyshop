namespace Maticsoft.Web.Admin.Shop.Products
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductsNoCategories : PageBaseAdmin
    {
        protected int Act_DelData = 0x1e8;
        protected int Act_UpdateData = 0x1e3;
        private Maticsoft.BLL.Shop.Products.ProductInfo bll = new Maticsoft.BLL.Shop.Products.ProductInfo();
        protected Button btnDelete;
        protected Button btnMove;
        protected Button btnSearch;
        protected DropDownList dropCategories;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[5].Visible = false;
            }
            Maticsoft.Model.Shop.Products.ProductInfo model = new Maticsoft.Model.Shop.Products.ProductInfo {
                SaleStatus = 2,
                CategoryId = -1
            };
            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text.Trim()))
            {
                model.ProductName = this.txtKeyword.Text.Trim();
            }
            this.gridView.DataSetSource = this.bll.GetListByCategoryIdSaleStatus(model);
            this.BindDropList();
        }

        private void BindDropList()
        {
            DataSet list = new Maticsoft.BLL.Shop.Products.CategoryInfo().GetList(" Depth = 1");
            if (!DataSetTools.DataSetIsNull(list))
            {
                this.dropCategories.DataSource = list;
                this.dropCategories.DataTextField = "Name";
                this.dropCategories.DataValueField = "CategoryId";
                this.dropCategories.DataBind();
            }
            this.dropCategories.Items.Insert(0, new ListItem(Site.PleaseSelect, "0"));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.UpdateList(selIDlist, ProductSaleStatus.Deleted);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (!string.IsNullOrWhiteSpace(selIDlist))
            {
                int categoryId = Globals.SafeInt(this.dropCategories.SelectedValue, 0);
                if (categoryId == 0)
                {
                    return;
                }
                if (this.bll.ChangeProductsCategory(selIDlist, categoryId))
                {
                    MessageBox.ShowSuccessTip(this, "修改成功！");
                }
            }
            this.gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
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
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
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
            }
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

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1e7;
            }
        }
    }
}

