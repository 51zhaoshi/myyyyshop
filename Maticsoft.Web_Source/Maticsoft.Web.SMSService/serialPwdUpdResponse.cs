namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough, MessageContract(WrapperName="serialPwdUpdResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class serialPwdUpdResponse
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public int @return;

        public serialPwdUpdResponse()
        {
        }

        public serialPwdUpdResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

