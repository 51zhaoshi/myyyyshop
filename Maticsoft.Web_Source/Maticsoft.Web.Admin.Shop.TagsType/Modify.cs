namespace Maticsoft.Web.Admin.Shop.TagsType
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Tags;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Tags.TagCategories bll = new Maticsoft.BLL.Shop.Tags.TagCategories();
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lblID;
        protected Label lblParentCate;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTypeName.Text.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowServerBusyTip(this, "标签类型名称不能为空！");
            }
            else if (str.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "标签类型名称不能大于50个字符！");
            }
            else
            {
                string str2 = this.txtRemark.Text.Trim();
                if (str2.Length > 100)
                {
                    MessageBox.ShowServerBusyTip(this, "备注不能大于100个字符！");
                }
                else
                {
                    Maticsoft.Model.Shop.Tags.TagCategories model = this.bll.GetModel(this.Id);
                    if (model != null)
                    {
                        model.CategoryName = str;
                        model.Remark = str2;
                        model.Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0));
                        if (this.bll.Update(model))
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
                        }
                        else
                        {
                            MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Tags.TagCategories model = this.bll.GetModel(this.Id);
            if (model != null)
            {
                string fullNameByCache = "";
                int? parentCategoryId = model.ParentCategoryId;
                int categoryId = parentCategoryId.HasValue ? parentCategoryId.GetValueOrDefault() : 0;
                if (model.ParentCategoryId > 0)
                {
                    fullNameByCache = this.bll.GetFullNameByCache(categoryId);
                }
                else
                {
                    fullNameByCache = "无上级标签";
                }
                this.lblParentCate.Text = fullNameByCache;
                this.lblID.Text = model.ID.ToString();
                this.txtTypeName.Text = model.CategoryName;
                this.txtRemark.Text = Globals.SafeString(model.Remark, "");
                if (model.Status.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x220;
            }
        }

        protected int Id
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

        protected int ParentCategoryId
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["ParentCategoryId"];
                if (!string.IsNullOrEmpty(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

