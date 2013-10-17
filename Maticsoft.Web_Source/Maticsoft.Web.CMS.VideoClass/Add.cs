namespace Maticsoft.Web.CMS.VideoClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkAddContinue;
        protected Literal ltlAdd;
        protected Literal ltlName;
        protected Literal ltlParentClass;
        protected Literal ltlTip;
        protected TextBox txtVideoClassName;
        protected HtmlGenericControl txtVideoClassNameTip;
        protected ValidateTarget ValidateTargetVideoClassName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;
        protected VideoClassDropList VideoClassDropList1;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtVideoClassName.Text.Trim();
            string msg = "";
            if (this.txtVideoClassName.Text.Trim().Length == 0)
            {
                msg = msg + CMSVideo.ErrorClassNameNull + @"\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
            }
            else
            {
                Maticsoft.Model.CMS.VideoClass model = new Maticsoft.Model.CMS.VideoClass {
                    VideoClassName = str,
                    ParentID = new int?(Globals.SafeInt(this.VideoClassDropList1.SelectedValue, 0))
                };
                if (this.bll.AddEx(model) > 0)
                {
                    if (this.chkAddContinue.Checked)
                    {
                        this.txtVideoClassName.Text = "";
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                    }
                    else
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                    }
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    this.VideoClassDropList1.SelectedValue = str;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x107;
            }
        }
    }
}

