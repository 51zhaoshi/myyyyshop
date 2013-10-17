namespace Maticsoft.Web.Areas.SNS.Controllers
{
    using Maticsoft.BLL.Members;
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using System;
    using System.Web.Mvc;

    public class UserController : UsersProfileControllerBase
    {
        private UsersExp UserExBll = new UsersExp();

        public UserController()
        {
            base.FavBasePageSize = base.FallInitDataSize;
            base.FavAllPageSize = base.FallDataSize;
            base._PostPageSize = base.PostDataSize;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public override bool LoadUserInfo(int UserID)
        {
            base.UserModel = this.UserExBll.GetUsersExpModel(UserID);
            if (base.UserModel != null)
            {
                base.UserID = UserID;
                base.DefaultPostType = EnumHelper.PostType.User;
                base.IsCurrentUser = false;
                Maticsoft.BLL.Members.Users users = new Maticsoft.BLL.Members.Users();
                Maticsoft.Model.Members.Users model = new Maticsoft.Model.Members.Users();
                model = users.GetModel(UserID);
                base.NickName = (model != null) ? model.NickName : "";
                base.Activity = (model != null) ? model.Activity.Value : false;
                return true;
            }
            return false;
        }
    }
}

