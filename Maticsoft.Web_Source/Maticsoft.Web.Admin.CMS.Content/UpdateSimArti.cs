namespace Maticsoft.Web.Admin.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class UpdateSimArti : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal7;
        protected TextBox txtContent;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CID <= 0)
            {
                MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent);
            }
            else
            {
                Maticsoft.Model.CMS.Content modelByClassIDByCache = this.bll.GetModelByClassIDByCache(this.CID);
                if (modelByClassIDByCache == null)
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent);
                }
                else
                {
                    modelByClassIDByCache.LastEditUserID = new int?(base.CurrentUser.UserID);
                    modelByClassIDByCache.LastEditDate = new DateTime?(DateTime.Now);
                    modelByClassIDByCache.Description = this.txtContent.Text;
                    if (this.bll.Update(modelByClassIDByCache))
                    {
                        DataCache.DeleteCache("ContentModelClassID-" + this.CID);
                        DataCache.DeleteCache("ContentModel-" + modelByClassIDByCache.ContentID);
                        DataCache.DeleteCache("ContentModelEx-" + modelByClassIDByCache.ContentID);
                        MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.CID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent);
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content modelByClassIDByCache = this.bll.GetModelByClassIDByCache(this.CID);
            if (modelByClassIDByCache != null)
            {
                this.txtContent.Text = modelByClassIDByCache.Description;
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe5;
            }
        }

        protected int CID
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["classid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["classid"], 0);
                }
                return num;
            }
        }
    }
}

