namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlibabaLogisticsRouteQueryRequest : ITopRequest<AlibabaLogisticsRouteQueryResponse>
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
            return "alibaba.logistics.route.query";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("air_transport", this.AirTransport);
            dictionary.Add("corp_list", this.CorpList);
            dictionary.Add("end_area_id", this.EndAreaId);
            dictionary.Add("merge_route", this.MergeRoute);
            dictionary.Add("page_index", this.PageIndex);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("show_cods", this.ShowCods);
            dictionary.Add("show_specials", this.ShowSpecials);
            dictionary.Add("show_statistics_info", this.ShowStatisticsInfo);
            dictionary.Add("sort_type", this.SortType);
            dictionary.Add("source", this.Source);
            dictionary.Add("start_area_id", this.StartAreaId);
            dictionary.Add("summary_total_corps", this.SummaryTotalCorps);
            dictionary.Add("summery_by_corp", this.SummeryByCorp);
            dictionary.Add("turn_level", this.TurnLevel);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxListSize("corp_list", this.CorpList, 15);
            RequestValidator.ValidateRequired("end_area_id", this.EndAreaId);
            RequestValidator.ValidateRequired("page_index", this.PageIndex);
            RequestValidator.ValidateMaxValue("page_index", this.PageIndex, 0x186a0L);
            RequestValidator.ValidateMinValue("page_index", this.PageIndex, 1L);
            RequestValidator.ValidateRequired("page_size", this.PageSize);
            RequestValidator.ValidateMaxValue("page_size", this.PageSize, 100L);
            RequestValidator.ValidateMinValue("page_size", this.PageSize, 1L);
            RequestValidator.ValidateRequired("start_area_id", this.StartAreaId);
        }

        public bool? AirTransport { get; set; }

        public string CorpList { get; set; }

        public long? EndAreaId { get; set; }

        public bool? MergeRoute { get; set; }

        public long? PageIndex { get; set; }

        public long? PageSize { get; set; }

        public bool? ShowCods { get; set; }

        public bool? ShowSpecials { get; set; }

        public bool? ShowStatisticsInfo { get; set; }

        public string SortType { get; set; }

        public string Source { get; set; }

        public long? StartAreaId { get; set; }

        public bool? SummaryTotalCorps { get; set; }

        public bool? SummeryByCorp { get; set; }

        public bool? TurnLevel { get; set; }
    }
}

