namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough, MessageContract(WrapperName="getBalanceResponse", WrapperNamespace="http://sdkhttp.eucp.b2m.cn/", IsWrapped=true), EditorBrowsable(EditorBrowsableState.Advanced)]
    public class getBalanceResponse
    {
        [MessageBodyMember(Namespace="http://sdkhttp.eucp.b2m.cn/", Order=0), XmlElement(Form=XmlSchemaForm.Unqualified)]
        public double @return;

        public getBalanceResponse()
        {
        }

        public getBalanceResponse(double @return)
        {
            this.@return = @return;
        }
    }
}

