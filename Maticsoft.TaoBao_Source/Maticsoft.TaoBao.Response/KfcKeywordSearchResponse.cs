namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class KfcKeywordSearchResponse : TopResponse
    {
        [XmlElement("kfc_search_result")]
        public Maticsoft.TaoBao.Domain.KfcSearchResult KfcSearchResult { get; set; }
    }
}

