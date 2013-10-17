namespace Maticsoft.Web.Components
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Web;
    using Maticsoft.Web.SMSService;
    using System;
    using System.Runtime.InteropServices;
    using System.Web;

    public class SMSHelper
    {
        public static bool Logout()
        {
            string valueByCache = ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            string str2 = ConfigSystem.GetValueByCache("Emay_SMS_Key");
            ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            if (string.IsNullOrWhiteSpace(valueByCache) || string.IsNullOrWhiteSpace(str2))
            {
                LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败", HttpContext.Current.Request);
                return false;
            }
            SDKClient client = new SDKClientClient();
            logoutRequest request = new logoutRequest(valueByCache, str2);
            logoutResponse response = client.logout(request);
            if (response.@return == 0)
            {
                return true;
            }
            LogHelp.AddErrorLog("亿美短信注销序列号出现异常，【" + LogoutException(response.@return) + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;
        }

        private static string LogoutException(int code)
        {
            switch (code)
            {
                case -1104:
                    return "路由失败，请联系系统管理员";

                case -1103:
                    return "序列号Key错误";

                case -1102:
                    return "序列号密码错误";

                case -1100:
                    return "序列号错误，序列号不存在内存中，或尝试攻击的用户";

                case -190:
                    return "数据操作失败";

                case -126:
                    return "路由信息失败";

                case -9003:
                    return "客户端Key格式错误";

                case -9002:
                    return "密码格式错误";

                case -9001:
                    return "序列号格式错误";

                case -1902:
                    return "数据库更新操作失败";

                case -9025:
                    return "客户端请求sdk5超时";

                case -122:
                    return "号码注销激活失败";

                case -104:
                    return "请求超过限制";

                case -101:
                    return "命令不被支持";

                case 0x131:
                    return "服务器端返回错误，错误的返回值";

                case 0x3e7:
                    return "操作频繁";

                case -2:
                    return "客户端异常";

                case -1:
                    return "系统异常";

                case 0x65:
                case 0x67:
                    return "客户端网络故障";
            }
            return "未知错误";
        }

        public static bool RegistEx()
        {
            string valueByCache = ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            string str2 = ConfigSystem.GetValueByCache("Emay_SMS_Key");
            string str3 = ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            if (string.IsNullOrWhiteSpace(valueByCache) || string.IsNullOrWhiteSpace(str2))
            {
                LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败", HttpContext.Current.Request);
                return false;
            }
            SDKClient client = new SDKClientClient();
            registExRequest request = new registExRequest(valueByCache, str2, str3);
            registExResponse response = client.registEx(request);
            if (response.@return == 0)
            {
                return true;
            }
            LogHelp.AddErrorLog("亿美短信注册序列号出现异常，【" + RegistExException(response.@return) + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;
        }

        private static string RegistExException(int code)
        {
            switch (code)
            {
                case -1108:
                    return "注册号状态异常, 停止 5";

                case -1107:
                    return "注册号状态异常, 停用 3";

                case -1105:
                    return "注册号状态异常, 未用 1";

                case -1104:
                    return "路由失败，请联系系统管理员";

                case -1103:
                    return "序列号Key错误";

                case -1102:
                    return "序列号密码错误";

                case -1100:
                    return "序列号错误，序列号不存在内存中，或尝试攻击的用户";

                case -190:
                    return "数据操作失败";

                case -126:
                    return "路由信息失败";

                case -9003:
                    return "客户端Key格式错误";

                case -9002:
                    return "密码格式错误";

                case -9001:
                    return "序列号格式错误";

                case -1901:
                    return "数据库插入操作失败";

                case -9025:
                    return "客户端请求sdk5超时";

                case -110:
                    return "号码注册激活失败";

                case -104:
                    return "请求超过限制";

                case -101:
                    return "命令不被支持";

                case 0x131:
                    return "服务器端返回错误，错误的返回值";

                case 0x3e7:
                    return "操作频繁";

                case -2:
                    return "客户端异常";

                case -1:
                    return "系统异常";

                case 0x65:
                case 0x67:
                    return "客户端网络故障";
            }
            return "未知错误";
        }

        public static bool SendSMS(string content, string[] numbers, int priority = 5)
        {
            string valueByCache = ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            string str2 = ConfigSystem.GetValueByCache("Emay_SMS_Key");
            if (string.IsNullOrWhiteSpace(valueByCache) || string.IsNullOrWhiteSpace(str2))
            {
                LogHelp.AddErrorLog("亿美短信接口缺少企业序列号或者自定义关键字Key", "亿美短信接口调用失败", HttpContext.Current.Request);
                return false;
            }
            SDKClient client = new SDKClientClient();
            sendSMSRequest request = new sendSMSRequest {
                arg0 = valueByCache,
                arg1 = str2,
                arg3 = numbers,
                arg4 = Globals.HtmlEncode(content),
                arg7 = priority
            };
            sendSMSResponse response = client.sendSMS(request);
            if (response.@return == 0)
            {
                return true;
            }
            LogHelp.AddErrorLog("亿美短信接口发送短信出现异常，【" + SendSMSException(response.@return) + "】", "亿美短信接口调用失败", HttpContext.Current.Request);
            return false;
        }

        private static string SendSMSException(int code)
        {
            switch (code)
            {
                case -1104:
                    return "路由失败，请联系系统管理员";

                case -117:
                    return "发送短信失败";

                case -104:
                    return "请求超过限制";

                case -9025:
                    return "客户端请求sdk5超时";

                case -9022:
                    return "发送短信唯一序列值错误";

                case -9021:
                    return "发送短信定时时间格式错误";

                case -9020:
                    return "发送短信手机号格式错误";

                case -9019:
                    return "发送短信优先级格式错误";

                case -9018:
                    return "发送短信扩展号格式错误";

                case -9017:
                    return "发送短信内容格式错误";

                case -9016:
                    return "发送短信包大小超出范围";

                case -9003:
                    return "客户端Key格式错误";

                case -9002:
                    return "密码格式错误";

                case -9001:
                    return "序列号格式错误";

                case -2:
                    return "客户端异常";

                case -1:
                    return "系统异常";

                case -101:
                    return "命令不被支持";

                case 0x65:
                case 0x67:
                    return "客户端网络故障";

                case 0x131:
                    return "服务器端返回错误，错误的返回值";

                case 0x133:
                    return "目标电话号码不符合规则，电话号码必须是以0、1开头";

                case 0x3e5:
                    return "平台返回找不到超时的短信，该信息是否成功无法确定";

                case 0x3e6:
                    return "由于客户端网络问题导致信息发送超时，该信息是否成功下发无法确定";
            }
            return "未知错误";
        }
    }
}

