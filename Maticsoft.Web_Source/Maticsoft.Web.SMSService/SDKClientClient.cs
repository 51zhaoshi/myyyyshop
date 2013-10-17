namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    [GeneratedCode("System.ServiceModel", "4.0.0.0"), DebuggerStepThrough]
    public class SDKClientClient : ClientBase<SDKClient>, SDKClient
    {
        public SDKClientClient()
        {
        }

        public SDKClientClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {
        }

        public SDKClientClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {
        }

        public SDKClientClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public SDKClientClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {
        }

        public int cancelMOForward(string arg0, string arg1)
        {
            cancelMOForwardRequest request = new cancelMOForwardRequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).cancelMOForward(request).@return;
        }

        public int chargeUp(string arg0, string arg1, string arg2, string arg3)
        {
            chargeUpRequest request = new chargeUpRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2,
                arg3 = arg3
            };
            return ((SDKClient) this).chargeUp(request).@return;
        }

        public double getBalance(string arg0, string arg1)
        {
            getBalanceRequest request = new getBalanceRequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).getBalance(request).@return;
        }

        public double getEachFee(string arg0, string arg1)
        {
            getEachFeeRequest request = new getEachFeeRequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).getEachFee(request).@return;
        }

        public mo[] getMO(string arg0, string arg1)
        {
            getMORequest request = new getMORequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).getMO(request).@return;
        }

        public statusReport[] getReport(string arg0, string arg1)
        {
            getReportRequest request = new getReportRequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).getReport(request).@return;
        }

        public string getVersion()
        {
            getVersionRequest request = new getVersionRequest();
            return ((SDKClient) this).getVersion(request).@return;
        }

        public int logout(string arg0, string arg1)
        {
            logoutRequest request = new logoutRequest {
                arg0 = arg0,
                arg1 = arg1
            };
            return ((SDKClient) this).logout(request).@return;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        cancelMOForwardResponse SDKClient.cancelMOForward(cancelMOForwardRequest request)
        {
            return base.Channel.cancelMOForward(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        chargeUpResponse SDKClient.chargeUp(chargeUpRequest request)
        {
            return base.Channel.chargeUp(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        getBalanceResponse SDKClient.getBalance(getBalanceRequest request)
        {
            return base.Channel.getBalance(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        getEachFeeResponse SDKClient.getEachFee(getEachFeeRequest request)
        {
            return base.Channel.getEachFee(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        getMOResponse SDKClient.getMO(getMORequest request)
        {
            return base.Channel.getMO(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        getReportResponse SDKClient.getReport(getReportRequest request)
        {
            return base.Channel.getReport(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        getVersionResponse SDKClient.getVersion(getVersionRequest request)
        {
            return base.Channel.getVersion(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        logoutResponse SDKClient.logout(logoutRequest request)
        {
            return base.Channel.logout(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        registDetailInfoResponse SDKClient.registDetailInfo(registDetailInfoRequest request)
        {
            return base.Channel.registDetailInfo(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        registExResponse SDKClient.registEx(registExRequest request)
        {
            return base.Channel.registEx(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        sendSMSResponse SDKClient.sendSMS(sendSMSRequest request)
        {
            return base.Channel.sendSMS(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        serialPwdUpdResponse SDKClient.serialPwdUpd(serialPwdUpdRequest request)
        {
            return base.Channel.serialPwdUpd(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        setMOForwardResponse SDKClient.setMOForward(setMOForwardRequest request)
        {
            return base.Channel.setMOForward(request);
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        setMOForwardExResponse SDKClient.setMOForwardEx(setMOForwardExRequest request)
        {
            return base.Channel.setMOForwardEx(request);
        }

        public int registDetailInfo(string arg0, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string arg7, string arg8, string arg9)
        {
            registDetailInfoRequest request = new registDetailInfoRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2,
                arg3 = arg3,
                arg4 = arg4,
                arg5 = arg5,
                arg6 = arg6,
                arg7 = arg7,
                arg8 = arg8,
                arg9 = arg9
            };
            return ((SDKClient) this).registDetailInfo(request).@return;
        }

        public int registEx(string arg0, string arg1, string arg2)
        {
            registExRequest request = new registExRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2
            };
            return ((SDKClient) this).registEx(request).@return;
        }

        public int sendSMS(string arg0, string arg1, string arg2, string[] arg3, string arg4, string arg5, string arg6, int arg7, long arg8)
        {
            sendSMSRequest request = new sendSMSRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2,
                arg3 = arg3,
                arg4 = arg4,
                arg5 = arg5,
                arg6 = arg6,
                arg7 = arg7,
                arg8 = arg8
            };
            return ((SDKClient) this).sendSMS(request).@return;
        }

        public int serialPwdUpd(string arg0, string arg1, string arg2, string arg3)
        {
            serialPwdUpdRequest request = new serialPwdUpdRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2,
                arg3 = arg3
            };
            return ((SDKClient) this).serialPwdUpd(request).@return;
        }

        public int setMOForward(string arg0, string arg1, string arg2)
        {
            setMOForwardRequest request = new setMOForwardRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2
            };
            return ((SDKClient) this).setMOForward(request).@return;
        }

        public int setMOForwardEx(string arg0, string arg1, string[] arg2)
        {
            setMOForwardExRequest request = new setMOForwardExRequest {
                arg0 = arg0,
                arg1 = arg1,
                arg2 = arg2
            };
            return ((SDKClient) this).setMOForwardEx(request).@return;
        }
    }
}

