namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [DebuggerStepThrough, MessageContract(WrapperName="logoutResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class logoutResponse
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public int @return;

        public logoutResponse()
        {
        }

        public logoutResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

