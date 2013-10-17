namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DeliveryTemplateUpdateRequest : ITopRequest<DeliveryTemplateUpdateResponse>
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
            return "taobao.delivery.template.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("assumer", this.Assumer);
            dictionary.Add("name", this.Name);
            dictionary.Add("template_add_fees", this.TemplateAddFees);
            dictionary.Add("template_add_standards", this.TemplateAddStandards);
            dictionary.Add("template_dests", this.TemplateDests);
            dictionary.Add("template_id", this.TemplateId);
            dictionary.Add("template_start_fees", this.TemplateStartFees);
            dictionary.Add("template_start_standards", this.TemplateStartStandards);
            dictionary.Add("template_types", this.TemplateTypes);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("template_add_fees", this.TemplateAddFees);
            RequestValidator.ValidateRequired("template_add_standards", this.TemplateAddStandards);
            RequestValidator.ValidateRequired("template_dests", this.TemplateDests);
            RequestValidator.ValidateRequired("template_id", this.TemplateId);
            RequestValidator.ValidateRequired("template_start_fees", this.TemplateStartFees);
            RequestValidator.ValidateRequired("template_start_standards", this.TemplateStartStandards);
            RequestValidator.ValidateRequired("template_types", this.TemplateTypes);
        }

        public long? Assumer { get; set; }

        public string Name { get; set; }

        public string TemplateAddFees { get; set; }

        public string TemplateAddStandards { get; set; }

        public string TemplateDests { get; set; }

        public long? TemplateId { get; set; }

        public string TemplateStartFees { get; set; }

        public string TemplateStartStandards { get; set; }

        public string TemplateTypes { get; set; }
    }
}

