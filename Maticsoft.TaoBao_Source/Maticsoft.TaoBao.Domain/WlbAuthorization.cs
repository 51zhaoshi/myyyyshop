namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbAuthorization : TopObject
    {
        [XmlElement("authorize_end_time")]
        public string AuthorizeEndTime { get; set; }

        [XmlElement("authorize_id")]
        public long AuthorizeId { get; set; }

        [XmlElement("authorize_start_time")]
        public string AuthorizeStartTime { get; set; }

        [XmlElement("consign_user_id")]
        public long ConsignUserId { get; set; }

        [XmlElement("item_id")]
        public long ItemId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("owner_user_id")]
        public long OwnerUserId { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("rule_code")]
        public string RuleCode { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}

