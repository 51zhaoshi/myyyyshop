namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureCategoryAddResponse : TopResponse
    {
        [XmlElement("picture_category")]
        public Maticsoft.TaoBao.Domain.PictureCategory PictureCategory { get; set; }
    }
}

