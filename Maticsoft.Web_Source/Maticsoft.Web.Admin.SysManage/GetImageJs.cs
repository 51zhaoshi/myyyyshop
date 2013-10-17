namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class GetImageJs : PageBaseAdmin
    {
        protected Button btnSetCollection;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;

        protected void btnSetCollection_Click(object sender, EventArgs e)
        {
            if (GenerateHtml.GenImageJs())
            {
                MessageBox.ShowSuccessTip(this, "图片采集工具生成成功");
            }
            else
            {
                MessageBox.ShowFailTip(this, "图片采集工具生成失败");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xd3;
            }
        }
    }
}

