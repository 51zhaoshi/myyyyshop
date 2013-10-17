namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemcatsGetResponse : TopResponse
    {
        [XmlArray("item_cats"), XmlArrayItem("item_cat")]
        public List<ItemCat> ItemCats { get; set; }

        [XmlElement("last_modified")]
        public string LastModified { get; set; }
    }
}

