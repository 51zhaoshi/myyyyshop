namespace Maticsoft.Web.CMS.VideoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
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

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();
        protected Button btnCancle;
        protected Button btnSave;
        protected HiddenField hfCoverVideo;
        protected HiddenField hfNewCoverVideo;
        protected HiddenField hfUploadFails;
        protected HiddenField hfUploadSuccess;
        protected Image imgCoverVideo;
        protected Label lblUploadFails;
        protected Label lblUploadSuccess;
        protected Literal ltlCoverVideo;
        protected Literal ltlDescription;
        protected Literal ltlModify;
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

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtAlbumName.Text.Trim();
            string str2 = this.hfNewCoverVideo.Value.Trim();
            if (string.IsNullOrWhiteSpace(str2))
            {
                str2 = this.hfCoverVideo.Value.Trim();
            }
            else
            {
                string str3 = "T_" + str2;
                string path = HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str3);
                if (File.Exists(path))
                {
                    ImageTools.MakeThumbnail(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str2), path, 120, 120, MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
                    str2 = str3;
                }
            }
            string str5 = this.txtDescription.Text.Trim();
            string selectedValue = this.radlState.SelectedValue;
            string inputData = this.txtSequence.Text.Trim();
            string str8 = this.radlPrivacy.SelectedValue;
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
            else if (!PageValidate.IsNumber(str8))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorPrivacyFormed);
            }
            else
            {
                Maticsoft.Model.CMS.VideoAlbum model = this.bll.GetModel(this.AlbumID);
                if (model != null)
                {
                    model.AlbumName = str;
                    model.CoverVideo = str2;
                    model.Description = str5;
                    model.LastUpdateUserID = new int?(base.CurrentUser.UserID);
                    model.LastUpdatedDate = new DateTime?(DateTime.Now);
                    model.State = Globals.SafeInt(selectedValue, 2);
                    model.Sequence = Globals.SafeInt(inputData, 1);
                    model.Privacy = new int?(Globals.SafeInt(str8, 0));
                    if (this.bll.Update(model))
                    {
                        MessageBox.ResponseScript(this, "parent.location.href='list.aspx'");
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
            Maticsoft.Model.CMS.VideoAlbum model = this.bll.GetModel(this.AlbumID);
            if (model != null)
            {
                this.txtAlbumName.Text = model.AlbumName;
                this.hfCoverVideo.Value = model.CoverVideo;
                this.imgCoverVideo.ImageUrl = ConfigSystem.GetValueByCache("UploadFolder") + model.CoverVideo;
                this.txtDescription.Text = model.Description;
                this.radlState.SelectedValue = model.State.ToString();
                this.txtSequence.Text = model.Sequence.ToString();
                if (model.Privacy.HasValue)
                {
                    this.radlPrivacy.SelectedValue = model.Privacy.ToString();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x101;
            }
        }

        public int AlbumID
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

