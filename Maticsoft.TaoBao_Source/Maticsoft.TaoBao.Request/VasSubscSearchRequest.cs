namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VasSubscSearchRequest : ITopRequest<VasSubscSearchResponse>
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
            return "taobao.vas.subsc.search";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("article_code", this.ArticleCode);
            dictionary.Add("autosub", this.Autosub);
            dictionary.Add("end_deadline", this.EndDeadline);
            dictionary.Add("expire_notice", this.ExpireNotice);
            dictionary.Add("item_code", this.ItemCode);
            dictionary.Add("nick", this.Nick);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("start_deadline", this.StartDeadline);
            dictionary.Add("status", this.Status);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("article_code", this.ArticleCode);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 200L);
        }

        public string ArticleCode { get; set; }

        public bool? Autosub { get; set; }

        public DateTime? EndDeadline { get; set; }

        public bool? ExpireNotice { get; set; }

        public string ItemCode { get; set; }

        public string Nick { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public DateTime? StartDeadline { get; set; }

        public long? Status { get; set; }
    }
}

