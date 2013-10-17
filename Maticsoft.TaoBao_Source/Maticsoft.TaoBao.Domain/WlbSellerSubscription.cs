namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbSellerSubscription : TopObject
    {
        [XmlElement("end_date")]
        public string EndDate { get; set; }

        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("is_own_service")]
        public long IsOwnService { get; set; }

        [XmlElement("parent_id")]
        public long ParentId { get; set; }

        [XmlElement("provider_user_id")]
        public long ProviderUserId { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("service_alias")]
        public string ServiceAlias { get; set; }

        [XmlElement("service_code")]
        public string ServiceCode { get; set; }

        [XmlElement("service_id")]
        public long ServiceId { get; set; }

        [XmlElement("service_name")]
        public string ServiceName { get; set; }

        [XmlElement("service_type")]
        public string ServiceType { get; set; }

        [XmlElement("start_date")]
        public string StartDate { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("subscriber_user_id")]
        public long SubscriberUserId { get; set; }

        [XmlElement("subscriber_user_nick")]
        public string SubscriberUserNick { get; set; }

        [XmlElement("wlb_partner_address")]
        public Maticsoft.TaoBao.Domain.WlbPartnerAddress WlbPartnerAddress { get; set; }

        [XmlElement("wlb_partner_contact")]
        public Maticsoft.TaoBao.Domain.WlbPartnerContact WlbPartnerContact { get; set; }
    }
}

