namespace Maticsoft.Web.Admin.Shop.Shipping
{
    using Maticsoft.BLL.Shop.Shipping;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Shipping;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    public class UpdateShipType : PageBaseAdmin
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

        public void BindData()
        {
            Maticsoft.Model.Shop.Shipping.ShippingType model = this.typeBll.GetModel(this.ModeId);
            if (model != null)
            {
                this.tAddPrice.Text = model.AddPrice.ToString();
                this.tAddWeight.Text = model.AddWeight.ToString();
                this.tDesc.Text = model.Description;
                this.tPrice.Text = model.Price.ToString();
                this.tWeight.Text = model.Weight.ToString();
                this.tAddWeight.Text = model.AddWeight.ToString();
                this.ddlType.SelectedValue = model.ExpressCompanyEn;
                this.tName.Text = model.Name;
            }
            List<Maticsoft.Model.Shop.Shipping.ShippingPayment> modelList = this.payBll.GetModelList(" ShippingModeId=" + this.ModeId);
            if ((modelList != null) && (modelList.Count > 0))
            {
                for (int i = 0; i < this.ckPayType.Items.Count; i++)
                {
                    if ((from c in modelList select c.PaymentModeId).Contains<int>(Globals.SafeInt(this.ckPayType.Items[i].Value, 0)))
                    {
                        this.ckPayType.Items[i].Selected = true;
                    }
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ShippingType.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Shipping.ShippingType model = this.typeBll.GetModel(this.ModeId);
            model.AddPrice = new decimal?(Globals.SafeDecimal(this.tAddPrice.Text, (decimal) 0M));
            model.Price = Globals.SafeDecimal(this.tPrice.Text, (decimal) 0M);
            model.Weight = Globals.SafeInt(this.tWeight.Text, 0);
            model.AddWeight = new int?(Globals.SafeInt(this.tAddWeight.Text, 0));
            model.Description = this.tDesc.Text;
            model.ExpressCompanyName = this.ddlType.SelectedItem.Text;
            model.ExpressCompanyEn = this.ddlType.SelectedValue;
            model.Name = this.tName.Text;
            if (this.typeBll.Update(model))
            {
                this.payBll.Delete(this.ModeId);
                for (int i = 0; i < this.ckPayType.Items.Count; i++)
                {
                    if (this.ckPayType.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Shipping.ShippingPayment payment = new Maticsoft.Model.Shop.Shipping.ShippingPayment {
                            PaymentModeId = Globals.SafeInt(this.ckPayType.Items[i].Value, 0),
                            ShippingModeId = this.ModeId
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
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x20a;
            }
        }

        public int ModeId
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

