namespace Maticsoft.Web.Admin.Shop.Package
{
    using Maticsoft.BLL.Shop.Package;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Package;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class CategoryModify : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Package.PackageCategory bll = new Maticsoft.BLL.Shop.Package.PackageCategory();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtName;
        protected TextBox txtRemark;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Categorylist.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "专辑类型的名称不能为空！");
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
                    Maticsoft.Model.Shop.Package.PackageCategory model = this.bll.GetModel(this.Id);
                    if (model != null)
                    {
                        model.Name = str;
                        model.Remark = str2;
                        if (this.bll.Update(model))
                        {
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改包装类型(id=" + model.CategoryId + ")成功", this);
                            MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "Categorylist.aspx");
                        }
                        else
                        {
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改专辑类型(id=" + model.CategoryId + ")失败", this);
                            MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.Id);
            }
        }

        private void ShowInfo(int CategoryId)
        {
            Maticsoft.Model.Shop.Package.PackageCategory model = new Maticsoft.BLL.Shop.Package.PackageCategory().GetModel(CategoryId);
            this.txtName.Text = model.Name;
            this.txtRemark.Text = model.Remark;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1bf;
            }
        }

        public int Id
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

