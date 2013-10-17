namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallCat : TopObject
    {
        [XmlElement("cat_id")]
        public long CatId { get; set; }

        [XmlElement("cat_name")]
        public string CatName { get; set; }
    }
}

