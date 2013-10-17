namespace Maticsoft.Web.Supplier.Order
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderShow : PageBaseSupplier
    {
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
        protected Button btnSave;
        protected Button Button1;
        protected GridViewEx gridView;
        protected GridViewEx gridView_Action;
        protected GridViewEx gridView_Remark;
        protected HiddenField hfOrderMainStatus;
        private Maticsoft.BLL.Shop.Order.OrdersHistory historyBll = new Maticsoft.BLL.Shop.Order.OrdersHistory();
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
        protected Label Label3;
        protected Label lblAmount;
        protected Label lblBuyerCellPhone;
        protected Label lblBuyerEmail;
        protected Label lblBuyerName;
        protected Label lblCouponAmount;
        protected Label lblDiscountAdjusted;
        protected Label lblExpressCompanyName;
        protected Label lblFreightAdjusted;
        protected Label lblOrderTotal;
        protected Label lblPaymentTypeName;
        protected Label lblPoint;
        protected Label lblRealShippingModeName;
        protected Label lblShipEmail;
        protected Label lblShipOrderNumber;
        protected Literal lblTitle;
        protected Label lblWeight;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal115;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal19;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal22;
        protected Literal Literal24;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        private Orders orderBll = new Orders();
        private Regions regionBll = new Regions();
        protected Region RegionList;
        private Maticsoft.BLL.Shop.Order.OrderRemark remarkBll = new Maticsoft.BLL.Shop.Order.OrderRemark();
        protected ScriptManager ScriptManager1;
        protected TextBox txtRemark;
        protected TextBox txtShipAddress;
        protected TextBox txtShipCellPhone;
        protected TextBox txtShipName;
        protected TextBox txtShipTelPhone;
        protected TextBox txtShipZipCode;
        protected UpdatePanel UpdatePanel1;

        public void BindAction()
        {
            this.gridView_Action.DataSetSource = this.actionBll.GetList(" OrderId=" + this.OrderId);
        }

        public void BindData()
        {
            this.gridView.DataSetSource = this.itemBll.GetListByCache(" OrderId=" + this.OrderId);
        }

        public void BindRemark()
        {
            this.gridView_Remark.DataSetSource = this.remarkBll.GetList(" OrderId=" + this.OrderId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.OrderStatus == -1)
            {
                Maticsoft.Model.Shop.Order.OrdersHistory modelByCache = this.historyBll.GetModelByCache((long) this.OrderId);
                if (modelByCache != null)
                {
                    modelByCache.Remark = this.txtRemark.Text;
                    if (this.historyBll.Update(modelByCache))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                }
            }
            else
            {
                OrderInfo model = this.orderBll.GetModelByCache((long) this.OrderId);
                if (model != null)
                {
                    model.Remark = this.txtRemark.Text;
                    if (this.orderBll.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            OrderInfo model = this.orderBll.GetModel((long) this.OrderId);
            if (model != null)
            {
                int num = this.RegionList.Region_iID;
                model.RegionId = new int?(num);
                model.ShipRegion = this.regionBll.GetRegionNameByRID(num);
                model.ShipName = this.txtShipName.Text;
                model.ShipAddress = this.txtShipAddress.Text;
                model.ShipTelPhone = this.txtShipTelPhone.Text;
                model.ShipCellPhone = this.txtShipCellPhone.Text;
                model.ShipZipCode = this.txtShipZipCode.Text;
                if (this.orderBll.Update(model))
                {
                    Maticsoft.Model.Shop.Order.OrderAction action = new Maticsoft.Model.Shop.Order.OrderAction {
                        ActionCode = "106",
                        ActionDate = DateTime.Now,
                        OrderCode = model.OrderCode,
                        OrderId = model.OrderId,
                        Remark = "修改收货信息",
                        UserId = base.CurrentUser.UserID,
                        Username = base.CurrentUser.NickName
                    };
                    this.actionBll.Add(action);
                    this.orderBll.RemoveModelInfoCache(model.OrderId);
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
            }
        }

        protected void gridView_Action_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView_Action.PageIndex = e.NewPageIndex;
            this.gridView_Action.OnBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_Remark_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView_Remark.PageIndex = e.NewPageIndex;
            this.gridView_Remark.OnBind();
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
            if (this.OrderStatus == -1)
            {
                Maticsoft.Model.Shop.Order.OrdersHistory modelByCache = this.historyBll.GetModelByCache((long) this.OrderId);
                if (modelByCache != null)
                {
                    this.lblTitle.Text = "你正在查看订单【" + modelByCache.OrderCode + "】的详细信息";
                    this.txtShipName.Text = modelByCache.ShipName;
                    this.txtShipZipCode.Text = modelByCache.ShipZipCode;
                    this.txtShipAddress.Text = modelByCache.ShipAddress;
                    this.txtShipCellPhone.Text = modelByCache.ShipCellPhone;
                    this.lblShipEmail.Text = modelByCache.ShipEmail;
                    this.RegionList.Region_iID = modelByCache.RegionId.HasValue ? modelByCache.RegionId.Value : 0;
                    this.txtShipTelPhone.Text = modelByCache.ShipTelPhone;
                    this.lblBuyerEmail.Text = modelByCache.BuyerEmail;
                    this.lblBuyerCellPhone.Text = modelByCache.BuyerCellPhone;
                    this.lblBuyerName.Text = modelByCache.BuyerName;
                    this.lblDiscountAdjusted.Text = modelByCache.DiscountAdjusted.HasValue ? modelByCache.DiscountAdjusted.Value.ToString("F") : "0";
                    this.lblFreightAdjusted.Text = modelByCache.FreightAdjusted.HasValue ? modelByCache.FreightAdjusted.Value.ToString("F") : "0";
                    this.lblOrderTotal.Text = modelByCache.OrderTotal.ToString("F");
                    this.lblAmount.Text = modelByCache.Amount.ToString("F");
                    this.lblCouponAmount.Text = (modelByCache.OrderTotal - modelByCache.Amount).ToString("F");
                    this.lblPaymentTypeName.Text = modelByCache.PaymentTypeName;
                    this.lblRealShippingModeName.Text = modelByCache.RealShippingModeName;
                    this.lblPoint.Text = modelByCache.OrderPoint.ToString();
                    this.lblExpressCompanyName.Text = modelByCache.ExpressCompanyName;
                    this.lblWeight.Text = modelByCache.Weight.ToString();
                    this.lblShipOrderNumber.Text = string.IsNullOrWhiteSpace(modelByCache.ShipOrderNumber) ? "无" : modelByCache.ShipOrderNumber;
                    this.txtRemark.Text = modelByCache.Remark;
                    this.hfOrderMainStatus.Value = "9";
                }
            }
            else
            {
                OrderInfo info = this.orderBll.GetModelByCache((long) this.OrderId);
                if (info != null)
                {
                    this.lblTitle.Text = "正在查看订单【" + info.OrderCode + "】的详细信息";
                    this.txtShipName.Text = info.ShipName;
                    this.txtShipZipCode.Text = info.ShipZipCode;
                    this.txtShipAddress.Text = info.ShipAddress;
                    this.txtShipCellPhone.Text = info.ShipCellPhone;
                    this.lblShipEmail.Text = info.ShipEmail;
                    this.RegionList.Region_iID = info.RegionId.HasValue ? info.RegionId.Value : 0;
                    this.txtShipTelPhone.Text = info.ShipTelPhone;
                    this.lblBuyerEmail.Text = info.BuyerEmail;
                    this.lblBuyerCellPhone.Text = info.BuyerCellPhone;
                    this.lblBuyerName.Text = info.BuyerName;
                    this.lblDiscountAdjusted.Text = info.DiscountAdjusted.HasValue ? info.DiscountAdjusted.Value.ToString("F") : "0";
                    this.lblFreightAdjusted.Text = info.FreightAdjusted.HasValue ? info.FreightAdjusted.Value.ToString("F") : "0";
                    this.lblOrderTotal.Text = info.OrderTotal.ToString("F");
                    this.lblAmount.Text = info.Amount.ToString("F");
                    this.lblCouponAmount.Text = (info.OrderTotal - info.Amount).ToString("F");
                    this.lblPaymentTypeName.Text = info.PaymentTypeName;
                    this.lblRealShippingModeName.Text = info.RealShippingModeName;
                    this.lblPoint.Text = info.OrderPoint.ToString();
                    this.lblExpressCompanyName.Text = info.ExpressCompanyName;
                    this.lblWeight.Text = info.Weight.ToString();
                    this.lblShipOrderNumber.Text = string.IsNullOrWhiteSpace(info.ShipOrderNumber) ? "无" : info.ShipOrderNumber;
                    this.txtRemark.Text = info.Remark;
                    this.hfOrderMainStatus.Value = ((int) this.orderBll.GetOrderType(info.PaymentGateway, info.OrderStatus, info.PaymentStatus, info.ShippingStatus)).ToString();
                }
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

