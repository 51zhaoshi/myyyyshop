namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class VasSubscSearchResponse : TopResponse
    {
        [XmlArrayItem("article_sub"), XmlArray("article_subs")]
        public List<ArticleSub> ArticleSubs { get; set; }

        [XmlElement("total_item")]
        public long TotalItem { get; set; }
    }
}

