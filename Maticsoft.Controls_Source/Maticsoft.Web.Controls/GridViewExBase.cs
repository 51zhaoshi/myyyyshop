namespace Maticsoft.Web.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:GridViewEx runat='server'></{0}:GridViewEx>")]
    public abstract class GridViewExBase : GridView
    {
        private DataSet _ds;
        private string _excelName = "FileName1";
        private IGridViewUIText _gridViewUiText;
        private string _UnExportedColumnNames = "";
        private BindEventHandler Bind;
        private LinkButton btnExport;
        private LinkButton btnExportWord;
        private LinkButton btnFirst;
        private LinkButton btnFirstFoot;
        private LinkButton btnLast;
        private LinkButton btnLastFoot;
        private LinkButton btnNext;
        private LinkButton btnNextFoot;
        private LinkButton btnPrev;
        private LinkButton btnPrevFoot;
        private Label lblCurrentPage;
        private Label lblPageCount;
        private Label lblRowsCount;
        private Maticsoft.Web.Controls.SortTip sortTip;
        private Table table = new Table();

        public event BindEventHandler Bind
        {
            add
            {
                BindEventHandler handler2;
                BindEventHandler bind = this.Bind;
                do
                {
                    handler2 = bind;
                    BindEventHandler handler3 = (BindEventHandler) Delegate.Combine(handler2, value);
                    bind = Interlocked.CompareExchange<BindEventHandler>(ref this.Bind, handler3, handler2);
                }
                while (bind != handler2);
            }
            remove
            {
                BindEventHandler handler2;
                BindEventHandler bind = this.Bind;
                do
                {
                    handler2 = bind;
                    BindEventHandler handler3 = (BindEventHandler) Delegate.Remove(handler2, value);
                    bind = Interlocked.CompareExchange<BindEventHandler>(ref this.Bind, handler3, handler2);
                }
                while (bind != handler2);
            }
        }

        public GridViewExBase(IGridViewUIText uiText)
        {
            this._gridViewUiText = uiText;
            this.AutoGenerateColumns = false;
            this.AllowSorting = true;
            this.AllowPaging = true;
        }

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int num = base.CreateChildControls(dataSource, dataBinding);
            if (this.ShowToolBar)
            {
                try
                {
                    GridViewRow child = new GridViewRow(0, 0, DataControlRowType.Pager, DataControlRowState.Normal);
                    TableCell cell = new TableCell {
                        Width = Unit.Empty,
                        ColumnSpan = this.Columns.Count
                    };
                    if (this.ShowCheckAll)
                    {
                        cell.ColumnSpan++;
                    }
                    child.Cells.Add(cell);
                    TableCell cell2 = new TableCell();
                    Table table = new Table();
                    TableRow row = new TableRow();
                    table.Rows.Add(row);
                    table.Width = Unit.Percentage(100.0);
                    cell.Controls.Add(table);
                    row.Cells.Add(cell2);
                    Literal literal = new Literal {
                        Text = this._gridViewUiText.Page + "："
                    };
                    cell2.Controls.Add(literal);
                    cell2.Wrap = false;
                    cell2.Controls.Add(this.lblCurrentPage);
                    literal = new Literal {
                        Text = "/"
                    };
                    cell2.Controls.Add(literal);
                    cell2.Controls.Add(this.lblPageCount);
                    literal = new Literal {
                        Text = "，" + this._gridViewUiText.Record + ":"
                    };
                    cell2.Controls.Add(literal);
                    cell2.Controls.Add(this.lblRowsCount);
                    literal = new Literal {
                        Text = ""
                    };
                    cell2.HorizontalAlign = HorizontalAlign.Left;
                    cell2.Controls.Add(literal);
                    TableCell cell3 = new TableCell {
                        HorizontalAlign = HorizontalAlign.Right,
                        Wrap = false
                    };
                    if (this.ShowExportExcel)
                    {
                        literal = new Literal {
                            Text = " ["
                        };
                        cell3.Controls.Add(literal);
                        cell3.Controls.Add(this.btnExport);
                        literal = new Literal {
                            Text = "] "
                        };
                        cell3.Controls.Add(literal);
                    }
                    if (this.ShowExportWord)
                    {
                        literal = new Literal {
                            Text = " ["
                        };
                        cell3.Controls.Add(literal);
                        cell3.Controls.Add(this.btnExportWord);
                        literal = new Literal {
                            Text = "] "
                        };
                        cell3.Controls.Add(literal);
                    }
                    literal = new Literal {
                        Text = " ["
                    };
                    cell3.Controls.Add(literal);
                    cell3.Controls.Add(this.btnFirst);
                    literal = new Literal {
                        Text = "] "
                    };
                    cell3.Controls.Add(literal);
                    literal = new Literal {
                        Text = " ["
                    };
                    cell3.Controls.Add(literal);
                    cell3.Controls.Add(this.btnPrev);
                    literal = new Literal {
                        Text = "] "
                    };
                    cell3.Controls.Add(literal);
                    literal = new Literal {
                        Text = " ["
                    };
                    cell3.Controls.Add(literal);
                    cell3.Controls.Add(this.btnNext);
                    literal = new Literal {
                        Text = "] "
                    };
                    cell3.Controls.Add(literal);
                    literal = new Literal {
                        Text = " ["
                    };
                    cell3.Controls.Add(literal);
                    cell3.Controls.Add(this.btnLast);
                    literal = new Literal {
                        Text = "] "
                    };
                    cell3.Controls.Add(literal);
                    row.Cells.Add(cell3);
                    this.Controls[0].Controls.AddAt(0, child);
                }
                catch
                {
                }
            }
            return num;
        }

        private void DisableControls(Control gv)
        {
            new LinkButton();
            Literal child = new Literal();
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    child.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, child);
                }
                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {
                    child.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, child);
                }
                if (gv.Controls[i].HasControls())
                {
                    this.DisableControls(gv.Controls[i]);
                }
            }
        }

        protected override void ExtractRowValues(IOrderedDictionary fieldValues, GridViewRow row, bool includeReadOnlyFields, bool includePrimaryKey)
        {
            TableCell cell = row.Cells[0];
            row.Cells.Remove(cell);
            base.ExtractRowValues(fieldValues, row, includeReadOnlyFields, includePrimaryKey);
        }

        public void NavigateToPage(object sender, CommandEventArgs e)
        {
            this.btnFirst.Enabled = true;
            this.btnPrev.Enabled = true;
            this.btnNext.Enabled = true;
            this.btnLast.Enabled = true;
            string str = e.CommandArgument.ToString();
            if (str != null)
            {
                if (!(str == "Prev"))
                {
                    if (str == "Next")
                    {
                        if (this.PageIndex < (this.PageCount - 1))
                        {
                            this.PageIndex++;
                        }
                    }
                    else if (str == "First")
                    {
                        this.PageIndex = 0;
                    }
                    else if (str == "Last")
                    {
                        this.PageIndex = this.PageCount - 1;
                    }
                }
                else if (this.PageIndex > 0)
                {
                    this.PageIndex--;
                }
            }
            if (this.PageIndex == 0)
            {
                this.btnFirst.Enabled = false;
                this.btnPrev.Enabled = false;
                if (this.PageCount == 1)
                {
                    this.btnLast.Enabled = false;
                    this.btnNext.Enabled = false;
                }
            }
            else if (this.PageIndex == (this.PageCount - 1))
            {
                this.btnLast.Enabled = false;
                this.btnNext.Enabled = false;
            }
            this.OnBind();
        }

        public void NavigateToPageFoot(object sender, CommandEventArgs e)
        {
            this.btnFirstFoot.Enabled = true;
            this.btnPrevFoot.Enabled = true;
            this.btnNextFoot.Enabled = true;
            this.btnLastFoot.Enabled = true;
            string str = e.CommandArgument.ToString();
            if (str != null)
            {
                if (!(str == "Prev"))
                {
                    if (str == "Next")
                    {
                        if (this.PageIndex < (this.PageCount - 1))
                        {
                            this.PageIndex++;
                        }
                    }
                    else if (str == "First")
                    {
                        this.PageIndex = 0;
                    }
                    else if (str == "Last")
                    {
                        this.PageIndex = this.PageCount - 1;
                    }
                }
                else if (this.PageIndex > 0)
                {
                    this.PageIndex--;
                }
            }
            if (this.PageIndex == 0)
            {
                this.btnFirstFoot.Enabled = false;
                this.btnPrevFoot.Enabled = false;
                if (this.PageCount == 1)
                {
                    this.btnLastFoot.Enabled = false;
                    this.btnNextFoot.Enabled = false;
                }
            }
            else if (this.PageIndex == (this.PageCount - 1))
            {
                this.btnLastFoot.Enabled = false;
                this.btnNextFoot.Enabled = false;
            }
            this.OnBind();
        }

        public virtual void OnBind()
        {
            if (this.Bind != null)
            {
                this.Bind();
                if (this.DataSetSource != null)
                {
                    int count = this.DataSetSource.Tables[0].Rows.Count;
                    DataView defaultView = this.DataSetSource.Tables[0].DefaultView;
                    string str = "";
                    if (count != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(this.SortExpressionStr))
                        {
                            str = this.SortExpressionStr + " " + this.SortDirectionStr;
                        }
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            defaultView.Sort = str;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(this.FilterExpressionStr))
                    {
                        defaultView.RowFilter = this.FilterExpressionStr;
                    }
                    this.DataSource = defaultView;
                    count = defaultView.ToTable().Rows.Count;
                    this.DataBind();
                    if (this.Controls.Count > 0)
                    {
                        Table table = this.Controls[0] as Table;
                        if (table != null)
                        {
                            foreach (TableRow row in table.Rows)
                            {
                                foreach (TableCell cell in row.Cells)
                                {
                                    cell.Wrap = this.Wrap;
                                }
                            }
                        }
                    }
                    int pageSize = this.PageSize;
                    int pageCount = this.PageCount;
                    int num3 = this.PageIndex + 1;
                    this.lblRowsCount.Text = count.ToString();
                    this.lblPageCount.Text = pageCount.ToString();
                    this.lblCurrentPage.Text = num3.ToString();
                    this.btnFirst.Enabled = true;
                    this.btnPrev.Enabled = true;
                    this.btnNext.Enabled = true;
                    this.btnLast.Enabled = true;
                    if (this.PageIndex == 0)
                    {
                        this.btnFirst.Enabled = false;
                        this.btnPrev.Enabled = false;
                        if (pageCount == 0)
                        {
                            this.btnNext.Enabled = false;
                            this.btnLast.Enabled = false;
                        }
                        if (this.PageCount == 1)
                        {
                            this.btnLast.Enabled = false;
                            this.btnNext.Enabled = false;
                        }
                    }
                    else if (this.PageIndex == (this.PageCount - 1))
                    {
                        this.btnLast.Enabled = false;
                        this.btnNext.Enabled = false;
                    }
                    this.btnFirstFoot.Enabled = true;
                    this.btnPrevFoot.Enabled = true;
                    this.btnNextFoot.Enabled = true;
                    this.btnLastFoot.Enabled = true;
                    if (this.PageIndex == 0)
                    {
                        this.btnFirstFoot.Enabled = false;
                        this.btnPrevFoot.Enabled = false;
                        if (pageCount == 0)
                        {
                            this.btnNextFoot.Enabled = false;
                            this.btnLastFoot.Enabled = false;
                        }
                        if (this.PageCount == 1)
                        {
                            this.btnLastFoot.Enabled = false;
                            this.btnNextFoot.Enabled = false;
                        }
                    }
                    else if (this.PageIndex == (this.PageCount - 1))
                    {
                        this.btnLastFoot.Enabled = false;
                        this.btnNextFoot.Enabled = false;
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.EnableViewState = true;
            this.btnExport = new LinkButton();
            this.btnExport.CommandName = "ExportToExcel";
            this.btnExport.EnableViewState = true;
            this.btnExport.Text = this._gridViewUiText.ExportExcel;
            this.btnExport.CausesValidation = false;
            this.btnExportWord = new LinkButton();
            this.btnExportWord.CommandName = "ExportToWord";
            this.btnExportWord.EnableViewState = true;
            this.btnExportWord.Text = this._gridViewUiText.ExportWord;
            this.btnExportWord.CausesValidation = false;
            this.lblCurrentPage = new Label();
            this.lblCurrentPage.ForeColor = ColorTranslator.FromHtml("#e78a29");
            this.lblCurrentPage.Text = "1";
            this.lblPageCount = new Label();
            this.lblPageCount.Text = "1";
            this.lblRowsCount = new Label();
            this.lblRowsCount.ForeColor = ColorTranslator.FromHtml("#e78a29");
            this.btnFirst = new LinkButton();
            this.btnFirst.Text = this._gridViewUiText.First;
            this.btnFirst.Command += new CommandEventHandler(this.NavigateToPage);
            this.btnFirst.CommandName = "Pager";
            this.btnFirst.CommandArgument = "First";
            this.btnFirst.CausesValidation = false;
            this.btnPrev = new LinkButton();
            this.btnPrev.Text = this._gridViewUiText.Previous;
            this.btnPrev.Command += new CommandEventHandler(this.NavigateToPage);
            this.btnPrev.CommandName = "Pager";
            this.btnPrev.CommandArgument = "Prev";
            this.btnPrev.CausesValidation = false;
            this.btnNext = new LinkButton();
            this.btnNext.Text = this._gridViewUiText.Next;
            this.btnNext.Command += new CommandEventHandler(this.NavigateToPage);
            this.btnNext.CommandName = "Pager";
            this.btnNext.CommandArgument = "Next";
            this.btnNext.CausesValidation = false;
            this.btnLast = new LinkButton();
            this.btnLast.Text = this._gridViewUiText.Last;
            this.btnLast.Command += new CommandEventHandler(this.NavigateToPage);
            this.btnLast.CommandName = "Pager";
            this.btnLast.CommandArgument = "Last";
            this.btnLast.CausesValidation = false;
            this.btnFirstFoot = new LinkButton();
            this.btnFirstFoot.Text = this._gridViewUiText.First;
            this.btnFirstFoot.Command += new CommandEventHandler(this.NavigateToPageFoot);
            this.btnFirstFoot.CommandName = "Pager";
            this.btnFirstFoot.CommandArgument = "First";
            this.btnFirstFoot.CausesValidation = false;
            this.btnPrevFoot = new LinkButton();
            this.btnPrevFoot.Text = this._gridViewUiText.Previous;
            this.btnPrevFoot.Command += new CommandEventHandler(this.NavigateToPageFoot);
            this.btnPrevFoot.CommandName = "Pager";
            this.btnPrevFoot.CommandArgument = "Prev";
            this.btnPrevFoot.CausesValidation = false;
            this.btnNextFoot = new LinkButton();
            this.btnNextFoot.Text = this._gridViewUiText.Next;
            this.btnNextFoot.Command += new CommandEventHandler(this.NavigateToPageFoot);
            this.btnNextFoot.CommandName = "Pager";
            this.btnNextFoot.CommandArgument = "Next";
            this.btnNextFoot.CausesValidation = false;
            this.btnLastFoot = new LinkButton();
            this.btnLastFoot.Text = this._gridViewUiText.Last;
            this.btnLastFoot.Command += new CommandEventHandler(this.NavigateToPageFoot);
            this.btnLastFoot.CommandName = "Pager";
            this.btnLastFoot.CommandArgument = "Last";
            this.btnLastFoot.CausesValidation = false;
            base.OnInit(e);
            this.BorderWidth = new Unit(1);
        }

        protected override void OnLoad(EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" <script type=\"text/javascript\">");
            builder.Append("function CheckAll(oCheckbox)");
            builder.Append("{");
            builder.Append("var GridView2 = document.getElementById(\"" + this.ClientID + "\");");
            builder.Append(" for(i = 1;i < GridView2.rows.length; i++)");
            builder.Append("{");
            builder.Append("var inputArray = GridView2.rows[i].getElementsByTagName(\"INPUT\");");
            builder.Append("for(var j=0;j<inputArray.length;j++)");
            builder.Append("{ if(inputArray[j].type=='checkbox')");
            builder.Append("{if(inputArray[j].id.indexOf('ItemCheckBox',0)>-1){inputArray[j].checked =oCheckbox.checked; }}  }");
            builder.Append("}");
            builder.Append(" }");
            builder.Append("</script>");
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "CheckFun", builder.ToString(), false);
            if (!this.Page.IsPostBack)
            {
                try
                {
                    if (HttpContext.Current.Session["Style"] != null)
                    {
                        string str = HttpContext.Current.Session["Style"] + "xtable_bordercolorlight";
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            string htmlColor = HttpContext.Current.Application[str].ToString();
                            if (this.ShowGridLine)
                            {
                                this.BorderColor = ColorTranslator.FromHtml(htmlColor);
                            }
                            if (this.ShowHeaderStyle)
                            {
                                base.HeaderStyle.BackColor = ColorTranslator.FromHtml(htmlColor);
                            }
                        }
                        string str3 = HttpContext.Current.Session["Style"] + "xtable_titlebgcolor";
                        if (!string.IsNullOrWhiteSpace(str3))
                        {
                            string str4 = HttpContext.Current.Application[str3].ToString();
                            if (this.ShowHeaderStyle)
                            {
                                base.HeaderStyle.BackColor = ColorTranslator.FromHtml(str4);
                            }
                        }
                    }
                    this.OnBind();
                }
                catch (Exception)
                {
                }
            }
            base.OnLoad(e);
        }

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            this.PageIndex = e.NewPageIndex;
            this.OnBind();
        }

        protected override void OnRowCommand(GridViewCommandEventArgs e)
        {
            base.OnRowCommand(e);
            if (e.CommandName == "ExportToExcel")
            {
                string[] strArray = this.UnExportedColumnNames.Split(new char[] { ',' });
                List<string> list = new List<string>();
                foreach (string str in strArray)
                {
                    if (str != ",")
                    {
                        list.Add(str);
                    }
                }
                this.ShowToolBar = false;
                this.AllowSorting = false;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=utf-8>");
                string str2 = HttpUtility.UrlEncode(this.ExcelFileName + ".xls", Encoding.UTF8);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + str2);
                HttpContext.Current.Response.Charset = Encoding.UTF8.WebName;
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/vnd.xls";
                StringWriter writer = new StringWriter();
                HtmlTextWriter writer2 = new HtmlTextWriter(writer);
                bool showCheckAll = this.ShowCheckAll;
                this.ShowCheckAll = false;
                this.AllowPaging = false;
                this.OnBind();
                this.DisableControls(this);
                foreach (DataControlField field in this.Columns)
                {
                    if (list.Contains(field.HeaderText) && !string.IsNullOrWhiteSpace(field.HeaderText))
                    {
                        field.Visible = false;
                    }
                }
                this.RenderControl(writer2);
                string s = Regex.Replace(writer.ToString(), "(<a[^>]+>)|(</a>)", "");
                HttpContext.Current.Response.Write(s);
                HttpContext.Current.Response.End();
                this.AllowPaging = true;
                this.AllowSorting = true;
                this.ShowToolBar = true;
                this.ShowCheckAll = showCheckAll;
                this.OnBind();
            }
            else if (e.CommandName == "ExportToWord")
            {
                string[] strArray2 = this.UnExportedColumnNames.Split(new char[] { ',' });
                List<string> list2 = new List<string>();
                foreach (string str4 in strArray2)
                {
                    if (str4 != ",")
                    {
                        list2.Add(str4);
                    }
                }
                this.ShowToolBar = false;
                this.AllowSorting = false;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=utf-8>");
                string str5 = HttpUtility.UrlEncode(this.ExcelFileName + ".doc", Encoding.UTF8);
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + str5);
                HttpContext.Current.Response.Charset = Encoding.UTF8.WebName;
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "application/ms-word";
                StringWriter writer3 = new StringWriter();
                HtmlTextWriter writer4 = new HtmlTextWriter(writer3);
                bool flag2 = this.ShowCheckAll;
                this.ShowCheckAll = false;
                this.AllowPaging = false;
                this.OnBind();
                this.DisableControls(this);
                foreach (DataControlField field2 in this.Columns)
                {
                    if (list2.Contains(field2.HeaderText) && !string.IsNullOrWhiteSpace(field2.HeaderText))
                    {
                        field2.Visible = false;
                    }
                }
                this.RenderControl(writer4);
                string str6 = Regex.Replace(writer3.ToString(), "(<a[^>]+>)|(</a>)", "");
                HttpContext.Current.Response.Write(str6);
                HttpContext.Current.Response.End();
                this.AllowPaging = true;
                this.AllowSorting = true;
                this.ShowToolBar = true;
                this.ShowCheckAll = flag2;
                this.OnBind();
            }
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.Attributes.Add("border", "0px");
            base.Attributes.Add("cellpadding", "4px");
            base.Attributes.Add("cellspacing", "1px");
            base.Attributes.Add("class", "GridViewTyle");
            e.Row.Style.Add("height", this.RowHeight);
            e.Row.Attributes.Add("height", this.RowHeight);
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    TableRow row = e.Row.Controls[0].Controls[0].Controls[0] as TableRow;
                    foreach (TableCell cell in row.Cells)
                    {
                        Control control = cell.Controls[0];
                        if (control is Label)
                        {
                            Label label = (Label) control;
                            label.ForeColor = ColorTranslator.FromHtml("#e78a29");
                            label.Font.Bold = true;
                            label.Text = "[" + label.Text + "]";
                        }
                        else if (control is LinkButton)
                        {
                            LinkButton button = (LinkButton) control;
                            button.Font.Bold = true;
                            button.Text = "[" + button.Text + "]";
                            button.Style.Add("color", "#1317fc");
                        }
                    }
                    if (this.ShowFootPageButton)
                    {
                        TableCell cell2 = new TableCell();
                        cell2.Controls.Add(this.btnFirstFoot);
                        row.Cells.AddAt(0, cell2);
                        TableCell cell3 = new TableCell();
                        cell3.Controls.Add(this.btnPrevFoot);
                        row.Cells.AddAt(1, cell3);
                        TableCell cell4 = new TableCell();
                        cell4.Controls.Add(this.btnNextFoot);
                        row.Cells.Add(cell4);
                        TableCell cell5 = new TableCell();
                        cell5.Controls.Add(this.btnLastFoot);
                        row.Cells.Add(cell5);
                    }
                }
                if (this.ShowCheckAll)
                {
                    GridViewRow row2 = e.Row;
                    if (row2.RowType == DataControlRowType.Header)
                    {
                        TableCell cell6 = new TableCell {
                            Wrap = this.Wrap,
                            Width = Unit.Pixel(0x12)
                        };
                        cell6.Style.Clear();
                        cell6.Style.Add("padding-left", "5px");
                        cell6.CssClass = "input_middle";
                        cell6.Text = " <input id='Checkbox2' type='checkbox' onclick='CheckAll(this)'/>";
                        if (this.CheckColumnAlign == Maticsoft.Web.Controls.CheckColumnAlign.Left)
                        {
                            row2.Cells.AddAt(0, cell6);
                        }
                        else
                        {
                            int count = row2.Cells.Count;
                            row2.Cells.AddAt(count, cell6);
                        }
                    }
                    else if (row2.RowType == DataControlRowType.DataRow)
                    {
                        TableCell cell7 = new TableCell {
                            Wrap = this.Wrap
                        };
                        switch (this.CheckColumnVAlign)
                        {
                            case Maticsoft.Web.Controls.CheckColumnVAlign.Top:
                                cell7.VerticalAlign = VerticalAlign.Top;
                                break;

                            case Maticsoft.Web.Controls.CheckColumnVAlign.Middle:
                                cell7.VerticalAlign = VerticalAlign.Middle;
                                break;

                            case Maticsoft.Web.Controls.CheckColumnVAlign.Bottom:
                                cell7.VerticalAlign = VerticalAlign.Bottom;
                                break;
                        }
                        CheckBox child = new CheckBox();
                        cell7.Width = Unit.Pixel(0x12);
                        cell7.Style.Clear();
                        cell7.CssClass = "input_middle";
                        child.ID = "ItemCheckBox";
                        cell7.Controls.Add(child);
                        if (this.CheckColumnAlign == Maticsoft.Web.Controls.CheckColumnAlign.Left)
                        {
                            row2.Cells.AddAt(0, cell7);
                        }
                        else
                        {
                            int index = row2.Cells.Count;
                            row2.Cells.AddAt(index, cell7);
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#CBE3F4';this.style.cursor='pointer';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
                    foreach (TableCell cell8 in e.Row.Cells)
                    {
                        bool showGridLine = this.ShowGridLine;
                        cell8.Style.Add("padding-left", "5px");
                        cell8.Style.Add("height", this.RowHeight);
                        cell8.Attributes.Add("height", this.RowHeight);
                    }
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        foreach (Control control2 in e.Row.Cells[i].Controls)
                        {
                            if (control2 is LinkButton)
                            {
                                LinkButton button2 = control2 as LinkButton;
                                button2.Style.Add("color", "#1317fc");
                            }
                            if (control2 is HtmlAnchor)
                            {
                                HtmlAnchor anchor = control2 as HtmlAnchor;
                                anchor.Style.Add("color", "#1317fc");
                            }
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    string str = ((base.HeaderStyle.Height == Unit.Empty) ? Unit.Pixel(0x23) : base.HeaderStyle.Height).ToString();
                    e.Row.Style.Add("height", str);
                    e.Row.Attributes.Add("height", str);
                    e.Row.Style.Add("color", "#003366");
                    for (int j = 0; j < e.Row.Cells.Count; j++)
                    {
                        if (j == 0)
                        {
                            if (this.ShowGridLine)
                            {
                                e.Row.Cells[j].Style.Add("border", "1px solid #dae2e8");
                            }
                            e.Row.Cells[j].Style.Add("border-right", "0px");
                        }
                        else if (j == e.Row.Cells.Count)
                        {
                            if (this.ShowGridLine)
                            {
                                e.Row.Cells[j].Style.Add("border", "1px solid #dae2e8");
                            }
                            e.Row.Cells[j].Style.Add("border-left", "0px");
                        }
                        else
                        {
                            if (this.ShowGridLine)
                            {
                                e.Row.Cells[j].Style.Add("border", "1px solid #dae2e8");
                            }
                            e.Row.Cells[j].Style.Add("border-left", "0px");
                            e.Row.Cells[j].Style.Add("border-right", "0px");
                        }
                        foreach (Control control3 in e.Row.Cells[j].Controls)
                        {
                            if (control3 is LinkButton)
                            {
                                LinkButton button3 = control3 as LinkButton;
                                button3.Style.Add("color", "#003366");
                            }
                            if (control3 is HtmlAnchor)
                            {
                                HtmlAnchor anchor2 = control3 as HtmlAnchor;
                                anchor2.Style.Add("color", "#003366");
                            }
                        }
                    }
                }
                if ((this.AllowSorting && !this.SortTip.IsNotSet) && (e.Row.RowType == DataControlRowType.Header))
                {
                    foreach (TableCell cell9 in e.Row.Cells)
                    {
                        foreach (Control control4 in cell9.Controls)
                        {
                            if (!(control4 is LinkButton))
                            {
                                continue;
                            }
                            LinkButton button4 = control4 as LinkButton;
                            if ((button4 != null) && (button4.CommandArgument == this.SortExpressionStr))
                            {
                                System.Web.UI.WebControls.Image image;
                                if (this.SortDirectionStr == "DESC")
                                {
                                    image = new System.Web.UI.WebControls.Image {
                                        ImageAlign = ImageAlign.AbsMiddle,
                                        ImageUrl = base.ResolveUrl(this.SortTip.DescImg)
                                    };
                                }
                                else
                                {
                                    image = new System.Web.UI.WebControls.Image {
                                        ImageAlign = ImageAlign.AbsMiddle,
                                        ImageUrl = base.ResolveUrl(this.SortTip.AscImg)
                                    };
                                }
                                if (image != null)
                                {
                                    cell9.Controls.Add(image);
                                }
                            }
                        }
                    }
                }
                base.OnRowCreated(e);
            }
            catch
            {
            }
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
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

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            this.SortExpressionStr = e.SortExpression;
            if (this.SortDirectionStr.ToLower() == "asc")
            {
                this.SortDirectionStr = "DESC";
            }
            else
            {
                this.SortDirectionStr = "ASC";
            }
            this.OnBind();
        }

        protected override void PrepareControlHierarchy()
        {
            if (this.ShowCheckAll)
            {
                if (this.Controls.Count != 0)
                {
                    bool controlStyleCreated = base.ControlStyleCreated;
                    Table table = (Table) this.Controls[0];
                    table.CopyBaseAttributes(this);
                    if (controlStyleCreated && !base.ControlStyle.IsEmpty)
                    {
                        table.ApplyStyle(base.ControlStyle);
                    }
                    else
                    {
                        table.GridLines = GridLines.Both;
                        table.CellSpacing = 0;
                    }
                    table.Caption = this.Caption;
                    table.CaptionAlign = this.CaptionAlign;
                    TableRowCollection rows = table.Rows;
                    Style s = null;
                    if (base.AlternatingRowStyle != null)
                    {
                        s = new TableItemStyle();
                        s.CopyFrom(base.RowStyle);
                        s.CopyFrom(base.AlternatingRowStyle);
                    }
                    else
                    {
                        s = base.RowStyle;
                    }
                    int num = 0;
                    bool flag2 = true;
                    foreach (GridViewRow row in rows)
                    {
                        Style style2;
                        switch (row.RowType)
                        {
                            case DataControlRowType.Header:
                                if (this.ShowHeader && (base.HeaderStyle != null))
                                {
                                    row.MergeStyle(base.HeaderStyle);
                                }
                                goto Label_0269;

                            case DataControlRowType.Footer:
                                if (this.ShowFooter && (base.FooterStyle != null))
                                {
                                    row.MergeStyle(base.FooterStyle);
                                }
                                goto Label_0269;

                            case DataControlRowType.DataRow:
                                if ((row.RowState & DataControlRowState.Edit) == DataControlRowState.Normal)
                                {
                                    goto Label_01FB;
                                }
                                style2 = new TableItemStyle();
                                if ((row.RowIndex % 2) == 0)
                                {
                                    break;
                                }
                                style2.CopyFrom(s);
                                goto Label_01C7;

                            case DataControlRowType.Pager:
                                if (row.Visible && (base.PagerStyle != null))
                                {
                                    row.MergeStyle(base.PagerStyle);
                                }
                                goto Label_0269;

                            case DataControlRowType.EmptyDataRow:
                                row.MergeStyle(base.EmptyDataRowStyle);
                                goto Label_0269;

                            default:
                                goto Label_0269;
                        }
                        style2.CopyFrom(base.RowStyle);
                    Label_01C7:
                        if (row.RowIndex == this.SelectedIndex)
                        {
                            style2.CopyFrom(base.SelectedRowStyle);
                        }
                        style2.CopyFrom(base.EditRowStyle);
                        row.MergeStyle(style2);
                        goto Label_0269;
                    Label_01FB:
                        if ((row.RowState & DataControlRowState.Selected) != DataControlRowState.Normal)
                        {
                            Style style3 = new TableItemStyle();
                            if ((row.RowIndex % 2) != 0)
                            {
                                style3.CopyFrom(s);
                            }
                            else
                            {
                                style3.CopyFrom(base.RowStyle);
                            }
                            style3.CopyFrom(base.SelectedRowStyle);
                            row.MergeStyle(style3);
                        }
                        else if ((row.RowState & DataControlRowState.Alternate) != DataControlRowState.Normal)
                        {
                            row.MergeStyle(s);
                        }
                        else
                        {
                            row.MergeStyle(base.RowStyle);
                        }
                    Label_0269:
                        if ((row.RowType != DataControlRowType.Pager) && (row.RowType != DataControlRowType.EmptyDataRow))
                        {
                            foreach (TableCell cell in row.Cells)
                            {
                                DataControlFieldCell cell2 = cell as DataControlFieldCell;
                                if (cell2 != null)
                                {
                                    DataControlField containingField = cell2.ContainingField;
                                    if (containingField != null)
                                    {
                                        if (!containingField.Visible)
                                        {
                                            cell.Visible = false;
                                            continue;
                                        }
                                        if ((row.RowType == DataControlRowType.DataRow) && flag2)
                                        {
                                            num++;
                                        }
                                        Style headerStyle = null;
                                        switch (row.RowType)
                                        {
                                            case DataControlRowType.Header:
                                                headerStyle = containingField.HeaderStyle;
                                                break;

                                            case DataControlRowType.Footer:
                                                headerStyle = containingField.FooterStyle;
                                                break;

                                            default:
                                                headerStyle = containingField.ItemStyle;
                                                break;
                                        }
                                        if (headerStyle != null)
                                        {
                                            cell.MergeStyle(headerStyle);
                                        }
                                        if (row.RowType == DataControlRowType.DataRow)
                                        {
                                            foreach (Control control in cell.Controls)
                                            {
                                                WebControl control2 = control as WebControl;
                                                Style controlStyle = containingField.ControlStyle;
                                                if (((control2 != null) && (controlStyle != null)) && !controlStyle.IsEmpty)
                                                {
                                                    control2.ControlStyle.CopyFrom(controlStyle);
                                                }
                                            }
                                            continue;
                                        }
                                    }
                                }
                            }
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                flag2 = false;
                            }
                        }
                    }
                    if ((this.Rows.Count > 0) && (num != this.Rows[0].Cells.Count))
                    {
                        if (this.ShowCheckAll)
                        {
                            num++;
                        }
                        if ((this.TopPagerRow != null) && (this.TopPagerRow.Cells.Count > 0))
                        {
                            this.TopPagerRow.Cells[0].ColumnSpan = num;
                        }
                        if ((this.BottomPagerRow != null) && (this.BottomPagerRow.Cells.Count > 0))
                        {
                            this.BottomPagerRow.Cells[0].ColumnSpan = num;
                        }
                    }
                }
            }
            else
            {
                base.PrepareControlHierarchy();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("\r\n<style type='text/css'>\r\n.text { mso-number-format:\\@; }\r\n.GridViewTyle tr td { border: 1px solid #CCCCCC;}\r\n.GridViewTyle tr td table tr td{ border: none;}\r\n.GridViewTyle tr td { border-spacing: 2px; border-color: #CCCCCC;border-collapse: collapse;empty-cells: show; }\r\n.GridViewTyle a{ color:#1317fc;text-decoration: none;}\r\n.GridViewTyle a:hover{ color:#1317fc;}\r\n.GridViewTyle span{  text-align:center;}\r\n</style>\r\n");
            base.Render(writer);
        }

        public string CheckBoxID
        {
            get
            {
                return "ItemCheckBox";
            }
        }

        [DefaultValue(0), Category("扩展"), Description("全选列的位置")]
        public virtual Maticsoft.Web.Controls.CheckColumnAlign CheckColumnAlign
        {
            get
            {
                object obj2 = this.ViewState["CheckColumnAlign"];
                if (obj2 != null)
                {
                    return (Maticsoft.Web.Controls.CheckColumnAlign) obj2;
                }
                return Maticsoft.Web.Controls.CheckColumnAlign.Left;
            }
            set
            {
                Maticsoft.Web.Controls.CheckColumnAlign checkColumnAlign = this.CheckColumnAlign;
                if (value != checkColumnAlign)
                {
                    this.ViewState["CheckColumnAlign"] = value;
                    if (base.Initialized)
                    {
                        base.RequiresDataBinding = true;
                    }
                }
            }
        }

        [DefaultValue(1), Description("选择列的位置"), Category("扩展")]
        public virtual Maticsoft.Web.Controls.CheckColumnVAlign CheckColumnVAlign
        {
            get
            {
                object obj2 = this.ViewState["CheckColumnVAlign"];
                if (obj2 != null)
                {
                    return (Maticsoft.Web.Controls.CheckColumnVAlign) obj2;
                }
                return Maticsoft.Web.Controls.CheckColumnVAlign.Middle;
            }
            set
            {
                Maticsoft.Web.Controls.CheckColumnVAlign checkColumnVAlign = this.CheckColumnVAlign;
                if (value != checkColumnVAlign)
                {
                    this.ViewState["CheckColumnVAlign"] = value;
                    if (base.Initialized)
                    {
                        base.RequiresDataBinding = true;
                    }
                }
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), Description("自定义的DataSet类型数据源"), Category("扩展"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual DataSet DataSetSource
        {
            get
            {
                return this._ds;
            }
            set
            {
                this._ds = value;
            }
        }

        [DefaultValue("FileName1"), Category("扩展")]
        public string ExcelFileName
        {
            get
            {
                return this._excelName;
            }
            set
            {
                this._excelName = value;
            }
        }

        [Category("扩展"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Description("过滤条件表达式")]
        public virtual string FilterExpressionStr
        {
            get
            {
                if (this.ViewState["FilterExpression"] == null)
                {
                    return null;
                }
                return this.ViewState["FilterExpression"].ToString();
            }
            set
            {
                this.ViewState["FilterExpression"] = value;
            }
        }

        [DefaultValue(0x1b), Category("扩展"), Description("行高")]
        public string RowHeight
        {
            get
            {
                if (this.ViewState["RowHeight"] != null)
                {
                    return this.ViewState["RowHeight"].ToString();
                }
                return "27px";
            }
            set
            {
                this.ViewState["RowHeight"] = value;
            }
        }

        [Category("扩展"), Description("显示全选列"), DefaultValue(false)]
        public virtual bool ShowCheckAll
        {
            get
            {
                object obj2 = this.ViewState["ShowCheckAll"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                bool showCheckAll = this.ShowCheckAll;
                if (value != showCheckAll)
                {
                    this.ViewState["ShowCheckAll"] = value;
                    if (base.Initialized)
                    {
                        base.RequiresDataBinding = true;
                    }
                }
            }
        }

        [Category("扩展"), DefaultValue(true), Description("显示导出到Excel")]
        public virtual bool ShowExportExcel
        {
            get
            {
                object obj2 = this.ViewState["ShowExportExcel"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                bool showExportExcel = this.ShowExportExcel;
                if (value != showExportExcel)
                {
                    this.ViewState["ShowExportExcel"] = value;
                    if (base.Initialized)
                    {
                        base.RequiresDataBinding = true;
                    }
                }
            }
        }

        [DefaultValue(true), Description("显示导出到Word"), Category("扩展")]
        public virtual bool ShowExportWord
        {
            get
            {
                object obj2 = this.ViewState["ShowExportWord"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                bool showExportWord = this.ShowExportWord;
                if (value != showExportWord)
                {
                    this.ViewState["ShowExportWord"] = value;
                    if (base.Initialized)
                    {
                        base.RequiresDataBinding = true;
                    }
                }
            }
        }

        [Description("显示页脚的上下页导航"), DefaultValue(false), Category("扩展")]
        public bool ShowFootPageButton
        {
            get
            {
                return ((this.ViewState["ShowFootPageButton"] != null) && Convert.ToBoolean(this.ViewState["ShowFootPageButton"]));
            }
            set
            {
                this.ViewState["ShowFootPageButton"] = value;
            }
        }

        [DefaultValue(true), Description("显示网格线"), Category("扩展")]
        public bool ShowGridLine
        {
            get
            {
                if (this.ViewState["ShowGridLine"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["ShowGridLine"]);
                }
                return true;
            }
            set
            {
                this.ViewState["ShowGridLine"] = value;
            }
        }

        [DefaultValue(true), Category("扩展"), Description("显示头样式")]
        public bool ShowHeaderStyle
        {
            get
            {
                if (this.ViewState["ShowHeaderStyle"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["ShowHeaderStyle"]);
                }
                return true;
            }
            set
            {
                this.ViewState["ShowHeaderStyle"] = value;
            }
        }

        [DefaultValue(true), Description("显示上部的工具栏"), Category("扩展")]
        public bool ShowToolBar
        {
            get
            {
                if (this.ViewState["ShowToolBar"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["ShowToolBar"]);
                }
                return true;
            }
            set
            {
                this.ViewState["ShowToolBar"] = value;
            }
        }

        [Description("排序方向"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Category("扩展")]
        public virtual string SortDirectionStr
        {
            get
            {
                if (this.ViewState["SortDirection"] == null)
                {
                    return "DESC";
                }
                if ((this.ViewState["SortDirection"].ToString().ToLower() != "asc") && (this.ViewState["SortDirection"].ToString().ToLower() != "desc"))
                {
                    return "DESC";
                }
                return this.ViewState["SortDirection"].ToString();
            }
            set
            {
                this.ViewState["SortDirection"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("排序表达式"), Category("扩展")]
        public virtual string SortExpressionStr
        {
            get
            {
                if (this.ViewState["SortExpression"] == null)
                {
                    return null;
                }
                return this.ViewState["SortExpression"].ToString();
            }
            set
            {
                this.ViewState["SortExpression"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty), Category("扩展"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("排序提示信息")]
        public Maticsoft.Web.Controls.SortTip SortTip
        {
            get
            {
                if (this.sortTip == null)
                {
                    this.sortTip = new Maticsoft.Web.Controls.SortTip();
                }
                return this.sortTip;
            }
            set
            {
                this.sortTip = value;
            }
        }

        [Category("扩展"), Description("不导出的数据列集合,将HeaderText用,隔开"), PersistenceMode(PersistenceMode.InnerProperty)]
        public string UnExportedColumnNames
        {
            get
            {
                return this._UnExportedColumnNames.Trim();
            }
            set
            {
                this._UnExportedColumnNames = value;
            }
        }

        [Description("单元格是否换行"), DefaultValue(true), Category("扩展")]
        public bool Wrap
        {
            get
            {
                if (this.ViewState["Wrap"] != null)
                {
                    return Convert.ToBoolean(this.ViewState["Wrap"]);
                }
                return true;
            }
            set
            {
                this.ViewState["Wrap"] = value;
            }
        }
    }
}

