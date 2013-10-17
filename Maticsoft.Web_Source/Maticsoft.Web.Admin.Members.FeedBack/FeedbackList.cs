namespace Maticsoft.Web.Admin.Members.FeedBack
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
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

    public class FeedbackList : PageBaseAdmin
    {
        protected int Act_DeleteList = 0x55;
        private Maticsoft.BLL.Members.Feedback bll = new Maticsoft.BLL.Members.Feedback();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList DropType;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected TextBox txtKeyword;
        private Maticsoft.BLL.Members.FeedbackType typeBll = new Maticsoft.BLL.Members.FeedbackType();

        public void BindData()
        {
            DataSet list = new DataSet();
            StringBuilder builder = new StringBuilder();
            int num = Globals.SafeInt(this.DropType.SelectedValue, 0);
            string text = this.txtKeyword.Text;
            builder.Append("  IsSolved=" + this.Type);
            if (num > 0)
            {
                builder.Append("  and TypeId=" + num);
            }
            if (!string.IsNullOrWhiteSpace(text))
            {
                builder.Append("  and Description like '%" + text + "%' ");
            }
            list = this.bll.GetList(builder.ToString());
            this.gridView.DataSetSource = list;
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
            this.gridView.OnBind();
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

        protected string GetTypeName(object target)
        {
            string typeName = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target) && PageValidate.IsNumber(target.ToString()))
            {
                int typeId = Globals.SafeInt(target.ToString(), 0);
                Maticsoft.Model.Members.FeedbackType model = this.typeBll.GetModel(typeId);
                if (model != null)
                {
                    typeName = model.TypeName;
                }
            }
            return typeName;
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
                object obj2 = DataBinder.Eval(e.Row.DataItem, "Description");
                if (((obj2 != null) && (obj2.ToString() != "")) && (obj2.ToString().Length > 20))
                {
                    e.Row.Cells[7].Text = obj2.ToString().Substring(0, 20);
                }
                object obj3 = DataBinder.Eval(e.Row.DataItem, "Result");
                if (((obj3 != null) && (obj3.ToString() != "")) && (obj3.ToString().Length > 20))
                {
                    e.Row.Cells[11].Text = obj3.ToString().Substring(0, 20);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DeleteList)) && (base.GetPermidByActID(this.Act_DeleteList) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                DataSet allList = this.typeBll.GetAllList();
                this.DropType.DataSource = allList;
                this.DropType.DataTextField = "TypeName";
                this.DropType.DataValueField = "TypeId";
                this.DropType.DataBind();
                this.DropType.Items.Insert(0, new ListItem("全部", "0"));
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x54;
            }
        }

        public int Type
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["type"]))
                {
                    num = Globals.SafeInt(base.Request.Params["type"], 0);
                }
                return num;
            }
        }
    }
}

