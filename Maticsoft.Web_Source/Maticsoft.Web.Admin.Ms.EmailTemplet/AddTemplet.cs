namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class AddTemplet : PageBaseAdmin
    {
        private Maticsoft.BLL.Ms.EmailTemplet bll = new Maticsoft.BLL.Ms.EmailTemplet();
        protected Button btnCancle;
        protected Button btnSave;
        protected DropDownList ddlType;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected TextBox txtBody;
        protected TextBox txtDescription;
        protected TextBox txtSubject;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("TempletList.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Ms.EmailTemplet model = new Maticsoft.Model.Ms.EmailTemplet {
                EmailType = this.ddlType.SelectedValue,
                EmailPriority = -1,
                EmailSubject = this.txtSubject.Text,
                EmailDescription = this.txtDescription.Text,
                EmailBody = this.txtBody.Text
            };
            if (this.bll.Add(model) > 0)
            {
                base.Response.Redirect("TempletList.aspx");
            }
            else
            {
                this.lblMsg.ForeColor = Color.Red;
                this.lblMsg.Text = "添加邮件模板出错！";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x137;
            }
        }
    }
}

