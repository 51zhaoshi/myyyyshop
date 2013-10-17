namespace Maticsoft.Web.Admin.Shop.TagsType
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Tags;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Tags.TagCategories bll = new Maticsoft.BLL.Shop.Tags.TagCategories();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField Hidden_SelectName;
        protected HiddenField Hidden_SelectValue;
        protected Literal Literal2;
        protected Literal Literal3;
        private Maticsoft.Model.Shop.Tags.TagCategories model = new Maticsoft.Model.Shop.Tags.TagCategories();
        protected RadioButtonList radlStatus;
        protected TextBox txtCategoryText;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
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
                    this.Hidden_SelectName.Value = this.txtCategoryText.Text;
                    if (!string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
                    {
                        this.model.ParentCategoryId = new int?(Globals.SafeInt(this.Hidden_SelectValue.Value, 0));
                    }
                    else
                    {
                        this.model.ParentCategoryId = 0;
                    }
                    this.model.CategoryName = str;
                    this.model.Remark = str2;
                    this.model.Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0));
                    if (this.bll.CreateCategory(this.model))
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加标签类型成功", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                    }
                    else
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加标签类型失败", this);
                        MessageBox.ShowFailTip(this, Site.TooltipSaveError);
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
                return 0x21f;
            }
        }
    }
}

