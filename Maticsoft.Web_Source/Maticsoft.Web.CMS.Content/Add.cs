namespace Maticsoft.Web.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.CMS;
    using Maticsoft.Model.SysManage;
    using Maticsoft.Web;
    using Maticsoft.Web.Components.Setting.CMS;
    using Maticsoft.Web.Validator;
    using Resources;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
        protected Button btnCancle;
        protected Button btnSave;
        protected CheckBox chkIndex;
        protected CheckBox chkIsColor;
        protected CheckBox chkIsHot;
        protected CheckBox chkIsRecomend;
        protected CheckBox chkIsTop;
        protected CheckBox chkQQ;
        protected CheckBox chkSina;
        protected CheckBox chkStatic;
        protected DropDownList ddlType;
        protected HiddenField hfClassID;
        protected HiddenField hfs_Attachment;
        protected HiddenField HiddenField_ICOPath;
        protected Image imgUrl;
        protected Literal lblHelpSecret;
        protected Literal Literal1;
        protected Literal Literal10;
        protected Literal Literal11;
        protected Literal Literal12;
        protected Literal Literal13;
        protected Literal Literal14;
        protected Literal Literal15;
        protected Literal Literal16;
        protected Literal Literal17;
        protected Literal Literal18;
        protected Literal Literal19;
        protected Literal Literal2;
        protected Literal Literal20;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected Literal Literal7;
        protected Literal Literal8;
        protected Literal Literal9;
        protected RadioButtonList radlState;
        public string strClassID = string.Empty;
        protected HtmlTableRow Tr1;
        protected HtmlTableRow trImageUrl;
        protected HtmlTableRow TrLinkUrl;
        protected HtmlTableRow TrRemary;
        protected HtmlTableRow TrSubTitle;
        protected HtmlTableRow TrSummary;
        protected TextBox txtBeFrom;
        protected TextBox txtContent;
        protected TextBox txtKeywords;
        protected TextBox txtLinkUrl;
        protected TextBox txtMeta_Description;
        protected TextBox txtMeta_Keywords;
        protected TextBox txtMeta_Title;
        protected TextBox txtOrders;
        protected TextBox txtRemary;
        protected TextBox txtSeoUrl;
        protected TextBox txtSubTitle;
        protected TextBox txtSummary;
        protected TextBox txtTitle;
        protected HtmlGenericControl txtTitleTip;
        private string uploadFolder = ConfigSystem.GetValueByCache("UploadFolder");
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = Globals.SafeString(row["ClassID"], "0");
                string text = Globals.SafeString(row["ClassName"], "0");
                text = blank + "『" + text + "』";
                this.ddlType.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.ddlType.Items.Clear();
            new Maticsoft.BLL.CMS.ContentClass();
            DataSet treeList = this.bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                this.ddlType.Items.Clear();
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        if (Globals.SafeBool(Globals.SafeString(row["AllowAddContent"], ""), false))
                        {
                            string str = Globals.SafeString(row["ClassID"], "0");
                            string text = Globals.SafeString(row["ClassName"], "0");
                            Globals.SafeString(row["ParentID"], "0");
                            text = "╋" + text;
                            this.ddlType.Items.Add(new ListItem(text, str));
                            int parentid = int.Parse(str);
                            string blank = "├";
                            this.BindNode(parentid, dt, blank);
                        }
                    }
                }
            }
            this.ddlType.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.hfClassID.Value))
            {
                base.Response.Redirect("List.aspx" + this.hfClassID.Value);
            }
            else
            {
                base.Response.Redirect("List.aspx?type=0");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.ddlType.SelectedValue, 0);
            if (classID <= 0)
            {
                MessageBox.ShowFailTip(this, CMS.ContentErrorAddClass);
            }
            else if (string.IsNullOrWhiteSpace(this.txtSeoUrl.Text))
            {
                MessageBox.ShowFailTip(this, "请填写SeoURL地址!");
            }
            else
            {
                Maticsoft.Model.CMS.ContentClass model = this.bllContentClass.GetModel(classID);
                if (model != null)
                {
                    if (!model.AllowAddContent)
                    {
                        MessageBox.ShowFailTip(this, CMS.ContentErrorAddContent);
                        return;
                    }
                    if ((2 == model.ClassModel) && this.bll.ExistsByClassID(classID))
                    {
                        MessageBox.ShowFailTip(this, CMS.ContentErrorAddMoreContent);
                        return;
                    }
                }
                if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, CMS.TitleErrorAddContent);
                }
                else if (this.bll.ExistTitle(this.txtTitle.Text.Trim()))
                {
                    MessageBox.ShowFailTip(this, CMS.ContentTooltipTitleExist);
                }
                else
                {
                    Maticsoft.Model.CMS.Content content = new Maticsoft.Model.CMS.Content {
                        Title = Globals.HtmlEncode(this.txtTitle.Text),
                        IsRecomend = this.chkIsRecomend.Checked,
                        IsHot = this.chkIsHot.Checked,
                        IsColor = this.chkIsColor.Checked,
                        IsTop = this.chkIsTop.Checked,
                        Meta_Title = this.txtMeta_Title.Text,
                        Meta_Keywords = this.txtMeta_Keywords.Text,
                        Meta_Description = this.txtMeta_Description.Text
                    };
                    if (!string.IsNullOrWhiteSpace(this.txtSubTitle.Text))
                    {
                        content.SubTitle = Globals.HtmlEncode(this.txtSubTitle.Text);
                    }
                    else
                    {
                        content.SubTitle = Globals.HtmlEncode(this.txtTitle.Text);
                    }
                    content.Summary = Globals.HtmlEncode(this.txtSummary.Text);
                    content.LinkUrl = Globals.HtmlEncode(this.txtLinkUrl.Text);
                    content.Remary = Globals.HtmlEncode(this.txtRemary.Text);
                    if (this.txtBeFrom.Text.Length > 0)
                    {
                        content.BeFrom = this.txtBeFrom.Text;
                    }
                    else
                    {
                        WebSiteSet set = new WebSiteSet(ApplicationKeyType.CMS);
                        content.BeFrom = set.WebName;
                    }
                    content.CreatedUserID = base.CurrentUser.UserID;
                    content.LastEditDate = new DateTime?(content.CreatedDate = DateTime.Now);
                    content.LastEditUserID = new int?(base.CurrentUser.UserID);
                    content.PvCount = 0;
                    content.State = Globals.SafeInt(this.radlState.SelectedValue, 0);
                    content.ClassID = classID;
                    content.Keywords = Globals.HtmlEncode(this.txtKeywords.Text);
                    content.Sequence = Globals.SafeInt(this.txtOrders.Text, 0);
                    content.Description = this.txtContent.Text;
                    content.SeoUrl = this.txtSeoUrl.Text;
                    string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                    string newValue = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));
                    string str3 = string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));
                    ArrayList fileNameList = new ArrayList();
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                    {
                        string str4 = string.Format(this.HiddenField_ICOPath.Value, "");
                        fileNameList.Add(str4.Replace(oldValue, ""));
                        content.ImageUrl = str4.Replace(oldValue, newValue);
                    }
                    string fileName = string.Empty;
                    if (!string.IsNullOrWhiteSpace(this.hfs_Attachment.Value))
                    {
                        string str6 = string.Format(this.hfs_Attachment.Value, "");
                        fileName = str6.Replace(oldValue, "");
                        content.Attachment = str6.Replace(oldValue, str3);
                    }
                    content.TotalComment = 0;
                    content.TotalSupport = 0;
                    content.TotalFav = 0;
                    content.TotalShare = 0;
                    int newId = this.bll.Add(content);
                    if (0 < newId)
                    {
                        if (!string.IsNullOrWhiteSpace(this.HiddenField_ICOPath.Value))
                        {
                            FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                        }
                        if (!string.IsNullOrWhiteSpace(this.hfs_Attachment.Value))
                        {
                            FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(str3), fileName);
                        }
                        if (this.chkStatic.Checked)
                        {
                            string str7 = ConfigSystem.GetValueByCache("MainArea");
                            string str8 = "";
                            string str9 = PageSetting.GetCMSUrl(newId, "CMS", ApplicationKeyType.CMS);
                            if (str7 == "CMS")
                            {
                                str8 = "/Article/Details/" + newId;
                            }
                            else
                            {
                                str8 = "/CMS/Article/Details/" + newId;
                            }
                            if (!string.IsNullOrWhiteSpace(str8) && !string.IsNullOrWhiteSpace(str9))
                            {
                                GenerateHtml.HttpToStatic(str8, str9);
                            }
                        }
                        if (this.chkIndex.Checked)
                        {
                            string str10 = ConfigSystem.GetValueByCache("MainArea");
                            string str11 = "";
                            string str12 = "/index.html";
                            if (str10 == "CMS")
                            {
                                str11 = "/Home/Index?type=1";
                            }
                            else
                            {
                                str11 = "/CMS/Home/Index?type=1";
                            }
                            if (!string.IsNullOrWhiteSpace(str11) && !string.IsNullOrWhiteSpace(str12))
                            {
                                GenerateHtml.HttpToStatic(str11, str12);
                            }
                        }
                        string str13 = "";
                        str13 = this.chkSina.Checked ? "3" : "";
                        if (this.chkQQ.Checked)
                        {
                            str13 = str13 + (string.IsNullOrWhiteSpace(str13) ? "13" : ",13");
                        }
                        UserBind bind = new UserBind();
                        string valueByCache = ConfigSystem.GetValueByCache("WeiBo_CMS_Url");
                        string url = "http://" + Globals.DomainFullName + string.Format(valueByCache, newId);
                        bind.SendWeiBo(-1, str13, content.Title, url, content.ImageUrl);
                        if (!string.IsNullOrWhiteSpace(this.ddlType.SelectedValue))
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx?classid=" + this.ddlType.SelectedValue);
                        }
                        else
                        {
                            MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx?type=0");
                        }
                    }
                }
            }
        }

        private void DoCallback()
        {
            string str3;
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            if (((str3 = str) != null) && (str3 == "GetName"))
            {
                s = this.GetName();
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetName()
        {
            JsonObject obj2 = new JsonObject();
            string pinyin = "";
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Name"]))
            {
                pinyin = PinyinHelper.GetPinyin(base.Request.Form["Name"].Trim());
            }
            obj2.Put("STATUS", "OK");
            obj2.Put("DATA", pinyin);
            return obj2.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
                {
                    this.Controls.Clear();
                    this.DoCallback();
                }
                this.BindTree();
                if (this.ClassID > 0)
                {
                    this.strClassID = this.hfClassID.Value = "?classid=" + this.ClassID;
                    this.ddlType.SelectedValue = this.ClassID.ToString();
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(base.Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe4;
            }
        }

        public int ClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                }
                return num;
            }
        }
    }
}

