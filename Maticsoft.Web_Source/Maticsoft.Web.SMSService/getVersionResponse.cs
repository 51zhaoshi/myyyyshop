namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, MessageContract(WrapperName="getVersionResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class getVersionResponse
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string @return;

        public getVersionResponse()
        {
        }

        public getVersionResponse(string @return)
        {
            this.@return = @return;
        }
    }
}

