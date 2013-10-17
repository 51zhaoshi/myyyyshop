namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ArticleSub : TopObject
    {
        [XmlElement("article_code")]
        public string ArticleCode { get; set; }

        [XmlElement("article_name")]
        public string ArticleName { get; set; }

        [XmlElement("autosub")]
        public bool Autosub { get; set; }

        [XmlElement("deadline")]
        public string Deadline { get; set; }

        [XmlElement("expire_notice")]
        public bool ExpireNotice { get; set; }

        [XmlElement("item_code")]
        public string ItemCode { get; set; }

        [XmlElement("item_name")]
        public string ItemName { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }
    }
}

