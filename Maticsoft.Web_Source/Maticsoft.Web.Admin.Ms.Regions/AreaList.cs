namespace Maticsoft.Web.Admin.Ms.Regions
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class AreaList : PageBaseAdmin
    {
        protected int Act_DelData = 0x14c;
        protected int Act_UpdateData = 0x14b;
        private Maticsoft.BLL.Ms.RegionAreas bll = new Maticsoft.BLL.Ms.RegionAreas();
        protected Button btnCancel;
        protected Button btnSave;
        protected GridViewEx gridView;
        protected Label lblMsg;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox tName;
        protected HiddenField txtAreaId;

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
            this.txtAreaId.Value = "";
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            int num = Globals.SafeInt(this.txtAreaId.Value, 0);
            Maticsoft.Model.Ms.RegionAreas model = new Maticsoft.Model.Ms.RegionAreas {
                Name = Globals.HtmlEncode(this.tName.Text.Trim())
            };
            if (num > 0)
            {
                model.AreaId = num;
                if (this.bll.Update(model))
                {
                    this.tName.Text = "";
                    this.txtAreaId.Value = "";
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OnUpdate")
            {
                int areaId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Ms.RegionAreas model = this.bll.GetModel(areaId);
                if (model != null)
                {
                    this.txtAreaId.Value = model.AreaId.ToString();
                    this.tName.Text = model.Name;
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
            int areaId = Globals.SafeInt(this.gridView.DataKeys[e.RowIndex].Value.ToString(), 0);
            this.bll.Delete(areaId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 330;
            }
        }
    }
}

