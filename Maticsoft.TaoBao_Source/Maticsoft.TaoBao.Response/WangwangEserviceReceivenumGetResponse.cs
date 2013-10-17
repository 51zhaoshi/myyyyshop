namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceReceivenumGetResponse : TopResponse
    {
        [XmlArray("reply_stat_list_on_days"), XmlArrayItem("reply_stat_on_day")]
        public List<ReplyStatOnDay> ReplyStatListOnDays { get; set; }
    }
}

