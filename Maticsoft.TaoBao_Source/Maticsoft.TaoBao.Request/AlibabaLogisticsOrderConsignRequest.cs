namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlibabaLogisticsOrderConsignRequest : ITopRequest<AlibabaLogisticsOrderConsignResponse>
    {
        private IDictionary<string, string> otherParameters;

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }

        public string GetApiName()
        {
            return "alibaba.logistics.order.consign";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("cargo_description", this.CargoDescription);
            dictionary.Add("cargo_name", this.CargoName);
            dictionary.Add("order_id", this.OrderId);
            dictionary.Add("pay_type", this.PayType);
            dictionary.Add("receiver_address", this.ReceiverAddress);
            dictionary.Add("receiver_area_id", this.ReceiverAreaId);
            dictionary.Add("receiver_city_name", this.ReceiverCityName);
            dictionary.Add("receiver_corp_name", this.ReceiverCorpName);
            dictionary.Add("receiver_county_name", this.ReceiverCountyName);
            dictionary.Add("receiver_mobile", this.ReceiverMobile);
            dictionary.Add("receiver_name", this.ReceiverName);
            dictionary.Add("receiver_phone_area_code", this.ReceiverPhoneAreaCode);
            dictionary.Add("receiver_phone_tel", this.ReceiverPhoneTel);
            dictionary.Add("receiver_phone_tel_ext", this.ReceiverPhoneTelExt);
            dictionary.Add("receiver_postcode", this.ReceiverPostcode);
            dictionary.Add("receiver_province_name", this.ReceiverProvinceName);
            dictionary.Add("receiver_wangwang_no", this.ReceiverWangwangNo);
            dictionary.Add("refunder_address", this.RefunderAddress);
            dictionary.Add("refunder_area_id", this.RefunderAreaId);
            dictionary.Add("refunder_city_name", this.RefunderCityName);
            dictionary.Add("refunder_corp_name", this.RefunderCorpName);
            dictionary.Add("refunder_county_name", this.RefunderCountyName);
            dictionary.Add("refunder_mobile", this.RefunderMobile);
            dictionary.Add("refunder_name", this.RefunderName);
            dictionary.Add("refunder_phone_area_code", this.RefunderPhoneAreaCode);
            dictionary.Add("refunder_phone_tel", this.RefunderPhoneTel);
            dictionary.Add("refunder_phone_tel_ext", this.RefunderPhoneTelExt);
            dictionary.Add("refunder_postcode", this.RefunderPostcode);
            dictionary.Add("refunder_province_name", this.RefunderProvinceName);
            dictionary.Add("refunder_wangwang_no", this.RefunderWangwangNo);
            dictionary.Add("remark", this.Remark);
            dictionary.Add("route_code", this.RouteCode);
            dictionary.Add("sender_address", this.SenderAddress);
            dictionary.Add("sender_area_id", this.SenderAreaId);
            dictionary.Add("sender_city_name", this.SenderCityName);
            dictionary.Add("sender_corp_name", this.SenderCorpName);
            dictionary.Add("sender_county_name", this.SenderCountyName);
            dictionary.Add("sender_mobile", this.SenderMobile);
            dictionary.Add("sender_name", this.SenderName);
            dictionary.Add("sender_phone_area_code", this.SenderPhoneAreaCode);
            dictionary.Add("sender_phone_tel", this.SenderPhoneTel);
            dictionary.Add("sender_phone_tel_ext", this.SenderPhoneTelExt);
            dictionary.Add("sender_postcode", this.SenderPostcode);
            dictionary.Add("sender_province_name", this.SenderProvinceName);
            dictionary.Add("sender_wangwang_no", this.SenderWangwangNo);
            dictionary.Add("source", this.Source);
            dictionary.Add("total_number", this.TotalNumber);
            dictionary.Add("total_volume", this.TotalVolume);
            dictionary.Add("total_weight", this.TotalWeight);
            dictionary.Add("vas_list", this.VasList);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("cargo_name", this.CargoName);
            RequestValidator.ValidateRequired("order_id", this.OrderId);
            RequestValidator.ValidateRequired("pay_type", this.PayType);
            RequestValidator.ValidateRequired("route_code", this.RouteCode);
            RequestValidator.ValidateRequired("source", this.Source);
            RequestValidator.ValidateRequired("total_number", this.TotalNumber);
            RequestValidator.ValidateMaxListSize("vas_list", this.VasList, 12);
        }

        public string CargoDescription { get; set; }

        public string CargoName { get; set; }

        public long? OrderId { get; set; }

        public string PayType { get; set; }

        public string ReceiverAddress { get; set; }

        public long? ReceiverAreaId { get; set; }

        public string ReceiverCityName { get; set; }

        public string ReceiverCorpName { get; set; }

        public string ReceiverCountyName { get; set; }

        public string ReceiverMobile { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverPhoneAreaCode { get; set; }

        public string ReceiverPhoneTel { get; set; }

        public string ReceiverPhoneTelExt { get; set; }

        public string ReceiverPostcode { get; set; }

        public string ReceiverProvinceName { get; set; }

        public string ReceiverWangwangNo { get; set; }

        public string RefunderAddress { get; set; }

        public long? RefunderAreaId { get; set; }

        public string RefunderCityName { get; set; }

        public string RefunderCorpName { get; set; }

        public string RefunderCountyName { get; set; }

        public string RefunderMobile { get; set; }

        public string RefunderName { get; set; }

        public string RefunderPhoneAreaCode { get; set; }

        public string RefunderPhoneTel { get; set; }

        public string RefunderPhoneTelExt { get; set; }

        public string RefunderPostcode { get; set; }

        public string RefunderProvinceName { get; set; }

        public string RefunderWangwangNo { get; set; }

        public string Remark { get; set; }

        public string RouteCode { get; set; }

        public string SenderAddress { get; set; }

        public long? SenderAreaId { get; set; }

        public string SenderCityName { get; set; }

        public string SenderCorpName { get; set; }

        public string SenderCountyName { get; set; }

        public string SenderMobile { get; set; }

        public string SenderName { get; set; }

        public string SenderPhoneAreaCode { get; set; }

        public string SenderPhoneTel { get; set; }

        public string SenderPhoneTelExt { get; set; }

        public string SenderPostcode { get; set; }

        public string SenderProvinceName { get; set; }

        public string SenderWangwangNo { get; set; }

        public string Source { get; set; }

        public long? TotalNumber { get; set; }

        public string TotalVolume { get; set; }

        public string TotalWeight { get; set; }

        public string VasList { get; set; }
    }
}

