namespace Maticsoft.Web.Admin.SNS.GroupUsers
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Wuqi.Webdiyer;

    public class List : PageBaseAdmin
    {
        protected int Act_DelData = 0x23b;
        protected AspNetPager AspNetPager1;
        private GroupUsers bll = new GroupUsers();
        protected Button btnDelete;
        protected Button btnSearch;
        protected DataList DataListUser;
        protected int groupid;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;
        protected TextBox txtKeyword;

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }

        public void BindData()
        {
            new DataSet();
            StringBuilder builder = new StringBuilder();
            if (this.GroupId > 0)
            {
                builder.Append("GroupID=" + this.GroupId);
            }
            if (this.txtKeyword.Text.Trim() != "")
            {
                if (builder.Length > 0)
                {
                    builder.Append("and");
                }
                builder.AppendFormat("NickName like '%{0}%'", this.txtKeyword.Text.Trim());
            }
            this.AspNetPager1.RecordCount = this.bll.GetRecordCount(builder.ToString());
            this.DataListUser.DataSource = this.bll.GetListByPage(builder.ToString(), "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            this.DataListUser.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int groupId = this.GroupId;
            string selIDlist = this.GetSelIDlist();
            if (selIDlist.Trim().Length != 0)
            {
                if (this.bll.DeleteEx(groupId, selIDlist))
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Concat(new object[] { "移除群组（id=", this.GroupId, "）用户（userid=", selIDlist, "）成功" }), this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                else
                {
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Concat(new object[] { "移除群组（id=", this.GroupId, "）用户（userid=", selIDlist, "）失败" }), this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelError);
                }
                this.BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindData();
        }

        protected void DataListUser_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                GroupUsers users = new GroupUsers();
                if (e.CommandArgument != null)
                {
                    string[] strArray = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int userID = Globals.SafeInt(strArray[0], 0);
                    int groupId = Globals.SafeInt(strArray[1], 0);
                    if (!users.DeleteEx(groupId, userID))
                    {
                        MessageBox.ShowFailTip(this, Site.TooltipDelError);
                        return;
                    }
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, string.Concat(new object[] { "移除群组（id=", groupId, "）用户（userid=", userID, "）成功" }), this);
                    MessageBox.ShowSuccessTip(this, Site.TooltipDelOK);
                }
                this.BindData();
            }
        }

        protected void DataListUser_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData)) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                LinkButton button = (LinkButton) e.Item.FindControl("lbtnDel");
                button.Visible = false;
            }
        }

        private string GetSelIDlist()
        {
            string str = "";
            bool flag = false;
            for (int i = 0; i < this.DataListUser.Items.Count; i++)
            {
                CheckBox box = (CheckBox) this.DataListUser.Items[i].FindControl("ckUser");
                HiddenField field = (HiddenField) this.DataListUser.Items[i].FindControl("hfUserId");
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

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button1 = (LinkButton) e.Row.FindControl("LinkButton1");
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && !base.UserPrincipal.HasPermissionID(base.GetPermidByActID(this.Act_DelData))) && (base.GetPermidByActID(this.Act_DelData) != -1))
            {
                this.btnDelete.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 570;
            }
        }

        public int GroupId
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["GroupID"]))
                {
                    num = Globals.SafeInt(base.Request.Params["GroupID"], 0);
                }
                return num;
            }
        }
    }
}

