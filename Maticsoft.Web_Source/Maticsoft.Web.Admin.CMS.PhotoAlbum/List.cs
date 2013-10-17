namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    using Maticsoft.BLL.CMS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Drawing;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class List : PageBaseAdmin
    {
        protected int Act_AddData = 0xf6;
        protected int Act_DelData = 0xf8;
        protected int Act_UpdateData = 0xf7;
        protected AspNetPager AspNetPager1;
        private PhotoAlbum bll = new PhotoAlbum();
        protected LinkButton btnDelete;
        protected Button btnSearch;
        protected HtmlGenericControl liAdd;
        protected HtmlGenericControl liDel;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected Literal Literal4;
        protected Literal Literal5;
        protected Literal Literal6;
        protected DataList RepeaterPhotoAlbum;
        protected TextBox txtKeyword;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData(this.txtKeyword.Text.Trim());
        }

        private void BindData(string strWhere)
        {
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strWhere = "AlbumName LIKE '%" + strWhere + "%'";
            }
            PhotoAlbum album = new PhotoAlbum();
            this.AspNetPager1.RecordCount = album.GetRecordCount(strWhere);
            this.RepeaterPhotoAlbum.DataSource = album.GetListByPage(strWhere, "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.RepeaterPhotoAlbum.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
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
                    MessageBox.ShowFailTip(this, Site.TooltipDelError);
                }
                this.BindData(this.txtKeyword.Text.Trim());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData(this.txtKeyword.Text.Trim());
        }

        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("lbtnDel");
                button.Visible = false;
            }
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_UpdateData)) && (base.GetPermidByActID(this.Act_UpdateData) != -1))
            {
                HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("lbtnModify");
                control.Visible = false;
            }
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.RepeaterPhotoAlbum.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.RepeaterPhotoAlbum.Items[i].FindControl("ckAlbum");
                HiddenField field = (HiddenField) this.RepeaterPhotoAlbum.Items[i].FindControl("hfAlbumID");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
                {
                    this.liDel.Visible = false;
                    this.btnDelete.Visible = false;
                }
                if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_AddData)) && (base.GetPermidByActID(this.Act_AddData) != -1))
                {
                    this.liAdd.Visible = false;
                }
                if (this.Session["Style"] != null)
                {
                    string str = this.Session["Style"] + "xtable_bordercolorlight";
                    if (base.Application[str] != null)
                    {
                        this.RepeaterPhotoAlbum.BorderColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                        this.RepeaterPhotoAlbum.HeaderStyle.BackColor = ColorTranslator.FromHtml(base.Application[str].ToString());
                    }
                }
                this.BindData(this.txtKeyword.Text.Trim());
            }
        }

        protected void RepeaterPhotoAlbum_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                PhotoAlbum album = new PhotoAlbum();
                if (e.CommandArgument != null)
                {
                    int albumID = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    if (!album.Delete(albumID))
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        return;
                    }
                    new Photo().UpdatePhotoAlbum(albumID, 0);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.BindData(this.txtKeyword.Text.Trim());
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0xf3;
            }
        }
    }
}

