namespace Maticsoft.OAuth.Tencent.QQ.Converters
{
    using Maticsoft.OAuth.Http.Converters.Json;
    using System;

    public class OpenIdJsonHttpMessageConverter : TextJsonHttpMessageConverter
    {
        protected override string ConvertToJson(string result)
        {
            return result.Replace("callback(", "").Replace(");", "").Replace("\n", "").Replace("\r\n", "");
        }
    }
}

