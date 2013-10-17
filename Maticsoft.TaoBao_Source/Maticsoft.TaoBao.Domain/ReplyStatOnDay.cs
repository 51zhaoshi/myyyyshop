namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ReplyStatOnDay : TopObject
    {
        [XmlElement("reply_date")]
        public string ReplyDate { get; set; }

        [XmlArray("reply_stat_by_ids"), XmlArrayItem("reply_stat_by_id")]
        public List<ReplyStatById> ReplyStatByIds { get; set; }
    }
}

