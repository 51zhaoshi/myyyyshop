namespace Maticsoft.Web.Admin.Ms.WeiBo
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public class Publish : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserBind bindBll = new Maticsoft.BLL.Members.UserBind();
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal4;
        protected Literal Literal6;
        private Maticsoft.BLL.Ms.WeiBoMsg msgBll = new Maticsoft.BLL.Ms.WeiBoMsg();
        protected TextBox txtDesc;
        protected Image txtimgUrl;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtDesc.Text.Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, "微博消息不能为空！");
            }
            else
            {
                Maticsoft.Model.Ms.WeiBoMsg model = new Maticsoft.Model.Ms.WeiBoMsg {
                    CreateDate = DateTime.Now,
                    WeiboMsg = str,
                    ImageUrl = this.txtimgUrl.ImageUrl
                };
                this.msgBll.Add(model);
                List<Maticsoft.Model.Members.UserBind> modelList = this.bindBll.GetModelList(" userid=" + base.CurrentUser.UserID);
                if ((modelList == null) || (modelList.Count == 0))
                {
                    MessageBox.ShowFailTip(this, "该帐号没有绑定任何微博，请先绑定微博！");
                }
                else
                {
                    string mediaIDs = string.Join<int>(",", from c in modelList select c.MediaID);
                    string url = "http://" + Globals.DomainFullName;
                    this.bindBll.SendWeiBo(-1, mediaIDs, str, url, "");
                    MessageBox.ShowSuccessTip(this, "发布成功！");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x15a;
            }
        }
    }
}

