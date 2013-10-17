namespace Maticsoft.Payment.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public class UserInfo : IUserInfo
    {
        public string Email { get; set; }

        public int UserId { get; set; }
    }
}

