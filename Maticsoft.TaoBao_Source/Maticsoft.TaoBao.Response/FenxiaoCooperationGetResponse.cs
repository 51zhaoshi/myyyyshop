namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoCooperationGetResponse : TopResponse
    {
        [XmlArray("cooperations"), XmlArrayItem("cooperation")]
        public List<Cooperation> Cooperations { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

