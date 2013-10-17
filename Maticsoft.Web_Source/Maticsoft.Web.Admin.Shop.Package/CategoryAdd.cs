namespace Maticsoft.Web.Admin.Shop.Package
{
    using Maticsoft.BLL.Shop.Package;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Package;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class CategoryAdd : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtName;
        protected TextBox txtRemark;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("CategoryList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "包装类型的名称不能为空！");
            }
            else
            {
                string str2 = this.txtRemark.Text.Trim();
                if (str2.Length > 200)
                {
                    MessageBox.ShowServerBusyTip(this, "备注不能超过200个字符！");
                }
                else
                {
                    Maticsoft.Model.Shop.Package.PackageCategory model = new Maticsoft.Model.Shop.Package.PackageCategory {
                        Name = str,
                        Remark = str2
                    };
                    Maticsoft.BLL.Shop.Package.PackageCategory category2 = new Maticsoft.BLL.Shop.Package.PackageCategory();
                    int num = category2.Add(model);
                    if (num > 0)
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加包装类型(id=" + num + ")成功", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "CategoryList.aspx");
                        base.Response.Redirect("CategoryList.aspx");
                    }
                    else
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加包装类型(id=" + num + ")失败", this);
                        MessageBox.ShowServerBusyTip(this, Site.TooltipSaveError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowFailTip(this, "您没有此权限");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1ca;
            }
        }
    }
}

