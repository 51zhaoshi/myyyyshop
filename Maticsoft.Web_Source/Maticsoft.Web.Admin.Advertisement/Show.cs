namespace Maticsoft.Web.Admin.Advertisement
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Image Image1;
        protected Label lblAdvertisementId;
        protected Label lblAdvertisementName;
        protected Label lblAdvHtml;
        protected Label lblAdvPositionId;
        protected Label lblAlternateText;
        protected Label lblCPMPrice;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblDayMaxIP;
        protected Label lblDayMaxPV;
        protected Label lblEndDate;
        protected Label lblEnterpriseID;
        protected Label lblImpressions;
        protected Label lblNavigateUrl;
        protected Label lblSequence;
        protected Label lblStartDate;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal litFlash;
        protected RadioButton rbAutoStop;
        protected RadioButton rbCodeContent;
        protected RadioButton rbFlashContent;
        protected RadioButton rbImgContent;
        protected RadioButton rbNoLimit;
        protected RadioButton rbNoStup;
        protected RadioButton rbStatusN;
        protected RadioButton rbStatusY;
        protected RadioButton rbStop;
        protected RadioButton rbTextContent;
        public string strid = "";

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(string.Format("SingleList.aspx?id={0}", this.AdPositionID));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int advertisementId = Convert.ToInt32(this.strid);
                this.ShowInfo(advertisementId);
            }
        }

        private void ShowInfo(int AdvertisementId)
        {
            Maticsoft.Model.Settings.Advertisement model = new Maticsoft.BLL.Settings.Advertisement().GetModel(AdvertisementId);
            this.lblAdvertisementId.Text = model.AdvertisementId.ToString();
            this.lblAdvertisementName.Text = model.AdvertisementName;
            Maticsoft.Model.Settings.AdvertisePosition position2 = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(model.AdvPositionId.Value);
            if (position2 != null)
            {
                this.lblAdvPositionId.Text = position2.AdvPositionName;
                switch (model.ContentType.Value)
                {
                    case 0:
                        this.rbTextContent.Checked = true;
                        break;

                    case 1:
                        this.rbImgContent.Checked = true;
                        this.Image1.ImageUrl = model.FileUrl;
                        this.Image1.Width = position2.Width.HasValue ? ((Unit) position2.Width.Value) : 0;
                        this.Image1.Height = position2.Height.HasValue ? ((Unit) position2.Height.Value) : 0;
                        break;

                    case 2:
                    {
                        this.rbFlashContent.Checked = true;
                        int num = position2.Width.HasValue ? position2.Width.Value : 0;
                        int num2 = position2.Height.HasValue ? position2.Height.Value : 0;
                        this.litFlash.Text = string.Concat(new object[] { "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"", num, "\" height=\"", num2, "\"> <param name=\"wmode\" value=\"opaque\" /> <param name=\"movie\" value=\"", model.FileUrl, "\" /><param name=\"quality\" value=\"high\" /><embed src=\"", model.FileUrl, "\" allowfullscreen=\"true\" quality=\"high\" width=\"", num, "\" height=\"", num2, "\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object>" });
                        break;
                    }
                    case 3:
                        this.rbCodeContent.Checked = true;
                        break;
                }
                switch (model.State.Value)
                {
                    case -1:
                        this.rbStop.Checked = true;
                        break;

                    case 0:
                        this.rbStatusN.Checked = true;
                        break;

                    case 1:
                        this.rbStatusY.Checked = true;
                        break;
                }
                this.lblAlternateText.Text = model.AlternateText;
                this.lblNavigateUrl.Text = model.NavigateUrl;
                this.lblAdvHtml.Text = Globals.HtmlEncode(model.AdvHtml);
                this.lblImpressions.Text = model.Impressions.ToString();
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
                this.lblStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "无限制";
                this.lblEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "无限制";
                this.lblDayMaxPV.Text = model.DayMaxPV.ToString();
                this.lblDayMaxIP.Text = model.DayMaxIP.ToString();
                this.lblCPMPrice.Text = model.CPMPrice.Value.ToString("0.00");
                if (model.AutoStop.Value.Equals(0))
                {
                    this.rbNoStup.Checked = true;
                }
                else if (model.AutoStop.Value.Equals(1))
                {
                    this.rbAutoStop.Checked = true;
                }
                else
                {
                    this.rbNoLimit.Checked = true;
                }
                this.lblSequence.Text = model.Sequence.ToString();
                this.lblEnterpriseID.Text = model.EnterpriseID.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x175;
            }
        }

        public int AdPositionID
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["Adid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["Adid"], 0);
                }
                return num;
            }
        }
    }
}

