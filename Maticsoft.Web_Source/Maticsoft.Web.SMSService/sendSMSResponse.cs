namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [DebuggerStepThrough, MessageContract(WrapperName="sendSMSResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), GeneratedCode("System.ServiceModel", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class sendSMSResponse
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public int @return;

        public sendSMSResponse()
        {
        }

        public sendSMSResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

