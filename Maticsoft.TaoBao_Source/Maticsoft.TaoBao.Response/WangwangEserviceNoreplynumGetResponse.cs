namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceNoreplynumGetResponse : TopResponse
    {
        [XmlArray("non_reply_stat_on_days"), XmlArrayItem("non_reply_stat_on_day")]
        public List<NonReplyStatOnDay> NonReplyStatOnDays { get; set; }
    }
}

