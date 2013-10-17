namespace Maticsoft.Web.Admin.Poll.Users
{
    using Maticsoft.BLL.Poll;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.Admin;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x16c;
        private PollUsers bll = new PollUsers();
        protected Button btnDelete;
        protected Button btnSearch;
        protected GridViewEx gridView;
        protected Label Label1;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected RadioButton radbtnTrueName;
        protected RadioButton radbtnUserID;
        protected TextBox txtKey;

        public void BindData()
        {
            string strWhere = "";
            if ((this.Session["strWherePollUser"] != null) && (this.Session["strWherePollUser"].ToString() != ""))
            {
                strWhere = strWhere + this.Session["strWherePollUser"].ToString();
            }
            DataSet list = new DataSet();
            list = this.bll.GetList(strWhere);
            this.gridView.DataSetSource = list;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string str = this.txtKey.Text.Trim();
            string str2 = "";
            if (this.radbtnTrueName.Checked)
            {
                if (str != "")
                {
                    str2 = str2 + " and (TrueName like'%" + str + "%')";
                }
            }
            else if (str != "")
            {
                str2 = str2 + " and (UserID =" + str + ")";
            }
            if (str2 != "")
            {
                this.Session["strWherePollUser"] = " (1=1) " + str2;
            }
            else
            {
                this.Session["strWherePollUser"] = "";
            }
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

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridView.PageIndex = e.NewPageIndex;
            this.BindData();
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
            this.bll.Delete(Convert.ToInt32(str));
            this.BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x16b;
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

