namespace Maticsoft.Web.Admin.Shop.Gift
{
    using Maticsoft.BLL.Shop.Gift;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ExchangeDetail : PageBaseAdmin
    {
        protected Button btnBatch;
        protected Button btnSearch;
        private Maticsoft.BLL.Shop.Gift.ExchangeDetail detailBll = new Maticsoft.BLL.Shop.Gift.ExchangeDetail();
        protected DropDownList DropDetailType;
        protected DropDownList dropType;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal2;
        protected TextBox txtKeyword;
        protected Literal txtUserName;

        public void BindData()
        {
            string text = this.txtKeyword.Text;
            int type = Convert.ToInt32(this.DropDetailType.SelectedValue);
            DataSet listEX = this.detailBll.GetListEX(type, text);
            if (listEX != null)
            {
                this.gridView.DataSetSource = listEX;
            }
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string idlist = this.GetIdlist();
            int status = Convert.ToInt32(this.dropType.SelectedValue);
            if (((idlist.Trim().Length != 0) && (status != -1)) && ((status != -1) && this.detailBll.SetStatusList(idlist, status)))
            {
                this.gridView.OnBind();
            }
        }

        public void btnReturn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("/Admin/Accounts/Admin/UserAdmin.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "SetStatus"))
            {
                s = this.SetStatus();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetIdlist()
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

        public string GetOrder(int OrderId)
        {
            if (OrderId <= 0)
            {
                return OrderId.ToString();
            }
            return ("<a href='#'>" + OrderId + "</a>");
        }

        public string GetStatusName(int Status)
        {
            switch (Status)
            {
                case 0:
                    return "待处理";

                case 1:
                    return "已处理，待发货";

                case 2:
                    return "已发货";

                case 3:
                    return "完成";

                case 4:
                    return "已缺货";
            }
            return "待处理";
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
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!this.Page.IsPostBack)
            {
                this.txtUserName.Text = "礼品兑换明细";
                if ((this.Session["Style"] != null) && (this.Session["Style"].ToString() != ""))
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if ((base.Application[str] != null) && (base.Application[str].ToString() != ""))
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.gridView.OnBind();
            }
        }

        private string SetStatus()
        {
            if (string.IsNullOrWhiteSpace(base.Request.Form["DetailID"]) || string.IsNullOrWhiteSpace(base.Request.Form["Status"]))
            {
                return "Fail";
            }
            int detailId = Globals.SafeInt(base.Request.Form["DetailID"], 0);
            int status = Globals.SafeInt(base.Request.Form["Status"], 0);
            JsonObject obj2 = new JsonObject();
            if (this.detailBll.SetStatus(detailId, status))
            {
                obj2.Put("STATUS", "OK");
            }
            else
            {
                obj2.Put("STATUS", "NODATA");
            }
            return obj2.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x1b2;
            }
        }

        public int UserId
        {
            get
            {
                int num = 0;
                if ((base.Request.Params["userid"] != null) && PageValidate.IsNumber(base.Request.Params["userid"]))
                {
                    num = int.Parse(base.Request.Params["userid"]);
                }
                return num;
            }
        }
    }
}

