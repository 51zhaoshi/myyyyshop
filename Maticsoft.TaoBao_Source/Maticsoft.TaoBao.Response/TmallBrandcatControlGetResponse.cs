namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallBrandcatControlGetResponse : TopResponse
    {
        [XmlElement("brand_cat_control_info")]
        public Maticsoft.TaoBao.Domain.BrandCatControlInfo BrandCatControlInfo { get; set; }
    }
}

