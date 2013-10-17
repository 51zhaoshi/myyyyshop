namespace Maticsoft.Web.CMS.Content
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
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

    public class Modify : PageBaseAdmin
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
        protected CheckBox chkStatic;
        protected DropDownList dropClass;
        protected HiddenField hfClassID;
        protected HiddenField hfCreatedDate;
        protected HiddenField hfPvCount;
        protected HiddenField hfs_Attachment;
        protected HiddenField HiddenField_ICOPath;
        protected HiddenField HiddenField_IsDeleteAttachment;
        protected HiddenField HiddenField_IsModifyAttachment;
        protected HiddenField HiddenField_ISModifyImage;
        protected HiddenField HiddenField_OldAttachPath;
        protected Image imgUrl;
        protected Label lblContentID;
        protected Literal lblHelpSecret;
        protected Label lblUser;
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
        protected Literal Literal21;
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
        protected ValidateTarget ValidateTargetName;
        protected Maticsoft.Web.Validator.ValidatorContainer ValidatorContainer;

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            foreach (DataRow row in dt.Select("ParentID= " + parentid))
            {
                string str = Globals.SafeString(row["ClassID"], "0");
                string text = Globals.SafeString(row["ClassName"], "0");
                text = blank + "『" + text + "』";
                this.dropClass.Items.Add(new ListItem(text, str));
                int num = int.Parse(str);
                string str3 = blank + "─";
                this.BindNode(num, dt, str3);
            }
        }

        private void BindTree()
        {
            this.dropClass.Items.Clear();
            DataSet treeList = this.bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(treeList))
            {
                DataTable dt = treeList.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    foreach (DataRow row in dt.Select("ParentID= " + 0))
                    {
                        string str = Globals.SafeString(row["ClassID"], "0");
                        string text = Globals.SafeString(row["ClassName"], "0");
                        Globals.SafeString(row["ParentID"], "0");
                        text = "╋" + text;
                        this.dropClass.Items.Add(new ListItem(text, str));
                        int parentid = int.Parse(str);
                        string blank = "├";
                        this.BindNode(parentid, dt, blank);
                    }
                }
            }
            this.dropClass.DataBind();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("List.aspx?type=0");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int classID = Globals.SafeInt(this.dropClass.SelectedValue, 0);
            Maticsoft.Model.CMS.ContentClass modelByCache = this.bllContentClass.GetModelByCache(classID);
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, CMS.TitleErrorAddContent);
            }
            else if ((modelByCache != null) && !modelByCache.AllowAddContent)
            {
                MessageBox.ShowFailTip(this, CMS.ContentErrorAddContent);
            }
            else if (!PageValidate.IsNumber(this.lblContentID.Text))
            {
                MessageBox.ShowFailTip(this, CMS.ContentErrorContentID);
            }
            else if (string.IsNullOrWhiteSpace(this.txtSeoUrl.Text))
            {
                MessageBox.ShowFailTip(this, "请填写SeoURL地址!");
            }
            else if (this.ContentID > 0)
            {
                Maticsoft.Model.CMS.Content model = this.bll.GetModel(this.ContentID);
                if (model == null)
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
                model.Title = Globals.HtmlEncode(this.txtTitle.Text);
                model.Attachment = this.hfs_Attachment.Value;
                model.IsRecomend = this.chkIsRecomend.Checked;
                model.IsHot = this.chkIsHot.Checked;
                model.IsColor = this.chkIsColor.Checked;
                model.IsTop = this.chkIsTop.Checked;
                model.Meta_Title = this.txtMeta_Title.Text;
                model.Meta_Keywords = this.txtMeta_Keywords.Text;
                model.Meta_Description = this.txtMeta_Description.Text;
                if (!string.IsNullOrWhiteSpace(this.txtSubTitle.Text))
                {
                    model.SubTitle = Globals.HtmlEncode(this.txtSubTitle.Text);
                }
                else
                {
                    model.SubTitle = Globals.HtmlEncode(this.txtTitle.Text);
                }
                model.Summary = Globals.HtmlEncode(this.txtSummary.Text);
                model.LinkUrl = Globals.HtmlEncode(this.txtLinkUrl.Text);
                model.Remary = Globals.HtmlEncode(this.txtRemary.Text);
                if (this.txtBeFrom.Text.Length > 0)
                {
                    model.BeFrom = this.txtBeFrom.Text;
                }
                else
                {
                    WebSiteSet set = new WebSiteSet(ApplicationKeyType.CMS);
                    model.BeFrom = set.WebName;
                }
                model.LastEditUserID = new int?(base.CurrentUser.UserID);
                model.LastEditDate = new DateTime?(DateTime.Now);
                model.State = int.Parse(this.radlState.SelectedValue);
                model.ClassID = classID;
                model.Keywords = Globals.HtmlEncode(this.txtKeywords.Text);
                model.Sequence = int.Parse(this.txtOrders.Text);
                model.Description = this.txtContent.Text;
                model.SeoUrl = this.txtSeoUrl.Text;
                string oldValue = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                string newValue = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));
                string str3 = string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));
                ArrayList fileNameList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                {
                    string str4 = string.Format(this.HiddenField_ICOPath.Value, "");
                    fileNameList.Add(str4.Replace(oldValue, ""));
                    model.ImageUrl = str4.Replace(oldValue, newValue);
                }
                else
                {
                    model.ImageUrl = this.HiddenField_ICOPath.Value;
                }
                string fileName = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.HiddenField_IsModifyAttachment.Value))
                {
                    string str6 = string.Format(this.hfs_Attachment.Value, "");
                    fileName = str6.Replace(oldValue, "");
                    model.Attachment = str6.Replace(oldValue, str3);
                }
                else
                {
                    model.Attachment = this.hfs_Attachment.Value;
                }
                if (this.bll.Update(model))
                {
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_IsDeleteAttachment.Value))
                    {
                        FileManage.DeleteFile(base.Server.MapPath(this.HiddenField_OldAttachPath.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_ISModifyImage.Value))
                    {
                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(newValue), fileNameList);
                    }
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_IsModifyAttachment.Value))
                    {
                        FileManage.MoveFile(base.Server.MapPath(oldValue), base.Server.MapPath(str3), fileName);
                    }
                    if (this.chkStatic.Checked)
                    {
                        string valueByCache = ConfigSystem.GetValueByCache("MainArea");
                        string str8 = "";
                        string str9 = PageSetting.GetCMSUrl(this.ContentID, "CMS", ApplicationKeyType.CMS);
                        if (valueByCache == "CMS")
                        {
                            str8 = "/Article/Details/" + this.ContentID;
                        }
                        else
                        {
                            str8 = "/CMS/Article/Details/" + this.ContentID;
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
                    DataCache.DeleteCache("ContentModel-" + model.ContentID);
                    DataCache.DeleteCache("ContentModelEx-" + model.ContentID);
                    MessageBox.ShowSuccessTip(this, Site.TooltipSaveOK, "List.aspx?type=0");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Site.TooltipSaveError);
                }
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.BindTree();
                if (this.ContentID > 0)
                {
                    this.ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content model = this.bll.GetModel(this.ContentID);
            if (model != null)
            {
                this.lblContentID.Text = model.ContentID.ToString();
                this.txtTitle.Text = Globals.HtmlDecode(model.Title);
                this.txtSubTitle.Text = Globals.HtmlDecode(model.SubTitle);
                this.txtBeFrom.Text = model.BeFrom;
                this.txtSummary.Text = Globals.HtmlDecode(model.Summary);
                this.txtContent.Text = model.Description;
                this.txtLinkUrl.Text = Globals.HtmlDecode(model.LinkUrl);
                this.hfPvCount.Value = model.PvCount.ToString();
                this.radlState.SelectedValue = model.State.ToString();
                this.dropClass.SelectedValue = model.ClassID.ToString();
                this.txtKeywords.Text = Globals.HtmlDecode(model.Keywords);
                this.txtOrders.Text = model.Sequence.ToString();
                this.chkIsRecomend.Checked = model.IsRecomend;
                this.chkIsHot.Checked = model.IsHot;
                this.chkIsColor.Checked = model.IsColor;
                this.chkIsTop.Checked = model.IsTop;
                this.txtRemary.Text = Globals.HtmlDecode(model.Remary);
                this.lblUser.Text = model.CreatedUserID.ToString();
                this.imgUrl.ImageUrl = "~/images/no_photo_s.png";
                this.txtSeoUrl.Text = model.SeoUrl;
                this.txtMeta_Title.Text = model.Meta_Title;
                this.txtMeta_Keywords.Text = model.Meta_Keywords;
                this.txtMeta_Description.Text = model.Meta_Description;
                if (!string.IsNullOrWhiteSpace(model.ImageUrl))
                {
                    this.HiddenField_ICOPath.Value = this.imgUrl.ImageUrl = model.ImageUrl;
                }
                if (!string.IsNullOrWhiteSpace(model.Attachment))
                {
                    this.hfs_Attachment.Value = model.Attachment;
                    this.HiddenField_OldAttachPath.Value = model.Attachment;
                }
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xe5;
            }
        }

        protected int ContentID
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

