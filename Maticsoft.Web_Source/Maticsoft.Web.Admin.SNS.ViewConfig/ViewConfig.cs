namespace Maticsoft.Web.Admin.SNS.ViewConfig
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class ViewConfig : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x77;
        protected Button btnSave;
        protected DropDownList ddlCommentDataSize;
        protected DropDownList ddlFallDataSize;
        protected DropDownList ddlFallInitDataSize;
        protected DropDownList ddlPostDataSize;
        protected HiddenField HiddenField_ID;
        private Maticsoft.BLL.SNS.TaoBaoConfig TaoBaoConfig = new Maticsoft.BLL.SNS.TaoBaoConfig(ApplicationKeyType.OpenAPI);

        private void BoundData()
        {
            string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS);
            this.ddlFallDataSize.SelectedValue = (valueByCache == null) ? "" : valueByCache;
            string str2 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS);
            this.ddlPostDataSize.SelectedValue = (valueByCache == null) ? "" : str2;
            string str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS);
            this.ddlCommentDataSize.SelectedValue = (str3 == null) ? "" : str3;
            string str4 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS);
            this.ddlFallInitDataSize.SelectedValue = (str4 == null) ? "" : str4;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_FallInitDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_FallInitDataSize", this.ddlFallInitDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_FallInitDataSize", this.ddlFallInitDataSize.SelectedValue, "瀑布流数次初次加载的数量", ApplicationKeyType.SNS);
                }
                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_FallDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_FallDataSize", this.ddlFallDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_FallDataSize", this.ddlFallDataSize.SelectedValue, "瀑布流每页的数量", ApplicationKeyType.SNS);
                }
                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostDataSize", this.ddlPostDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostDataSize", this.ddlPostDataSize.SelectedValue, "动态每页显示的数量", ApplicationKeyType.SNS);
                }
                if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_CommentDataSize"))
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_CommentDataSize", this.ddlCommentDataSize.SelectedValue, ApplicationKeyType.SNS);
                }
                else
                {
                    Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_CommentDataSize", this.ddlCommentDataSize.SelectedValue, "评论显示的条数", ApplicationKeyType.SNS);
                }
                base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.SNS);
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "设置全局配置成功", this);
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "ViewConfig.aspx");
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "设置全局配置出现异常", this);
                MessageBox.ShowFailTip(this, Site.TooltipTryAgainLater, "ViewConfig.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                this.BoundData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x76;
            }
        }
    }
}

