namespace Maticsoft.Web.Admin
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.BLL.Members;
    using Maticsoft.Common;
    using Maticsoft.Model.Members;
    using Maticsoft.Web;
    using System;
    using System.Web.Security;
    using System.Web.UI;

    public class Logout : Page
    {
        private string defaullogin = ConfigSystem.GetValueByCache("DefaultLoginAdmin");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session[Globals.SESSIONKEY_ADMIN] != null)
            {
                User user = (User) this.Session[Globals.SESSIONKEY_ADMIN];
                LogHelp.AddUserLog(user.UserName, user.UserType, "退出系统", this);
                UsersExp exp = new UsersExp();
                UsersExpModel usersExpModel = new UsersExpModel();
                usersExpModel = exp.GetUsersExpModel(user.UserID);
                if (usersExpModel != null)
                {
                    usersExpModel.LastAccessIP = base.Request.UserHostAddress;
                    usersExpModel.LastLoginTime = DateTime.Now;
                    exp.UpdateUsersExp(usersExpModel);
                }
            }
            FormsAuthentication.SignOut();
            this.Session.Remove(Globals.SESSIONKEY_ADMIN);
            this.Session.Clear();
            this.Session.Abandon();
            base.Response.Clear();
            base.Response.Redirect(this.defaullogin);
            base.Response.End();
        }
    }
}

