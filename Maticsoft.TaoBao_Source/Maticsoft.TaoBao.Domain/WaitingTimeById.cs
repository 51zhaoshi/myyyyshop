namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WaitingTimeById : TopObject
    {
        [XmlElement("avg_waiting_times")]
        public long AvgWaitingTimes { get; set; }

        [XmlElement("service_staff_id")]
        public string ServiceStaffId { get; set; }
    }
}

