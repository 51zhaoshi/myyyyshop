namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CaipiaoPresentItemsGetResponse : TopResponse
    {
        [XmlArray("results"), XmlArrayItem("lottery_wangcai_present")]
        public List<LotteryWangcaiPresent> Results { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

