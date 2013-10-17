namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureCategoryGetResponse : TopResponse
    {
        [XmlArrayItem("picture_category"), XmlArray("picture_categories")]
        public List<PictureCategory> PictureCategories { get; set; }
    }
}

