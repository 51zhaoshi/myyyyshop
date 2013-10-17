namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SkusCustomGetResponse : TopResponse
    {
        [XmlArrayItem("sku"), XmlArray("skus")]
        public List<Sku> Skus { get; set; }
    }
}

