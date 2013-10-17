namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class LogisticsAddressSearchResponse : TopResponse
    {
        [XmlArray("addresses"), XmlArrayItem("address_result")]
        public List<AddressResult> Addresses { get; set; }
    }
}

