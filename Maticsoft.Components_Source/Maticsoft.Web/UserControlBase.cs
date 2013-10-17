namespace Maticsoft.Web
{
    using Maticsoft.Accounts.Bus;
    using Maticsoft.Common;
    using System;
    using System.Web.UI;

    public abstract class UserControlBase : UserControl
    {
        protected int Act_ShowInvalid = 2;

        protected UserControlBase()
        {
        }

        public int GetPermidByActID(int ActionID)
        {
            Actions actions = new Actions();
            object obj2 = actions.GetHashListByCache()[ActionID.ToString()];
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return -1;
        }

        public User CurrentUser
        {
            get
            {
                if (base.Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    return new User();
                }
                return (User) base.Session[Globals.SESSIONKEY_ADMIN];
            }
        }

        public AccountsPrincipal UserPrincipal
        {
            get
            {
                if (!this.Context.User.Identity.IsAuthenticated)
                {
                    return null;
                }
                if (base.Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    return null;
                }
                return new AccountsPrincipal(((User) base.Session[Globals.SESSIONKEY_ADMIN]).UserName);
            }
        }
    }
}

