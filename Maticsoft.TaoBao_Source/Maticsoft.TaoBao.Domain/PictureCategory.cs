namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class PictureCategory : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("parent_id")]
        public long ParentId { get; set; }

        [XmlElement("picture_category_id")]
        public long PictureCategoryId { get; set; }

        [XmlElement("picture_category_name")]
        public string PictureCategoryName { get; set; }

        [XmlElement("position")]
        public long Position { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

