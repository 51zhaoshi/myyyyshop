namespace Maticsoft.Web.CMS.VideoClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal ltlModify;
        protected Literal ltlName;
        protected Literal ltlTip;
        protected TextBox txtVideoClassName;
        protected HtmlGenericControl txtVideoClassNameTip;
        protected ValidateTarget ValidateTargetVideoClassName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtVideoClassName.Text.Trim();
            string msg = "";
            if (str.Length == 0)
            {
                msg = msg + CMSVideo.ErrorClassNameNull + @"\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                Maticsoft.Model.CMS.VideoClass model = this.bll.GetModel(this.VideoClassID);
                if (model != null)
                {
                    model.VideoClassName = str;
                    if (this.bll.Update(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
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
            Maticsoft.Model.CMS.VideoClass model = this.bll.GetModel(this.VideoClassID);
            if (model != null)
            {
                this.txtVideoClassName.Text = model.VideoClassName;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x108;
            }
        }

        public int VideoClassID
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

