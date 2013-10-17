namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class CollectItem : TopObject
    {
        [XmlElement("item_numid")]
        public long ItemNumid { get; set; }

        [XmlElement("item_owner_nick")]
        public string ItemOwnerNick { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }
    }
}

