namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ItemcatsIncrementGetResponse : TopResponse
    {
        [XmlArray("item_cats"), XmlArrayItem("item_cat")]
        public List<ItemCat> ItemCats { get; set; }

        [XmlArray("item_props"), XmlArrayItem("item_prop")]
        public List<ItemProp> ItemProps { get; set; }

        [XmlArrayItem("prop_value"), XmlArray("prop_values")]
        public List<PropValue> PropValues { get; set; }
    }
}

