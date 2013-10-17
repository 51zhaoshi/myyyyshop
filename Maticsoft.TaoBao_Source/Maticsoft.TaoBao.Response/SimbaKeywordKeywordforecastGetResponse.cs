namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordKeywordforecastGetResponse : TopResponse
    {
        [XmlElement("keyword_forecast")]
        public Maticsoft.TaoBao.Domain.KeywordForecast KeywordForecast { get; set; }
    }
}

