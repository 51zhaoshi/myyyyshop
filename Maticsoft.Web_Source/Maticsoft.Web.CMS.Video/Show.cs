namespace Maticsoft.Web.CMS.Video
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Components;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnEdit;
        protected Image imgImageUrl;
        protected Image imgNormalImageUrl;
        protected Image imgThumbImageUrl;
        protected Label lblAlbumID;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblDescription;
        protected Label lblDomain;
        protected Label lblGrade;
        protected Label lblIsRecomend;
        protected Label lblLastUpdateDate;
        protected Label lblLastUpdateUserID;
        protected Label lblPrivacy;
        protected Label lblPvCount;
        protected Label lblReference;
        protected Label lblRemark;
        protected Label lblSequence;
        protected Label lblState;
        protected Label lblTags;
        protected Label lblTitle;
        protected Label lblTotalComment;
        protected Label lblTotalFav;
        protected Label lblTotalTime;
        protected Label lblTotalUp;
        protected Label lblUrlType;
        protected Label lblVideoClassID;
        protected Label lblVideoFormat;
        protected Label lblVideoUrl;
        protected Literal Literal1;
        protected HyperLink lnkAttachment;
        public string localVideoUrl = "";
        protected Literal ltlAlbum;
        protected Literal ltlAttachment;
        protected Literal ltlCategory;
        protected Literal ltlCreatedDate;
        protected Literal ltlCreatedUser;
        protected Literal ltlDescription;
        protected Literal ltlDomain;
        protected Literal ltlDuration;
        protected Literal ltlFormat;
        protected Literal ltlGrade;
        protected Literal ltlImageUrl;
        protected Literal ltlIsRecommend;
        protected Literal ltlLastUpdateDate;
        protected Literal ltlLastUpdateUser;
        protected Literal ltlLocalVideo;
        protected Literal ltlNormalImageUrl;
        protected Literal ltlOnlineVideo;
        protected Literal ltlPrivacy;
        protected Literal ltlPvCount;
        protected Literal ltlReference;
        protected Literal ltlRemark;
        protected Literal ltlSequence;
        protected Literal ltlShow;
        protected Literal ltlSite;
        protected Literal ltlState;
        protected Literal ltlTags;
        protected Literal ltlThumbImageUrl;
        protected Literal ltlTip;
        protected Literal ltlTitle;
        protected Literal ltlTotalComment;
        protected Literal ltlTotalFav;
        protected Literal ltlTotalUp;
        protected Literal ltlType;
        public string onlineVideoUrl = "";
        protected HtmlTableRow trDomain;
        protected HtmlTableRow trLocalVideo;
        protected HtmlTableRow trOnlineVideo;
        protected HtmlTableRow trVideoFormat;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Modify.aspx?id=" + this.VideoID);
        }

        public string GetFlashUrl(string videoUrl)
        {
            string valueByCache = ConfigSystem.GetValueByCache("YouKuAPI");
            string flash = "";
            if (VideoHelper.IsYouKuVideoUrl(videoUrl))
            {
                YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(videoUrl);
                if (youKuInfo != null)
                {
                    flash = string.Format(valueByCache, youKuInfo.VidEncoded);
                }
            }
            if (VideoHelper.IsKu6VideoUrl(videoUrl))
            {
                Ku6Info info2 = VideoHelper.GetKu6Info(videoUrl);
                if (info2 != null)
                {
                    flash = info2.flash;
                }
            }
            return flash;
        }

        public string GetUrlType(object target)
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
                    return str;
                }
            }
            else
            {
                return CMSVideo.LocalVideo;
            }
            return CMSVideo.OnlineVideo;
        }

        public string GetVideoPrivacy(object target)
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

        public string GetVideoState(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        return CMSVideo.TurnCode;

                    case "1":
                        return CMSVideo.TranscodingFail;

                    case "2":
                        return CMSVideo.PendingReview;

                    case "3":
                        return CMSVideo.NotYetReleased;

                    case "4":
                        return CMSVideo.Screen;

                    case "5":
                        return CMSVideo.Publish;
                }
            }
            return str;
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
            Maticsoft.Model.CMS.Video modelEx = new Maticsoft.BLL.CMS.Video().GetModelEx(this.VideoID);
            if (modelEx == null)
            {
                return;
            }
            this.lblTitle.Text = modelEx.Title;
            this.lblDescription.Text = modelEx.Description;
            if (modelEx.AlbumID.HasValue)
            {
                this.lblAlbumID.Text = modelEx.AlbumID.ToString();
            }
            this.lblCreatedUserID.Text = modelEx.CreatedUserName;
            this.lblCreatedDate.Text = modelEx.CreatedDate.ToString();
            if (modelEx.LastUpdateUserID.HasValue)
            {
                this.lblLastUpdateUserID.Text = modelEx.LastUpdateUserName;
            }
            if (modelEx.LastUpdateDate.HasValue)
            {
                this.lblLastUpdateDate.Text = modelEx.LastUpdateDate.ToString();
            }
            this.lblSequence.Text = modelEx.Sequence.ToString();
            if (modelEx.VideoClassID.HasValue)
            {
                this.lblVideoClassID.Text = modelEx.VideoClassID.ToString();
            }
            this.lblIsRecomend.Text = base.GetboolText(modelEx.IsRecomend);
            this.imgImageUrl.ImageUrl = Maticsoft.Components.MvcApplication.UploadFolder + modelEx.ImageUrl;
            this.imgThumbImageUrl.ImageUrl = Maticsoft.Components.MvcApplication.UploadFolder + modelEx.ThumbImageUrl;
            this.imgNormalImageUrl.ImageUrl = Maticsoft.Components.MvcApplication.UploadFolder + modelEx.NormalImageUrl;
            if (modelEx.TotalTime.HasValue)
            {
                this.lblTotalTime.Text = TimeParser.SecondToDateTime(modelEx.TotalTime.Value);
            }
            this.lblTotalComment.Text = modelEx.TotalComment.ToString();
            this.lblTotalFav.Text = modelEx.TotalFav.ToString();
            this.lblTotalUp.Text = modelEx.TotalUp.ToString();
            this.lblReference.Text = modelEx.Reference.ToString();
            this.lblPvCount.Text = modelEx.PvCount.ToString();
            this.lblTags.Text = modelEx.Tags;
            string videoUrl = modelEx.VideoUrl;
            this.lblVideoUrl.Text = videoUrl;
            int urlType = modelEx.UrlType;
            if (!string.IsNullOrWhiteSpace(videoUrl))
            {
                switch (urlType)
                {
                    case 0:
                        this.trLocalVideo.Visible = true;
                        this.trVideoFormat.Visible = true;
                        this.localVideoUrl = videoUrl;
                        break;

                    case 1:
                    {
                        this.trDomain.Visible = true;
                        string flashUrl = this.GetFlashUrl(modelEx.VideoUrl);
                        if (!string.IsNullOrWhiteSpace(flashUrl))
                        {
                            this.trOnlineVideo.Visible = true;
                            this.onlineVideoUrl = flashUrl;
                        }
                        goto Label_02EA;
                    }
                }
            }
        Label_02EA:
            this.lblUrlType.Text = this.GetUrlType(urlType);
            this.lblVideoFormat.Text = modelEx.VideoFormat;
            this.lblDomain.Text = modelEx.Domain;
            this.lblGrade.Text = modelEx.Grade.ToString();
            string attachment = modelEx.Attachment;
            if (!string.IsNullOrWhiteSpace(attachment))
            {
                this.lnkAttachment.NavigateUrl = Maticsoft.Components.MvcApplication.UploadFolder + attachment;
            }
            this.lblPrivacy.Text = this.GetVideoPrivacy(modelEx.Privacy);
            this.lblState.Text = this.GetVideoState(modelEx.State);
            this.lblRemark.Text = modelEx.Remark;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x110;
            }
        }

        public int VideoID
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

