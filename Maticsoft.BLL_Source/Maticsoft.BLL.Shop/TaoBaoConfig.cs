namespace Maticsoft.BLL.Shop
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using Maticsoft.TaoBao;
    using System;

    public class TaoBaoConfig
    {
        public ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;
        public const string TAOBAO_APIURL = "OpenAPI_Shop_TaobaoApiUrl";
        public const string TAOBAO_APPKEY = "OpenAPI_Shop_TaoBaoAppkey";
        public const string TAOBAO_APPSECRET = "OpenAPI_Shop_TaobaoAppsecret";

        public TaoBaoConfig(ApplicationKeyType keyType)
        {
            this.applicationKeyType = keyType;
        }

        public static ITopClient GetTopClient()
        {
            string appKey = ConfigSystem.GetValue("OpenAPI_Shop_TaoBaoAppkey");
            return new DefaultTopClient(ConfigSystem.GetValue("OpenAPI_Shop_TaobaoApiUrl"), appKey, ConfigSystem.GetValue("OpenAPI_Shop_TaobaoAppsecret"));
        }

        public string TaobaoApiUrl
        {
            get
            {
                return ConfigSystem.GetValueByCache("OpenAPI_Shop_TaobaoApiUrl", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("OpenAPI_Shop_TaobaoApiUrl", value, "淘宝卖家接口APIURL", this.applicationKeyType);
            }
        }

        public string TaoBaoAppkey
        {
            get
            {
                return ConfigSystem.GetValueByCache("OpenAPI_Shop_TaoBaoAppkey", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("OpenAPI_Shop_TaoBaoAppkey", value, "淘宝卖家接口APPKEY", this.applicationKeyType);
            }
        }

        public string TaobaoAppsecret
        {
            get
            {
                return ConfigSystem.GetValueByCache("OpenAPI_Shop_TaobaoAppsecret", this.applicationKeyType);
            }
            set
            {
                ConfigSystem.Modify("OpenAPI_Shop_TaobaoAppsecret", value, "淘宝卖家接口APPSECRET", this.applicationKeyType);
            }
        }
    }
}

