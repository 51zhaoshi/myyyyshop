namespace Maticsoft.Web.Admin.Shop.Brands
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected ProductTypesCheckBoxList chkProductTpyes;
        protected DropDownList ddlTheme;
        protected HiddenField hfLogoUrl;
        protected HiddenField HiddenField_IsDeleteAttachment;
        protected HiddenField HiddenField_ISModifyImage;
        protected HiddenField HiddenField_OldImage;
        protected Image imgLogo;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal32;
        protected Literal Literal4;
        protected TextBox txtBrandName;
        protected HtmlGenericControl txtBrandNameTip;
        protected TextBox txtBrandSpell;
        protected TextBox txtCompanyUrl;
        protected HtmlGenericControl txtCompanyUrlTip;
        protected TextBox txtDescription;
        protected TextBox txtDisplaySequence;
        protected HtmlGenericControl txtDisplaySequenceTip;
        protected TextBox txtMeta_Description;
        protected TextBox txtMeta_Keywords;
        protected ValidateTarget ValidateTarget1;
        protected ValidateTarget ValidateTargetName;
        protected ValidateTarget ValidateTargetOffUrl;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Alist.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtBrandName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "请输入品牌名称！");
            }
            else if (string.IsNullOrWhiteSpace(this.txtDisplaySequence.Text.Trim()) || !PageValidate.IsNumber(this.txtDisplaySequence.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的显示顺序！");
            }
            else
            {
                string text = this.txtBrandName.Text;
                string str2 = this.txtBrandSpell.Text;
                string str3 = this.txtMeta_Description.Text;
                string str4 = this.txtMeta_Keywords.Text;
                string str5 = this.txtCompanyUrl.Text;
                string str6 = this.txtDescription.Text;
                int num = int.Parse(this.txtDisplaySequence.Text);
                IList<int> list = new List<int>();
                foreach (ListItem item in this.chkProductTpyes.Items)
                {
                    if (item.Selected)
                    {
                        list.Add(int.Parse(item.Value));
                    }
                }
                Maticsoft.Model.Shop.Products.BrandInfo model = new Maticsoft.Model.Shop.Products.BrandInfo {
                    BrandId = this.BrandId,
                    ProductTypes = list,
                    BrandName = text,
                    BrandSpell = str2,
                    Meta_Description = str3,
                    Meta_Keywords = str4,
                    CompanyUrl = str5
                };
                string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = "/Upload/Shop/Brands";
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                {
                    string str9 = string.Format(this.hfLogoUrl.Value, "");
                    fileNameList.Add(str9.Replace(oldValue, ""));
                    model.Logo = str9.Replace(oldValue, newValue);
                }
                else
                {
                    model.Logo = this.hfLogoUrl.Value;
                }
                model.Description = str6;
                model.DisplaySequence = num;
                if (this.bll.CreateBrandsAndTypes(model, DataProviderAction.Update))
                {
                    this.btnCancle.Enabled = false;
                    this.btnSave.Enabled = false;
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                    {
                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                        if (!string.IsNullOrWhiteSpace(this.HiddenField_OldImage.Value))
                        {
                            FileManage.DeleteFile(base.Server.MapPath(this.HiddenField_OldImage.Value));
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(this.HiddenField_IsDeleteAttachment.Value))
                    {
                        FileManage.DeleteFile(base.Server.MapPath(this.HiddenField_OldImage.Value));
                    }
                    MessageBox.ShowSuccessTip(this, "保存成功！", "Alist.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！", "Alist.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.chkProductTpyes.DataBind();
                this.ShowInfo(this.BrandId);
            }
        }

        private void ShowInfo(int BrandId)
        {
            Maticsoft.BLL.Shop.Products.BrandInfo info = new Maticsoft.BLL.Shop.Products.BrandInfo();
            Maticsoft.Model.Shop.Products.BrandInfo model = info.GetModel(BrandId);
            this.txtBrandName.Text = model.BrandName;
            this.txtBrandSpell.Text = model.BrandSpell;
            this.txtMeta_Description.Text = model.Meta_Description;
            this.txtMeta_Keywords.Text = model.Meta_Keywords;
            this.txtCompanyUrl.Text = model.CompanyUrl;
            this.txtDescription.Text = model.Description;
            this.imgLogo.ImageUrl = model.Logo;
            this.hfLogoUrl.Value = model.Logo;
            this.HiddenField_OldImage.Value = model.Logo;
            this.txtDisplaySequence.Text = model.DisplaySequence.ToString();
            Maticsoft.Model.Shop.Products.BrandInfo relatedProduct = info.GetRelatedProduct(new int?(BrandId), null);
            foreach (ListItem item in this.chkProductTpyes.Items)
            {
                if (relatedProduct.ProductTypeIdOrBrandsId.Contains(int.Parse(item.Value)))
                {
                    item.Selected = true;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x18f;
            }
        }

        private int BrandId
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

