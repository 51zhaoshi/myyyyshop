namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoPresentStatGetResponse : TopResponse
    {
        [XmlArray("results"), XmlArrayItem("lottery_wangcai_present_stat")]
        public List<LotteryWangcaiPresentStat> Results { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

