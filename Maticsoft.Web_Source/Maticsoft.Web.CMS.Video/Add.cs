namespace Maticsoft.Web.CMS.Video
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Components;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkAddContinue;
        protected CheckBox chkIsRecomend;
        protected DropDownList dropAlbumID;
        private string ffmpegTools = ConfigSystem.GetValueByCache("FFmpeg");
        protected HiddenField hfLocalVideo;
        protected HiddenField hfUploadSuccess;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal ltlAdd;
        protected Literal ltlAlbum;
        protected Literal ltlCategory;
        protected Literal ltlDescription;
        protected Literal ltlIsRecommend;
        protected Literal ltlPrivacy;
        protected Literal ltlSequence;
        protected Literal ltlSite;
        protected Literal ltlSiteTip;
        protected Literal ltlTags;
        protected Literal ltlTagsTip;
        protected Literal ltlTip;
        protected Literal ltlTitle;
        protected Literal ltlType;
        protected Literal ltlUploadVideo;
        private string normalImageHeight = ConfigSystem.GetValueByCache("NormalImageHeight");
        private string normalImageWidth = ConfigSystem.GetValueByCache("NormalImageWidth");
        protected RadioButton radLocalVideo;
        protected RadioButtonList radlPrivacy;
        protected RadioButtonList radlVideoClassID;
        protected RadioButton radOnlineVideo;
        private Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");
        private const string SavePath = "/Upload/CMS/Videos/{0}/";
        private const string TempPath = "/Upload/Temp/{0}/";
        private string thumbImageHeight = ConfigSystem.GetValueByCache("ThumbImageHeight");
        private string thumbImageWidth = ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected TextBox txtDescription;
        protected TextBox txtOnlineVideo;
        protected TextBox txtSequence;
        protected TextBox txtTags;
        protected TextBox txtTitle;
        protected HtmlGenericControl txtTitleTip;
        protected ValidateTarget ValidateTargetTitle;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            MessageBox.ShowLoadingTip(this, "正在跳转...", "list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str13;
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            int userID = base.CurrentUser.UserID;
            DateTime now = DateTime.Now;
            string str = this.txtTitle.Text.Trim();
            string text = this.txtDescription.Text;
            string selectedValue = this.radlPrivacy.SelectedValue;
            string inputData = this.txtSequence.Text;
            string path = this.hfLocalVideo.Value;
            string url = this.txtOnlineVideo.Text;
            int type = this.GetType(url);
            if (!this.radOnlineVideo.Checked && !this.radLocalVideo.Checked)
            {
                MessageBox.ShowFailTip(this, CMSVideo.TooltipSwitchType);
                return;
            }
            if (str.Length == 0)
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorTitleNull);
                return;
            }
            if (this.radLocalVideo.Checked && (path.Length == 0))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorNoVideo);
                return;
            }
            if (this.radOnlineVideo.Checked && (!this.IsUrl(url) || (type == 0)))
            {
                return;
            }
            if ((inputData.Length > 0) && !PageValidate.IsNumber(inputData))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorOrderFormat);
                return;
            }
            if (!PageValidate.IsNumber(selectedValue))
            {
                MessageBox.ShowFailTip(this, CMSVideo.ErrorPrivacyFormed);
                return;
            }
            string newValue = string.Format("/Upload/CMS/Videos/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
            Maticsoft.Model.CMS.Video model = new Maticsoft.Model.CMS.Video {
                Title = str,
                Description = text,
                AlbumID = new int?(Globals.SafeInt(this.dropAlbumID.SelectedValue, 0)),
                CreatedUserID = userID,
                CreatedDate = now,
                LastUpdateUserID = new int?(userID),
                LastUpdateDate = new DateTime?(now),
                Sequence = Globals.SafeInt(inputData, 1),
                VideoClassID = new int?(Globals.SafeInt(this.radlVideoClassID.SelectedValue, 0)),
                IsRecomend = false,
                TotalTime = null,
                TotalComment = 0,
                TotalFav = 0,
                TotalUp = 0,
                Reference = 0,
                Tags = this.txtTags.Text
            };
            string address = "";
            if (this.radLocalVideo.Checked)
            {
                model.UrlType = 0;
                model.VideoUrl = path.Replace(oldValue, newValue);
                model.VideoFormat = Path.GetExtension(path);
                string str10 = path + ".jpg";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(str10)))
                {
                    address = HttpContext.Current.Server.MapPath(str10);
                    model.ImageUrl = str10.Replace(oldValue, newValue);
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(path)))
                {
                    model.TotalTime = new int?(this.GetVideoTotalTime(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + path)));
                }
            }
            if (this.radOnlineVideo.Checked)
            {
                string domain = "";
                string subDomain = "";
                UrlOper.GetDomain(url, out domain, out subDomain);
                if (!string.IsNullOrWhiteSpace(domain))
                {
                    model.Domain = domain;
                }
                switch (type)
                {
                    case 1:
                    {
                        YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(url);
                        if (youKuInfo != null)
                        {
                            model.VideoUrl = url;
                            address = youKuInfo.Logo;
                            if (!string.IsNullOrWhiteSpace(model.Title) && string.IsNullOrWhiteSpace(youKuInfo.Title))
                            {
                                model.Title = youKuInfo.Title;
                            }
                            model.UrlType = 1;
                        }
                        break;
                    }
                    case 2:
                    {
                        Ku6Info info2 = VideoHelper.GetKu6Info(url);
                        if (info2 != null)
                        {
                            model.VideoUrl = url;
                            address = info2.coverurl;
                            if (string.IsNullOrWhiteSpace(model.Title) && !string.IsNullOrWhiteSpace(info2.title))
                            {
                                model.Title = info2.title;
                            }
                            model.UrlType = 1;
                        }
                        goto Label_03FA;
                    }
                }
            }
        Label_03FA:
            str13 = "";
            string thumbImage = "";
            string normalImage = "";
            this.Thumbnail(model.UrlType, address, true, true, out str13, out thumbImage, out normalImage);
            if (((model.UrlType == 1) && !string.IsNullOrWhiteSpace(str13)) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str13)))
            {
                model.ImageUrl = str13;
            }
            if (!string.IsNullOrWhiteSpace(thumbImage) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + thumbImage)))
            {
                model.ThumbImageUrl = thumbImage;
            }
            if (!string.IsNullOrWhiteSpace(normalImage) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + normalImage)))
            {
                model.NormalImageUrl = normalImage;
            }
            model.Grade = 0;
            model.Attachment = null;
            model.IsRecomend = this.chkIsRecomend.Checked;
            model.Privacy = Globals.SafeInt(selectedValue, 0);
            model.State = 5;
            model.Remark = "";
            model.PvCount = 0;
            if (this.bll.Add(model) > 0)
            {
                if (this.chkAddContinue.Checked)
                {
                    this.radLocalVideo.Checked = false;
                    this.radOnlineVideo.Checked = false;
                    this.txtTitle.Text = "";
                    this.txtTags.Text = "";
                    this.radlPrivacy.SelectedValue = "0";
                    this.LoadVideoAlbumData();
                    this.LoadVideoClassData();
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK);
                }
                else
                {
                    MessageBox.ShowLoadingTip(this, "新增成功，正在跳转...", "list.aspx");
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
            }
        }

        public int GetType(string url)
        {
            int num = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                num = 1;
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                num = 2;
            }
            return num;
        }

        private int GetVideoTotalTime(string videoPath)
        {
            TimeSpan videoTotalTime = new ConvertVideo().GetVideoTotalTime(videoPath);
            return TimeParser.TimeToSecond(videoTotalTime.Hours, videoTotalTime.Minutes, videoTotalTime.Seconds);
        }

        public bool IsUrl(string content)
        {
            return this.regUrl.Match(content).Success;
        }

        protected void LoadVideoAlbumData()
        {
            DataSet allList = new Maticsoft.BLL.CMS.VideoAlbum().GetAllList();
            if (!DataSetTools.DataSetIsNull(allList))
            {
                this.dropAlbumID.DataSource = allList;
                this.dropAlbumID.DataTextField = "AlbumName";
                this.dropAlbumID.DataValueField = "AlbumID";
                this.dropAlbumID.DataBind();
            }
            this.dropAlbumID.Items.Insert(0, new ListItem(Site.PleaseSelect, "0"));
            this.dropAlbumID.SelectedValue = "0";
        }

        protected void LoadVideoClassData()
        {
            DataSet allList = new Maticsoft.BLL.CMS.VideoClass().GetAllList();
            if (!DataSetTools.DataSetIsNull(allList))
            {
                this.radlVideoClassID.DataSource = allList;
                this.radlVideoClassID.DataTextField = "VideoClassName";
                this.radlVideoClassID.DataValueField = "VideoClassID";
                this.radlVideoClassID.DataBind();
            }
            this.radlVideoClassID.Items.Insert(0, new ListItem(CMSVideo.NoCategory, "0"));
            this.radlVideoClassID.SelectedValue = "0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.LoadVideoAlbumData();
                this.LoadVideoClassData();
                this.txtSequence.Text = this.bll.GetMaxSequence().ToString();
            }
        }

        protected void Thumbnail(int urlType, string address, bool IsThumbImage, bool IsNormalImage, out string imageUrl, out string thumbImage, out string normalImage)
        {
            string path = base.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = "";
            if (urlType == 0)
            {
                fileName = address;
            }
            imageUrl = Guid.NewGuid() + ".jpg";
            if (urlType == 1)
            {
                fileName = HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + imageUrl);
                new System.Net.WebClient().DownloadFile(address, fileName);
            }
            thumbImage = "T_" + imageUrl;
            normalImage = "N_" + imageUrl;
            if (System.IO.File.Exists(fileName))
            {
                if (IsThumbImage)
                {
                    ImageTools.MakeThumbnail(fileName, HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + thumbImage), Globals.SafeInt(this.thumbImageWidth, 120), Globals.SafeInt(this.thumbImageHeight, 90), MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
                }
                if (IsNormalImage)
                {
                    ImageTools.MakeThumbnail(fileName, HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + normalImage), Globals.SafeInt(this.normalImageHeight, 240), Globals.SafeInt(this.normalImageHeight, 180), MakeThumbnailMode.Auto, InterpolationMode.High, SmoothingMode.HighQuality);
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 270;
            }
        }
    }
}

