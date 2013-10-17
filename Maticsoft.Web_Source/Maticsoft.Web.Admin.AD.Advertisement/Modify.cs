namespace Maticsoft.Web.Admin.AD.Advertisement
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected HtmlGenericControl FlashPlay;
        protected HiddenField hfFileUrl;
        protected HiddenField hfSwfUrl;
        protected HiddenField HiddenField_ISModifyImage;
        protected Image imgAd;
        protected HtmlGenericControl imgShow;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal litVideo;
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
        protected TextBox txtAdvertisementName;
        protected HtmlGenericControl txtAdvertisementNameTip;
        protected TextBox txtAdvHtml;
        protected TextBox txtAlternateText;
        protected TextBox txtCPMPrice;
        protected TextBox txtDayMaxIP;
        protected TextBox txtDayMaxPV;
        protected TextBox txtEndDate;
        protected TextBox txtEnterpriseID;
        protected TextBox txtImpressions;
        protected TextBox txtNavigateUrl;
        protected TextBox txtStartDate;
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void AltInfo()
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(this.AdPositionID);
            if (model != null)
            {
                this.Literal2.Text = model.AdvPositionName + "】广告位修改广告内容";
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Settings.Advertisement advertisement = new Maticsoft.BLL.Settings.Advertisement();
            Maticsoft.Model.Settings.Advertisement model = advertisement.GetModel(this.AdvertisementId);
            model.AdvertisementName = this.txtAdvertisementName.Text;
            string s = string.Empty;
            if (this.rbTextContent.Checked)
            {
                s = "0";
            }
            else if (this.rbImgContent.Checked)
            {
                s = "1";
            }
            else if (this.rbFlashContent.Checked)
            {
                s = "2";
            }
            else
            {
                s = "3";
            }
            string oldValue = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string newValue = string.Format("/Upload/AD/{0}", this.AdPositionID);
            ArrayList fileNameList = new ArrayList();
            if (s.Equals("1"))
            {
                if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                {
                    MessageBox.ShowFailTip(this, "请选择要上传的图片！");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                {
                    string str4 = string.Format(this.hfFileUrl.Value, "");
                    fileNameList.Add(str4.Replace(oldValue, ""));
                    model.FileUrl = str4.Replace(oldValue, newValue);
                }
                else
                {
                    model.FileUrl = this.hfFileUrl.Value;
                }
            }
            if (s.Equals("2"))
            {
                model.FileUrl = this.hfSwfUrl.Value;
            }
            if (s.Equals("3"))
            {
                if (string.IsNullOrWhiteSpace(this.txtAdvHtml.Text))
                {
                    MessageBox.ShowFailTip(this, "广告HTML代码不能为空！");
                    return;
                }
                model.AdvHtml = this.txtAdvHtml.Text;
            }
            model.ContentType = new int?(int.Parse(s));
            model.AlternateText = this.txtAlternateText.Text;
            model.NavigateUrl = this.txtNavigateUrl.Text;
            if (!PageValidate.IsNumber(this.txtImpressions.Text))
            {
                MessageBox.ShowFailTip(this, "显示频率格式不正确！");
            }
            else
            {
                model.Impressions = new int?(int.Parse(this.txtImpressions.Text));
                if (this.rbStatusY.Checked)
                {
                    model.State = 1;
                }
                else if (this.rbStatusN.Checked)
                {
                    model.State = 0;
                }
                else
                {
                    model.State = -1;
                }
                if (!string.IsNullOrWhiteSpace(this.txtStartDate.Text))
                {
                    if (!PageValidate.IsDateTime(this.txtStartDate.Text))
                    {
                        MessageBox.ShowFailTip(this, "请输入正确的开始时间！");
                        return;
                    }
                    model.StartDate = new DateTime?(DateTime.Parse(this.txtStartDate.Text));
                }
                if (!string.IsNullOrWhiteSpace(this.txtEndDate.Text))
                {
                    if (!PageValidate.IsDateTime(this.txtEndDate.Text))
                    {
                        MessageBox.ShowFailTip(this, "请输入正确的结束时间！");
                        return;
                    }
                    model.EndDate = new DateTime?(DateTime.Parse(this.txtEndDate.Text));
                }
                if (!PageValidate.IsNumber(this.txtDayMaxPV.Text))
                {
                    MessageBox.ShowFailTip(this, "最大PV格式不正确！");
                }
                else
                {
                    model.DayMaxPV = new int?(int.Parse(this.txtDayMaxPV.Text));
                    if (!PageValidate.IsNumber(this.txtDayMaxIP.Text))
                    {
                        MessageBox.ShowFailTip(this, "最大IP格式不正确！");
                    }
                    else
                    {
                        model.DayMaxIP = new int?(int.Parse(this.txtDayMaxIP.Text));
                        if (string.IsNullOrWhiteSpace(this.txtCPMPrice.Text))
                        {
                            MessageBox.ShowFailTip(this, "请输入正确的价格！");
                        }
                        else
                        {
                            decimal result = 0M;
                            if (!decimal.TryParse(this.txtCPMPrice.Text, out result))
                            {
                                MessageBox.ShowFailTip(this, "价格格式不正确！");
                            }
                            else
                            {
                                model.CPMPrice = new decimal?(result);
                                if (this.rbAutoStop.Checked)
                                {
                                    model.AutoStop = 1;
                                }
                                else if (this.rbNoStup.Checked)
                                {
                                    model.AutoStop = 0;
                                }
                                else
                                {
                                    model.AutoStop = -1;
                                }
                                if (!string.IsNullOrWhiteSpace(this.txtEnterpriseID.Text))
                                {
                                    string text = this.txtEnterpriseID.Text;
                                    Maticsoft.BLL.Ms.Enterprise enterprise = new Maticsoft.BLL.Ms.Enterprise();
                                    if (!string.IsNullOrWhiteSpace(text))
                                    {
                                        List<Maticsoft.Model.Ms.Enterprise> modelByEnterpriseName = enterprise.GetModelByEnterpriseName(text);
                                        if (modelByEnterpriseName.Count <= 0)
                                        {
                                            MessageBox.ShowFailTip(this, "没有找到相应商户，请重新输入！");
                                            return;
                                        }
                                        model.EnterpriseID = new int?(modelByEnterpriseName[0].EnterpriseID);
                                    }
                                    else
                                    {
                                        model.EnterpriseID = -1;
                                    }
                                }
                                else
                                {
                                    model.EnterpriseID = -1;
                                }
                                if (advertisement.Update(model))
                                {
                                    string url = string.Format("SingleList.aspx?id={0}", this.AdPositionID);
                                    this.btnCancle.Enabled = false;
                                    this.btnSave.Enabled = false;
                                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                                    {
                                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                                    }
                                    MessageBox.ShowSuccessTip(this, "保存成功", url);
                                }
                                else
                                {
                                    MessageBox.ShowFailTip(this, "网络异常，请稍后再试！");
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.AltInfo();
                this.ShowInfo(this.AdvertisementId);
            }
        }

        private void ShowInfo(int AdvertisementId)
        {
            Maticsoft.Model.Settings.Advertisement model = new Maticsoft.BLL.Settings.Advertisement().GetModel(AdvertisementId);
            this.txtAdvertisementName.Text = model.AdvertisementName;
            switch (model.ContentType.Value)
            {
                case 0:
                    this.rbTextContent.Checked = true;
                    this.imgShow.Visible = false;
                    this.FlashPlay.Visible = false;
                    break;

                case 1:
                    this.rbImgContent.Checked = true;
                    this.hfFileUrl.Value = model.FileUrl;
                    this.imgShow.Visible = true;
                    this.FlashPlay.Visible = false;
                    break;

                case 2:
                    this.rbFlashContent.Checked = true;
                    this.hfSwfUrl.Value = model.FileUrl;
                    this.imgShow.Visible = false;
                    this.FlashPlay.Visible = true;
                    this.litVideo.Text = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" width=\"200\" height=\"170\"  ></a></li><param name=\"wmode\" value=\"opaque\" /><param name=\"quality\" value=\"high\" /><param name=\"movie\" value=\"" + model.FileUrl + "\" /><embed src=\"" + model.FileUrl + "\" allowfullscreen=\"true\" quality=\"high\" width=\"200\" height=\"170\"\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object></td></tr>";
                    break;

                case 3:
                    this.rbCodeContent.Checked = true;
                    this.imgShow.Visible = false;
                    this.FlashPlay.Visible = false;
                    break;
            }
            this.txtAdvHtml.Text = model.AdvHtml;
            this.txtAlternateText.Text = model.AlternateText;
            this.txtCPMPrice.Text = model.CPMPrice.Value.ToString("0.00");
            this.txtDayMaxIP.Text = model.DayMaxIP.ToString();
            this.txtDayMaxPV.Text = model.DayMaxPV.ToString();
            this.txtEndDate.Text = model.EndDate.HasValue ? model.EndDate.Value.ToString("yyyy-MM-dd") : "";
            this.imgAd.ImageUrl = model.FileUrl;
            Maticsoft.Model.Ms.Enterprise enterprise2 = new Maticsoft.BLL.Ms.Enterprise().GetModel(model.EnterpriseID.Value);
            if (enterprise2 != null)
            {
                this.txtEnterpriseID.Text = enterprise2.Name;
            }
            else
            {
                this.txtEnterpriseID.Text = "";
            }
            this.txtImpressions.Text = model.Impressions.Value.ToString();
            this.txtNavigateUrl.Text = model.NavigateUrl;
            this.txtStartDate.Text = model.StartDate.HasValue ? model.StartDate.Value.ToString("yyyy-MM-dd") : "";
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
            switch (model.State.Value)
            {
                case -1:
                    this.rbStop.Checked = true;
                    return;

                case 0:
                    this.rbStatusN.Checked = true;
                    return;

                case 1:
                    this.rbStatusY.Checked = true;
                    return;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x174;
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

        public int AdvertisementId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

