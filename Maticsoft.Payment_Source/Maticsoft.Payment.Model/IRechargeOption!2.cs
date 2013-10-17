namespace Maticsoft.Payment.Model
{
    using System;
    using System.Web;

    public interface IRechargeOption<T, U> where T: class, IRechargeRequest, new() where U: class, IUserInfo, new()
    {
        U GetCurrentUser(HttpContext context);
        T GetRechargeRequest(long rechargeId);
        bool PayForRechargeRequest(T rechargeRequest);

        string NotifyUrl { get; }

        string ReturnUrl { get; }
    }
}

