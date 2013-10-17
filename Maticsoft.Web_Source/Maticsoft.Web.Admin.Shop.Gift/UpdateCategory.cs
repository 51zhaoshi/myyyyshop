namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Gift;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateCategory : PageBaseAdmin
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
            if ((base.Request.Params["categoryId"] != null) && (base.Request.Params["categoryId"].ToString() != ""))
            {
                int categoryID = Globals.SafeInt(base.Request.Params["categoryId"], 0);
                Maticsoft.Model.Shop.Gift.GiftsCategory model = this.bll.GetModel(categoryID);
                model.Name = this.txtName.Text;
                model.Description = this.txtDescription.Text;
                if (this.bll.UpdateCategory(model))
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["categoryId"] != null)) && (base.Request.Params["categoryId"].ToString() != ""))
            {
                int categoryID = Globals.SafeInt(base.Request.Params["categoryId"], 0);
                Maticsoft.Model.Shop.Gift.GiftsCategory model = this.bll.GetModel(categoryID);
                if (model == null)
                {
                    base.Response.Redirect("CategoryList.aspx");
                }
                this.txtName.Text = model.Name;
                this.txtDescription.Text = model.Description;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1b1;
            }
        }
    }
}

