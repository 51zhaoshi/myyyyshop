namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsAdd;
        protected HiddenField Hidden_SelectName;
        protected HiddenField Hidden_SelectValue;
        protected HiddenField HiddenField_ICOPath;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtCategoryText;
        protected TextBox txtDescription;
        protected TextBox txtMeta_Description;
        protected TextBox txtMeta_Keywords;
        protected TextBox txtMeta_Title;
        protected TextBox txtName;
        protected TextBox txtRewriteName;
        protected TextBox txtSKUPrefix;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                this.btnCancle.Enabled = true;
                this.btnSave.Enabled = true;
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
            }
            else
            {
                this.Hidden_SelectName.Value = this.txtCategoryText.Text;
                Maticsoft.Model.Shop.Products.CategoryInfo model = new Maticsoft.Model.Shop.Products.CategoryInfo {
                    Name = this.txtName.Text,
                    Meta_Description = this.txtMeta_Description.Text,
                    Meta_Keywords = this.txtMeta_Keywords.Text,
                    Description = this.txtDescription.Text,
                    Meta_Title = this.txtMeta_Title.Text
                };
                if (!string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
                {
                    model.ParentCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
                }
                else
                {
                    model.ParentCategoryId = 0;
                }
                if (this.bll.IsExisted(model.ParentCategoryId, model.Name, 0))
                {
                    this.btnCancle.Enabled = true;
                    this.btnSave.Enabled = true;
                    MessageBox.ShowFailTip(this, "该分类下已存在同名分类");
                }
                else
                {
                    string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                    string newValue = "/Upload/Shop/Images/Categories";
                    ArrayList fileNameList = new ArrayList();
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                    {
                        string str3 = string.Format(this.HiddenField_ICOPath.Value, "");
                        fileNameList.Add(str3.Replace(oldValue, ""));
                        model.ImageUrl = str3.Replace(oldValue, newValue);
                    }
                    else
                    {
                        model.ImageUrl = "/Content/themes/base/Shop/images/none.png";
                    }
                    model.AssociatedProductType = -1;
                    model.RewriteName = this.txtRewriteName.Text;
                    model.SKUPrefix = this.txtSKUPrefix.Text;
                    model.HasChildren = false;
                    if (this.bll.CreateCategory(model))
                    {
                        this.btnSave.Enabled = false;
                        this.btnCancle.Enabled = false;
                        if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                        {
                            FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                        }
                        if (this.chkIsAdd.Checked)
                        {
                            this.btnCancle.Enabled = true;
                            this.btnSave.Enabled = true;
                            base.Cache.Remove("GetAllCateList-CateList");
                            MessageBox.ShowSuccessTip(this, "添加成功");
                            this.txtCategoryText.Text = this.Hidden_SelectName.Value;
                            this.HiddenField_ICOPath.Value = "";
                            this.txtDescription.Text = this.txtMeta_Description.Text = this.txtMeta_Title.Text = "";
                            this.txtName.Text = "";
                            this.txtSKUPrefix.Text = "";
                            this.txtRewriteName.Text = "";
                            this.txtMeta_Keywords.Text = "";
                        }
                        else
                        {
                            base.Cache.Remove("GetAllCateList-CateList");
                            MessageBox.ShowSuccessTip(this, "添加成功!", "list.aspx");
                        }
                    }
                    else
                    {
                        this.btnSave.Enabled = false;
                        this.btnCancle.Enabled = false;
                        MessageBox.ShowSuccessTip(this, "添加失败！");
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x210;
            }
        }
    }
}

