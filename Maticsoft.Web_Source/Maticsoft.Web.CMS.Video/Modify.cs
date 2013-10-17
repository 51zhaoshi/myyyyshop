namespace Maticsoft.Web.CMS.Video
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Components;
    using Maticsoft.Controls;
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

    public class Modify : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsRecomend;
        protected DropDownList dropAlbumID;
        protected HiddenField hfAttachment;
        protected HiddenField hfLocalVideo;
        protected HiddenField hfNewAttachment;
        protected HiddenField hfNewLocalVideo;
        protected HiddenField hfUploadSuccess;
        protected HiddenField hfUrlType;
        protected Label lblMsg;
        protected Label lblStatus;
        protected Literal Literal1;
        protected HyperLink lnkAttachment;
        protected Literal ltlAdd;
        protected Literal ltlAlbum;
        protected Literal ltlAttachment;
        protected Literal ltlCancelUpload;
        protected Literal ltlCategory;
        protected Literal ltlCreatedDate;
        protected Literal ltlCreatedUser;
        protected Literal ltlDescription;
        protected Literal ltlDomain;
        protected Literal ltlDuration;
        protected Literal ltlFormat;
        protected Literal ltlHours;
        protected Literal ltlImageUrl;
        protected Literal ltlIsRecommend;
        protected Literal ltlMinutes;
        protected Literal ltlNormalImageUrl;
        protected Literal ltlPrivacy;
        protected Literal ltlRemark;
        protected Literal ltlSeconds;
        protected Literal ltlSequence;
        protected Literal ltlSite;
        protected Literal ltlSiteTip;
        protected Literal ltlState;
        protected Literal ltlTags;
        protected Literal ltlTagsTip;
        protected Literal ltlThumbImageUrl;
        protected Literal ltlTip;
        protected Literal ltlTitle;
        protected Literal ltlType;
        protected Literal ltlUpload;
        protected Literal ltlUploadVideo;
        private string normalImageHeight = ConfigSystem.GetValueByCache("NormalImageHeight");
        private string normalImageWidth = ConfigSystem.GetValueByCache("NormalImageWidth");
        protected RadioButtonList radlPrivacy;
        protected RadioButtonList radlState;
        protected RadioButtonList radlVideoClassID;
        private static Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");
        protected StatusMessage statusMessage;
        public string strStyle = "display:none";
        private string thumbImageHeight = ConfigSystem.GetValueByCache("ThumbImageHeight");
        private string thumbImageWidth = ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected HtmlTableRow trAttachment;
        protected HtmlTableRow trLocalVideo;
        protected HtmlTableRow trOnlineVideo;
        protected TextBox txtCreatedDate;
        protected TextBox txtCreatedUserID;
        protected TextBox txtDescription;
        protected TextBox txtDomain;
        protected TextBox txtImageUrl;
        protected TextBox txtMinutes;
        protected TextBox txtNormalImageUrl;
        protected TextBox txtOnlineVideo;
        protected TextBox txtRemark;
        protected TextBox txtSeconds;
        protected TextBox txtSequence;
        protected TextBox txtTags;
        protected TextBox txtThumbImageUrl;
        protected TextBox txtTitle;
        protected HtmlGenericControl txtTitleTip;
        protected TextBox txtTotalHours;
        protected TextBox txtVideoFormat;
        protected ValidateTarget ValidateTargetTitle;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        protected void BindVideoAlbumData()
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
        }

        protected void BindVideoClassData()
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
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string str15;
            string str = this.txtTitle.Text.Trim();
            string text = this.txtDescription.Text;
            string inputData = this.txtSequence.Text;
            string url = this.txtOnlineVideo.Text;
            string singleUrl = "";
            int type = 0;
            this.GetSingleUrl(url, out singleUrl, out type);
            string selectedValue = this.radlPrivacy.SelectedValue;
            string msg = "";
            int num2 = -1;
            if (!PageValidate.IsNumber(this.hfUrlType.Value.Trim()))
            {
                msg = msg + CMSVideo.ErrorVideoType + @"\n";
            }
            else
            {
                num2 = int.Parse(this.hfUrlType.Value);
            }
            if (str.Length == 0)
            {
                msg = msg + CMSVideo.ErrorVideoTitleNull + @"\n";
            }
            if ((num2 == 1) && (type == 0))
            {
                this.ShowMsg(CMSVideo.TooltipVideoUrl, false, true);
                return;
            }
            if ((inputData.Length > 0) && !PageValidate.IsNumber(inputData))
            {
                msg = msg + CMSVideo.ErrorOrderFormat + @"\n";
            }
            if (!PageValidate.IsNumber(selectedValue))
            {
                msg = msg + CMSVideo.ErrorPrivacyFormed + @"\n";
            }
            if (msg != "")
            {
                MessageBox.Show(this, msg);
                return;
            }
            string address = "";
            Maticsoft.Model.CMS.Video model = this.bll.GetModel(this.VideoID);
            if (model == null)
            {
                return;
            }
            model.Title = str;
            model.Description = text;
            model.AlbumID = new int?(Globals.SafeInt(this.dropAlbumID.SelectedValue, 0));
            model.LastUpdateUserID = new int?(base.CurrentUser.UserID);
            model.LastUpdateDate = new DateTime?(DateTime.Now);
            model.Sequence = Globals.SafeInt(inputData, 1);
            model.VideoClassID = new int?(Globals.SafeInt(this.radlVideoClassID.SelectedValue, 0));
            model.IsRecomend = this.chkIsRecomend.Checked;
            if (num2 == 0)
            {
                model.UrlType = 0;
                string str9 = this.hfNewLocalVideo.Value;
                if (!string.IsNullOrWhiteSpace(str9))
                {
                    model.VideoUrl = str9;
                    model.VideoFormat = Path.GetExtension(str9);
                    string str10 = str9 + ".jpg";
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str10)))
                    {
                        address = HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str10);
                        model.ImageUrl = str10;
                    }
                }
                else
                {
                    model.VideoUrl = this.hfLocalVideo.Value;
                    model.VideoFormat = this.txtVideoFormat.Text;
                    model.ImageUrl = this.txtImageUrl.Text;
                }
            }
            if (num2 == 1)
            {
                switch (type)
                {
                    case 1:
                    {
                        YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(singleUrl);
                        if (youKuInfo != null)
                        {
                            model.VideoUrl = singleUrl;
                            string domain = "";
                            string subDomain = "";
                            UrlOper.GetDomain(singleUrl, out domain, out subDomain);
                            if (!string.IsNullOrWhiteSpace(domain))
                            {
                                model.Domain = domain;
                            }
                            address = youKuInfo.Logo;
                            if (string.IsNullOrWhiteSpace(model.Title) && !string.IsNullOrWhiteSpace(youKuInfo.Title))
                            {
                                model.Title = youKuInfo.Title;
                            }
                            model.UrlType = 1;
                        }
                        break;
                    }
                    case 2:
                    {
                        Ku6Info info2 = VideoHelper.GetKu6Info(singleUrl);
                        if (info2 != null)
                        {
                            model.VideoUrl = singleUrl;
                            string str13 = "";
                            string str14 = "";
                            UrlOper.GetDomain(singleUrl, out str13, out str14);
                            if (!string.IsNullOrWhiteSpace(str13))
                            {
                                model.Domain = str13;
                            }
                            address = info2.coverurl;
                            model.ImageUrl = address;
                            if (string.IsNullOrWhiteSpace(model.Title) && !string.IsNullOrWhiteSpace(info2.title))
                            {
                                model.Title = info2.title;
                            }
                            model.UrlType = 1;
                        }
                        goto Label_03BA;
                    }
                }
            }
        Label_03BA:
            str15 = "";
            string thumbImage = "";
            string normalImage = "";
            this.Thumbnail(model.UrlType, address, true, true, out str15, out thumbImage, out normalImage);
            if (((model.UrlType == 1) && !string.IsNullOrWhiteSpace(str15)) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + str15)))
            {
                model.ImageUrl = str15;
            }
            if (!string.IsNullOrWhiteSpace(thumbImage) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + thumbImage)))
            {
                model.ThumbImageUrl = thumbImage;
            }
            if (!string.IsNullOrWhiteSpace(normalImage) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(Maticsoft.Components.MvcApplication.UploadFolder + normalImage)))
            {
                model.NormalImageUrl = normalImage;
            }
            model.Tags = this.txtTags.Text;
            model.UrlType = Globals.SafeInt(this.hfUrlType.Value, 0);
            string str18 = this.hfNewAttachment.Value;
            if (!string.IsNullOrWhiteSpace(str18))
            {
                model.Attachment = str18;
            }
            else
            {
                model.Attachment = this.hfAttachment.Value;
            }
            model.Privacy = Globals.SafeInt(this.radlPrivacy.SelectedValue, 0);
            model.State = Globals.SafeInt(this.radlState.SelectedValue, 5);
            model.Remark = this.txtRemark.Text;
            model.TotalTime = new int?(TimeParser.TimeToSecond(Globals.SafeInt(this.txtTotalHours.Text, 0), Globals.SafeInt(this.txtMinutes.Text, 0), Globals.SafeInt(this.txtSeconds.Text, 0)));
            if (this.bll.Update(model))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, Site.TooltipUpdateOK, "list.aspx");
            }
            else
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowFailTip(this, Site.TooltipUpdateError);
            }
        }

        public string GetSingleUrl(string url, out string singleUrl, out int type)
        {
            singleUrl = "";
            type = 0;
            if (this.IsUrl(url))
            {
                if (VideoHelper.IsYouKuVideoUrl(url))
                {
                    singleUrl = url;
                    type = 1;
                    return singleUrl;
                }
                if (VideoHelper.IsKu6VideoUrl(url))
                {
                    singleUrl = url;
                    type = 2;
                    return singleUrl;
                }
            }
            return singleUrl;
        }

        private int GetVideoTotalTime(string videoPath)
        {
            TimeSpan videoTotalTime = new ConvertVideo().GetVideoTotalTime(videoPath);
            return TimeParser.TimeToSecond(videoTotalTime.Hours, videoTotalTime.Minutes, videoTotalTime.Seconds);
        }

        public bool IsUrl(string content)
        {
            return regUrl.Match(content).Success;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindVideoAlbumData();
                this.BindVideoClassData();
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Video model = this.bll.GetModel(this.VideoID);
            if (model != null)
            {
                int urlType = model.UrlType;
                this.hfUrlType.Value = urlType.ToString();
                switch (urlType)
                {
                    case 0:
                        this.hfLocalVideo.Value = model.VideoUrl;
                        this.trLocalVideo.Visible = true;
                        break;

                    case 1:
                        this.txtOnlineVideo.Text = model.VideoUrl;
                        this.trOnlineVideo.Visible = true;
                        break;
                }
                this.txtTitle.Text = model.Title;
                this.txtDescription.Text = model.Description;
                if (model.AlbumID.HasValue)
                {
                    this.dropAlbumID.SelectedValue = model.AlbumID.ToString();
                }
                this.txtCreatedUserID.Text = model.CreatedUserID.ToString();
                this.txtCreatedDate.Text = model.CreatedDate.ToString();
                this.txtSequence.Text = model.Sequence.ToString();
                if (model.VideoClassID.HasValue)
                {
                    this.radlVideoClassID.SelectedValue = model.VideoClassID.ToString();
                }
                this.chkIsRecomend.Checked = model.IsRecomend;
                this.txtImageUrl.Text = model.ImageUrl;
                this.txtThumbImageUrl.Text = model.ThumbImageUrl;
                this.txtNormalImageUrl.Text = model.NormalImageUrl;
                this.txtTags.Text = model.Tags;
                this.txtVideoFormat.Text = model.VideoFormat;
                this.txtDomain.Text = model.Domain;
                string attachment = model.Attachment;
                if (!string.IsNullOrWhiteSpace(attachment))
                {
                    this.strStyle = "";
                    this.hfAttachment.Value = attachment;
                    this.lnkAttachment.NavigateUrl = Maticsoft.Components.MvcApplication.UploadFolder + attachment;
                }
                this.radlPrivacy.SelectedValue = model.Privacy.ToString();
                this.radlState.Text = model.State.ToString();
                this.txtRemark.Text = model.Remark;
                if (model.TotalTime.HasValue)
                {
                    TimeSpan span = new TimeSpan(0, 0, model.TotalTime.Value);
                    if (span.TotalHours < 10.0)
                    {
                        this.txtTotalHours.Text = "0" + ((int) span.TotalHours);
                    }
                    else
                    {
                        this.txtTotalHours.Text = ((int) span.TotalHours).ToString();
                    }
                    if (span.Minutes < 10)
                    {
                        this.txtMinutes.Text = "0" + span.Minutes.ToString();
                    }
                    else
                    {
                        this.txtMinutes.Text = span.Minutes.ToString();
                    }
                    if (span.Seconds < 10)
                    {
                        this.txtSeconds.Text = "0" + span.Seconds.ToString();
                    }
                    else
                    {
                        this.txtSeconds.Text = span.Seconds.ToString();
                    }
                }
            }
        }

        protected void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
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
                return 0x10f;
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

