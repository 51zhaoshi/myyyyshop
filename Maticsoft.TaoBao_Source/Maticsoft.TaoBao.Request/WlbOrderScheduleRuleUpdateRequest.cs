namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class WlbOrderScheduleRuleUpdateRequest : ITopRequest<WlbOrderScheduleRuleUpdateResponse>
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
            return "taobao.wlb.order.schedule.rule.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("backup_store_id", this.BackupStoreId);
            dictionary.Add("default_store_id", this.DefaultStoreId);
            dictionary.Add("option", this.Option);
            dictionary.Add("prov_area_ids", this.ProvAreaIds);
            dictionary.Add("schedule_rule_id", this.ScheduleRuleId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("schedule_rule_id", this.ScheduleRuleId);
        }

        public long? BackupStoreId { get; set; }

        public long? DefaultStoreId { get; set; }

        public string Option { get; set; }

        public string ProvAreaIds { get; set; }

        public long? ScheduleRuleId { get; set; }
    }
}

