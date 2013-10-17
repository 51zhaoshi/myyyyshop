namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LogOnModel
    {
        [Required(ErrorMessage="请输入邮箱"), Display(Name="电子邮件地址"), RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage="请输入正确的邮箱格式"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage="请输入密码"), DataType(DataType.Password), Display(Name="密码")]
        public string Password { get; set; }

        [Display(Name="记住登录状态")]
        public bool RememberMe { get; set; }
    }
}

