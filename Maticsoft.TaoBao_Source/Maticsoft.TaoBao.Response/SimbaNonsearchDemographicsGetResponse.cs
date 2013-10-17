namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaNonsearchDemographicsGetResponse : TopResponse
    {
        [XmlArrayItem("demographic_setting"), XmlArray("demographic_setting_list")]
        public List<DemographicSetting> DemographicSettingList { get; set; }
    }
}

