namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class AddCategory : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Gift.GiftsCategory bll = new Maticsoft.BLL.Shop.Gift.GiftsCategory();
        protected Button btnCancle;
        protected Button btnSave;
        protected GiftCategoryDropList DropParentId;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox txtDescription;
        protected TextBox txtName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("CategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Gift.GiftsCategory model = new Maticsoft.Model.Shop.Gift.GiftsCategory {
                Name = this.txtName.Text,
                Description = this.txtDescription.Text
            };
            if (!string.IsNullOrWhiteSpace(this.DropParentId.SelectedValue.Trim()))
            {
                model.ParentCategoryId = new int?(int.Parse(this.DropParentId.SelectedValue));
            }
            else
            {
                model.ParentCategoryId = 0;
            }
            model.Depth = 0;
            model.Path = "";
            if (this.bll.AddCategory(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...", "CategoryList.aspx");
            }
            else
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                MessageBox.ShowFailTip(this, "添加失败！");
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
                return 0x1b0;
            }
        }
    }
}

