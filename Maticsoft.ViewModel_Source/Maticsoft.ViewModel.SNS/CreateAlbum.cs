namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class CreateAlbum
    {
        [Required(ErrorMessage="请输入专辑名称"), Display(Name="专辑名称")]
        public string AlbumName { get; set; }

        [DataType(DataType.MultilineText), Display(Name="专辑描述")]
        public string Description { get; set; }

        [Display(Name="专辑标签")]
        public string Tags { get; set; }

        [Display(Name="专辑类型")]
        public string Type { get; set; }

        [Required(ErrorMessage="请选择专辑类型"), Display(Name="专辑类型")]
        public List<SelectListItem> TypeList { get; set; }
    }
}

