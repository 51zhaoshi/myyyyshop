namespace Maticsoft.Web.Admin.Ms.WaterMarks
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Setting : PageBaseAdmin
    {
        protected Button btnSave;
        protected CheckBox chkHD;
        protected CheckBox chkThum;
        protected DropDownList ddlFont;
        protected DropDownList ddlPosition;
        protected DropDownList ddlType;
        protected HiddenField hfLogoUrl;
        private Hashtable ht;
        protected Literal Literal1;
        protected Literal Literal16;
        protected Literal Literal32;
        protected Literal Literal4;
        protected HyperLink lnkDelete;
        protected HtmlImage logo;
        protected RadioButtonList radlStatus;
        protected Button ReSet;
        protected TextBox txtColor;
        protected HiddenField txtIsAddWater;
        protected TextBox txtTransparent;
        protected TextBox txtWaterWords;
        protected TextBox txtWordsSize;

        private void BindData()
        {
            string str = ConfigSystem.GetValue("System_ThumbImage_AddWater");
            this.radlStatus.SelectedValue = str;
            this.txtIsAddWater.Value = str;
            string valueByCache = ConfigSystem.GetValueByCache("System_waterMarkType");
            if (!string.IsNullOrEmpty(valueByCache))
            {
                this.ddlType.SelectedValue = valueByCache;
            }
            string str3 = ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (!string.IsNullOrEmpty(str3))
            {
                this.txtWaterWords.Text = str3;
            }
            string str4 = ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (!string.IsNullOrEmpty(str4))
            {
                this.ddlFont.SelectedValue = str4;
            }
            string str5 = ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (!string.IsNullOrEmpty(str5))
            {
                this.txtWordsSize.Text = str5;
            }
            string str6 = ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (!string.IsNullOrEmpty(str6))
            {
                this.ddlPosition.SelectedValue = str6;
            }
            string str7 = ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (!string.IsNullOrEmpty(str7))
            {
                this.txtColor.Text = str7;
            }
            string str8 = ConfigSystem.GetValueByCache("System_IswaterMarkThumPic");
            if (!string.IsNullOrEmpty(str8) && (Globals.SafeInt(str8, 0) > 0))
            {
                this.chkThum.Checked = true;
            }
            string text = ConfigSystem.GetValueByCache("System_IswaterMarkHDPic");
            if (!string.IsNullOrEmpty(str8) && (Globals.SafeInt(text, 0) > 0))
            {
                this.chkHD.Checked = true;
            }
            string str10 = ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (!string.IsNullOrEmpty(str10))
            {
                this.txtTransparent.Text = str10;
            }
            string str11 = ConfigSystem.GetValueByCache("System_waterMarkPhotoUrl");
            if (!string.IsNullOrEmpty(str11))
            {
                this.logo.Src = str11;
                this.hfLogoUrl.Value = str11;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.ht = ConfigSystem.GetHashListByCache(ApplicationKeyType.Shop);
            int num = 0;
            num += this.UpdateConfigSystem("System_waterMarkType", "生成水印的类型", this.ddlType.SelectedValue, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkContent", "生成水印的内容", this.txtWaterWords.Text, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkFont", "生成水印的内容的字体", this.ddlFont.SelectedValue, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkFontSize", "生成水印的内容的字体的大小", this.txtWordsSize.Text, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkPosition", "生成水印内容的位置", this.ddlPosition.SelectedValue, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_IswaterMarkThumPic", "是否生成给缩略图生成水印(0:否 1：是)", this.chkThum.Checked ? "1" : "0", ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_IswaterMarkHDPic", "是否生成给清晰图生成水印(0:否 1：是)", this.chkHD.Checked ? "1" : "0", ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkTransparent", "水印的透明度", this.txtTransparent.Text, ApplicationKeyType.Shop);
            num += this.UpdateConfigSystem("System_waterMarkFontColor", "水印的颜色", this.txtColor.Text, ApplicationKeyType.Shop);
            if (this.ddlType.SelectedValue == "1")
            {
                if (string.IsNullOrEmpty(this.hfLogoUrl.Value))
                {
                    MessageBox.ShowSuccessTip(this, "请上传文件");
                    return;
                }
                num += this.UpdateConfigSystem("System_waterMarkPhotoUrl", "水印的图片", this.hfLogoUrl.Value, ApplicationKeyType.Shop);
                this.logo.Src = this.hfLogoUrl.Value;
            }
            if (num == 0)
            {
                MessageBox.ShowSuccessTip(this, "保存成功");
            }
            else
            {
                MessageBox.ShowFailTip(this, "出现异常，请重试");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void radlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = this.radlStatus.SelectedValue;
            ConfigSystem.Modify("System_ThumbImage_AddWater", selectedValue, "图片缩略图是否添加水印", ApplicationKeyType.System);
            base.Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);
            base.Response.Redirect("Setting.aspx");
        }

        protected void ReSet_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Setting.aspx");
        }

        private int UpdateConfigSystem(string key, string description, string value, ApplicationKeyType type = 3)
        {
            try
            {
                ConfigSystem.Modify(key, value, description, type);
                if (this.ht != null)
                {
                    this.ht[key] = value;
                }
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x155;
            }
        }
    }
}

