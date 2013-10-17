namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();
        protected Button btnCancle;
        protected Button btnSave;
        protected CategoriesDropList ddlCateList;
        protected HiddenField HiddenField_ICOPath;
        protected HiddenField HiddenField_OldPath;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtAssociatedProductType;
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

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "分类名称不能为空，在1至60个字符之间");
            }
            else
            {
                int parentId = Globals.SafeInt(this.ddlCateList.SelectedValue, 0);
                if (parentId == this.CategoryId)
                {
                    MessageBox.ShowFailTip(this, "不能选自己作为父分类");
                }
                else
                {
                    string text = this.txtName.Text;
                    string str2 = this.txtMeta_Description.Text;
                    string str3 = this.txtMeta_Keywords.Text;
                    string str4 = this.txtDescription.Text;
                    string str5 = this.txtRewriteName.Text;
                    string str6 = this.txtSKUPrefix.Text;
                    Maticsoft.Model.Shop.Products.CategoryInfo model = this.bll.GetModel(this.CategoryId);
                    if (this.bll.IsExisted(parentId, text, model.CategoryId))
                    {
                        MessageBox.ShowFailTip(this, "该分类下已存在同名分类");
                    }
                    else
                    {
                        model.ParentCategoryId = parentId;
                        model.Name = text;
                        model.Meta_Description = str2;
                        model.Meta_Keywords = str3;
                        model.Description = str4;
                        model.RewriteName = str5;
                        model.SKUPrefix = str6;
                        model.AssociatedProductType = -1;
                        model.Meta_Title = this.txtMeta_Title.Text;
                        string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
                        string newValue = "/Upload/Shop/Images/Categories";
                        ArrayList fileNameList = new ArrayList();
                        if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                        {
                            string str9 = string.Format(this.HiddenField_ICOPath.Value, "");
                            fileNameList.Add(str9.Replace(oldValue, ""));
                            model.ImageUrl = str9.Replace(oldValue, newValue);
                        }
                        else
                        {
                            model.ImageUrl = this.HiddenField_OldPath.Value;
                        }
                        if (this.bll.UpdateCategory(model))
                        {
                            this.btnSave.Enabled = false;
                            this.btnCancle.Enabled = false;
                            if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                            {
                                FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                            }
                            base.Cache.Remove("GetAllCateList-CateList");
                            MessageBox.ShowSuccessTip(this, "保存成功，正在跳转列表页...", "list.aspx");
                        }
                        else
                        {
                            this.btnSave.Enabled = false;
                            this.btnCancle.Enabled = false;
                            MessageBox.ShowSuccessTip(this, "保存失败", "list.aspx");
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                int categoryId = Convert.ToInt32(base.Request.Params["id"]);
                this.ShowInfo(categoryId);
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.Shop.Products.CategoryInfo model = this.bll.GetModel(CategoryId);
            this.ddlCateList.SelectedValue = model.ParentCategoryId.ToString();
            this.txtName.Text = model.Name;
            this.txtMeta_Description.Text = model.Meta_Description;
            this.txtMeta_Keywords.Text = model.Meta_Keywords;
            this.txtDescription.Text = model.Description;
            this.txtRewriteName.Text = model.RewriteName;
            this.txtSKUPrefix.Text = model.SKUPrefix;
            this.txtAssociatedProductType.Text = model.AssociatedProductType.ToString();
            this.txtMeta_Title.Text = model.Meta_Title;
            this.HiddenField_OldPath.Value = model.ImageUrl;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x211;
            }
        }

        public int CategoryId
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

