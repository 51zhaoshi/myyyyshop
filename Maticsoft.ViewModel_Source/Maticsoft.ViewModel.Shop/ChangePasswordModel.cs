namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class ChangePasswordModel
    {
        [Compare("NewPassword", ErrorMessage="新密码和确认密码不匹配"), DataType(DataType.Password), Display(Name="确认新密码")]
        public string ConfirmPassword { get; set; }

        [StringLength(100, ErrorMessage="{0} 必须至少包含 {2} 个字符", MinimumLength=6), Required, DataType(DataType.Password), Display(Name="新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Display(Name="当前密码"), Required]
        public string OldPassword { get; set; }
    }
}

