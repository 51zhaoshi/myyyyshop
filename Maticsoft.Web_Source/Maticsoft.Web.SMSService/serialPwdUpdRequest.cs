namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [MessageContract(WrapperName="serialPwdUpd", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), EditorBrowsable(EditorBrowsableState.Advanced), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class serialPwdUpdRequest
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public string arg0;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg1;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=2)]
        public string arg2;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=3)]
        public string arg3;

        public serialPwdUpdRequest()
        {
        }

        public serialPwdUpdRequest(string arg0, string arg1, string arg2, string arg3)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }
    }
}

