namespace Maticsoft.Web.Installer
{
    using Maticsoft.Components;
    using System;
    using System.Web.UI;

    public class Default : Page
    {
        public string strTitle;

        protected void Page_Load(object sender, EventArgs e)
        {
            string productInfo = MvcApplication.ProductInfo;
            if (productInfo != null)
            {
                if (!(productInfo == "Maticsoft SNS"))
                {
                    if (productInfo == "Maticsoft Shop")
                    {
                        this.strTitle = "动软商城系统";
                        goto Label_004A;
                    }
                }
                else
                {
                    this.strTitle = "动软分享社区系统";
                    goto Label_004A;
                }
            }
            this.strTitle = "动软.NET系统框架";
        Label_004A:
            if (MvcApplication.IsInstall)
            {
                base.Response.Redirect("/", true);
            }
        }
    }
}

