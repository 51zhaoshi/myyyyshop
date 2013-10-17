namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class RegisterModel
    {
        [Display(Name="确认密码"), Compare("Password", ErrorMessage="密码和确认密码不匹配"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress), Display(Name="电子邮件地址"), RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$", ErrorMessage="请输入正确的邮箱格式"), Remote("IsExistEmail", "Account", "SNS", ErrorMessage="该邮箱地址已经被注册过"), Required(ErrorMessage="邮箱地址不能为空")]
        public string Email { get; set; }

        [Remote("IsExistNickName", "Account", "SNS", ErrorMessage="昵称已经被Ta人抢注, 换个试试"), Display(Name="昵称"), Required(ErrorMessage="昵称不能为空")]
        public string NickName { get; set; }

        [Required(ErrorMessage="密码不能为空"), DataType(DataType.Password), Display(Name="密码"), StringLength(100, ErrorMessage="{0} 必须至少包含 {2} 个字符", MinimumLength=6)]
        public string Password { get; set; }
    }
}

