namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [MessageContract(WrapperName="getReportResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough]
    public class getReportResponse
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement("return", Form=XmlSchemaForm.Unqualified)]
        public statusReport[] @return;

        public getReportResponse()
        {
        }

        public getReportResponse(statusReport[] @return)
        {
            this.@return = @return;
        }
    }
}

