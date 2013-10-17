namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected HtmlForm Form1;
        protected Image imgCoverPhoto;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblLastUpdatedDate;
        protected Label lblPVCount;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList radlPrivacy;
        protected RadioButtonList radlState;
        protected string strThumbImageHeight = ConfigSystem.GetValueByCache("ThumbImageHeight");
        protected string strThumbImageWidth = ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected TextBox txtAlbumName;
        protected TextBox txtDescription;
        protected TextBox txtSequence;

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtAlbumName.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorAlbumNull);
            }
            else if (!PageValidate.IsNumber(this.txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, CMSPhoto.ErrorOrderFormat);
            }
            else
            {
                string text = this.txtAlbumName.Text;
                string str2 = this.txtDescription.Text;
                int num = int.Parse(this.radlState.SelectedValue);
                int num2 = int.Parse(this.txtSequence.Text);
                Maticsoft.BLL.CMS.PhotoAlbum album = new Maticsoft.BLL.CMS.PhotoAlbum();
                Maticsoft.Model.CMS.PhotoAlbum model = album.GetModel(this.AlbumID);
                model.AlbumName = text;
                model.Description = str2;
                model.State = num;
                model.Sequence = num2;
                model.Privacy = int.Parse(this.radlPrivacy.SelectedValue);
                model.LastUpdatedDate = DateTime.Now;
                if (album.Update(model))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack && (this.AlbumID != 0))
            {
                this.ShowInfo(this.AlbumID);
            }
        }

        private void ShowInfo(int AlbumID)
        {
            Maticsoft.Model.CMS.PhotoAlbum model = new Maticsoft.BLL.CMS.PhotoAlbum().GetModel(AlbumID);
            this.txtAlbumName.Text = model.AlbumName;
            this.txtDescription.Text = model.Description;
            Maticsoft.Model.CMS.Photo photo2 = new Maticsoft.BLL.CMS.Photo().GetModel(model.CoverPhoto.Value);
            if (photo2 != null)
            {
                this.imgCoverPhoto.ImageUrl = FileHelper.GeThumbImage(photo2.ThumbImageUrl, "T235x1280_");
            }
            this.radlState.SelectedValue = model.State.ToString();
            this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblPVCount.Text = model.PVCount.ToString();
            this.txtSequence.Text = model.Sequence.ToString();
            this.radlPrivacy.SelectedValue = model.Privacy.ToString();
            this.lblLastUpdatedDate.Text = model.LastUpdatedDate.ToString();
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xf5;
            }
        }

        public int AlbumID
        {
            get
            {
                int num = 0;
                if ((base.Request.Params["id"] != null) && (base.Request.Params["id"].Trim() != ""))
                {
                    num = Convert.ToInt32(base.Request.Params["id"]);
                }
                return num;
            }
        }
    }
}

