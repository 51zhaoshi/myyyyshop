namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TmallTmCat : TopObject
    {
        [XmlElement("sub_cat_id")]
        public long SubCatId { get; set; }

        [XmlElement("sub_cat_name")]
        public string SubCatName { get; set; }
    }
}

