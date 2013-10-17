namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), MessageContract(WrapperName="setMOForwardExResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), DebuggerStepThrough, EditorBrowsable(EditorBrowsableState.Advanced)]
    public class setMOForwardExResponse
    {
        [XmlElement(Form=XmlSchemaForm.Unqualified), MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0)]
        public int @return;

        public setMOForwardExResponse()
        {
        }

        public setMOForwardExResponse(int @return)
        {
            this.@return = @return;
        }
    }
}

