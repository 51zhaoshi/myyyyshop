namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WidgetCartpanelGetResponse : TopResponse
    {
        [XmlArrayItem("widget_cart_info"), XmlArray("cart_info")]
        public List<WidgetCartInfo> CartInfo { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

