namespace Maticsoft.Web.Admin.SNS.Categories
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI.WebControls;

    public class Relation : PageBaseAdmin
    {
        private CategorySource bll = new CategorySource();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlState;
        protected SNSCategoryDropList SNSCate;
        protected TaoBaoCategoryDropList TaoBaoCate;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("CateList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int sNSCateId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            int categoryId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            bool isLoop = Globals.SafeBool(this.radlState.SelectedValue, false);
            if (this.bll.UpdateSNSCate(categoryId, sNSCateId, isLoop))
            {
                MessageBox.ShowSuccessTip(this, "商品对应成功");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "商品对应类别成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "商品对应失败！");
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "商品对应类别失败!", this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && !string.IsNullOrWhiteSpace(base.Request.Params["id"].Trim()))
            {
                this.SNSCate.SelectedValue = base.Request.Params["id"];
            }
        }
    }
}

