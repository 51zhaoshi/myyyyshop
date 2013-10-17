namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceOnlinetimeGetResponse : TopResponse
    {
        [XmlArrayItem("online_times_on_day"), XmlArray("online_times_list_on_days")]
        public List<OnlineTimesOnDay> OnlineTimesListOnDays { get; set; }
    }
}

