namespace Maticsoft.Web.Admin.TaoPayment
{
    using Maticsoft.Controls;
    using Maticsoft.Payment.BLL;
    using Maticsoft.Payment.Configuration;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Maticsoft.Web.Validator;
    using System;
    using System.Globalization;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class PaymentModes : PageBaseAdmin
    {
        protected int Act_AddData = 0x41;
        protected int Act_DelData = 0x43;
        protected int Act_UpdateData = 0x42;
        protected GridViewEx grdPaymentMode;
        protected HtmlGenericControl liAdd;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected StatusMessage statusMessage;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        protected void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.grdPaymentMode.Columns[5].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.grdPaymentMode.Columns[6].Visible = false;
            }
            this.grdPaymentMode.DataSource = PaymentModeManage.GetPaymentModes();
            this.grdPaymentMode.DataBind();
            CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
        }

        protected void grdPaymentMode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Sort")
            {
                int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
                string commandName = e.CommandName;
                if (commandName != null)
                {
                    if (!(commandName == "Fall"))
                    {
                        if (!(commandName == "Rise"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (rowIndex != this.grdPaymentMode.Rows.Count)
                        {
                            PaymentModeManage.DescPaymentMode((int) this.grdPaymentMode.DataKeys[rowIndex].Value);
                            this.BindData();
                        }
                        return;
                    }
                    if (rowIndex != 0)
                    {
                        PaymentModeManage.AscPaymentMode((int) this.grdPaymentMode.DataKeys[rowIndex].Value);
                        this.BindData();
                    }
                }
            }
        }

        protected void grdPaymentMode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAFED7';this.style.cursor='pointer';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
                if ((e.Row.RowIndex % 2) == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                Label label = (Label) e.Row.FindControl("lblGatawayType");
                if (label != null)
                {
                    GatewayProvider provider = PayConfiguration.GetConfig().Providers[label.Text.ToLower()] as GatewayProvider;
                    if (provider != null)
                    {
                        label.Text = provider.DisplayName;
                    }
                }
            }
        }

        protected void grdPaymentMode_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (PaymentModeManage.DeletePaymentMode((int) this.grdPaymentMode.DataKeys[e.RowIndex].Value))
            {
                this.BindData();
                this.ShowMsg((string) HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_Message_Delete_Success"), true);
            }
            else
            {
                this.ShowMsg((string) HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_ErrorMessage_UnKownError"), false);
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdPaymentMode.PageIndex = e.NewPageIndex;
            this.grdPaymentMode.OnBind();
        }

        protected void lkbDelectCheck_Click(object sender, EventArgs e)
        {
            int num = 0;
            foreach (GridViewRow row in this.grdPaymentMode.Rows)
            {
                CheckBox box = (CheckBox) row.FindControl("checkboxCol");
                if (box.Checked && PaymentModeManage.DeletePaymentMode(Convert.ToInt32(this.grdPaymentMode.DataKeys[row.RowIndex].Value, CultureInfo.InvariantCulture)))
                {
                    num++;
                }
            }
            if (num == 0)
            {
                this.ShowMsg((string) HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_ErrorMessage_No_Check"), false, true);
            }
            else
            {
                this.BindData();
                this.ShowMsg(string.Format(CultureInfo.InvariantCulture, (string) HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_Message_Delete_Number"), new object[] { num }), true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.grdPaymentMode.RowDataBound += new GridViewRowEventHandler(this.grdPaymentMode_RowDataBound);
            this.grdPaymentMode.RowDeleting += new GridViewDeleteEventHandler(this.grdPaymentMode_RowDeleting);
            this.grdPaymentMode.RowCommand += new GridViewCommandEventHandler(this.grdPaymentMode_RowCommand);
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)))
                {
                    base.GetPermidByActID(this.Act_DelData);
                }
                this.BindData();
                CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
            }
        }

        protected virtual void ShowMsg(string msg, bool success)
        {
            this.ShowMsg(msg, success, false);
        }

        protected void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x40;
            }
        }
    }
}

