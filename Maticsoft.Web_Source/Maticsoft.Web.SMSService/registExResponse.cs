namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced), MessageContract(WrapperName="registExResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true)]
    public class registExResponse
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public int @return;

        public registExResponse()
        {
        }

        public registExResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

