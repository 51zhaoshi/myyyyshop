namespace Maticsoft.Web.Admin.Ms.WeiBo
{
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.TimerTask;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class WeiBoList : PageBaseAdmin
    {
        protected int Act_AddData = 0x157;
        protected int Act_DelData = 0x158;
        private UserBind bindBll = new UserBind();
        protected Button btnSave;
        protected Button btnSearch;
        protected Button btnSendWeibo;
        protected CheckBox chkSetTime;
        protected CheckBoxList ChkWeibo;
        protected CheckBoxList ChkWeibo2;
        protected DropDownList ddlHour;
        protected DropDownList ddlMins;
        protected GridViewEx gridView;
        protected HiddenField hfImage;
        protected HiddenField hfWeiboCount;
        private const string ImagePath = "/Upload/Weibo/{0}/";
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        private Maticsoft.BLL.Ms.WeiBoMsg msgBll = new Maticsoft.BLL.Ms.WeiBoMsg();
        private Maticsoft.BLL.Ms.WeiBoTaskMsg taskMsgBll = new Maticsoft.BLL.Ms.WeiBoTaskMsg();
        protected TextBox txtDate;
        protected HtmlTextArea txtDesc;
        protected TextBox txtKeyword;
        private const string UploadPath = "/Upload/Temp/{0}/";

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[3].Visible = false;
            }
            DataSet set = new DataSet();
            string strWhere = "";
            string text = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                strWhere = " WeiboMsg  like '%" + text + "%' ";
            }
            set = this.msgBll.GetList(-1, strWhere, " CreateDate  desc");
            if (set != null)
            {
                this.gridView.DataSetSource = set;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = this.txtDesc.Value;
            if (string.IsNullOrWhiteSpace(str))
            {
                MessageBox.ShowFailTip(this, "微博消息不能为空！");
            }
            else
            {
                Maticsoft.Model.Ms.WeiBoMsg model = new Maticsoft.Model.Ms.WeiBoMsg {
                    CreateDate = DateTime.Now,
                    WeiboMsg = str
                };
                string str2 = this.hfImage.Value;
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                    string path = string.Format("/Upload/Weibo/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    }
                    string str5 = str2.Replace(oldValue, path);
                    File.Move(HttpContext.Current.Server.MapPath(str2), HttpContext.Current.Server.MapPath(str5));
                    model.ImageUrl = str5;
                }
                if (Globals.SafeInt(this.hfWeiboCount.Value, 0) == 0)
                {
                    MessageBox.ShowFailTip(this, "该帐号没有绑定任何微博，请先绑定微博！");
                }
                else
                {
                    string str6 = "";
                    bool flag = false;
                    for (int i = 0; i < this.ChkWeibo.Items.Count; i++)
                    {
                        if (this.ChkWeibo.Items[i].Selected)
                        {
                            flag = true;
                            str6 = str6 + this.ChkWeibo.Items[i].Value.ToString() + ",";
                        }
                    }
                    if (flag)
                    {
                        str6 = str6.Substring(0, str6.LastIndexOf(","));
                    }
                    if (string.IsNullOrWhiteSpace(str6))
                    {
                        MessageBox.ShowFailTip(this, "请选择同步微博帐号！");
                    }
                    else
                    {
                        string url = "http://" + Globals.DomainFullName;
                        if (this.chkSetTime.Checked)
                        {
                            string text = this.txtDate.Text + " " + this.ddlHour.SelectedValue + ":" + this.ddlMins.SelectedValue;
                            model.PublishDate = new DateTime?((Globals.SafeDateTime(text, DateTime.Now) > DateTime.Now) ? Globals.SafeDateTime(text, DateTime.Now) : DateTime.Now);
                            int num3 = this.taskMsgBll.AddEx(model);
                            string[] args = new string[] { num3.ToString(), str6, str, url, model.ImageUrl };
                            Task.Add(Globals.SafeDateTime(text, DateTime.MinValue), new Action<string[]>(this.WeiBoCallBack), args);
                        }
                        else
                        {
                            model.PublishDate = new DateTime?(DateTime.Now);
                            this.msgBll.Add(model);
                            this.bindBll.SendWeiBo(str6, str, url, model.ImageUrl);
                        }
                        MessageBox.ShowSuccessTip(this, "发布成功！");
                        this.hfImage.Value = "";
                        this.txtDesc.Value = "";
                        this.gridView.OnBind();
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        protected void btnSendWeibo_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (string.IsNullOrWhiteSpace(selIDlist))
            {
                MessageBox.ShowFailTip(this, "请选择要发布的微博信息！");
            }
            else
            {
                List<Maticsoft.Model.Ms.WeiBoMsg> modelList = this.msgBll.GetModelList(" WeiBoId in (" + selIDlist + ") ");
                string str2 = "";
                bool flag = false;
                for (int i = 0; i < this.ChkWeibo2.Items.Count; i++)
                {
                    if (this.ChkWeibo2.Items[i].Selected)
                    {
                        flag = true;
                        str2 = str2 + this.ChkWeibo2.Items[i].Value.ToString() + ",";
                    }
                }
                if (flag)
                {
                    str2 = str2.Substring(0, str2.LastIndexOf(","));
                }
                if (string.IsNullOrWhiteSpace(str2))
                {
                    MessageBox.ShowFailTip(this, "请选择同步微博帐号！");
                }
                else
                {
                    if ((modelList != null) && (modelList.Count > 0))
                    {
                        string url = "http://" + Globals.DomainFullName;
                        foreach (Maticsoft.Model.Ms.WeiBoMsg msg in modelList)
                        {
                            this.bindBll.SendWeiBo(str2, msg.WeiboMsg, url, msg.ImageUrl);
                        }
                    }
                    MessageBox.ShowSuccessTip(this, "发布成功！");
                    this.gridView.OnBind();
                }
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
            int weiBoId = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.msgBll.Delete(weiBoId);
            this.gridView.OnBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.btnSave.Visible = false;
                }
                List<UserBind> weiBoList = this.bindBll.GetWeiBoList(-1);
                this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                for (int i = 0; i < 0x18; i++)
                {
                    this.ddlHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                this.ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
                for (int j = 0; j < 60; j++)
                {
                    this.ddlMins.Items.Add(new ListItem(j.ToString(), j.ToString()));
                }
                this.ddlMins.SelectedValue = DateTime.Now.Minute.ToString();
                this.hfWeiboCount.Value = weiBoList.Count.ToString();
                if ((weiBoList != null) && (weiBoList.Count > 0))
                {
                    this.ChkWeibo.DataSource = weiBoList;
                    this.ChkWeibo.DataTextField = "WeiboLogo";
                    this.ChkWeibo.DataValueField = "BindId";
                    this.ChkWeibo.DataBind();
                    this.ChkWeibo2.DataSource = weiBoList;
                    this.ChkWeibo2.DataTextField = "WeiboLogo";
                    this.ChkWeibo2.DataValueField = "BindId";
                    this.ChkWeibo2.DataBind();
                    for (int k = 0; k < this.ChkWeibo.Items.Count; k++)
                    {
                        this.ChkWeibo.Items[k].Selected = true;
                    }
                    for (int m = 0; m < this.ChkWeibo2.Items.Count; m++)
                    {
                        this.ChkWeibo2.Items[m].Selected = true;
                    }
                }
            }
        }

        private void WeiBoCallBack(string[] str_arr)
        {
            int taskId = Globals.SafeInt(str_arr[0], 0);
            try
            {
                this.taskMsgBll.RunTask(taskId);
            }
            catch (Exception)
            {
                throw;
            }
            this.bindBll.SendWeiBo(str_arr[1], str_arr[2], str_arr[3], str_arr[4]);
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x156;
            }
        }
    }
}

