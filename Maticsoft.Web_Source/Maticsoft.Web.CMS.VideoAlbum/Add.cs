namespace Maticsoft.Web.CMS.VideoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Components;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField hfCoverVideo;
        protected HiddenField hfUploadFails;
        protected HiddenField hfUploadSuccess;
        protected Image imgCoverVideo;
        protected Label lblUploadFails;
        protected Label lblUploadSuccess;
        protected Literal ltlAdd;
        protected Literal ltlCoverVideo;
        protected Literal ltlDescription;
        protected Literal ltlName;
        protected Literal ltlPrivacy;
        protected Literal ltlSequence;
        protected Literal ltlState;
        protected Literal ltlTip;
        protected RadioButtonList radlPrivacy;
        protected RadioButtonList radlState;
        protected TextBox txtAlbumName;
        protected HtmlGenericControl txtAlbumNameTip;
        protected TextBox txtDescription;
        protected TextBox txtSequence;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtAlbumName.Text.Trim();
            string str2 = this.hfCoverVideo.Value.Trim();
            string str3 = this.txtDescription.Text.Trim();
            string selectedValue = this.radlState.SelectedValue;
            string inputData = this.txtSequence.Text.Trim();
            string str6 = this.radlPrivacy.SelectedValue;
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorVideoNameNull);
            }
            else if (!PageValidate.IsNumber(selectedValue))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorStartFormat);
            }
            else if ((inputData.Length > 0) && !PageValidate.IsNumber(inputData))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorOrderFormat);
            }
            else if (!PageValidate.IsNumber(str6))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorPrivacyFormed);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    string str7 = "T_" + str2;
                    string path = HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str7);
                    if (File.Exists(path))
                    {
                        ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str2), path, 120, 120, MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
                        str2 = str7;
                    }
                }
                Maticsoft.Model.CMS.VideoAlbum model = new Maticsoft.Model.CMS.VideoAlbum {
                    AlbumName = str,
                    CoverVideo = str2,
                    Description = str3,
                    CreatedUserID = base.CurrentUser.UserID,
                    CreatedDate = DateTime.Now,
                    LastUpdateUserID = new int?(base.CurrentUser.UserID),
                    LastUpdatedDate = new DateTime?(DateTime.Now),
                    State = Globals.SafeInt(selectedValue, 2),
                    Sequence = Globals.SafeInt(inputData, 1),
                    Privacy = new int?(Globals.SafeInt(str6, 0)),
                    PvCount = 0
                };
                if (this.bll.Add(model) > 0)
                {
                    MessageBox.ResponseScript(this, "parent.location.href='list.aspx'");
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
                this.txtSequence.Text = this.bll.GetMaxSequence().ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x100;
            }
        }
    }
}

