namespace Maticsoft.Web.Admin.Shop.Order
{
    using Maticsoft.BLL.Shop.Order;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Order;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class OrderLookupItem : PageBaseAdmin
    {
        protected int Act_AddData = 440;
        protected int Act_DelData = 0x1ba;
        protected int Act_UpdateData = 0x1b9;
        private Maticsoft.BLL.Shop.Order.OrderLookupItems bll = new Maticsoft.BLL.Shop.Order.OrderLookupItems();
        protected Button btnCancel;
        protected Button btnSave;
        protected DropDownList ddlCalculateMode;
        protected GridViewEx gridView;
        protected Label lblMsg;
        private Maticsoft.BLL.Shop.Order.OrderLookupList listBll = new Maticsoft.BLL.Shop.Order.OrderLookupList();
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButton Required_N;
        protected RadioButton Required_Y;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox tAppendMoney;
        protected TextBox tDesc;
        protected TextBox tName;
        protected TextBox tTitle;
        protected Literal txtDesc;
        protected HiddenField txtLookupItemId;
        protected Literal txtTitle;

        public void BindData()
        {
            DataSet list = new DataSet();
            list = this.bll.GetList(" LookupListId=" + this.ListId);
            if (list != null)
            {
                this.gridView.DataSetSource = list;
            }
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.tName.Text = "";
            this.tDesc.Text = "";
            this.txtLookupItemId.Value = "";
            this.Required_N.Checked = true;
            this.ddlCalculateMode.SelectedValue = "1";
            this.tTitle.Text = "";
            this.tAppendMoney.Text = "0";
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.txtLookupItemId.Value, 0);
            Maticsoft.Model.Shop.Order.OrderLookupItems model = new Maticsoft.Model.Shop.Order.OrderLookupItems {
                IsInputRequired = this.Required_Y.Checked,
                InputTitle = this.Required_Y.Checked ? this.tTitle.Text : "",
                AppendMoney = new decimal?(Globals.SafeDecimal(this.tAppendMoney.Text, (decimal) 0M)),
                CalculateMode = new int?(Globals.SafeInt(this.ddlCalculateMode.SelectedValue, 0)),
                Name = this.tName.Text.Trim(),
                Remark = this.tDesc.Text.Trim(),
                LookupListId = this.ListId
            };
            if (num > 0)
            {
                model.LookupItemId = num;
                if (this.bll.Update(model))
                {
                    this.tName.Text = "";
                    this.tDesc.Text = "";
                    this.txtLookupItemId.Value = "";
                    this.Required_N.Checked = true;
                    this.ddlCalculateMode.SelectedValue = "1";
                    this.tTitle.Text = "";
                    this.tAppendMoney.Text = "0";
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "编辑失败！请重试。");
                }
            }
            else if (this.bll.Add(model) > 0)
            {
                this.gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, "添加失败！请重试。");
            }
        }

        protected string GetMoneyInfo(object target, object mode)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            if (Globals.SafeInt(mode.ToString(), 1) == 1)
            {
                return ("￥" + Globals.SafeDecimal(target.ToString(), (decimal) 0M));
            }
            return (Globals.SafeDecimal(target.ToString(), (decimal) 0M) + "%");
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OnUpdate")
            {
                int lookupItemId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Shop.Order.OrderLookupItems model = this.bll.GetModel(lookupItemId);
                if (model != null)
                {
                    this.tDesc.Text = model.Remark;
                    this.txtLookupItemId.Value = model.LookupItemId.ToString();
                    this.tName.Text = model.Name;
                    this.ddlCalculateMode.SelectedValue = model.CalculateMode.ToString();
                    if (model.IsInputRequired)
                    {
                        this.Required_Y.Checked = true;
                        this.Required_N.Checked = false;
                    }
                    else
                    {
                        this.Required_Y.Checked = false;
                    }
                    this.tTitle.Text = model.InputTitle;
                    this.tAppendMoney.Text = model.AppendMoney.ToString();
                }
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    LinkButton button2 = (LinkButton) e.Row.FindControl("linkModify");
                    button2.Visible = false;
                }
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lookupItemId = Globals.SafeInt(this.gridView.DataKeys[e.RowIndex].Value.ToString(), 0);
            this.bll.Delete(lookupItemId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                Maticsoft.Model.Shop.Order.OrderLookupList model = this.listBll.GetModel(this.ListId);
                if (model != null)
                {
                    this.txtTitle.Text = "订单可选项【" + model.Name + "】的内容管理";
                    this.txtDesc.Text = "您可以对网站订单可选项【" + model.Name + "】的内容进行添加，编辑，删除等操作";
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1b7;
            }
        }

        public int ListId
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(str))
                {
                    num = Globals.SafeInt(str, 0);
                }
                return num;
            }
        }
    }
}

