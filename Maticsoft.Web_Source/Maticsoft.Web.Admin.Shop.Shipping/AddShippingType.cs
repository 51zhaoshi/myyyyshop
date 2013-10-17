namespace Maticsoft.Web.Admin.Shop.Shipping
{
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class AddShippingType : PageBaseAdmin
    {
        protected Button Button1;
        protected Button Button2;
        protected CheckBoxList CheckBoxList2;
        protected CheckBoxList ckPayType;
        protected DropDownList ddlType;
        protected Label Label1;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal2;
        protected Literal Literal21;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal9;
        private Maticsoft.BLL.Shop.Shipping.ShippingPayment payBll = new Maticsoft.BLL.Shop.Shipping.ShippingPayment();
        protected RangeValidator RangeValidator1;
        protected RangeValidator RangeValidator2;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox tAddPrice;
        protected TextBox tAddWeight;
        protected TextBox tDesc;
        protected TextBox tName;
        protected TextBox tPrice;
        protected TextBox tWeight;
        private Maticsoft.BLL.Shop.Shipping.ShippingType typeBll = new Maticsoft.BLL.Shop.Shipping.ShippingType();

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ShippingType.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Shipping.ShippingType model = new Maticsoft.Model.Shop.Shipping.ShippingType {
                AddPrice = new decimal?(Globals.SafeDecimal(this.tAddPrice.Text, (decimal) 0M)),
                Price = Globals.SafeDecimal(this.tPrice.Text, (decimal) 0M),
                Weight = Globals.SafeInt(this.tWeight.Text, 0),
                AddWeight = new int?(Globals.SafeInt(this.tAddWeight.Text, 0)),
                Description = this.tDesc.Text,
                DisplaySequence = -1,
                ExpressCompanyName = this.ddlType.SelectedItem.Text,
                ExpressCompanyEn = this.ddlType.SelectedValue,
                Name = this.tName.Text
            };
            int num = this.typeBll.Add(model);
            if (num > 0)
            {
                for (int i = 0; i < this.ckPayType.Items.Count; i++)
                {
                    if (this.ckPayType.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Shipping.ShippingPayment payment = new Maticsoft.Model.Shop.Shipping.ShippingPayment {
                            PaymentModeId = Globals.SafeInt(this.ckPayType.Items[i].Value, 0),
                            ShippingModeId = num
                        };
                        this.payBll.Add(payment);
                    }
                }
                base.Response.Redirect("ShippingType.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, "添加失败！请重试。");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ckPayType.DataSource = PaymentModeManage.GetPaymentModes();
                this.ckPayType.DataTextField = "Name";
                this.ckPayType.DataValueField = "ModeId";
                this.ckPayType.DataBind();
                for (int i = 0; i < this.ckPayType.Items.Count; i++)
                {
                    this.ckPayType.Items[i].Selected = true;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x209;
            }
        }
    }
}

