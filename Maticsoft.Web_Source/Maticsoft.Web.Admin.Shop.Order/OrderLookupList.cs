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

    public class OrderLookupList : PageBaseAdmin
    {
        protected int Act_AddData = 0x1b4;
        protected int Act_DelData = 0x1b6;
        protected int Act_UpdateData = 0x1b5;
        private Maticsoft.BLL.Shop.Order.OrderLookupList bll = new Maticsoft.BLL.Shop.Order.OrderLookupList();
        protected Button btnCancel;
        protected Button btnSave;
        protected DropDownList ddlType;
        protected GridViewEx gridView;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal7;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox tDesc;
        protected TextBox tName;
        protected HiddenField txtLookupListId;

        public void BindData()
        {
            DataSet allList = new DataSet();
            allList = this.bll.GetAllList();
            if (allList != null)
            {
                this.gridView.DataSetSource = allList;
            }
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.tName.Text = "";
            this.tDesc.Text = "";
            this.txtLookupListId.Value = "";
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.txtLookupListId.Value, 0);
            Maticsoft.Model.Shop.Order.OrderLookupList model = new Maticsoft.Model.Shop.Order.OrderLookupList {
                SelectMode = Globals.SafeInt(this.ddlType.SelectedValue, 1),
                Name = this.tName.Text.Trim(),
                Description = this.tDesc.Text.Trim()
            };
            if (num > 0)
            {
                model.LookupListId = num;
                if (this.bll.Update(model))
                {
                    this.tName.Text = "";
                    this.tDesc.Text = "";
                    this.txtLookupListId.Value = "";
                    this.ddlType.SelectedValue = "1";
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

        protected string GetModeName(object target)
        {
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target))
            {
                return str;
            }
            switch (target.ToString())
            {
                case "1":
                    return "下拉列表";

                case "2":
                    return "单选按钮";

                case "3":
                    return "复选框";
            }
            return "未知";
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
                int lookupListId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Shop.Order.OrderLookupList model = this.bll.GetModel(lookupListId);
                if (model != null)
                {
                    this.tDesc.Text = model.Description;
                    this.txtLookupListId.Value = model.LookupListId.ToString();
                    this.tName.Text = model.Name;
                    this.ddlType.SelectedValue = model.SelectMode.ToString();
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
            int lookupListId = Globals.SafeInt(this.gridView.DataKeys[e.RowIndex].Value.ToString(), 0);
            this.bll.Delete(lookupListId);
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
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1b3;
            }
        }
    }
}

