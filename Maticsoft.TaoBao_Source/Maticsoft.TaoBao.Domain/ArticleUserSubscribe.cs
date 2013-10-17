namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ArticleUserSubscribe : TopObject
    {
        [XmlElement("deadline")]
        public string Deadline { get; set; }

        [XmlElement("item_code")]
        public string ItemCode { get; set; }
    }
}

