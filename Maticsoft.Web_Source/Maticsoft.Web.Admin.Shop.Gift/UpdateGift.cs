namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class UpdateGift : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        private Maticsoft.BLL.Shop.Gift.Gifts GiftBll = new Maticsoft.BLL.Shop.Gift.Gifts();
        protected HiddenField hfGiftImages;
        protected HiddenField hfProductImagesThumbSize;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList rblUpselling;
        protected TextBox txtCostPrice;
        protected TextBox txtDescription;
        protected TextBox txtGiftName;
        protected HtmlGenericControl txtGiftNameTip;
        protected TextBox txtMarketPrice;
        protected TextBox txtMeta_Description;
        protected TextBox txtMeta_Keywords;
        protected TextBox txtMeta_Title;
        protected TextBox txtPoints;
        protected TextBox txtSalePrice;
        protected TextBox txtShortDescription;
        protected TextBox txtStock;
        protected TextBox txtUnit;
        protected TextBox txtWeight;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindGiftBaseInfo()
        {
            Maticsoft.Model.Shop.Gift.Gifts model = this.GiftBll.GetModel(this.GiftId);
            if (model != null)
            {
                this.txtGiftName.Text = model.Name;
                this.txtUnit.Text = model.Unit;
                this.txtMarketPrice.Text = model.MarketPrice.ToString();
                this.txtCostPrice.Text = model.CostPrice.ToString();
                this.txtSalePrice.Text = model.SalePrice.ToString();
                this.txtWeight.Text = model.Weight.ToString();
                this.txtStock.Text = model.Stock.ToString();
                this.txtPoints.Text = model.NeedPoint.ToString();
                this.txtShortDescription.Text = model.ShortDescription;
                this.txtDescription.Text = model.LongDescription;
                this.txtMeta_Title.Text = model.Title;
                this.txtMeta_Description.Text = model.Meta_Description;
                this.txtMeta_Keywords.Text = model.Meta_Keywords;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("GiftsList.aspx");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            this.GetProdictInfo();
        }

        private void GetProdictInfo()
        {
            string text = this.txtGiftName.Text;
            string str2 = this.txtUnit.Text;
            decimal? nullable = new decimal?(Globals.SafeDecimal(this.txtMarketPrice.Text, (decimal) 0M));
            decimal? nullable2 = new decimal?(Globals.SafeDecimal(this.txtCostPrice.Text, (decimal) 0M));
            decimal? nullable3 = new decimal?(Globals.SafeDecimal(this.txtSalePrice.Text, (decimal) 0M));
            int num = Globals.SafeInt(this.txtWeight.Text, 0);
            int num2 = Globals.SafeInt(this.txtStock.Text, 0);
            int num3 = Globals.SafeInt(this.txtPoints.Text, 0);
            string str3 = this.txtShortDescription.Text;
            string str4 = this.txtDescription.Text;
            string str5 = this.txtMeta_Title.Text;
            string str6 = this.txtMeta_Description.Text;
            string str7 = this.txtMeta_Keywords.Text;
            string str8 = this.hfGiftImages.Value;
            string[] strArray = new string[0];
            if (!string.IsNullOrWhiteSpace(str3))
            {
                str3 = Globals.HtmlEncodeForSpaceWrap(str3);
            }
            if (!string.IsNullOrWhiteSpace(str8))
            {
                strArray = str8.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
            Maticsoft.Model.Shop.Gift.Gifts model = this.GiftBll.GetModel(this.GiftId);
            if (model != null)
            {
                model.Name = text;
                model.Unit = str2;
                model.CostPrice = nullable2;
                model.NeedPoint = num3;
                model.SaleCounts = 0;
                model.SalePrice = nullable3;
                model.Stock = new int?(num2);
                model.Weight = num;
                model.MarketPrice = nullable;
                model.CreateDate = DateTime.Now;
                model.Enabled = Globals.SafeBool(this.rblUpselling.SelectedValue, false);
                model.ShortDescription = str3;
                model.LongDescription = str4;
                model.Title = str5;
                model.Meta_Description = str6;
                model.Meta_Keywords = str7;
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (i == 0)
                    {
                        model.ThumbnailsUrl = string.Format(strArray[i], "");
                        model.InFocusImageUrl = string.Format(strArray[i], "T_");
                    }
                }
                if (this.GiftBll.Update(model))
                {
                    base.Response.Redirect("GiftsList.aspx");
                }
                else
                {
                    MessageBox.Show(this, "保存失败! 请重试.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindGiftBaseInfo();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ab;
            }
        }

        public int GiftId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["giftId"]))
                {
                    num = Globals.SafeInt(base.Request.Params["giftId"], 0);
                }
                return num;
            }
        }
    }
}

