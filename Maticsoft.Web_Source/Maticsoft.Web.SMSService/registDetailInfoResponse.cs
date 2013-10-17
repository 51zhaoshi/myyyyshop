namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(WrapperName="registDetailInfoResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class registDetailInfoResponse
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public int @return;

        public registDetailInfoResponse()
        {
        }

        public registDetailInfoResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

