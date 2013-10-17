namespace Maticsoft.Web.Admin.Ms.Themes
{
    using Maticsoft.BLL.Ms;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.Ms;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData;
        protected AspNetPager AspNetPager1;
        private readonly Maticsoft.BLL.Ms.Theme bll = new Maticsoft.BLL.Ms.Theme();
        protected Button btnDelete;
        protected Button btnIndex;
        protected DataList DataListPhoto;
        private string DedecompressionPath = "/Areas/{0}/Themes/";
        private string FilePath = "/Upload/Themes/";
        protected Button goback;
        protected Literal Literal8;
        protected Literal txtPhoto;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void BindData()
        {
            List<Maticsoft.Model.Ms.Theme> themes = FileHelper.GetThemes(ConfigSystem.GetValueByCache("MainArea"));
            string valueByCache = ConfigSystem.GetValueByCache("ThemeCurrent");
            foreach (Maticsoft.Model.Ms.Theme theme in themes)
            {
                if (theme.Name == valueByCache)
                {
                    theme.IsCurrent = true;
                }
            }
            this.DataListPhoto.DataSource = themes;
            this.DataListPhoto.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteList(selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "批量删除模板(" + selIDlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                    this.BindData();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
            }
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("javascript:window.top.location.href='/'");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if ((e.CommandName == "delete") && (e.CommandArgument != null))
            {
                int iD = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Ms.Theme model = this.bll.GetModel(iD);
                if (model == null)
                {
                    MessageBox.ShowFailTip(this, "重现异常，请刷新重试");
                    return;
                }
                if (model.Name == "Default")
                {
                    MessageBox.ShowFailTip(this, "默认模版不能删除");
                    return;
                }
                List<string> fileUrls = new List<string> {
                    model.PreviewPhotoSrc,
                    model.ZipPackageSrc
                };
                if (this.bll.Delete(iD))
                {
                    try
                    {
                        string error = "";
                        FileHelper.DeleteFile(fileUrls, ref error);
                        this.DeleteDir(string.Format(this.DedecompressionPath, "SNS") + model.Name);
                        this.DeleteDir(string.Format(this.DedecompressionPath, "CMS") + model.Name);
                        this.DeleteDir(string.Format(this.DedecompressionPath, "Shop") + model.Name);
                        MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                        goto Label_0134;
                    }
                    catch (Exception)
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        return;
                    }
                }
                MessageBox.ShowFailTip(this, Site.TooltipDelError);
                return;
            }
        Label_0134:
            if ((e.CommandName == "start") && (e.CommandArgument != null))
            {
                string str2 = e.CommandArgument.ToString();
                ConfigSystem.Modify("ThemeCurrent", str2, "当前主模板的名称", ApplicationKeyType.None);
                DataCache.SetCache("ThemeCurrent", str2);
                HttpRuntime.UnloadAppDomain();
                MessageBox.ShowSuccessTip(this, "启用成功", "List.aspx");
            }
            if ((e.CommandName == "reload") && (e.CommandArgument != null))
            {
                int num2 = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Ms.Theme theme2 = this.bll.GetModel(num2);
                if (theme2.Name == "Default")
                {
                    MessageBox.ShowFailTip(this, "默认模版不能重新加载");
                }
                else
                {
                    this.DeleteDir(string.Format(this.DedecompressionPath, "SNS") + theme2.Name);
                    this.DeleteDir(string.Format(this.DedecompressionPath, "CMS") + theme2.Name);
                    this.DeleteDir(string.Format(this.DedecompressionPath, "Shop") + theme2.Name);
                    if (!File.Exists(base.Server.MapPath(theme2.ZipPackageSrc)))
                    {
                        MessageBox.ShowFailTip(this, "原模版压缩文件丢失...请重新上传相应的文件后重新加载");
                    }
                    else if (FileHelper.UnpackFiles(base.Server.MapPath(theme2.ZipPackageSrc), base.Server.MapPath(this.FilePath)))
                    {
                        MessageBox.ShowSuccessTip(this, "重新加载成功");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "重新加载失败，请重试");
                    }
                }
            }
        }

        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("lbtnDel");
                LinkButton button2 = (LinkButton) e.Item.FindControl("linkstart");
                HiddenField field = (HiddenField) e.Item.FindControl("hidIsCurrent");
                Literal literal = (Literal) e.Item.FindControl("lblCurrent");
                if (Globals.SafeBool(field.Value, false))
                {
                    button2.Visible = false;
                    literal.Visible = true;
                }
                else
                {
                    literal.Visible = false;
                    button2.Visible = true;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    button.Visible = false;
                }
            }
        }

        protected void ddrSampleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void DeleteDir(string dir)
        {
            if (Directory.Exists(base.Server.MapPath(dir)))
            {
                try
                {
                    Directory.Delete(base.Server.MapPath(dir), true);
                }
                catch (Exception)
                {
                    MessageBox.ShowFailTip(this, "删除目录【" + dir + "】失败，请稍后再试");
                }
            }
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListPhoto.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListPhoto.Items[i].FindControl("ckPhoto");
                HiddenField field = (HiddenField) this.DataListPhoto.Items[i].FindControl("hfPhotoId");
                if ((box != null) && box.Checked)
                {
                    flag = true;
                    if (field.Value != null)
                    {
                        str = str + field.Value + ",";
                    }
                }
            }
            if (flag)
            {
                str = str.Substring(0, str.LastIndexOf(","));
            }
            return str;
        }

        protected void goback_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("SampleList.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindData();
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x14e;
            }
        }

        public int SampleId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["id"]))
                {
                    num = Globals.SafeInt(base.Request.Params["id"], 0);
                }
                return num;
            }
        }
    }
}

