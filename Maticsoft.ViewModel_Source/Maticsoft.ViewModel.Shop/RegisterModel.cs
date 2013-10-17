namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class RegisterModel
    {
        [Compare("Password", ErrorMessage="密码和确认密码不匹配"), DataType(DataType.Password), Display(Name="确认密码")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage="邮箱地址不能为空"), Remote("IsExistEmail", "Account", "Shop", ErrorMessage="该邮箱地址已经被注册过"), DataType(DataType.EmailAddress), RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage="请输入正确的邮箱格式"), Display(Name="电子邮件地址")]
        public string Email { get; set; }

        [Required(ErrorMessage="昵称不能为空"), Display(Name="昵称"), Remote("IsExistNickName", "Account", "Shop", ErrorMessage="昵称已经被Ta人抢注, 换个试试")]
        public string NickName { get; set; }

        [Required(ErrorMessage="密码不能为空"), Display(Name="密码"), StringLength(100, ErrorMessage="{0} 必须至少包含 {2} 个字符", MinimumLength=6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="手机号码"), DataType(DataType.PhoneNumber), RegularExpression(@"^(1(([35][0-9])|(47)|[8][0126789]))\d{8}$", ErrorMessage="请输入正确的手机号码")]
        public string Phone { get; set; }

        [Display(Name="短信效验码")]
        public string SMSCode { get; set; }

        public string UserAgreement { get; set; }
    }
}

