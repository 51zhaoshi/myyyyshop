namespace Maticsoft.Web.Admin.Ms.EmailTemplet
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Drawing;
    using System.Web.UI.WebControls;

    public class UpdateTemplet : PageBaseAdmin
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
            Maticsoft.Model.Ms.EmailTemplet model = this.bll.GetModel(this.Type);
            if (model != null)
            {
                model.EmailSubject = this.txtSubject.Text;
                model.EmailDescription = this.txtDescription.Text;
                model.EmailBody = this.txtBody.Text;
                if (this.bll.Update(model))
                {
                    base.Response.Redirect("TempletList.aspx");
                }
                else
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = "编辑邮件模板出错！";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Maticsoft.Model.Ms.EmailTemplet model = this.bll.GetModel(this.Type);
                if (model == null)
                {
                    base.Response.Redirect("TempletList.aspx");
                }
                this.txtBody.Text = model.EmailBody;
                this.txtDescription.Text = model.EmailDescription;
                this.ddlType.SelectedValue = model.EmailType;
                this.txtSubject.Text = model.EmailSubject;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x138;
            }
        }

        public int Type
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return num;
            }
        }
    }
}

