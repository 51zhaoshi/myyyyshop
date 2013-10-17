namespace Maticsoft.Web
{
    using Resources;
    using System;

    public class PageBaseMessageTip : IPageBaseMessageTip
    {
        public string lblFalse
        {
            get
            {
                return Site.lblFalse;
            }
        }

        public string lblTrue
        {
            get
            {
                return Site.lblTrue;
            }
        }

        public string TooltipDelConfirm
        {
            get
            {
                return Site.TooltipDelConfirm;
            }
        }

        public string TooltipForceLogin
        {
            get
            {
                return Site.TooltipForceLogin;
            }
        }

        public string TooltipNoAuthenticated
        {
            get
            {
                return Site.TooltipNoAuthenticated;
            }
        }

        public string TooltipNoPermission
        {
            get
            {
                return Site.TooltipNoPermission;
            }
        }
    }
}

