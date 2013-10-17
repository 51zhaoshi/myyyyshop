namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ProductImgUploadRequest : ITopUploadRequest<ProductImgUploadResponse>, ITopRequest<ProductImgUploadResponse>
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
            return "taobao.product.img.upload";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("image", this.Image);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("id", this.Id);
            dictionary.Add("is_major", this.IsMajor);
            dictionary.Add("position", this.Position);
            dictionary.Add("product_id", this.ProductId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("image", this.Image);
            RequestValidator.ValidateMaxLength("image", this.Image, 0x100000);
            RequestValidator.ValidateRequired("product_id", this.ProductId);
        }

        public long? Id { get; set; }

        public FileItem Image { get; set; }

        public bool? IsMajor { get; set; }

        public long? Position { get; set; }

        public long? ProductId { get; set; }
    }
}

