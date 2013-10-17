namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemPropimgUploadRequest : ITopUploadRequest<ItemPropimgUploadResponse>, ITopRequest<ItemPropimgUploadResponse>
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
            return "taobao.item.propimg.upload";
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
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("position", this.Position);
            dictionary.Add("properties", this.Properties);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("image", this.Image, 0x100000);
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0L);
            RequestValidator.ValidateRequired("properties", this.Properties);
        }

        public long? Id { get; set; }

        public FileItem Image { get; set; }

        public long? NumIid { get; set; }

        public long? Position { get; set; }

        public string Properties { get; set; }
    }
}

