namespace Maticsoft.Web.Supplier.Order
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderShip : PageBaseSupplier
    {
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
        protected Button btnSave;
        protected DropDownList ddlShipType;
        protected GridViewEx gridView;
        protected HiddenField hfSuccess;
        protected HiddenField hfWeight;
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
        protected Label lblBuyerCellPhone;
        protected Label lblBuyerEmail;
        protected Label lblBuyerName;
        protected Label lblCreatedDate;
        protected Label lblOrderCode;
        protected Label lblShipEmail;
        protected Label lblShipZipCode;
        protected Literal lblTitle;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal123;
        protected Literal Literal14;
        protected Literal Literal16;
        protected Literal Literal18;
        protected Literal Literal2;
        protected Literal Literal21;
        protected Literal Literal22;
        protected Literal Literal23;
        protected Literal Literal24;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal9;
        private Orders orderBll = new Orders();
        private Regions regionBll = new Regions();
        protected Region RegionList;
        protected ScriptManager ScriptManager1;
        private SKUInfo skuBll = new SKUInfo();
        protected TextBox txtFreightActual;
        protected TextBox txtFreightAdjusted;
        protected TextBox txtRemark;
        protected TextBox txtShipAddress;
        protected TextBox txtShipCellPhone;
        protected TextBox txtShipName;
        protected TextBox txtShipOrderNumber;
        protected TextBox txtShipTelPhone;
        private Maticsoft.BLL.Shop.Shipping.ShippingType typeBll = new Maticsoft.BLL.Shop.Shipping.ShippingType();
        protected UpdatePanel UpdatePanel1;

        public void BindData()
        {
            this.gridView.DataSetSource = this.itemBll.GetListByCache(" OrderId=" + this.OrderId);
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            OrderInfo model = this.orderBll.GetModel((long) this.OrderId);
            int modeId = Globals.SafeInt(this.ddlShipType.SelectedValue, 0);
            Maticsoft.Model.Shop.Shipping.ShippingType modelByCache = this.typeBll.GetModelByCache(modeId);
            model.ExpressCompanyName = modelByCache.ExpressCompanyName;
            model.ExpressCompanyAbb = modelByCache.ExpressCompanyEn;
            model.ShippingModeId = new int?(modelByCache.ModeId);
            model.ShippingModeName = modelByCache.Name;
            model.RealShippingModeName = modelByCache.Name;
            model.ShipOrderNumber = this.txtShipOrderNumber.Text;
            model.FreightAdjusted = new decimal?(Globals.SafeDecimal(this.txtFreightAdjusted.Text, (decimal) 0M));
            model.FreightActual = new decimal?(Globals.SafeDecimal(this.txtFreightActual.Text, (decimal) 0M));
            int num2 = this.RegionList.Region_iID;
            model.RegionId = new int?(num2);
            model.ShipRegion = this.regionBll.GetRegionNameByRID(num2);
            model.ShipName = this.txtShipName.Text;
            model.ShipAddress = this.txtShipAddress.Text;
            model.ShipTelPhone = this.txtShipTelPhone.Text;
            model.ShipCellPhone = this.txtShipCellPhone.Text;
            model.ShippingStatus = 2;
            model.OrderStatus = 1;
            if (this.orderBll.UpdateShipped(model))
            {
                Maticsoft.Model.Shop.Order.OrderAction action = new Maticsoft.Model.Shop.Order.OrderAction {
                    ActionCode = "104",
                    ActionDate = DateTime.Now,
                    OrderCode = model.OrderCode,
                    OrderId = model.OrderId,
                    Remark = "供应商发货操作",
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

        protected int GetStock(object obj_sku, object obj_Id)
        {
            if ((obj_sku != null) && !string.IsNullOrWhiteSpace(obj_sku.ToString()))
            {
                return this.skuBll.GetStockBySKU(obj_sku.ToString());
            }
            if ((obj_Id != null) && !string.IsNullOrWhiteSpace(obj_Id.ToString()))
            {
                long productId = Globals.SafeLong(obj_Id.ToString(), (long) 0L);
                return this.skuBll.GetStockById(productId);
            }
            return 0;
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

        protected void ShipType_Changed(object sender, EventArgs e)
        {
            int modeId = Globals.SafeInt(this.ddlShipType.SelectedValue, 0);
            Maticsoft.Model.Shop.Shipping.ShippingType modelByCache = this.typeBll.GetModelByCache(modeId);
            if (modelByCache != null)
            {
                this.txtFreightActual.Text = this.typeBll.GetFreight(modelByCache, Globals.SafeInt(this.hfWeight.Value, 0)).ToString("F");
                this.txtFreightAdjusted.Text = this.txtFreightActual.Text;
            }
            else
            {
                this.txtFreightActual.Text = "0.00";
                this.txtFreightAdjusted.Text = "0.00";
            }
        }

        private void ShowInfo()
        {
            OrderInfo model = this.orderBll.GetModel((long) this.OrderId);
            if (model != null)
            {
                this.lblOrderCode.Text = model.OrderCode;
                this.lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtShipName.Text = model.ShipName;
                this.lblShipZipCode.Text = model.ShipZipCode;
                this.txtShipAddress.Text = model.ShipAddress;
                this.txtShipCellPhone.Text = model.ShipCellPhone;
                this.lblShipEmail.Text = model.ShipEmail;
                this.RegionList.Region_iID = model.RegionId.HasValue ? model.RegionId.Value : 0;
                this.txtShipTelPhone.Text = model.ShipTelPhone;
                this.lblBuyerEmail.Text = model.BuyerEmail;
                this.lblBuyerCellPhone.Text = model.BuyerCellPhone;
                this.lblBuyerName.Text = model.BuyerName;
                this.txtShipOrderNumber.Text = model.ShipOrderNumber;
                this.hfWeight.Value = model.Weight.ToString();
                this.txtFreightActual.Text = model.FreightActual.HasValue ? model.FreightActual.Value.ToString("F") : "0.00";
                this.txtFreightAdjusted.Text = model.FreightAdjusted.HasValue ? model.FreightAdjusted.Value.ToString("F") : "0.00";
                this.ddlShipType.DataSource = this.typeBll.GetList("");
                this.ddlShipType.DataTextField = "Name";
                this.ddlShipType.DataValueField = "ModeId";
                this.ddlShipType.DataBind();
                this.ddlShipType.Items.Insert(0, new ListItem("请选择配送方式", "0"));
                this.ddlShipType.SelectedValue = model.ShippingModeId.HasValue ? model.ShippingModeId.Value.ToString() : "0";
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

