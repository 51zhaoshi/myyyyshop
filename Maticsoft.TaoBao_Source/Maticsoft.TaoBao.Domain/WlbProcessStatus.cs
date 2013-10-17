namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbProcessStatus : TopObject
    {
        [XmlElement("content")]
        public string Content { get; set; }

        [XmlElement("operater")]
        public string Operater { get; set; }

        [XmlElement("operate_time")]
        public string OperateTime { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("status_code")]
        public string StatusCode { get; set; }

        [XmlElement("store_code")]
        public string StoreCode { get; set; }

        [XmlElement("store_tp_code")]
        public string StoreTpCode { get; set; }

        [XmlElement("tms_order_code")]
        public string TmsOrderCode { get; set; }

        [XmlElement("tms_tp_code")]
        public string TmsTpCode { get; set; }
    }
}

