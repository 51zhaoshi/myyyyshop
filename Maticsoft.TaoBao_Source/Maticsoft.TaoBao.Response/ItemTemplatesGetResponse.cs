namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemTemplatesGetResponse : TopResponse
    {
        [XmlArray("item_template_list"), XmlArrayItem("item_template")]
        public List<ItemTemplate> ItemTemplateList { get; set; }
    }
}

