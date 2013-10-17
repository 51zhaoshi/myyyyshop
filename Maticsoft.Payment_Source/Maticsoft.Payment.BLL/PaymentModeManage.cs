namespace Maticsoft.Payment.BLL
{
    using Maticsoft.Payment.Core;
    using Maticsoft.Payment.DAL;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Generic;

    public static class PaymentModeManage
    {
        private static PaymentModeService service = new PaymentModeService();

        public static bool AddBalanceDetail(BalanceDetailInfo balanceDetails)
        {
            return service.AddBalanceDetail(balanceDetails);
        }

        public static long AddRechargeBalance(RechargeRequestInfo rechargeRequest)
        {
            return service.AddRechargeBlance(rechargeRequest);
        }

        public static void AscPaymentMode(int modeId)
        {
            service.SortPaymentMode(modeId, SortAction.ASC);
        }

        public static PaymentModeActionStatus CreatePaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode == null)
            {
                return PaymentModeActionStatus.UnknowError;
            }
            EncryptPaymentMode(paymentMode);
            return service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Create);
        }

        internal static void DecryptPaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode != null)
            {
                using (MsCryptographer cryptographer = new MsCryptographer(true, false))
                {
                    if (!string.IsNullOrEmpty(paymentMode.SecretKey))
                    {
                        paymentMode.SecretKey = cryptographer.Decrypt(paymentMode.SecretKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.SecondKey))
                    {
                        paymentMode.SecondKey = cryptographer.Decrypt(paymentMode.SecondKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Password))
                    {
                        paymentMode.Password = cryptographer.Decrypt(paymentMode.Password);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Partner))
                    {
                        paymentMode.Partner = cryptographer.Decrypt(paymentMode.Partner);
                    }
                }
            }
        }

        public static bool DeletePaymentMode(int modeId)
        {
            PaymentModeInfo info2 = new PaymentModeInfo {
                ModeId = modeId
            };
            PaymentModeInfo paymentMode = info2;
            return (service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Delete) == PaymentModeActionStatus.Success);
        }

        public static void DescPaymentMode(int modeId)
        {
            service.SortPaymentMode(modeId, SortAction.Desc);
        }

        internal static void EncryptPaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode != null)
            {
                using (MsCryptographer cryptographer = new MsCryptographer(false, true))
                {
                    if (!string.IsNullOrEmpty(paymentMode.SecretKey))
                    {
                        paymentMode.SecretKey = cryptographer.Encrypt(paymentMode.SecretKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.SecondKey))
                    {
                        paymentMode.SecondKey = cryptographer.Encrypt(paymentMode.SecondKey);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Password))
                    {
                        paymentMode.Password = cryptographer.Encrypt(paymentMode.Password);
                    }
                    if (!string.IsNullOrEmpty(paymentMode.Partner))
                    {
                        paymentMode.Partner = cryptographer.Encrypt(paymentMode.Partner);
                    }
                }
            }
        }

        public static AccountSummaryInfo GetAccountSummary(int userId)
        {
            return service.GetAccountSummary(userId);
        }

        [Obsolete]
        public static PaymentModeInfo GetPaymentMode(int modeId)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(modeId);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        [Obsolete]
        public static PaymentModeInfo GetPaymentMode(string gateway)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(gateway);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        public static PaymentModeInfo GetPaymentModeById(int modeId)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(modeId);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        public static PaymentModeInfo GetPaymentModeByName(string gateway)
        {
            PaymentModeInfo paymentMode = service.GetPaymentMode(gateway);
            DecryptPaymentMode(paymentMode);
            return paymentMode;
        }

        public static List<PaymentModeInfo> GetPaymentModes()
        {
            return service.GetPaymentModes();
        }

        public static RechargeRequestInfo GetRechargeRequest(long rechargeId)
        {
            return service.GetRechargeRequest(rechargeId);
        }

        public static bool RemoveRechargeRequest(long rechargeId)
        {
            return (service.RemoveRechargeRequest(rechargeId) > 0);
        }

        public static PaymentModeActionStatus UpdatePaymentMode(PaymentModeInfo paymentMode)
        {
            if (paymentMode == null)
            {
                return PaymentModeActionStatus.UnknowError;
            }
            EncryptPaymentMode(paymentMode);
            return service.CreateUpdateDeletePaymentMode(paymentMode, DataProviderAction.Update);
        }
    }
}

