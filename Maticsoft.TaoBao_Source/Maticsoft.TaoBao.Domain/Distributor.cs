namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Distributor : TopObject
    {
        [XmlElement("alipay_account")]
        public string AlipayAccount { get; set; }

        [XmlElement("appraise")]
        public long Appraise { get; set; }

        [XmlElement("category_id")]
        public long CategoryId { get; set; }

        [XmlElement("contact_person")]
        public string ContactPerson { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("distributor_id")]
        public long DistributorId { get; set; }

        [XmlElement("distributor_name")]
        public string DistributorName { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("full_name")]
        public string FullName { get; set; }

        [XmlElement("level")]
        public long Level { get; set; }

        [XmlElement("mobile_phone")]
        public string MobilePhone { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("shop_web_link")]
        public string ShopWebLink { get; set; }

        [XmlElement("starts")]
        public string Starts { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

