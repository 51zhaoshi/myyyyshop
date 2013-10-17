namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class NonReplyStatOnDay : TopObject
    {
        [XmlElement("nonreply_date")]
        public string NonreplyDate { get; set; }

        [XmlArrayItem("nonreply_stat_by_id"), XmlArray("nonreply_stat_by_ids")]
        public List<NonreplyStatById> NonreplyStatByIds { get; set; }
    }
}

