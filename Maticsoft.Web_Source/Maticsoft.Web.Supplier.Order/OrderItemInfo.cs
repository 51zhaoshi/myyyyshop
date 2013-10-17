namespace Maticsoft.Web.Supplier.Order
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderItemInfo : PageBaseSupplier
    {
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
        protected Button btnSave;
        protected GridViewEx gridView;
        protected HiddenField hfSuccess;
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
        protected Label lblBuyerCellPhone;
        protected Label lblBuyerEmail;
        protected Label lblBuyerName;
        protected Label lblCreatedDate;
        protected Literal lblFreightActual;
        protected Label lblOrderCode;
        protected Literal lblShipTypeName;
        protected Literal lblTitle;
        protected Literal Literal1;
        protected Literal Literal18;
        protected Literal Literal2;
        protected Literal Literal21;
        protected Literal Literal22;
        protected Literal Literal23;
        protected Literal Literal24;
        private Orders orderBll = new Orders();
        protected ScriptManager ScriptManager1;

        public void BindData()
        {
            this.gridView.DataSetSource = this.itemBll.GetListByCache(" OrderId=" + this.OrderId);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            OrderInfo model = this.orderBll.GetModel((long) this.OrderId);
            model.ShippingStatus = 1;
            model.OrderStatus = 1;
            if (this.orderBll.Update(model))
            {
                Maticsoft.Model.Shop.Order.OrderAction action = new Maticsoft.Model.Shop.Order.OrderAction {
                    ActionCode = "103",
                    ActionDate = DateTime.Now,
                    OrderCode = model.OrderCode,
                    OrderId = model.OrderId,
                    Remark = "供应商配货操作",
                    UserId = base.CurrentUser.UserID,
                    Username = base.CurrentUser.NickName
                };
                this.actionBll.Add(action);
                this.orderBll.RemoveModelInfoCache(model.OrderId);
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
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
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            OrderInfo model = this.orderBll.GetModel((long) this.OrderId);
            if (model != null)
            {
                this.lblTitle.Text = "正在进行订单【" + model.OrderCode + "】配货操作";
                this.lblOrderCode.Text = model.OrderCode;
                this.lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblBuyerEmail.Text = model.BuyerEmail;
                this.lblBuyerCellPhone.Text = model.BuyerCellPhone;
                this.lblBuyerName.Text = model.BuyerName;
                this.lblShipTypeName.Text = model.ShippingModeName;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        public int OrderId
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["orderId"], 0);
            }
        }

        public int OrderStatus
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["type"], 0);
            }
        }
    }
}

