namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using System;
    using System.Drawing;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AddGift : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected GiftCategoryDropList DropParentId;
        private Maticsoft.BLL.Shop.Gift.Gifts GiftBll = new Maticsoft.BLL.Shop.Gift.Gifts();
        protected HiddenField hfGiftImages;
        protected HiddenField hfImage;
        protected HiddenField hfProductImagesThumbSize;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList rblUpselling;
        protected HiddenField thfImage;
        protected TextBox txtCostPrice;
        protected TextBox txtDescription;
        protected TextBox txtMarketPrice;
        protected TextBox txtMeta_Description;
        protected TextBox txtMeta_Keywords;
        protected TextBox txtMeta_Title;
        protected TextBox txtPoints;
        protected TextBox txtProductName;
        protected HtmlGenericControl txtProductNameTip;
        protected TextBox txtSalePrice;
        protected TextBox txtShortDescription;
        protected TextBox txtStock;
        protected TextBox txtUnit;
        protected TextBox txtWeight;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

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
            int num = 0;
            if (!string.IsNullOrWhiteSpace(this.DropParentId.SelectedValue.Trim()))
            {
                num = int.Parse(this.DropParentId.SelectedValue);
            }
            string text = this.txtProductName.Text;
            string str2 = this.txtUnit.Text;
            decimal? nullable = new decimal?(Globals.SafeDecimal(this.txtMarketPrice.Text, (decimal) 0M));
            decimal? nullable2 = new decimal?(Globals.SafeDecimal(this.txtCostPrice.Text, (decimal) 0M));
            decimal? nullable3 = new decimal?(Globals.SafeDecimal(this.txtSalePrice.Text, (decimal) 0M));
            int num2 = Globals.SafeInt(this.txtWeight.Text, 0);
            int num3 = Globals.SafeInt(this.txtStock.Text, 0);
            int num4 = Globals.SafeInt(this.txtPoints.Text, 0);
            string str3 = this.txtShortDescription.Text;
            string str4 = this.txtDescription.Text;
            string str5 = this.txtMeta_Title.Text;
            string str6 = this.txtMeta_Description.Text;
            string str7 = this.txtMeta_Keywords.Text;
            string format = this.hfImage.Value;
            string str9 = this.thfImage.Value;
            if (!string.IsNullOrWhiteSpace(str3))
            {
                str3 = Globals.HtmlEncodeForSpaceWrap(str3);
            }
            Maticsoft.Model.Shop.Gift.Gifts model = new Maticsoft.Model.Shop.Gift.Gifts {
                Name = text,
                CategoryID = num,
                Unit = str2,
                CostPrice = nullable2,
                NeedPoint = num4,
                SaleCounts = 0,
                SalePrice = nullable3,
                Stock = new int?(num3),
                Weight = num2,
                MarketPrice = nullable,
                CreateDate = DateTime.Now,
                Enabled = Globals.SafeBool(this.rblUpselling.SelectedValue, false),
                ShortDescription = str3,
                LongDescription = str4,
                Title = str5,
                Meta_Description = str6,
                Meta_Keywords = str7,
                ThumbnailsUrl = string.Format(format, ""),
                InFocusImageUrl = string.Format(str9, "T_")
            };
            if (this.GiftBll.Add(model) > 0)
            {
                base.Response.Redirect("GiftsList.aspx");
            }
            else
            {
                MessageBox.Show(this, "保存失败! 请重试.");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Size size = StringPlus.SplitToSize(ConfigSystem.GetValueByCache("ProductNormalImageSize"), '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);
                this.hfProductImagesThumbSize.Value = size.Width + "," + size.Height;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1aa;
            }
        }
    }
}

