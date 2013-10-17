namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupCatmatchforecastGetResponse : TopResponse
    {
        [XmlElement("adgroup_catmatch_forecast")]
        public ADGroupCatMatchForecast AdgroupCatmatchForecast { get; set; }
    }
}

