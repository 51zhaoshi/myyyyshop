namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordsRecommendGetResponse : TopResponse
    {
        [XmlElement("recommend_words")]
        public RecommendWordPage RecommendWords { get; set; }
    }
}

