namespace Maticsoft.ShoppingCart.Model
{
    using System;

    public interface IUserInfo
    {
        string CellPhone { get; set; }

        string Email { get; set; }

        string Name { get; set; }

        string TelPhone { get; set; }

        int UserId { get; set; }
    }
}

