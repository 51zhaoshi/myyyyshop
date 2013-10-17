namespace Maticsoft.Web.Admin.SNS.Photos
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class ImageReGen : PageBaseAdmin
    {
        protected Literal Literal3;
        protected Literal Literal4;
        private TaskQueue taskBll = new TaskQueue();
        protected TextBox txtFrom;
        protected HiddenField txtIsStatic;
        protected HiddenField txtTaskCount;
        protected HiddenField txtTaskCount_C;
        protected Literal txtTaskDate;
        protected Literal txtTaskId;
        protected Literal txtTaskReCount;
        protected TextBox txtTo;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = this.Page.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x251;
            }
        }
    }
}

