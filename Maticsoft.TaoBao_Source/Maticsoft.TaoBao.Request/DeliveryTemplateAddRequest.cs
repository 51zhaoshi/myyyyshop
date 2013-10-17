namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DeliveryTemplateAddRequest : ITopRequest<DeliveryTemplateAddResponse>
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
            return "taobao.delivery.template.add";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("assumer", this.Assumer);
            dictionary.Add("consign_area_id", this.ConsignAreaId);
            dictionary.Add("name", this.Name);
            dictionary.Add("template_add_fees", this.TemplateAddFees);
            dictionary.Add("template_add_standards", this.TemplateAddStandards);
            dictionary.Add("template_dests", this.TemplateDests);
            dictionary.Add("template_start_fees", this.TemplateStartFees);
            dictionary.Add("template_start_standards", this.TemplateStartStandards);
            dictionary.Add("template_types", this.TemplateTypes);
            dictionary.Add("valuation", this.Valuation);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("assumer", this.Assumer);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 50);
            RequestValidator.ValidateRequired("template_add_fees", this.TemplateAddFees);
            RequestValidator.ValidateRequired("template_add_standards", this.TemplateAddStandards);
            RequestValidator.ValidateRequired("template_dests", this.TemplateDests);
            RequestValidator.ValidateRequired("template_start_fees", this.TemplateStartFees);
            RequestValidator.ValidateRequired("template_start_standards", this.TemplateStartStandards);
            RequestValidator.ValidateRequired("template_types", this.TemplateTypes);
            RequestValidator.ValidateRequired("valuation", this.Valuation);
        }

        public long? Assumer { get; set; }

        public long? ConsignAreaId { get; set; }

        public string Name { get; set; }

        public string TemplateAddFees { get; set; }

        public string TemplateAddStandards { get; set; }

        public string TemplateDests { get; set; }

        public string TemplateStartFees { get; set; }

        public string TemplateStartStandards { get; set; }

        public string TemplateTypes { get; set; }

        public long? Valuation { get; set; }
    }
}

