namespace Maticsoft.Web.Admin.Pay.BalanceDrawRequest
{
    using Maticsoft.BLL.Pay;
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
        protected int Act_DelData = 0x2ac;
        protected int Act_UpdateData = 0x2ab;
        private BalanceDrawRequest bll = new BalanceDrawRequest();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DropDownList dropStatusSearch;
        protected DropDownList dropStatusUpdate;
        protected GridViewEx gridView;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            DataSet listEx = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.dropStatusSearch.SelectedValue != "0")
            {
                builder.AppendFormat(" RequestStatus= '{0}'", this.dropStatusSearch.SelectedValue);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat(" UserName= '{0}'", this.txtKeyword.Text.Trim());
            }
            listEx = this.bll.GetListEx(builder.ToString(), " JournalNumber desc");
            this.gridView.DataSetSource = listEx;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    this.gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void dropStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                int status = Globals.SafeInt(this.dropStatusUpdate.SelectedValue, 0);
                switch (status)
                {
                    case 1:
                    case 2:
                    case 3:
                        if (this.bll.UpdateStatus(selIDlist, status))
                        {
                            MessageBox.ShowSuccessTip(this, "修改成功");
                            this.gridView.OnBind();
                            return;
                        }
                        MessageBox.ShowFailTip(this, "修改失败");
                        break;
                }
            }
        }

        protected string GetCardType(int typeID)
        {
            switch (typeID)
            {
                case 1:
                    return "银行卡号";

                case 2:
                    return "支付宝帐号";
            }
            return "";
        }

        protected string GetDrawState(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "1"))
            {
                if (str2 != "2")
                {
                    if (str2 != "3")
                    {
                        return str;
                    }
                    return "审核通过";
                }
            }
            else
            {
                return "未审核";
            }
            return "审核失败";
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
                str = str.TrimEnd(new char[] { ',' });
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
            DataControlRowType rowType = e.Row.RowType;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.btnDelete.Visible = false;
                    this.liDel.Visible = false;
                }
                this.btnDelete.Attributes.Add("onclick", "return confirm(\"" + Site.TooltipDelConfirm + "\")");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)))
                {
                    this.btnDelete.Visible = false;
                }
                this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[this.Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
                this.gridView.OnBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x2aa;
            }
        }
    }
}

