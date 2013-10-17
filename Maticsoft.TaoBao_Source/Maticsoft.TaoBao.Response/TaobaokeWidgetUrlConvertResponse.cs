namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TaobaokeWidgetUrlConvertResponse : TopResponse
    {
        [XmlElement("taobaoke_item")]
        public Maticsoft.TaoBao.Domain.TaobaokeItem TaobaokeItem { get; set; }
    }
}

