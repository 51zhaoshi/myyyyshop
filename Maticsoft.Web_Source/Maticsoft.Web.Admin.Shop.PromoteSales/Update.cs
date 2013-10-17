namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.PromoteSales;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.PromoteSales;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Update : PageBaseAdmin
    {
        protected Button btnSave;
        protected CheckBox chkStatus;
        private Maticsoft.BLL.Shop.PromoteSales.CountDown downBll = new Maticsoft.BLL.Shop.PromoteSales.CountDown();
        protected Label lblProductName;
        protected Literal Literal1;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal2;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal9;
        private ProductInfo productInfoBll = new ProductInfo();
        protected TextBox txtDesc;
        protected TextBox txtEndDate;
        protected TextBox txtPrice;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown model = this.downBll.GetModel(this.DownId);
            decimal target = Globals.SafeDecimal(this.txtPrice.Text, (decimal) -1M);
            if (target == -1M)
            {
                MessageBox.ShowFailTip(this, "请填写商品价格");
            }
            else if (string.IsNullOrWhiteSpace(this.txtEndDate.Text))
            {
                MessageBox.ShowFailTip(this, "请选择活动结束时间");
            }
            else
            {
                model.Description = this.txtDesc.Text;
                model.EndDate = Globals.SafeDateTime(this.txtEndDate.Text, DateTime.Now);
                model.Price = Globals.SafeDecimal(target, (decimal) 0M);
                model.Status = this.chkStatus.Checked ? 1 : 0;
                if (this.downBll.Update(model))
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.PromoteSales.CountDown model = this.downBll.GetModel(this.DownId);
            if (model != null)
            {
                this.txtDesc.Text = model.Description;
                this.txtPrice.Text = model.Price.ToString("F");
                this.txtEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
                this.lblProductName.Text = this.productInfoBll.GetProductName(model.ProductId);
                this.chkStatus.Checked = model.Status == 1;
            }
        }

        public int DownId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

