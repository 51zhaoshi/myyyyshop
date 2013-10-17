namespace Maticsoft.Web
{
    using System;

    public interface IPageBaseMessageTip
    {
        string lblFalse { get; }

        string lblTrue { get; }

        string TooltipDelConfirm { get; }

        string TooltipForceLogin { get; }

        string TooltipNoAuthenticated { get; }

        string TooltipNoPermission { get; }
    }
}

