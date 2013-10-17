namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoPresentWinItemsGetResponse : TopResponse
    {
        [XmlArrayItem("lottery_wangcai_present"), XmlArray("results")]
        public List<LotteryWangcaiPresent> Results { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

