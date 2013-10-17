namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class show : PageBaseAdmin
    {
        protected Button btnCancle;
        public string id = "";
        protected Image Image1;
        protected Label lblDescription;
        protected Label lblEnable;
        protected Label lblID;
        protected Label lblName;
        protected Label lblOrderid;
        protected Label lblPermission;
        protected Label lblTarget;
        protected Label lblTreeType;
        protected Label lblUrl;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.id = base.Request.Params["id"];
                if ((this.id == null) || (this.id.Trim() == ""))
                {
                    base.Response.Redirect("treelist.aspx?TreeType=" + this.TreeType);
                    base.Response.End();
                }
                SysTree tree = new SysTree();
                SysNode node = tree.GetNode(int.Parse(this.id));
                this.lblID.Text = this.id;
                this.lblOrderid.Text = node.OrderID.ToString();
                this.lblName.Text = node.TreeText;
                if (node.ParentID == 0)
                {
                    this.lblTarget.Text = Site.lblRootDirectory;
                }
                else
                {
                    this.lblTarget.Text = tree.GetNode(node.ParentID).TreeText;
                }
                this.lblUrl.Text = node.Url;
                this.Image1.ImageUrl = node.ImageUrl;
                Permissions permissions = new Permissions();
                if (node.PermissionID == -1)
                {
                    this.lblPermission.Text = SysManage.lblPermissionText;
                }
                else
                {
                    this.lblPermission.Text = permissions.GetPermissionName(node.PermissionID);
                }
                switch (node.TreeType)
                {
                    case 0:
                        this.lblTreeType.Text = SysManage.dropBackendSystem;
                        break;

                    case 1:
                        this.lblTreeType.Text = SysManage.dropBackendEnterprise;
                        break;

                    case 2:
                        this.lblTreeType.Text = SysManage.dropBackendAgent;
                        break;

                    case 3:
                        this.lblTreeType.Text = SysManage.dropBackendUser;
                        break;
                }
                this.lblEnable.Text = node.Enabled ? SysManage.lblEnableTrue : SysManage.lblEnableFalse;
                this.lblDescription.Text = node.Comment;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd5;
            }
        }

        public int TreeType
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["TreeType"], -1);
            }
        }
    }
}

