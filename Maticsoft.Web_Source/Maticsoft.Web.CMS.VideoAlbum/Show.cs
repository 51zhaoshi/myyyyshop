namespace Maticsoft.Web.CMS.VideoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnClose;
        protected Button btnEdit;
        protected Image imgCoverVideo;
        protected Label lblAlbumName;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserName;
        protected Label lblDescription;
        protected Label lblLastUpdatedDate;
        protected Label lblLastUpdateUserName;
        protected Label lblPrivacy;
        protected Label lblPvCount;
        protected Label lblSequence;
        protected Label lblState;
        protected Literal ltlCoverVideo;
        protected Literal ltlCreatedDate;
        protected Literal ltlCreatedUser;
        protected Literal ltlDescription;
        protected Literal ltlLastUpdateDate;
        protected Literal ltlLastUpdateUser;
        protected Literal ltlName;
        protected Literal ltlPrivacy;
        protected Literal ltlPvCount;
        protected Literal ltlSequence;
        protected Literal ltlShow;
        protected Literal ltlState;
        protected Literal ltlTip;
        public string strid = string.Empty;

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Modify.aspx?id=" + this.AlbumID);
        }

        public string GetVideoAlbumPrivacy(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return CMSVideo.SemiOpen;
                }
            }
            else
            {
                return CMSVideo.Open;
            }
            return CMSVideo.Private;
        }

        public string GetVideoAlbumState(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return "正常";
                }
            }
            else
            {
                return "未审核";
            }
            return "待审核";
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
            Maticsoft.Model.CMS.VideoAlbum modelEx = new Maticsoft.BLL.CMS.VideoAlbum().GetModelEx(this.AlbumID);
            if (modelEx != null)
            {
                this.lblAlbumName.Text = modelEx.AlbumName;
                this.imgCoverVideo.ImageUrl = ConfigSystem.GetValueByCache("UploadFolder") + modelEx.CoverVideo;
                this.lblDescription.Text = modelEx.Description;
                this.lblCreatedUserName.Text = modelEx.CreatedUserName;
                this.lblCreatedDate.Text = modelEx.CreatedDate.ToString();
                if (modelEx.LastUpdateUserID.HasValue)
                {
                    this.lblLastUpdateUserName.Text = modelEx.LastUpdateUserName;
                }
                if (modelEx.LastUpdatedDate.HasValue)
                {
                    this.lblLastUpdatedDate.Text = modelEx.LastUpdatedDate.ToString();
                }
                this.lblState.Text = this.GetVideoAlbumState(modelEx.State);
                this.lblSequence.Text = modelEx.Sequence.ToString();
                this.lblPrivacy.Text = this.GetVideoAlbumPrivacy(modelEx.Privacy);
                this.lblPvCount.Text = modelEx.PvCount.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x102;
            }
        }

        public int AlbumID
        {
            get
            {
                int num = 0;
                this.strid = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(this.strid) && PageValidate.IsNumber(this.strid))
                {
                    num = int.Parse(this.strid);
                }
                return num;
            }
        }
    }
}

