namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VasOrderSearchRequest : ITopRequest<VasOrderSearchResponse>
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
            return "taobao.vas.order.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("article_code", this.ArticleCode);
            dictionary.Add("biz_order_id", this.BizOrderId);
            dictionary.Add("biz_type", this.BizType);
            dictionary.Add("end_created", this.EndCreated);
            dictionary.Add("item_code", this.ItemCode);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("order_id", this.OrderId);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_created", this.StartCreated);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("article_code", this.ArticleCode);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
        }

        public string ArticleCode { get; set; }

        public long? BizOrderId { get; set; }

        public long? BizType { get; set; }

        public DateTime? EndCreated { get; set; }

        public string ItemCode { get; set; }

        public string Nick { get; set; }

        public long? OrderId { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartCreated { get; set; }
    }
}

