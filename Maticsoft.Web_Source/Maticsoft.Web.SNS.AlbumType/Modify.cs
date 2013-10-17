namespace Maticsoft.Web.SNS.AlbumType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.AlbumType bll = new Maticsoft.BLL.SNS.AlbumType();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsMenu;
        protected CheckBox chkMenuIsShow;
        protected Label lblID;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtAlbumsCount;
        protected TextBox txtMenuSequence;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Typelist.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
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
                    Maticsoft.Model.SNS.AlbumType model = this.bll.GetModel(this.Id);
                    if (model != null)
                    {
                        model.TypeName = str;
                        model.IsMenu = this.chkIsMenu.Checked;
                        model.MenuIsShow = this.chkMenuIsShow.Checked;
                        model.MenuSequence = Globals.SafeInt(this.txtMenuSequence.Text, 1);
                        model.AlbumsCount = new int?(Globals.SafeInt(this.txtAlbumsCount.Text, 1));
                        model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                        model.Remark = str2;
                        if (this.bll.Update(model))
                        {
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改专辑类型(id=" + model.ID + ")成功", this);
                            MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "Typelist.aspx");
                        }
                        else
                        {
                            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改专辑类型(id=" + model.ID + ")失败", this);
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
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.AlbumType model = this.bll.GetModel(this.Id);
            if (model != null)
            {
                this.lblID.Text = model.ID.ToString();
                this.txtTypeName.Text = model.TypeName;
                this.chkIsMenu.Checked = model.IsMenu;
                this.chkMenuIsShow.Checked = model.MenuIsShow;
                this.txtMenuSequence.Text = model.MenuSequence.ToString();
                if (model.AlbumsCount.HasValue)
                {
                    this.txtAlbumsCount.Text = model.AlbumsCount.ToString();
                }
                this.radlStatus.SelectedValue = model.Status.ToString();
                this.txtRemark.Text = model.Remark;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x26e;
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

