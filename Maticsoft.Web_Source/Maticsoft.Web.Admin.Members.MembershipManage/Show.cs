namespace Maticsoft.Web.Admin.Members.MembershipManage
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.BLL.Ms;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Show : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.SiteMessage bll = new Maticsoft.BLL.Members.SiteMessage();
        protected Button btnCancle;
        protected Image imageGra;
        protected Label lblActivity;
        protected Label lblAddress;
        protected Label lblCreTime;
        protected Label lblEmail;
        protected Label lblID;
        protected Label lblLoginDate;
        protected Label lblNickName;
        protected Label lblPhone;
        protected Label lblPoints;
        protected Label lblSex;
        protected Label lblTrueName;
        protected Label lblUserName;
        protected Literal Literal1;
        public string strid = "";
        private Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!this.Page.IsPostBack && (base.Request.Params["id"] != null)) && (base.Request.Params["id"].Trim() != ""))
            {
                this.strid = base.Request.Params["id"];
                int iD = Convert.ToInt32(this.strid);
                this.ShowInfo(iD);
            }
        }

        private void ShowInfo(int ID)
        {
            AccountsPrincipal existingPrincipal = new AccountsPrincipal(ID);
            User user = new User(existingPrincipal);
            UsersExpModel usersExpModel = new UsersExp().GetUsersExpModel(ID);
            if ((user != null) && (usersExpModel != null))
            {
                this.lblUserName.Text = user.UserName;
                this.lblTrueName.Text = user.TrueName;
                this.lblPhone.Text = user.Phone;
                this.lblNickName.Text = user.NickName;
                this.lblEmail.Text = user.Email;
                this.lblSex.Text = (!string.IsNullOrWhiteSpace(user.Sex) && (user.Sex.Trim() == "0")) ? "女" : "男";
                this.lblActivity.Text = user.Activity ? "正常使用" : "已经冻结";
                this.lblCreTime.Text = user.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (usersExpModel != null)
            {
                Regions regions = new Regions();
                this.imageGra.ImageUrl = string.Format("/Upload/User/Gravatar/{0}.jpg", usersExpModel.UserID);
                string regionNameByRID = regions.GetRegionNameByRID(Globals.SafeInt(usersExpModel.Address, 0));
                if (regionNameByRID.Contains("北京北京"))
                {
                    regionNameByRID = regionNameByRID.Replace("北京北京", "北京");
                }
                else if (regionNameByRID.Contains("上海上海"))
                {
                    regionNameByRID = regionNameByRID.Replace("上海上海", "上海");
                }
                else if (regionNameByRID.Contains("重庆重庆"))
                {
                    regionNameByRID = regionNameByRID.Replace("重庆重庆", "重庆");
                }
                else if (regionNameByRID.Contains("天津天津"))
                {
                    regionNameByRID = regionNameByRID.Replace("天津天津", "天津");
                }
                this.lblAddress.Text = string.IsNullOrEmpty(usersExpModel.Address) ? "暂未设置" : regionNameByRID;
                this.lblPoints.Text = usersExpModel.Points.ToString();
                this.lblLoginDate.Text = usersExpModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x11f;
            }
        }
    }
}

