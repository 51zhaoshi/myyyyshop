namespace Maticsoft.Web.SMSService
{
    using System.CodeDom.Compiler;
    using System.ServiceModel;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), ServiceContract(Namespace="http://sdkhttp.eucp.b2m.cn/", ConfigurationName="SMSService.SDKClient")]
    public interface SDKClient
    {
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        cancelMOForwardResponse cancelMOForward(cancelMOForwardRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        chargeUpResponse chargeUp(chargeUpRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        getBalanceResponse getBalance(getBalanceRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        getEachFeeResponse getEachFee(getEachFeeRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        getMOResponse getMO(getMORequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        getReportResponse getReport(getReportRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        getVersionResponse getVersion(getVersionRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        logoutResponse logout(logoutRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        registDetailInfoResponse registDetailInfo(registDetailInfoRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        registExResponse registEx(registExRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        sendSMSResponse sendSMS(sendSMSRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        serialPwdUpdResponse serialPwdUpd(serialPwdUpdRequest request);
        [return: MessageParameter(Name="return")]
        [XmlSerializerFormat(SupportFaults=true), OperationContract(Action="", ReplyAction="*")]
        setMOForwardResponse setMOForward(setMOForwardRequest request);
        [return: MessageParameter(Name="return")]
        [OperationContract(Action="", ReplyAction="*"), XmlSerializerFormat(SupportFaults=true)]
        setMOForwardExResponse setMOForwardEx(setMOForwardExRequest request);
    }
}

