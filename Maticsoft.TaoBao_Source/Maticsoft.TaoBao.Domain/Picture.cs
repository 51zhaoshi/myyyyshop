namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Picture : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("deleted")]
        public string Deleted { get; set; }

        [XmlElement("md5")]
        public string Md5 { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("picture_category_id")]
        public long PictureCategoryId { get; set; }

        [XmlElement("picture_id")]
        public long PictureId { get; set; }

        [XmlElement("picture_path")]
        public string PicturePath { get; set; }

        [XmlElement("pixel")]
        public string Pixel { get; set; }

        [XmlElement("referenced")]
        public bool Referenced { get; set; }

        [XmlElement("sizes")]
        public long Sizes { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("uid")]
        public long Uid { get; set; }
    }
}

