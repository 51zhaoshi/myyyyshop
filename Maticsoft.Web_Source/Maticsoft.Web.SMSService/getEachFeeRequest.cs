namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [DebuggerStepThrough, MessageContract(WrapperName="getEachFee", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class getEachFeeRequest
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public string arg0;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg1;

        public getEachFeeRequest()
        {
        }

        public getEachFeeRequest(string arg0, string arg1)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }
    }
}

