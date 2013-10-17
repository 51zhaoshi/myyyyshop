namespace Maticsoft.Web.CMS.ClassType
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtClassTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("List.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassTypeName.Text))
            {
                MessageBox.ShowFailTip(this, CMS.ClassErrorCLassNameNull);
            }
            else
            {
                Maticsoft.Model.CMS.ClassType model = new Maticsoft.Model.CMS.ClassType {
                    ClassTypeName = this.txtClassTypeName.Text
                };
                if (this.bll.Add(model))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
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
                return 0xc2;
            }
        }
    }
}

