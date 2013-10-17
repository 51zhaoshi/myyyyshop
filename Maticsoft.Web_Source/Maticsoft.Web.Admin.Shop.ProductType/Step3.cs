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
    using System.Web.UI.WebControls;

    public class Step3 : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        private Maticsoft.BLL.Shop.Products.AttributeValue bllValue = new Maticsoft.BLL.Shop.Products.AttributeValue();
        protected Button btnDelete;
        protected Button btnSave;
        protected GridViewEx gridView;
        protected Literal Literal1;
        protected Literal Literal3;
        private Maticsoft.BLL.Shop.Products.ProductType ptypeBll = new Maticsoft.BLL.Shop.Products.ProductType();

        public void BindData()
        {
            DataSet list = new DataSet();
            list = this.bll.GetList(new long?((long) this.ProductTypeId), SearchType.Specification);
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
            base.Response.Redirect("List.aspx");
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
                this.ptypeBll.SwapSeqManage(new int?(this.ProductTypeId), new long?(num2), null, SwapSequenceIndex.Down, true);
            }
            if (e.CommandName == "Rise")
            {
                this.ptypeBll.SwapSeqManage(new int?(this.ProductTypeId), new long?(num2), null, SwapSequenceIndex.Up, true);
            }
            this.gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal literal = (Literal) e.Row.FindControl("litAttValue");
                object obj2 = DataBinder.Eval(e.Row.DataItem, "UseAttributeImage");
                long num = (long) this.gridView.DataKeys[e.Row.RowIndex].Value;
                List<Maticsoft.Model.Shop.Products.AttributeValue> modelList = this.bllValue.GetModelList(new long?(num));
                if ((bool) obj2)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (Maticsoft.Model.Shop.Products.AttributeValue value2 in modelList)
                    {
                        builder.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", value2.ValueId, "'><span class='span1'><a id='aValue", value2.ValueId, "'  class='a_none' href='#'><img src=\"", value2.ImageUrl, "\" width=\"23\" height=\"20\" alt=\"", value2.ValueStr, "\" /></a></span><span class='span2'  title='删除'><a href=\"javascript:deleteAttributeValue(this,'", value2.ValueId, "');\">删除</a></span> </span>" }));
                    }
                    literal.Text = builder.ToString();
                }
                else
                {
                    StringBuilder builder2 = new StringBuilder();
                    foreach (Maticsoft.Model.Shop.Products.AttributeValue value3 in modelList)
                    {
                        builder2.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", value3.ValueId, "'><span class='span1'><a id='aValue", value3.ValueId, "'>", value3.ValueStr, "</a></span><span class='span2' title='删除'><a href=\"javascript:deleteAttributeValue(this,'", value3.ValueId, "');\">删除</a></span> </span>" }));
                    }
                    literal.Text = builder2.ToString();
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
                return 0x1ed;
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

