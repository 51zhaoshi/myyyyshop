namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CreativeRecord : TopObject
    {
        [XmlElement("audit_desc")]
        public string AuditDesc { get; set; }

        [XmlElement("audit_status")]
        public string AuditStatus { get; set; }

        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("creative_id")]
        public long CreativeId { get; set; }

        [XmlElement("img_url")]
        public string ImgUrl { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlElement("modify_time")]
        public string ModifyTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("old_img_url")]
        public string OldImgUrl { get; set; }

        [XmlElement("old_title")]
        public string OldTitle { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

