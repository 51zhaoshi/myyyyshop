namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PromotionActivityGetResponse : TopResponse
    {
        [XmlArrayItem("activity"), XmlArray("activitys")]
        public List<Activity> Activitys { get; set; }
    }
}

