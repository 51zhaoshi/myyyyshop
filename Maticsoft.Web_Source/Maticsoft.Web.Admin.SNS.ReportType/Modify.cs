namespace Maticsoft.Web.Admin.SNS.ReportType
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
        private Maticsoft.BLL.SNS.ReportType bll = new Maticsoft.BLL.SNS.ReportType();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string text = this.txtTypeName.Text;
            string str2 = this.txtRemark.Text;
            if (text.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "举报类型的名称不能为空！");
            }
            else if (this.bll.Exists(this.Id, text))
            {
                MessageBox.ShowServerBusyTip(this, "举报类型的名称已存在！");
            }
            else if (str2.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能超过50个字符！");
            }
            else
            {
                Maticsoft.Model.SNS.ReportType model = this.bll.GetModel(this.Id);
                if (model != null)
                {
                    model.TypeName = text;
                    model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                    model.Remark = str2;
                    if (this.bll.Update(model))
                    {
                        MessageBox.ShowSuccessTipScript(this, "编辑举报类型成功", "window.parent.location.reload();");
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新举报类型(id=" + model.ID + ")成功", this);
                    }
                    else
                    {
                        LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "更新举报类型(id=" + model.ID + ")失败", this);
                        MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
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
            Maticsoft.Model.SNS.ReportType model = this.bll.GetModel(this.Id);
            if (model != null)
            {
                this.txtTypeName.Text = model.TypeName;
                this.radlStatus.SelectedValue = model.Status.ToString();
                this.txtRemark.Text = model.Remark;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x261;
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

