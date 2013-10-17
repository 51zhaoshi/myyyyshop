namespace Maticsoft.OAuth.Tencent.QQ
{
    using Maticsoft.OAuth.Tencent.QQ.Converters;
    using Maticsoft.OAuth.v2;
    using System;
    using System.Collections.Generic;

    public class QConnectOpenIdTemplate : QConnectTemplate
    {
        public QConnectOpenIdTemplate(AccessGrant accessGrant) : base(accessGrant, null)
        {
            base._accessGrant = accessGrant;
        }

        protected override IList<IHttpMessageConverter> GetMessageConverters()
        {
            IList<IHttpMessageConverter> messageConverters = base.GetMessageConverters();
            messageConverters.Clear();
            messageConverters.Add(new OpenIdJsonHttpMessageConverter());
            return messageConverters;
        }
    }
}

