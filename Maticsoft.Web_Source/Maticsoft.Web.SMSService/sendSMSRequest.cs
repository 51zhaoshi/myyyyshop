namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough, MessageContract(WrapperName="sendSMS", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class sendSMSRequest
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg0;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1)]
        public string arg1;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=2), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg2;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=3), XmlElement("arg3", Form=XmlSchemaForm.Unqualified, IsNullable=true)]
        public string[] arg3;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=4), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg4;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=5), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg5;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=6), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg6;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=7), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public int arg7;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=8)]
        public long arg8;

        public sendSMSRequest()
        {
        }

        public sendSMSRequest(string arg0, string arg1, string arg2, string[] arg3, string arg4, string arg5, string arg6, int arg7, long arg8)
        {
            this.arg0 = arg0;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.arg4 = arg4;
            this.arg5 = arg5;
            this.arg6 = arg6;
            this.arg7 = arg7;
            this.arg8 = arg8;
        }
    }
}

