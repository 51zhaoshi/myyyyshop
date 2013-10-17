namespace Maticsoft.Web.Admin.Options
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Model.Poll;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected int Act_AddData = 0x169;
        protected int Act_DelData = 0x16a;
        private Maticsoft.BLL.Poll.Options blloption = new Maticsoft.BLL.Poll.Options();
        private Maticsoft.BLL.Poll.Topics blltop = new Maticsoft.BLL.Poll.Topics();
        protected Button btnAdd;
        protected Button btnBack;
        protected Button btnDelete;
        protected CheckBox chkisChecked;
        protected GridViewEx gridView;
        protected Label Label1;
        protected Label lblTitle;
        protected Label lblTopicID;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected TextBox txtName;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[4].Visible = false;
            }
            string strWhere = "";
            if ((this.Session["strWhereOptions"] != null) && (this.Session["strWhereOptions"].ToString() != ""))
            {
                strWhere = strWhere + this.Session["strWhereOptions"].ToString();
            }
            this.gridView.DataSetSource = this.blloption.GetList(strWhere);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.ShowFailTip(this, Poll.ErrorOptionsNotNull);
            }
            else
            {
                string text = this.txtName.Text;
                int num = int.Parse(this.lblTopicID.Text);
                int num2 = this.chkisChecked.Checked ? 1 : 0;
                Maticsoft.Model.Poll.Options model = new Maticsoft.Model.Poll.Options {
                    Name = text,
                    TopicID = new int?(num),
                    isChecked = new int?(num2),
                    SubmitNum = 0
                };
                if (this.blloption.Exists(num, text))
                {
                    MessageBox.ShowFailTip(this, Poll.ErrorOptionsExists);
                }
                else
                {
                    this.blloption.Add(model);
                    this.Session["strWhereOptions"] = " TopicID= " + this.lblTopicID.Text;
                    this.gridView.OnBind();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string inputData = this.lblTopicID.Text.Trim();
            if (PageValidate.IsNumber(inputData))
            {
                Maticsoft.Model.Poll.Topics model = this.blltop.GetModel(Convert.ToInt32(inputData));
                if ((model != null) && model.FormID.HasValue)
                {
                    base.Response.Redirect("../Topics/index.aspx?fid=" + model.FormID);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.blloption.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string str = this.gridView.DataKeys[e.RowIndex].Value.ToString();
            this.blloption.Delete(Convert.ToInt32(str));
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnAdd.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
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
                string str2 = base.Request.Params["tid"];
                if (!string.IsNullOrWhiteSpace(str2) && PageValidate.IsNumber(str2))
                {
                    Maticsoft.Model.Poll.Topics model = this.blltop.GetModel(Convert.ToInt32(str2));
                    if (model != null)
                    {
                        this.lblTopicID.Text = model.ID.ToString();
                        this.lblTitle.Text = model.Title;
                    }
                    this.Session["strWhereOptions"] = " TopicID= " + this.lblTopicID.Text;
                }
                this.gridView.OnBind();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 360;
            }
        }

        public Basic Master
        {
            get
            {
                return (Basic) base.Master;
            }
        }
    }
}

