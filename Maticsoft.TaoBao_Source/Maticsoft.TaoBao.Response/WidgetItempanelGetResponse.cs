namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WidgetItempanelGetResponse : TopResponse
    {
        [XmlElement("add_url")]
        public string AddUrl { get; set; }

        [XmlElement("item")]
        public WidgetItem Item { get; set; }
    }
}

