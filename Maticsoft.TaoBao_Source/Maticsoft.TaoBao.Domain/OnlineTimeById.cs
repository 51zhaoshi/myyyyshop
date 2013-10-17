namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class OnlineTimeById : TopObject
    {
        [XmlElement("online_times")]
        public long OnlineTimes { get; set; }

        [XmlElement("service_staff_id")]
        public string ServiceStaffId { get; set; }
    }
}

