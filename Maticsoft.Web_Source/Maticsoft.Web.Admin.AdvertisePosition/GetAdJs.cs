namespace Maticsoft.Web.Admin.AdvertisePosition
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class GetAdJs : PageBaseAdmin
    {
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtJs;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(base.Request.Params["k"]))
                {
                    int advPositionId = Globals.SafeInt(str, 0);
                    if (new AdvertisePosition().Exists(advPositionId))
                    {
                        string authority = base.Request.Url.Authority;
                        this.txtJs.Text = string.Format("<script src=\"http://{0}/Scripts/Maticsoftpic.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'http://{0}&c={1}&a=0'; </script>  ", authority, str);
                    }
                    else
                    {
                        this.txtJs.Text = "广告位不存在，请重试！";
                    }
                }
                else
                {
                    int num2 = Globals.SafeInt(str, 0);
                    if (new AdvertisePosition().Exists(num2))
                    {
                        string text1 = base.Request.Url.Authority;
                        this.txtJs.Text = string.Format("<script src=\"/Scripts/Maticsoftpic.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = '&c={0}&a=0'; </script>  ", str);
                    }
                    else
                    {
                        this.txtJs.Text = "广告位不存在，请重试！";
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x171;
            }
        }
    }
}

