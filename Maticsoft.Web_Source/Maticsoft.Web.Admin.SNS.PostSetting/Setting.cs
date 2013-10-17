namespace Maticsoft.Web.Admin.SNS.PostSetting
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;

    public class Setting : PageBaseAdmin
    {
        protected int Act_UpdateData = 0x73;
        private Maticsoft.BLL.SNS.Posts bll = new Maticsoft.BLL.SNS.Posts();
        protected Button btnSave;
        protected CheckBox cbx_check_picture;
        protected CheckBox cbx_check_product;
        protected CheckBox cbx_Narmal_Audio;
        protected CheckBox cbx_Narmal_Pricture;
        protected CheckBox cbx_Narmal_Video;
        protected CheckBox cbx_Picture;
        protected CheckBox cbx_PostType_All;
        protected CheckBox cbx_PostType_EachOther;
        protected CheckBox cbx_PostType_Fellow;
        protected CheckBox cbx_PostType_ReferMe;
        protected CheckBox cbx_PostType_User;
        protected CheckBox cbx_Product;
        protected CheckBox check_Narmal_word;
        protected CheckBox chk_Blog;
        protected CheckBox chk_check_audio;
        protected CheckBox chk_check_photo;
        protected CheckBox chk_check_video;
        protected CheckBox chk_check_word;
        protected CheckBox chk_OpenComment;
        protected CheckBox chk_OpenPost;
        protected Literal Literal1;
        protected Literal Literal3;
        private PostsSet model = new PostsSet();
        protected TextBox txtBanTopicCount;
        protected TextBox txtBanTopicTime;
        protected int type = -1;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.model._Narmal_Audio = this.cbx_Narmal_Audio.Checked;
            this.model._Narmal_Pricture = this.cbx_Narmal_Pricture.Checked;
            this.model._Narmal_Video = this.cbx_Narmal_Video.Checked;
            this.model._Picture = this.cbx_Picture.Checked;
            this.model._Product = this.cbx_Product.Checked;
            this.model._PostType_EachOther = this.cbx_PostType_EachOther.Checked;
            this.model._PostType_Fellow = this.cbx_PostType_Fellow.Checked;
            this.model._PostType_ReferMe = this.cbx_PostType_ReferMe.Checked;
            this.model._PostType_User = this.cbx_PostType_User.Checked;
            this.model._PostType_All = this.cbx_PostType_All.Checked;
            this.model._Blog = this.chk_Blog.Checked;
            Hashtable hashListByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetHashListByCache(ApplicationKeyType.SNS);
            this.UpdateKey("SNS_check_word", this.chk_check_word.Checked ? "1" : "0", "上传文字的审核状态");
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_word"] = this.chk_check_word.Checked ? "1" : "0";
            }
            this.UpdateKey("SNS_check_audio", this.chk_check_audio.Checked ? "1" : "0", "上传音频的审核状态");
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_audio"] = this.chk_check_audio.Checked ? "1" : "0";
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_check_video"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_check_video", this.chk_check_video.Checked ? "1" : "0", ApplicationKeyType.SNS);
                hashListByCache.Remove("SNS_check_video");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_check_video", this.chk_check_video.Checked ? "1" : "0", "上传视频的审核状态", ApplicationKeyType.SNS);
            }
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_video"] = this.chk_check_audio.Checked ? "1" : "0";
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_check_photo"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_check_photo", this.chk_check_photo.Checked ? "1" : "0", ApplicationKeyType.SNS);
                DataCache.DeleteCache("SNS_check_photo");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_check_photo", this.chk_check_photo.Checked ? "1" : "0", "上传图片的审核状态", ApplicationKeyType.SNS);
            }
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_photo"] = this.chk_check_audio.Checked ? "1" : "0";
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_check_picture"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_check_picture", this.cbx_check_picture.Checked ? "1" : "0", ApplicationKeyType.SNS);
                DataCache.DeleteCache("SNS_check_picture");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_check_picture", this.cbx_check_picture.Checked ? "1" : "0", "分享照片的审核状态", ApplicationKeyType.SNS);
            }
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_picture"] = this.chk_check_audio.Checked ? "1" : "0";
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_check_product"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_check_product", this.cbx_check_product.Checked ? "1" : "0", ApplicationKeyType.SNS);
                DataCache.DeleteCache("SNS_check_product");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_check_product", this.cbx_check_product.Checked ? "1" : "0", "分享照片的审核状态", ApplicationKeyType.SNS);
            }
            if (hashListByCache != null)
            {
                hashListByCache["SNS_check_product"] = this.chk_check_audio.Checked ? "1" : "0";
            }
            this.UpdateKey("SNS_Open_Comment", this.chk_OpenComment.Checked.ToString(), "是否启用社区的评论功能");
            if (hashListByCache != null)
            {
                hashListByCache["SNS_Open_Comment"] = this.chk_OpenComment.Checked;
            }
            this.UpdateKey("SNS_Open_Post", this.chk_OpenPost.Checked.ToString(), "是否启用社区的发表动态功能");
            if (hashListByCache != null)
            {
                hashListByCache["SNS_Open_Post"] = this.chk_OpenPost.Checked;
            }
            this.UpdateKey("SNS_BAN_TOPIC_TIME", this.txtBanTopicTime.Text, "SNS连续发帖时长(分钟)");
            Maticsoft.BLL.SysManage.ConfigSystem.ClearCacheByKey("SNS_BAN_TOPIC_TIME");
            this.UpdateKey("SNS_BAN_TOPIC_COUNT", this.txtBanTopicCount.Text, "SNS连续发帖禁用数");
            Maticsoft.BLL.SysManage.ConfigSystem.ClearCacheByKey("SNS_BAN_TOPIC_COUNT");
            Maticsoft.BLL.SNS.ConfigSystem.UpdatePostSet(this.model);
            LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "全局更新动态默认审核状态成功", this);
            MessageBox.ShowSuccessTip(this, "保存成功");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                PostsSet postSetByCache = Maticsoft.BLL.SNS.ConfigSystem.GetPostSetByCache();
                this.cbx_Narmal_Audio.Checked = postSetByCache._Narmal_Audio;
                this.cbx_Narmal_Pricture.Checked = postSetByCache._Narmal_Pricture;
                this.cbx_Narmal_Video.Checked = postSetByCache._Narmal_Video;
                this.cbx_Picture.Checked = postSetByCache._Picture;
                this.cbx_Product.Checked = postSetByCache._Product;
                this.cbx_PostType_EachOther.Checked = postSetByCache._PostType_EachOther;
                this.cbx_PostType_Fellow.Checked = postSetByCache._PostType_Fellow;
                this.cbx_PostType_ReferMe.Checked = postSetByCache._PostType_ReferMe;
                this.cbx_PostType_User.Checked = postSetByCache._PostType_User;
                this.cbx_PostType_All.Checked = postSetByCache._PostType_All;
                this.chk_Blog.Checked = postSetByCache._Blog;
                string valueByCache = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
                this.chk_check_audio.Checked = (valueByCache != null) && (Globals.SafeInt(valueByCache, 0) == 1);
                string text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_audio");
                this.chk_check_audio.Checked = (text != null) && (Globals.SafeInt(text, 0) == 1);
                string str3 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_video");
                this.chk_check_video.Checked = (str3 != null) && (Globals.SafeInt(str3, 0) == 1);
                string str4 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_photo");
                this.chk_check_photo.Checked = (str4 != null) && (Globals.SafeInt(str4, 0) == 1);
                string str5 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_picture");
                this.cbx_check_picture.Checked = (str5 != null) && (Globals.SafeInt(str5, 0) == 1);
                string str6 = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
                this.cbx_check_product.Checked = (str6 != null) && (Globals.SafeInt(str6, 0) == 1);
                this.chk_OpenComment.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Open_Comment");
                this.chk_OpenPost.Checked = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Open_Post");
                this.txtBanTopicTime.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_BAN_TOPIC_TIME");
                this.txtBanTopicCount.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_BAN_TOPIC_COUNT");
            }
        }

        public bool UpdateKey(string keyName, string value, string desc)
        {
            return Maticsoft.BLL.SysManage.ConfigSystem.Modify(keyName, value, desc, ApplicationKeyType.SNS);
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x72;
            }
        }
    }
}

