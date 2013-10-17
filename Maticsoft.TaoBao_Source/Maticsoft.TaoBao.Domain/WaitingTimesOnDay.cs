namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WaitingTimesOnDay : TopObject
    {
        [XmlElement("waiting_date")]
        public string WaitingDate { get; set; }

        [XmlArray("waiting_time_by_ids"), XmlArrayItem("waiting_time_by_id")]
        public List<WaitingTimeById> WaitingTimeByIds { get; set; }
    }
}

