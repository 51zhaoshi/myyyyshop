namespace Maticsoft.Web.Admin.Members.FeedBack
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class AddType : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtDesc;
        protected TextBox txtName;
        private Maticsoft.BLL.Members.FeedbackType typeBll = new Maticsoft.BLL.Members.FeedbackType();

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TypeList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空");
            }
            else
            {
                Maticsoft.Model.Members.FeedbackType model = new Maticsoft.Model.Members.FeedbackType {
                    TypeName = str,
                    Description = this.txtDesc.Text
                };
                if (this.typeBll.Add(model) > 0)
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "添加反馈类型成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "TypeList.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = this.Page.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 280;
            }
        }
    }
}

