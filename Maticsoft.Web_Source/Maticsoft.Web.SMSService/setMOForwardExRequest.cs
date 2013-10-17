namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [MessageContract(WrapperName="setMOForwardEx", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class setMOForwardExRequest
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public string arg0;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg1;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=2), XmlElement("arg2", Form=XmlSchemaForm.Unqualified, IsNullable=true)]
        public string[] arg2;

        public setMOForwardExRequest()
        {
        }

        public setMOForwardExRequest(string arg0, string arg1, string[] arg2)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }
    }
}

