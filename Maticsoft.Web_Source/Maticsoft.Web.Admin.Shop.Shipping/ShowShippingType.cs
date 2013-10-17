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

    public class ShowShippingType : PageBaseAdmin
    {
        protected CheckBoxList CheckBoxList2;
        protected CheckBoxList ckPayType;
        protected Literal lblAddPrice;
        protected Literal lblAddWeight;
        protected Literal lblCompanyName;
        protected Literal lblDesc;
        protected Literal lblName;
        protected Literal lblPrice;
        protected Literal lblWeight;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal21;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        private Maticsoft.BLL.Shop.Shipping.ShippingPayment payBll = new Maticsoft.BLL.Shop.Shipping.ShippingPayment();
        private Maticsoft.BLL.Shop.Shipping.ShippingType typeBll = new Maticsoft.BLL.Shop.Shipping.ShippingType();

        public void BindData()
        {
            Maticsoft.Model.Shop.Shipping.ShippingType model = this.typeBll.GetModel(this.ModeId);
            if (model != null)
            {
                this.lblAddPrice.Text = model.AddPrice.ToString();
                this.lblAddWeight.Text = model.AddWeight.ToString();
                this.lblDesc.Text = model.Description;
                this.lblPrice.Text = model.Price.ToString();
                this.lblWeight.Text = model.Weight.ToString();
                this.lblAddWeight.Text = model.AddWeight.ToString();
                this.lblCompanyName.Text = model.ExpressCompanyName;
                this.lblName.Text = model.Name;
            }
            List<Maticsoft.Model.Shop.Shipping.ShippingPayment> modelList = this.payBll.GetModelList(" ShippingModeId=" + this.ModeId);
            for (int i = 0; i < this.ckPayType.Items.Count; i++)
            {
                if ((from c in modelList select c.PaymentModeId).Contains<int>(Globals.SafeInt(this.ckPayType.Items[i].Value, 0)))
                {
                    this.ckPayType.Items[i].Selected = true;
                }
                this.ckPayType.Items[i].Enabled = false;
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("ShippingType.aspx");
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
                return 0x20b;
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

