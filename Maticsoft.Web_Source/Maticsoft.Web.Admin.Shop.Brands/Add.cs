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

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected ProductTypesCheckBoxList chkProductTpyes;
        protected DropDownList ddlTheme;
        protected HiddenField hfLogoUrl;
        protected Literal Literal1;
        protected Literal Literal16;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal32;
        protected Literal Literal4;
        protected HyperLink lnkDelete;
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

        protected void btnSave_Click(object sender, EventArgs e)
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
                    ProductTypes = list,
                    BrandName = text,
                    BrandSpell = str2,
                    Meta_Description = str3,
                    Meta_Keywords = str4
                };
                string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = "/Upload/Shop/Brands";
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.hfLogoUrl.Value))
                {
                    string str9 = string.Format(this.hfLogoUrl.Value, "");
                    fileNameList.Add(str9.Replace(oldValue, ""));
                    model.Logo = str9.Replace(oldValue, newValue);
                }
                model.CompanyUrl = str5;
                model.Description = str6;
                model.DisplaySequence = num;
                model.Theme = this.Theme;
                if (this.bll.CreateBrandsAndTypes(model, DataProviderAction.Create))
                {
                    this.btnCancle.Enabled = false;
                    this.btnSave.Enabled = false;
                    if (!string.IsNullOrWhiteSpace(this.hfLogoUrl.Value))
                    {
                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
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
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
                else
                {
                    this.chkProductTpyes.DataBind();
                    this.txtDisplaySequence.Text = this.bll.GetMaxDisplaySequence().ToString();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x18e;
            }
        }
    }
}

