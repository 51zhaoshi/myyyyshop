namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FoodSecurity : TopObject
    {
        [XmlElement("contact")]
        public string Contact { get; set; }

        [XmlElement("design_code")]
        public string DesignCode { get; set; }

        [XmlElement("factory")]
        public string Factory { get; set; }

        [XmlElement("factory_site")]
        public string FactorySite { get; set; }

        [XmlElement("food_additive")]
        public string FoodAdditive { get; set; }

        [XmlElement("mix")]
        public string Mix { get; set; }

        [XmlElement("period")]
        public string Period { get; set; }

        [XmlElement("plan_storage")]
        public string PlanStorage { get; set; }

        [XmlElement("prd_license_no")]
        public string PrdLicenseNo { get; set; }

        [XmlElement("product_date_end")]
        public string ProductDateEnd { get; set; }

        [XmlElement("product_date_start")]
        public string ProductDateStart { get; set; }

        [XmlElement("stock_date_end")]
        public string StockDateEnd { get; set; }

        [XmlElement("stock_date_start")]
        public string StockDateStart { get; set; }

        [XmlElement("supplier")]
        public string Supplier { get; set; }
    }
}

