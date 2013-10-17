namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoLotterytypesGetResponse : TopResponse
    {
        [XmlArrayItem("lottery_type"), XmlArray("results")]
        public List<LotteryType> Results { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

