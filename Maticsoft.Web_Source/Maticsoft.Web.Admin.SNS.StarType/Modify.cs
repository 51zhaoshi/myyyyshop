namespace Maticsoft.Web.Admin.SNS.StarType
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.StarType bll = new Maticsoft.BLL.SNS.StarType();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButtonList radlStatus;
        protected TextBox txtCheckRule;
        protected TextBox txtRemark;
        protected TextBox txtTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("StarTypeList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (this.txtTypeName.Text.Trim().Length == 0)
            {
                msg = msg + @"达人类型（如新晋达人不能为空！\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                Maticsoft.Model.SNS.StarType model = this.bll.GetModel(this.TypeID);
                string text = this.txtTypeName.Text;
                string str3 = this.txtCheckRule.Text;
                string str4 = this.txtRemark.Text;
                int num = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                model.TypeName = text;
                model.CheckRule = str3;
                model.Remark = str4;
                model.Status = new int?(num);
                if (this.bll.Update(model))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改达人类型(id=" + model.TypeID + ")成功", this);
                    MessageBox.ShowSuccessTip(this, "保存成功！", "StarTypeList.aspx");
                }
                LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "修改达人类型(id=" + model.TypeID + ")失败", this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo(this.TypeID);
            }
        }

        private void ShowInfo(int TypeID)
        {
            Maticsoft.Model.SNS.StarType model = this.bll.GetModel(TypeID);
            this.txtTypeName.Text = model.TypeName;
            this.txtCheckRule.Text = model.CheckRule;
            this.txtRemark.Text = model.Remark;
            this.radlStatus.SelectedValue = model.Status.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x267;
            }
        }

        protected int TypeID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

