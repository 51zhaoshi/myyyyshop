namespace Maticsoft.Web.Admin.SNS.TagType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected SNSCategoryDropList dropCid;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TagTypelist.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtTypeName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能为空！");
            }
            else if (str.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能大于50个字符！");
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
                    Maticsoft.Model.SNS.TagType model = new Maticsoft.Model.SNS.TagType {
                        TypeName = str,
                        Remark = str2,
                        Status = new int?(Globals.SafeInt(this.radlStatus.SelectedValue, 0)),
                        Cid = new int?(Globals.SafeInt(this.dropCid.SelectedValue, 0))
                    };
                    Maticsoft.BLL.SNS.TagType type2 = new Maticsoft.BLL.SNS.TagType();
                    if (type2.Add(model) > 0)
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
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x255;
            }
        }
    }
}

