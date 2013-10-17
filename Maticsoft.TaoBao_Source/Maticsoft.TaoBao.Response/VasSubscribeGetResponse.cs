namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class VasSubscribeGetResponse : TopResponse
    {
        [XmlArray("article_user_subscribes"), XmlArrayItem("article_user_subscribe")]
        public List<ArticleUserSubscribe> ArticleUserSubscribes { get; set; }
    }
}

