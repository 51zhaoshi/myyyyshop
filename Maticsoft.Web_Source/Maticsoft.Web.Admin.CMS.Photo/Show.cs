namespace Maticsoft.Web.Admin.CMS.Photo
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Label lblAlbumID;
        protected Label lblClassID;
        protected Label lblCommentCount;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblDescription;
        protected Label lblImageUrl;
        protected Label lblIsRecomend;
        protected Label lblNormalImageUrl;
        protected Label lblPhotoID;
        protected Label lblPhotoName;
        protected Label lblPVCount;
        protected Label lblSequence;
        protected Label lblState;
        protected Label lblTags;
        protected Label lblThumbImageUrl;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        public string strid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int photoID = Convert.ToInt32(this.strid);
                this.ShowInfo(photoID);
            }
        }

        private void ShowInfo(int PhotoID)
        {
            Maticsoft.Model.CMS.Photo model = new Maticsoft.BLL.CMS.Photo().GetModel(PhotoID);
            this.lblPhotoID.Text = model.PhotoID.ToString();
            this.lblPhotoName.Text = model.PhotoName;
            this.lblImageUrl.Text = model.ImageUrl;
            this.lblDescription.Text = model.Description;
            this.lblAlbumID.Text = model.AlbumID.ToString();
            this.lblState.Text = model.State.ToString();
            this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblPVCount.Text = model.PVCount.ToString();
            this.lblClassID.Text = model.ClassID.ToString();
            this.lblThumbImageUrl.Text = model.ThumbImageUrl;
            this.lblNormalImageUrl.Text = model.NormalImageUrl;
            this.lblSequence.Text = model.Sequence.ToString();
            this.lblIsRecomend.Text = model.IsRecomend.Value ? Site.lblTrue : Site.lblFalse;
            this.lblCommentCount.Text = model.CommentCount.ToString();
            this.lblTags.Text = model.Tags;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xee;
            }
        }
    }
}

