namespace Maticsoft.Web.Admin.Shop.Order
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrdersList : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x1bc;
        public Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
        protected Button btnSearch;
        protected DropDownList dropPaymentStatus;
        protected DropDownList dropShippingStatus;
        protected GridViewEx gridView;
        protected HiddenField hfCancel;
        protected HiddenField hfHandling;
        protected HiddenField hfLocking;
        protected HiddenField hfPaying;
        protected HiddenField hfPreConfirm;
        protected HiddenField hfPreHandle;
        protected HiddenField hfShiped;
        protected HiddenField hfShipping;
        protected HiddenField hfSuccess;
        public Maticsoft.BLL.Shop.Order.OrdersHistory historyBll = new Maticsoft.BLL.Shop.Order.OrdersHistory();
        protected Literal Literal1;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected Literal LiteralBuyerName;
        protected Literal LiteralCreatedDate;
        protected Literal LiteralPaymentStatus;
        protected Literal LiteralShipName;
        protected Literal LiteralShippingStatus;
        public Orders orderBll = new Orders();
        protected TextBox txtBuyerName;
        protected TextBox txtCreatedDateEnd;
        protected TextBox txtCreatedDateStart;
        protected TextBox txtOrderCode;
        protected TextBox txtShipName;
        public int Type;

        public void BindData()
        {
            StringBuilder builder = new StringBuilder();
            if (this.OrderStatus > 0)
            {
                builder.Append(this.orderBll.GetWhereByStatus(this.OrderStatus));
            }
            if (!string.IsNullOrWhiteSpace(this.txtOrderCode.Text.Trim()))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.QuoteFilter(this.txtOrderCode.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.txtShipName.Text.Trim()))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.QuoteFilter(this.txtShipName.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.txtBuyerName.Text.Trim()))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.QuoteFilter(this.txtBuyerName.Text.Trim()));
            }
            if (this.dropPaymentStatus.SelectedValue != "-1")
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" PaymentStatus = {0}", this.dropPaymentStatus.SelectedValue);
            }
            if (this.dropShippingStatus.SelectedValue != "-1")
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" ShippingStatus = {0}", this.dropShippingStatus.SelectedValue);
            }
            if (PageValidate.IsDateTime(this.txtCreatedDateEnd.Text.Trim()) && PageValidate.IsDateTime(this.txtCreatedDateStart.Text.Trim()))
            {
                if (builder.Length > 1)
                {
                    builder.Append(" and ");
                }
                builder.AppendFormat(" CreatedDate between  '{0}' and  '{1}' ", InjectionFilter.QuoteFilter(this.txtCreatedDateStart.Text.Trim()), InjectionFilter.QuoteFilter(this.txtCreatedDateEnd.Text.Trim()));
            }
            if (builder.Length > 1)
            {
                builder.Append(" and ");
            }
            builder.AppendFormat(" ((OrderType = 1 AND HasChildren = 0) OR (OrderType = 2 AND PaymentStatus > 1) OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2))", new object[0]);
            if (this.OrderStatus == -1)
            {
                this.gridView.DataSetSource = this.historyBll.GetList(-1, builder.ToString(), "CreatedDate desc");
            }
            else
            {
                this.gridView.DataSetSource = this.orderBll.GetList(-1, builder.ToString(), "CreatedDate desc");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected string GetOrderStatus(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (target.ToString())
            {
                case "-4":
                    return "系统锁定";

                case "-3":
                    return "后台锁定";

                case "-2":
                    return "用户锁定";

                case "-1":
                    return "死单（取消）";

                case "0":
                    return "未处理";

                case "1":
                    return "进行中";

                case "2":
                    return "完成";
            }
            return "未知状态";
        }

        protected string GetOrderType(object paymentGateway_obj, object orderStatus_obj, object paymentStatus_obj, object shippingStatus_obj)
        {
            string str = string.Empty;
            if ((StringPlus.IsNullOrEmpty(paymentGateway_obj) || StringPlus.IsNullOrEmpty(orderStatus_obj)) || (StringPlus.IsNullOrEmpty(paymentStatus_obj) || StringPlus.IsNullOrEmpty(shippingStatus_obj)))
            {
                return str;
            }
            switch (this.orderBll.GetOrderType(paymentGateway_obj.ToString(), Globals.SafeInt(orderStatus_obj.ToString(), 0), Globals.SafeInt(paymentStatus_obj.ToString(), 0), Globals.SafeInt(shippingStatus_obj.ToString(), 0)))
            {
                case EnumHelper.OrderMainStatus.Paying:
                    return "等待付款";

                case EnumHelper.OrderMainStatus.PreHandle:
                    return "等待处理";

                case EnumHelper.OrderMainStatus.Cancel:
                    return "取消订单";

                case EnumHelper.OrderMainStatus.Locking:
                    return "订单锁定";

                case EnumHelper.OrderMainStatus.PreConfirm:
                    return "等待付款确认";

                case EnumHelper.OrderMainStatus.Handling:
                    return "正在处理";

                case EnumHelper.OrderMainStatus.Shipping:
                    return "配货中";

                case EnumHelper.OrderMainStatus.Shiped:
                    return "已发货";

                case EnumHelper.OrderMainStatus.Complete:
                    return "已完成";
            }
            return "未知状态";
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

        protected string GetShippingStatus(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (target.ToString())
            {
                case "0":
                    return "未发货";

                case "1":
                    return "打包中";

                case "2":
                    return "已发货";

                case "3":
                    return "已确认收货";

                case "4":
                    return "拒收退货中";

                case "5":
                    return "拒收已退货";
            }
            return "未知状态";
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelOrder")
            {
                long orderId = Globals.SafeLong(e.CommandArgument.ToString().Split(new char[] { ',' })[0].ToString(), (long) 0L);
                if (OrderManage.CancelOrder(this.orderBll.GetModel(orderId), base.CurrentUser))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            if (e.CommandName == "Success")
            {
                object[] objArray2 = e.CommandArgument.ToString().Split(new char[] { ',' });
                long num2 = Globals.SafeLong(objArray2[0].ToString(), (long) 0L);
                string str = objArray2[1].ToString();
                if (this.orderBll.SetOrderSuccess(num2))
                {
                    Maticsoft.Model.Shop.Order.OrderAction model = new Maticsoft.Model.Shop.Order.OrderAction {
                        ActionCode = "105",
                        ActionDate = DateTime.Now,
                        OrderCode = str,
                        OrderId = num2,
                        Remark = "系统完成订单",
                        UserId = base.CurrentUser.UserID,
                        Username = base.CurrentUser.NickName
                    };
                    this.actionBll.Add(model);
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            if (e.CommandName == "Pay")
            {
                object[] objArray3 = e.CommandArgument.ToString().Split(new char[] { ',' });
                long num3 = Globals.SafeLong(objArray3[0].ToString(), (long) 0L);
                objArray3[1].ToString();
                OrderInfo orderInfo = this.orderBll.GetModel(num3);
                if ((orderInfo != null) && OrderManage.PayForOrder(orderInfo, base.CurrentUser))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkComplete");
                    button.Visible = false;
                    LinkButton button2 = (LinkButton) e.Row.FindControl("linkCancel");
                    button2.Visible = false;
                    LinkButton button3 = (LinkButton) e.Row.FindControl("linkReturn");
                    button3.Visible = false;
                }
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.orderBll.Delete((long) ((int) this.gridView.DataKeys[e.RowIndex].Value)))
            {
                this.gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.Type = this.OrderStatus;
                if (this.Session["Style"] != null)
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if (base.Application[str] != null)
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.hfPaying.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Paying");
                this.hfPreHandle.Value = ConfigSystem.GetValueByCache("Shop_OrderList_PreHandle");
                this.hfCancel.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Cancel");
                this.hfLocking.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Locking");
                this.hfPreConfirm.Value = ConfigSystem.GetValueByCache("Shop_OrderList_PreConfirm");
                this.hfHandling.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Handling");
                this.hfShipping.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Shipping");
                this.hfShiped.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Shiped");
                this.hfSuccess.Value = ConfigSystem.GetValueByCache("Shop_OrderList_Complete");
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1bb;
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

