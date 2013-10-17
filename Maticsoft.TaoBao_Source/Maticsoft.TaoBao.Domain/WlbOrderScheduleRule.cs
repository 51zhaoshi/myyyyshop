namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbOrderScheduleRule : TopObject
    {
        [XmlElement("area_ids")]
        public string AreaIds { get; set; }

        [XmlElement("backup_store_id")]
        public long BackupStoreId { get; set; }

        [XmlElement("default_store_id")]
        public long DefaultStoreId { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("options")]
        public string Options { get; set; }

        [XmlElement("presell_store_id")]
        public long PresellStoreId { get; set; }

        [XmlElement("rule_id")]
        public long RuleId { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("user_nick")]
        public string UserNick { get; set; }
    }
}

