namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OnlineTimesOnDay : TopObject
    {
        [XmlElement("online_date")]
        public string OnlineDate { get; set; }

        [XmlArrayItem("online_time_by_id"), XmlArray("online_time_by_ids")]
        public List<OnlineTimeById> OnlineTimeByIds { get; set; }
    }
}

