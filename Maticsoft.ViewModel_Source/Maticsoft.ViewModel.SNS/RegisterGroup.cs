namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class RegisterGroup
    {
        [DataType(DataType.MultilineText), Display(Name="小组简介")]
        public string GroupDescription { get; set; }

        [DataType(DataType.Text), Required(ErrorMessage="请上传小组Logo"), Display(Name="小组Logo")]
        public string GroupLogo { get; set; }

        [DataType(DataType.Text), Display(Name="小组名称"), Remote("IsExistGroupName", "Group", ErrorMessage="小组名称已经被Ta人抢注, 换个试试"), Required(ErrorMessage="小组名称不能为空")]
        public string GroupName { get; set; }

        public string TagList { get; set; }

        [Required(ErrorMessage="请选择小组标签"), Display(Name="小组标签")]
        public string Tags { get; set; }
    }
}

