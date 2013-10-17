namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [DebuggerStepThrough, MessageContract(WrapperName="setMOForward", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class setMOForwardRequest
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public string arg0;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1)]
        public string arg1;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=2), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg2;

        public setMOForwardRequest()
        {
        }

        public setMOForwardRequest(string arg0, string arg1, string arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
    }
}

