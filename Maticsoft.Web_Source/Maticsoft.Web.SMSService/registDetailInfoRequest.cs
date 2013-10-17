namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [MessageContract(WrapperName="registDetailInfo", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), DebuggerStepThrough, GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class registDetailInfoRequest
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public string arg0;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=1), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg1;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=2)]
        public string arg2;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=3)]
        public string arg3;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=4)]
        public string arg4;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=5)]
        public string arg5;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=6)]
        public string arg6;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=7), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg7;
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=8)]
        public string arg8;
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=9), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public string arg9;

        public registDetailInfoRequest()
        {
        }

        public registDetailInfoRequest(string arg0, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
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
            this.arg9 = arg9;
        }
    }
}

