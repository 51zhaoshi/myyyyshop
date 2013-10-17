namespace Maticsoft.Web.Admin.Settings
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class RegistStatement : PageBaseAdmin
    {
        protected int Act_UpdateData = 0xa1;
        protected Button btnReset;
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal7;
        protected TextBox txtContent;
        private Maticsoft.BLL.SysManage.WebSiteSet WebSiteSet = new Maticsoft.BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

        public void BoundData()
        {
            this.txtContent.Text = Globals.HtmlDecode(this.WebSiteSet.RegistStatement);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            this.BoundData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string target = this.txtContent.Text.Trim().Replace("\n", "");
            this.WebSiteSet.RegistStatement = Globals.HtmlDecode(target);
            base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
            this.btnSave.Enabled = false;
            this.btnReset.Enabled = false;
            MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "RegistStatement.aspx");
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
                return 160;
            }
        }
    }
}

