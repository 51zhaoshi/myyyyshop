namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LogOnModel
    {
        [DataType(DataType.Password), Display(Name="密码"), Required(ErrorMessage="请输入密码")]
        public string Password { get; set; }

        [Display(Name="记住登录状态")]
        public bool RememberMe { get; set; }

        [DataType(DataType.EmailAddress), Required(ErrorMessage="请输入用户名"), Display(Name="用户名")]
        public string UserName { get; set; }
    }
}

