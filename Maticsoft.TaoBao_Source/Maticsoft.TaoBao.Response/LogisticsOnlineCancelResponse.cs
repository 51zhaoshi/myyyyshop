namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsOnlineCancelResponse : TopResponse
    {
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

        [XmlElement("modify_time")]
        public string ModifyTime { get; set; }

        [XmlElement("recreated_order_id")]
        public long RecreatedOrderId { get; set; }
    }
}

