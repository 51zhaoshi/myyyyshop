namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbConsignMent : TopObject
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("number")]
        public long Number { get; set; }

        [XmlElement("tgt_item_id")]
        public long TgtItemId { get; set; }

        [XmlElement("tgt_user_id")]
        public long TgtUserId { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

