namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Model.SysManage;
    using Maticsoft.TaoBao;
    using System;

    public class TaoBaoConfig
    {
        public ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;
        public const string TAOBAO_APIURL = "OpenAPI_TaobaoApiUrl";
        public const string TAOBAO_APPKEY = "OpenAPI_TaoBaoAppkey";
        public const string TAOBAO_APPSECRET = "OpenAPI_TaobaoAppsecret";

        public TaoBaoConfig(ApplicationKeyType keyType)
        {
            this.applicationKeyType = keyType;
        }

        public static ITopClient GetTopClient()
        {
            string appKey = Maticsoft.BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaoBaoAppkey");
            return new DefaultTopClient(Maticsoft.BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaobaoApiUrl"), appKey, Maticsoft.BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaobaoAppsecret"));
        }

        public string TaobaoApiUrl
        {
            get
            {
                return Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoApiUrl", this.applicationKeyType);
            }
            set
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("OpenAPI_TaobaoApiUrl", value, this.applicationKeyType);
            }
        }

        public string TaoBaoAppkey
        {
            get
            {
                return Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaoBaoAppkey", this.applicationKeyType);
            }
            set
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("OpenAPI_TaoBaoAppkey", value, this.applicationKeyType);
            }
        }

        public string TaobaoAppsecret
        {
            get
            {
                return Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_TaobaoAppsecret", this.applicationKeyType);
            }
            set
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("OpenAPI_TaobaoAppsecret", value, this.applicationKeyType);
            }
        }
    }
}

