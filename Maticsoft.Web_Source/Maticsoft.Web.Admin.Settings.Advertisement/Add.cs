namespace Maticsoft.Web.Admin.Settings.Advertisement
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using Maticsoft.Web.Validator;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
        private Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new Maticsoft.BLL.Settings.AdvertisePosition();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIsValid;
        protected HiddenField hfFileUrl;
        protected HiddenField hfSwfUrl;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RadioButton rbAutoStop;
        protected RadioButton rbCodeContent;
        protected RadioButton rbFlashContent;
        protected RadioButton rbImgContent;
        protected RadioButton rbNoLimit;
        protected RadioButton rbNoStup;
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
                this.Literal2.Text = model.AdvPositionName + "】广告位新增广告内容";
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.Advertisement model = new Maticsoft.Model.Settings.Advertisement {
                AdvPositionId = new int?(this.AdPositionID)
            };
            if (string.IsNullOrWhiteSpace(this.txtAdvertisementName.Text))
            {
                MessageBox.ShowFailTip(this, "广告名称不能为空！");
            }
            else
            {
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
                    string str4 = string.Format(this.hfFileUrl.Value, "");
                    fileNameList.Add(str4.Replace(oldValue, ""));
                    model.FileUrl = str4.Replace(oldValue, newValue);
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
                    model.CreatedDate = new DateTime?(DateTime.Now);
                    model.CreatedUserID = new int?(base.CurrentUser.UserID);
                    if (this.chkIsValid.Checked)
                    {
                        model.State = 1;
                    }
                    else
                    {
                        model.State = 0;
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
                                    model.Sequence = new int?(this.bll.GetMaxSequence());
                                    string text = this.txtEnterpriseID.Text;
                                    Enterprise enterprise = new Enterprise();
                                    if (!string.IsNullOrWhiteSpace(text))
                                    {
                                        List<Enterprise> modelByEnterpriseName = enterprise.GetModelByEnterpriseName(text);
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
                                    if (this.bll.Add(model))
                                    {
                                        string url = string.Format("SingleList.aspx?id={0}", this.AdPositionID);
                                        this.btnCancle.Enabled = false;
                                        this.btnSave.Enabled = false;
                                        if (!string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
                else
                {
                    this.AltInfo();
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x173;
            }
        }

        public int AdPositionID
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

