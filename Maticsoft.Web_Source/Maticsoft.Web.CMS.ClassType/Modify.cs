namespace Maticsoft.Web.CMS.ClassType
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected Button btnCancle;
        protected Button btnSave;
        protected Label lblClassTypeID;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected TextBox txtClassTypeName;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("List.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtClassTypeName.Text))
            {
                MessageBox.ShowFailTip(this, CMS.ClassErrorNameNotNull);
            }
            else
            {
                Maticsoft.Model.CMS.ClassType model = new Maticsoft.Model.CMS.ClassType {
                    ClassTypeID = Globals.SafeInt(this.lblClassTypeID.Text, 0),
                    ClassTypeName = this.txtClassTypeName.Text
                };
                if (this.bll.Update(model))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError, "List.aspx");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.ClassTypeID >= 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ClassType modelByCache = this.bll.GetModelByCache(this.ClassTypeID);
            if (modelByCache != null)
            {
                this.lblClassTypeID.Text = modelByCache.ClassTypeID.ToString();
                this.txtClassTypeName.Text = modelByCache.ClassTypeName;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd8;
            }
        }

        public int ClassTypeID
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

