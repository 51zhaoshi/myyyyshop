namespace Maticsoft.Web.CMS.VideoClass
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        protected Label lblDepth;
        protected Label lblParentID;
        protected Label lblPath;
        protected Label lblSequence;
        protected Label lblVideoClassName;
        protected Literal ltlDepth;
        protected Literal ltlName;
        protected Literal ltlParentClass;
        protected Literal ltlPath;
        protected Literal ltlSequence;
        protected Literal ltlShow;
        protected Literal ltlTip;

        public string GetVideoClassNameByParentID(object target)
        {
            Maticsoft.BLL.CMS.VideoClass class2 = new Maticsoft.BLL.CMS.VideoClass();
            string videoClassName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string inputData = target.ToString();
                if (PageValidate.IsNumber(inputData))
                {
                    Maticsoft.Model.CMS.VideoClass modelByParentID = class2.GetModelByParentID(int.Parse(inputData));
                    if (modelByParentID != null)
                    {
                        videoClassName = modelByParentID.VideoClassName;
                    }
                }
            }
            return videoClassName;
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
            Maticsoft.Model.CMS.VideoClass model = new Maticsoft.BLL.CMS.VideoClass().GetModel(this.VideoClassID);
            if (model != null)
            {
                this.lblVideoClassName.Text = model.VideoClassName;
                if (model.ParentID.HasValue)
                {
                    this.lblParentID.Text = this.GetVideoClassNameByParentID(model.ParentID);
                }
                this.lblSequence.Text = model.Sequence.ToString();
                this.lblPath.Text = model.Path;
                this.lblDepth.Text = model.Depth.ToString();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x109;
            }
        }

        public int VideoClassID
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

