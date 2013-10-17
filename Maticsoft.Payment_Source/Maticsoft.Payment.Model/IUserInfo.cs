namespace Maticsoft.Payment.Model
{
    using System;

    public interface IUserInfo
    {
        string Email { get; set; }

        int UserId { get; set; }
    }
}

