namespace Maticsoft.Web.Admin.CMS.Video
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class VideoPreview : PageBaseAdmin
    {
        protected HtmlForm form1;
        public string localVideoUrl = "";
        protected Literal ltlTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ShowInfo();
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Video model = new Maticsoft.BLL.CMS.Video().GetModel(this.VideoID);
            if (model != null)
            {
                string videoUrl = model.VideoUrl;
                int urlType = model.UrlType;
                if (!string.IsNullOrWhiteSpace(videoUrl) && (urlType == 0))
                {
                    this.localVideoUrl = videoUrl;
                }
            }
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

