namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class DeliveryTemplatesGetResponse : TopResponse
    {
        [XmlArray("delivery_templates"), XmlArrayItem("delivery_template")]
        public List<DeliveryTemplate> DeliveryTemplates { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

