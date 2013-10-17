namespace Maticsoft.Web.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        protected Button btnCancle;
        protected CheckBox chkIsColor;
        protected CheckBox chkIsCom;
        protected CheckBox chkIsHot;
        protected CheckBox chkIsTop;
        protected Image imgUrl;
        protected Label lblClassID;
        protected Label lblContent;
        protected Label lblContentID;
        protected Label lblCreatedDate;
        protected Label lblCreatedUserID;
        protected Label lblKeywords;
        protected Label lblLastEditDate;
        protected Label lblLastEditUserID;
        protected Label lblLinkUrl;
        protected Label lblOrders;
        protected Label lblPvCount;
        protected Label lblRemary;
        protected Label lblState;
        protected Label lblSubTitle;
        protected Label lblTitle;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal19;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal21;
        protected Literal Literal22;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected HyperLink lnkAttachment;
        public string localVideoUrl = string.Empty;
        public string strClassID = string.Empty;
        public string strid = string.Empty;
        protected TextBox txtSummary;

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("OrdersList.aspx");
        }

        private string GetStatusName(int state)
        {
            switch (state)
            {
                case 0:
                    return "已发布";

                case 1:
                    return "待审核";

                case 2:
                    return "草稿";
            }
            return "已发布";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (this.ClassID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
                if (this.ClassID > 0)
                {
                    this.strClassID = "?classid=" + this.ClassID;
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content model = this.bll.GetModel(this.ContentID);
            if (model != null)
            {
                this.lblContentID.Text = model.ContentID.ToString();
                this.lblTitle.Text = Globals.HtmlDecode(model.Title);
                this.lblSubTitle.Text = Globals.HtmlDecode(model.SubTitle);
                this.txtSummary.Text = Globals.HtmlDecode(model.Summary);
                this.lblContent.Text = model.Description;
                this.imgUrl.ImageUrl = model.ImageUrl;
                this.lblCreatedDate.Text = model.CreatedDate.ToString();
                this.lblCreatedUserID.Text = model.CreatedUserID.ToString();
                this.lblLastEditUserID.Text = model.LastEditUserID.ToString();
                this.lblLastEditDate.Text = model.LastEditDate.ToString();
                this.lblLinkUrl.Text = Globals.HtmlDecode(model.LinkUrl);
                this.lblPvCount.Text = model.PvCount.ToString();
                this.lblState.Text = this.GetStatusName(model.State);
                this.lblClassID.Text = model.ClassID.ToString();
                this.lblKeywords.Text = model.Keywords;
                this.lblOrders.Text = model.Sequence.ToString();
                this.chkIsCom.Checked = model.IsRecomend;
                this.chkIsHot.Checked = model.IsHot;
                this.chkIsColor.Checked = model.IsColor;
                this.chkIsTop.Checked = model.IsTop;
                if (!string.IsNullOrWhiteSpace(model.Attachment))
                {
                    this.lnkAttachment.NavigateUrl = model.Attachment;
                }
                else
                {
                    this.lnkAttachment.Visible = false;
                }
                this.lblRemary.Text = Globals.HtmlDecode(model.Remary);
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 230;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }

        public int ContentID
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

