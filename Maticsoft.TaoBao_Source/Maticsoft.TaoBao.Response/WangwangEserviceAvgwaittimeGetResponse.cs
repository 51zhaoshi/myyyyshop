namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceAvgwaittimeGetResponse : TopResponse
    {
        [XmlArray("waiting_time_list_on_days"), XmlArrayItem("waiting_times_on_day")]
        public List<WaitingTimesOnDay> WaitingTimeListOnDays { get; set; }
    }
}

