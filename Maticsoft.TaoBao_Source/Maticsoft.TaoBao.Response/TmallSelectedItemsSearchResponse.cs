namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallSelectedItemsSearchResponse : TopResponse
    {
        [XmlArray("item_list"), XmlArrayItem("selected_item")]
        public List<SelectedItem> ItemList { get; set; }
    }
}

