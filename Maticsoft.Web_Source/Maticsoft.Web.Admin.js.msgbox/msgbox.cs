namespace Maticsoft.Web.Admin.js.msgbox
{
    using Maticsoft.Common;
    using Maticsoft.Web;
    using System;

    public class msgbox : PageBaseAdmin
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.ShowServerBusyTip(this, "服务器繁忙，请稍后再试。");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.ShowSuccessTip(this, "设置成功！");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.ShowFailTip(this, "数据拉取失败！");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.ShowLoadingTip(this, "正在加载中，请稍后...");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}

