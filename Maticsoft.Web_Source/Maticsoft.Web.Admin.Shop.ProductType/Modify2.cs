namespace Maticsoft.Web.Admin.Shop.ProductType
{
    using Maticsoft.BLL.Shop.Products;
    using Maticsoft.Common;
    using Maticsoft.Model.Shop.Products;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Modify2 : PageBaseAdmin
    {
        protected int Act_AddData = 0x1ef;
        protected int Act_DelData = 0x1f1;
        protected int Act_UpdateData = 0x1f0;
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        private Maticsoft.BLL.Shop.Products.AttributeValue bllValue = new Maticsoft.BLL.Shop.Products.AttributeValue();
        protected Button btnDelete;
        protected Button btnSave;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Products.ProductType ptypeBll = new Maticsoft.BLL.Shop.Products.ProductType();
        protected HtmlTableCell tdbtnaddatt;

        public void BindData()
        {
            DataSet list = new DataSet();
            list = this.bll.GetList(new long?((long) this.ProductTypeId), SearchType.ExtAttribute);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("Step3.aspx?tid=" + this.ProductTypeId);
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow) ((Control) e.CommandSource).NamingContainer).RowIndex;
            long num2 = (long) this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                this.ptypeBll.SwapSeqManage(new int?(this.ProductTypeId), new long?(num2), null, SwapSequenceIndex.Down, false);
            }
            if (e.CommandName == "Rise")
            {
                this.ptypeBll.SwapSeqManage(new int?(this.ProductTypeId), new long?(num2), null, SwapSequenceIndex.Up, false);
            }
            if (e.CommandName == "ChoseAny")
            {
                this.bll.ChangeImageStatue(num2, ProductAttributeModel.One);
            }
            if (e.CommandName == "ChoseOne")
            {
                this.bll.ChangeImageStatue(num2, ProductAttributeModel.Any);
            }
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='white';this.style.cursor='pointer';");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    HtmlGenericControl control = (HtmlGenericControl) e.Row.FindControl("lbtnAdd");
                    control.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
                {
                    HtmlGenericControl control2 = (HtmlGenericControl) e.Row.FindControl("lbtnModify");
                    control2.Visible = false;
                }
                LinkButton button = (LinkButton) e.Row.FindControl("linkDel");
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "UsageMode");
                ImageButton button2 = (ImageButton) e.Row.FindControl("imgChose");
                ImageButton button3 = (ImageButton) e.Row.FindControl("imgNochose");
                switch (((int) obj2))
                {
                    case 0:
                        button2.Visible = false;
                        button3.Visible = true;
                        break;

                    case 1:
                        button2.Visible = true;
                        button3.Visible = false;
                        break;

                    case 2:
                        button.Visible = false;
                        button2.Visible = false;
                        button3.Visible = false;
                        e.Row.Cells[4].Controls.Clear();
                        e.Row.Cells[2].Text = "(自定义属性)";
                        break;

                    default:
                        button2.Visible = false;
                        button3.Visible = false;
                        break;
                }
                Literal literal = (Literal) e.Row.FindControl("litAttValue");
                long num2 = (long) this.gridView.DataKeys[e.Row.RowIndex].Value;
                List<Maticsoft.Model.Shop.Products.AttributeValue> modelList = this.bllValue.GetModelList(new long?(num2));
                StringBuilder builder = new StringBuilder();
                foreach (Maticsoft.Model.Shop.Products.AttributeValue value2 in modelList)
                {
                    builder.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", value2.ValueId, "'><span class='span1'><a id='aValue", value2.ValueId, "'>", value2.ValueStr, "</a></span><span class='span2'><a href=\"javascript:deleteAttributeValue(this,'", value2.ValueId, "');\">删除</a></span> </span>" }));
                }
                if (literal != null)
                {
                    literal.Text = builder.ToString();
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            long num = (long) this.gridView.DataKeys[e.RowIndex].Value;
            this.ptypeBll.DeleteManage(null, new long?(num), null);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.tdbtnaddatt.Visible = false;
                }
                if (this.ProductTypeId == 0)
                {
                    MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                }
                else
                {
                    if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_DeleteList)) && (base.GetPermidByActID(base.Act_DeleteList) != -1))
                    {
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
                    this.gridView.OnBind();
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
                return 0x1ee;
            }
        }

        private int ProductTypeId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["tid"]))
                {
                    num = Globals.SafeInt(base.Request.Params["tid"], 0);
                }
                return num;
            }
        }
    }
}

