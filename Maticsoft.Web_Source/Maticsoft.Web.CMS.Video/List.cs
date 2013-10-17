namespace Maticsoft.Web.CMS.Video
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Common.Video;
    using Maticsoft.Model.CMS;
    using Maticsoft.Web;
    using Maticsoft.Web.Controls;
    using Resources;
    using System;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0x111;
        protected int Act_DelData = 0x113;
        protected int Act_UpdateData = 0x112;
        private Maticsoft.BLL.CMS.Video bll = new Maticsoft.BLL.CMS.Video();
        protected Button btnBatch;
        protected Button btnSearch;
        protected DropDownList dropAlbum;
        protected DropDownList dropState;
        protected DropDownList dropType;
        protected GridViewEx gridView;
        protected LinkButton lbtnDelete;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal ltlAdd;
        protected Literal ltlAlbum;
        protected Literal ltlList;
        protected Literal ltlSearch;
        protected Literal ltlState;
        protected Literal ltlTip;
        protected TextBox txtKeyword;
        protected VideoClassDropList VideoClassDropList1;

        public void BindData()
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                this.gridView.Columns[7].Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.gridView.Columns[8].Visible = false;
            }
            DataSet listEx = new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.txtKeyword.Text.Trim() != "")
            {
                builder.AppendFormat(" Title like '%{0}%' ", this.txtKeyword.Text.Trim());
            }
            if (this.dropState.SelectedValue != "")
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND State={0} ", this.dropState.SelectedValue);
                }
                else
                {
                    builder.AppendFormat(" State={0} ", this.dropState.SelectedValue);
                }
            }
            if ((this.dropAlbum.SelectedValue != "") && (this.dropAlbum.SelectedValue != "0"))
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND AlbumID={0} ", this.dropAlbum.SelectedValue);
                }
                else
                {
                    builder.AppendFormat(" AlbumID={0} ", this.dropAlbum.SelectedValue);
                }
            }
            if (this.AlbumID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND AlbumID={0} ", this.AlbumID);
                }
                else
                {
                    builder.AppendFormat(" AlbumID={0} ", this.AlbumID);
                }
            }
            if ((this.VideoClassDropList1.SelectedValue != "") && (this.VideoClassDropList1.SelectedValue != "0"))
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND VideoClassID={0} ", this.VideoClassDropList1.SelectedValue);
                }
                else
                {
                    builder.AppendFormat(" VideoClassID={0} ", this.VideoClassDropList1.SelectedValue);
                }
            }
            if (this.VideoClassID > 0)
            {
                if (builder.Length > 0)
                {
                    builder.AppendFormat(" AND VideoClassID={0} ", this.VideoClassID);
                }
                else
                {
                    builder.AppendFormat(" VideoClassID={0} ", this.VideoClassID);
                }
            }
            listEx = this.bll.GetListEx(builder.ToString(), "");
            this.gridView.DataSetSource = listEx;
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string selIDlist = this.GetSelIDlist();
            if ((selIDlist.Trim().Length != 0) && ((!string.IsNullOrWhiteSpace(this.dropType.SelectedValue) && PageValidate.IsNumber(this.dropType.SelectedValue)) && this.bll.UpdateList(selIDlist, " State=" + this.dropType.SelectedValue)))
            {
                this.gridView.OnBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.gridView.OnBind();
        }

        public string GetFlashUrl(string videoUrl)
        {
            string valueByCache = ConfigSystem.GetValueByCache("YouKuAPI");
            string flash = "";
            if (VideoHelper.IsYouKuVideoUrl(videoUrl))
            {
                YouKuInfo youKuInfo = VideoHelper.GetYouKuInfo(videoUrl);
                if (youKuInfo != null)
                {
                    flash = string.Format(valueByCache, youKuInfo.VidEncoded);
                }
            }
            if (VideoHelper.IsKu6VideoUrl(videoUrl))
            {
                Ku6Info info2 = VideoHelper.GetKu6Info(videoUrl);
                if (info2 != null)
                {
                    flash = info2.flash;
                }
            }
            return flash;
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

        public string GetUrlType(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    return str;
                }
            }
            else
            {
                return CMSVideo.LocalVideo;
            }
            return CMSVideo.OnlineVideo;
        }

        public int? GetVideInfo(object target, out string videourl, out int? UType, out int vid)
        {
            videourl = "";
            UType = 0;
            vid = 0;
            if (!StringPlus.IsNullOrEmpty(target) && PageValidate.IsNumber(target.ToString()))
            {
                Maticsoft.Model.CMS.Video modelEx = new Maticsoft.BLL.CMS.Video().GetModelEx(Globals.SafeInt(target.ToString(), 0));
                if (modelEx == null)
                {
                    return null;
                }
                vid = modelEx.VideoID;
                string videoUrl = modelEx.VideoUrl;
                UType = new int?(modelEx.UrlType);
                if (UType.HasValue)
                {
                    switch (UType.Value)
                    {
                        case 0:
                            videourl = videoUrl;
                            return UType;

                        case 1:
                        {
                            string flashUrl = this.GetFlashUrl(videoUrl);
                            if (!string.IsNullOrWhiteSpace(flashUrl))
                            {
                                videourl = flashUrl;
                            }
                            return UType;
                        }
                    }
                }
            }
            return UType;
        }

        public string GetVideoPrivacy(object target)
        {
            string str2;
            string str = string.Empty;
            if (StringPlus.IsNullOrEmpty(target) || ((str2 = target.ToString()) == null))
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return CMSVideo.SemiOpen;
                }
            }
            else
            {
                return CMSVideo.Open;
            }
            return CMSVideo.Private;
        }

        public string GetVideoState(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        return CMSVideo.TurnCode;

                    case "1":
                        return CMSVideo.TranscodingFail;

                    case "2":
                        return CMSVideo.PendingReview;

                    case "3":
                        return CMSVideo.NotYetReleased;

                    case "4":
                        return CMSVideo.Screen;

                    case "5":
                        return CMSVideo.Publish;
                }
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
            int videoID = (int) this.gridView.DataKeys[e.RowIndex].Value;
            this.bll.Delete(videoID);
            this.gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
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
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelError);
                }
                this.gridView.OnBind();
            }
        }

        protected void LoadVideoAlbumData()
        {
            DataSet allList = new Maticsoft.BLL.CMS.VideoAlbum().GetAllList();
            if (!DataSetTools.DataSetIsNull(allList))
            {
                this.dropAlbum.DataSource = allList;
                this.dropAlbum.DataTextField = "AlbumName";
                this.dropAlbum.DataValueField = "AlbumID";
                this.dropAlbum.DataBind();
            }
            this.dropAlbum.Items.Insert(0, new ListItem(Site.PleaseSelect, "0"));
        }

        public string OutHtmlCodeByVideoID(string localVideoCss, string onlineVideoCss, object target, string imageUrl, int width, int height)
        {
            int num;
            string format = "<a class=\"{0}\" {1} href=\"{2}\"/><img src=\"{3}\" alt=\"\" width=\"{4}px\" height=\"{5}px\" style=\"border:none\"  /></a>";
            string videourl = "";
            int? uType = null;
            this.GetVideInfo(target, out videourl, out uType, out num);
            if ((num > 0) && uType.HasValue)
            {
                switch (uType.Value)
                {
                    case 0:
                        return string.Format(format, new object[] { localVideoCss, "", "VideoPreview.aspx?id=" + num, imageUrl, width, height });

                    case 1:
                        return string.Format(format, new object[] { onlineVideoCss, "", videourl, imageUrl, width, height });
                }
            }
            return format;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.lbtnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                this.LoadVideoAlbumData();
                if (this.Session["Style"] != null)
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if (base.Application[str] != null)
                    {
                        this.gridView.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
            }
        }

        public string SecondToDateTime(object target)
        {
            string str = "00:00:00";
            if (!StringPlus.IsNullOrEmpty(target) && PageValidate.IsNumber(target.ToString()))
            {
                str = TimeParser.SecondToDateTime(Convert.ToInt32(target));
            }
            return str;
        }

        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x10d;
            }
        }

        public int AlbumID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["AlbumID"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                    this.dropAlbum.Enabled = false;
                    this.dropAlbum.SelectedValue = str;
                }
                return num;
            }
        }

        public int VideoClassID
        {
            get
            {
                int num = 0;
                string str = base.Request.Params["VideoClassID"];
                if (!string.IsNullOrWhiteSpace(str) && PageValidate.IsNumber(str))
                {
                    num = int.Parse(str);
                    this.VideoClassDropList1.SelectedValue = str;
                }
                return num;
            }
        }
    }
}

