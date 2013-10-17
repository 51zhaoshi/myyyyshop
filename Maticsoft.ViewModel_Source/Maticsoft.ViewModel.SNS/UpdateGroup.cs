namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class UpdateGroup
    {
        [Display(Name="小组简介"), DataType(DataType.MultilineText)]
        public string GroupDescription { get; set; }

        public int GroupId { get; set; }

        [DataType(DataType.Text), Display(Name="小组Logo"), Required(ErrorMessage="请上传小组Logo")]
        public string GroupLogo { get; set; }

        [Required(ErrorMessage="小组名称不能为空"), DataType(DataType.Text), Display(Name="小组名称")]
        public string GroupName { get; set; }

        public string TagList { get; set; }

        [Display(Name="小组标签"), Required(ErrorMessage="请选择小组标签")]
        public string Tags { get; set; }
    }
}

