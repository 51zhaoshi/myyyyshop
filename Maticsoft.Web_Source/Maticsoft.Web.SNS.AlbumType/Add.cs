namespace Maticsoft.Web.SNS.AlbumType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsMenu;
        protected CheckBox chkMenuIsShow;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtAlbumsCount;
        protected TextBox txtMenuSequence;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TypeList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTypeName.Text.Trim();
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
                    Maticsoft.Model.SNS.AlbumType model = new Maticsoft.Model.SNS.AlbumType {
                        TypeName = str,
                        IsMenu = this.chkIsMenu.Checked,
                        MenuIsShow = this.chkMenuIsShow.Checked,
                        MenuSequence = Globals.SafeInt(this.txtMenuSequence.Text, 1),
                        AlbumsCount = new int?(Globals.SafeInt(this.txtAlbumsCount.Text, 1)),
                        Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0),
                        Remark = str2
                    };
                    Maticsoft.BLL.SNS.AlbumType type2 = new Maticsoft.BLL.SNS.AlbumType();
                    int num = type2.Add(model);
                    if (num > 0)
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加专辑类型(id=" + num + ")成功", this);
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "TypeList.aspx");
                    }
                    else
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加专辑类型(id=" + num + ")失败", this);
                        MessageBox.ShowServerBusyTip(this, Site.TooltipSaveError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
            {
                MessageBox.ShowFailTip(this, "请内有此权限");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x26d;
            }
        }
    }
}

