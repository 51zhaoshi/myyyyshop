namespace Maticsoft.Web.Admin.Members.FeedBack
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateType : PageBaseAdmin
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

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtName.Text.Trim();
            if (str.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能为空！");
            }
            else
            {
                Maticsoft.Model.Members.FeedbackType model = this.typeBll.GetModel(this.Id);
                if (model != null)
                {
                    model.TypeName = str;
                    model.Description = this.txtDesc.Text;
                    if (this.typeBll.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "TypeList.aspx");
                    }
                    else
                    {
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
            Maticsoft.Model.Members.FeedbackType model = this.typeBll.GetModel(this.Id);
            if (model != null)
            {
                this.txtName.Text = model.TypeName;
                this.txtDesc.Text = model.Description;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x119;
            }
        }

        protected int Id
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

