namespace Maticsoft.Web.Admin.Shop.Inquiry
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.Shop.Inquiry;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Inquiry;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ShowInquiry : PageBaseAdmin
    {
        protected Button Button2;
        protected GridViewEx gridView;
        protected HiddenField hfOrderMainStatus;
        private Maticsoft.BLL.Shop.Inquiry.InquiryInfo infoBll = new Maticsoft.BLL.Shop.Inquiry.InquiryInfo();
        private Maticsoft.BLL.Shop.Inquiry.InquiryItem itemBll = new Maticsoft.BLL.Shop.Inquiry.InquiryItem();
        protected Label lblAddress;
        protected Label lblCellPhone;
        protected Label lblCompany;
        protected Label lblDate;
        protected Label lblEmail;
        protected Label lblStatus;
        protected Label lblTelephone;
        protected Literal lblTitle;
        protected Label lblUpdateDate;
        protected Label lblUpdateUser;
        protected Label lblUserName;
        protected Literal Literal1;
        protected Literal Literal11;
        protected Literal Literal15;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Label Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        private Regions regionBll = new Regions();
        protected ScriptManager ScriptManager1;
        protected TextBox txtAmount;
        protected TextBox txtLeaveMsg;
        protected TextBox txtReplyMsg;

        public void BindData()
        {
            this.gridView.DataSetSource = this.itemBll.GetList(" InquiryId=" + this.InquiryId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Inquiry.InquiryInfo model = this.infoBll.GetModel((long) this.InquiryId);
            if (model != null)
            {
                if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
                {
                    MessageBox.ShowFailTip(this, "请输入总价！");
                }
                else
                {
                    model.Amount = Globals.SafeDecimal(this.txtAmount.Text, (decimal) 0M);
                    model.Status = 2;
                    model.UpdatedDate = new DateTime?(DateTime.Now);
                    model.UpdatedUserId = base.CurrentUser.UserID;
                    model.ReplyMsg = Globals.HtmlDecode(this.txtReplyMsg.Text);
                    if (this.infoBll.Update(model))
                    {
                        MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                }
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
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
            Maticsoft.Model.Shop.Inquiry.InquiryInfo model = this.infoBll.GetModel((long) this.InquiryId);
            if (model != null)
            {
                this.lblTitle.Text = "正在查看询盘的详细信息";
                User user = new User(model.UpdatedUserId);
                this.lblAddress.Text = this.regionBll.GetFullNameById4Cache(model.RegionId) + model.Address;
                this.lblCellPhone.Text = model.CellPhone;
                this.lblDate.Text = model.CreatedDate.ToString("yyyy-MM-dd");
                this.lblEmail.Text = model.Email;
                this.lblTelephone.Text = model.Telephone;
                this.lblUserName.Text = model.UserName;
                this.lblCompany.Text = model.Company;
                this.txtAmount.Text = model.Amount.ToString();
                this.txtLeaveMsg.Text = model.LeaveMsg;
                this.txtReplyMsg.Text = model.ReplyMsg;
                this.lblUpdateDate.Text = model.UpdatedDate.HasValue ? model.UpdatedDate.Value.ToString("yyyy-MM-dd") : "";
                this.lblUpdateUser.Text = user.UserName;
                this.lblStatus.Text = (model.Status == 1) ? "未处理" : "已处理";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        public int InquiryId
        {
            get
            {
                return Globals.SafeInt(base.Request.Params["id"], 0);
            }
        }
    }
}

