namespace Maticsoft.Web.Admin.SNS.PostsVideo
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
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
            Maticsoft.Model.SNS.Posts model = new Maticsoft.BLL.SNS.Posts().GetModel(this.PostID);
            if (model != null)
            {
                this.localVideoUrl = model.VideoUrl;
            }
        }

        public int PostID
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

