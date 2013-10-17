namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class MarketingTagsGetResponse : TopResponse
    {
        [XmlArrayItem("user_tag"), XmlArray("user_tags")]
        public List<UserTag> UserTags { get; set; }
    }
}

