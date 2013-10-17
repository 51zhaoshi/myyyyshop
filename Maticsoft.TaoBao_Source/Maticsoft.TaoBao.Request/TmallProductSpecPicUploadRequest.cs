namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TmallProductSpecPicUploadRequest : ITopUploadRequest<TmallProductSpecPicUploadResponse>, ITopRequest<TmallProductSpecPicUploadResponse>
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
            return "tmall.product.spec.pic.upload";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("certify_pic", this.CertifyPic);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("certify_type", this.CertifyType);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("certify_pic", this.CertifyPic);
            RequestValidator.ValidateRequired("certify_type", this.CertifyType);
        }

        public FileItem CertifyPic { get; set; }

        public long? CertifyType { get; set; }
    }
}

