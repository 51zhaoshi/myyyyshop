namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.PromoteSales;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.PromoteSales;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class AddCountDown : PageBaseAdmin
    {
        protected Button btnSave;
        private CategoryInfo categoryInfoBll = new CategoryInfo();
        protected CheckBox chkStatus;
        protected DropDownList ddlCateList;
        protected DropDownList ddlCateList2;
        protected DropDownList ddlProduct;
        private Maticsoft.BLL.Shop.PromoteSales.CountDown downBll = new Maticsoft.BLL.Shop.PromoteSales.CountDown();
        protected Literal Literal1;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal9;
        private ProductInfo productInfoBll = new ProductInfo();
        protected TextBox txtDesc;
        protected TextBox txtEndDate;
        protected TextBox txtPrice;
        protected TextBox txtSequence;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown model = new Maticsoft.Model.Shop.PromoteSales.CountDown();
            long productId = Globals.SafeLong(this.ddlProduct.SelectedValue, (long) 0L);
            if (productId == 0L)
            {
                MessageBox.ShowFailTip(this, "请选择限时抢购商品！");
            }
            else
            {
                decimal target = Globals.SafeDecimal(this.txtPrice.Text, (decimal) -1M);
                if (target == -1M)
                {
                    MessageBox.ShowFailTip(this, "请填写商品价格");
                }
                else if (string.IsNullOrWhiteSpace(this.txtEndDate.Text))
                {
                    MessageBox.ShowFailTip(this, "请选择活动结束时间");
                }
                else if (this.downBll.IsExists(productId))
                {
                    MessageBox.ShowFailTip(this, "该商品已加入限时抢购活动");
                }
                else
                {
                    model.Description = this.txtDesc.Text;
                    model.EndDate = Globals.SafeDateTime(this.txtEndDate.Text, DateTime.Now);
                    model.Price = Globals.SafeDecimal(target, (decimal) 0M);
                    model.ProductId = productId;
                    model.Sequence = Globals.SafeInt(this.txtSequence.Text, 0);
                    model.Status = this.chkStatus.Checked ? 1 : 0;
                    if (this.downBll.Add(model) > 0)
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功", "CountDownList.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败");
                    }
                }
            }
        }

        protected void ddlCateList_Changed(object sender, EventArgs e)
        {
            int cid = Globals.SafeInt(this.ddlCateList.SelectedValue, 0);
            if (cid == 0)
            {
                this.ddlCateList2.Visible = false;
            }
            else
            {
                this.ddlCateList2.DataSource = this.categoryInfoBll.GetList("ParentCategoryId=" + cid);
                this.ddlCateList2.DataTextField = "Name";
                this.ddlCateList2.DataValueField = "CategoryId";
                this.ddlCateList2.DataBind();
                this.ddlCateList2.Items.Insert(0, new ListItem("请选择", "0"));
                this.ddlCateList2.Visible = true;
                this.ddlProduct.DataSource = this.productInfoBll.GetProductsByCid(cid);
                this.ddlProduct.DataTextField = "ProductName";
                this.ddlProduct.DataValueField = "ProductId";
                this.ddlProduct.DataBind();
            }
        }

        protected void ddlCateList2_Changed(object sender, EventArgs e)
        {
            int cid = Globals.SafeInt(this.ddlCateList2.SelectedValue, 0);
            if (cid == 0)
            {
                cid = Globals.SafeInt(this.ddlCateList.SelectedValue, 0);
            }
            this.ddlProduct.DataSource = this.productInfoBll.GetProductsByCid(cid);
            this.ddlProduct.DataTextField = "ProductName";
            this.ddlProduct.DataValueField = "ProductId";
            this.ddlProduct.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ddlCateList.DataSource = this.categoryInfoBll.GetList("Depth=1");
                this.ddlCateList.DataTextField = "Name";
                this.ddlCateList.DataValueField = "CategoryId";
                this.ddlCateList.DataBind();
                this.ddlCateList.Items.Insert(0, new ListItem("请选择", "0"));
                this.txtSequence.Text = (this.downBll.MaxSequence() + 1).ToString();
            }
        }
    }
}

