namespace Maticsoft.Web.Settings.MainMenus
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x185;
        protected int Act_DelData = 0x187;
        protected int Act_UpdateData = 390;
        private MainMenus bll = new MainMenus();
        protected Button btnAddtwo;
        protected Button btnDelete;
        protected Button btnDeletetwo;
        protected Button btnSearch;
        protected Button butAdd;
        protected DropDownList ddlType;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal9;
        protected TextBox txtKeyword;

        public void BindData()
        {
            string strWhere = "";
            this.Session["MainMenuslistPageIndex"] = 0;
            if ((this.Session["strWhereMainMenuslist"] != null) && (this.Session["strWhereMainMenuslist"].ToString() != ""))
            {
                strWhere = this.Session["strWhereMainMenuslist"].ToString();
            }
            DataSet set = new DataSet();
            set = this.bll.GetList(-1, strWhere, " Sequence");
            this.gridView.DataSetSource = set;
            this.Session["MainMenuslistPageIndex"] = this.gridView.PageIndex;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                this.bll.DeleteList(selIDlist);
                MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Session["strWhereMainMenuslist"] = "";
            this.gridView.PageIndex = 0;
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(this.ddlType.SelectedValue, -1);
            if (num != -1)
            {
                builder.AppendFormat("NavArea ={0}", num);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (!string.IsNullOrWhiteSpace(builder.ToString()))
                {
                    builder.Append("And  ");
                }
                builder.AppendFormat("  MenuName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            this.Session["strWhereMainMenuslist"] = builder.ToString();
            this.gridView.OnBind();
        }

        protected string GetAreaName(object target)
        {
            int num = Globals.SafeInt(target.ToString(), -1);
            switch (num)
            {
                case 0:
                    return "CMS";

                case 1:
                    return "SNS";

                case 2:
                    return "Shop";
            }
            return "未知区域";
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.gridView.Rows.Count; i++)
            {
                CheckBox box = (CheckBox) this.gridView.Rows[i].FindControl(this.gridView.CheckBoxID);
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (this.gridView.DataKeys[i].Value != null)
                    {
                        str = str + this.gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected string GetURLType(object target)
        {
            int num = Globals.SafeInt(target.ToString(), -1);
            switch (num)
            {
                case 0:
                    return "自定义";

                case 1:
                    return "CMS文章栏目";

                case 2:
                    return "社区商品分类";

                case 3:
                    return "社区图片分类";

                case 4:
                    return "商城商品分类";
            }
            return "自定义";
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
                LinkButton button = (LinkButton) e.Row.FindControl("LinkButton1");
                HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("spanmodify");
                HiddenField field = (HiddenField) e.Row.FindControl("HiddenField1");
                field.Value = DataBinder.Eval(e.Row.DataItem, "MenuID").ToString();
                CheckBox box = (CheckBox) e.Row.FindControl(this.gridView.CheckBoxID);
                object obj2 = DataBinder.Eval(e.Row.DataItem, "MenuType");
                if ((obj2 != null) && (obj2.ToString() == "0"))
                {
                    button.Visible = false;
                    box.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    control.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HiddenField field = (HiddenField) this.gridView.Rows[e.RowIndex].FindControl("HiddenField1");
            int menuID = int.Parse(field.Value);
            this.bll.Delete(menuID);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                    this.btnDeletetwo.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.butAdd.Visible = false;
                    this.btnAddtwo.Visible = false;
                }
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                if (!string.IsNullOrWhiteSpace(base.Request.Params["all"]))
                {
                    this.Session["strWhereMainMenuslist"] = "";
                    this.Session["MainMenuslistPageIndex"] = 0;
                }
                if ((this.Session["MainMenuslistPageIndex"] != null) && (this.Session["MainMenuslistPageIndex"].ToString() != ""))
                {
                    this.gridView.PageIndex = (int) this.Session["MainMenuslistPageIndex"];
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x182;
            }
        }
    }
}

