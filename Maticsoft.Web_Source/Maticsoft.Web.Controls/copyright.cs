namespace Maticsoft.Web.Controls
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Components;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class copyright : UserControl
    {
        protected Literal litCopyright;
        protected Literal LitWebRecord;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.litCopyright.Text = (!MvcApplication.IsAuthorize ? string.Format("Powered by <a href=\"http://www.maticsoft.com/\" target=\"_blank\" style=\"color: #333;\" >{0}</a> {1} \x00a9 2011-{2} Maticsoft Inc. <br />", MvcApplication.ProductInfo, MvcApplication.Version, DateTime.Now.Year) : "") + ConfigSystem.GetValueByCache("WebPowerBy");
            this.LitWebRecord.Text = base.Server.HtmlEncode(ConfigSystem.GetValueByCache("WebRecord"));
        }
    }
}

