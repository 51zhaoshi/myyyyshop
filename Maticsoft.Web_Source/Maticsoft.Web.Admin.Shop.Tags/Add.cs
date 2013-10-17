namespace Maticsoft.Web.Admin.Shop.Tags
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
        private Maticsoft.BLL.Shop.Tags.Tags bll = new Maticsoft.BLL.Shop.Tags.Tags();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField Hidden_SelectName;
        protected HiddenField Hidden_SelectValue;
        protected Literal Literal2;
        protected Literal Literal3;
        protected DropDownList radlIsRecommand;
        protected DropDownList radlStatus;
        protected TextBox txtCategoryText;
        protected TextBox txtTagName;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTagName.Text.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空！");
            }
            else
            {
                Maticsoft.Model.Shop.Tags.Tags model = new Maticsoft.Model.Shop.Tags.Tags();
                this.Hidden_SelectName.Value = this.txtCategoryText.Text;
                if (!string.IsNullOrWhiteSpace(this.Hidden_SelectValue.Value))
                {
                    model.TagCategoryId = Globals.SafeInt(this.Hidden_SelectValue.Value, 0);
                }
                else
                {
                    model.TagCategoryId = 0;
                }
                model.TagName = str;
                model.IsRecommand = bool.Parse(this.radlIsRecommand.SelectedValue);
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                if (this.bll.Add(model) > 0)
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加商品标签成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x225;
            }
        }
    }
}

