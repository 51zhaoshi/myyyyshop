namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class BrandCatControlInfo : TopObject
    {
        [XmlArrayItem("brand_cat_control"), XmlArray("brand_cat_controls")]
        public List<BrandCatControl> BrandCatControls { get; set; }
    }
}

