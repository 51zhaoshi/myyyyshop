namespace Maticsoft.Payment.Core
{
    using Maticsoft.Payment.Handler;
    using Maticsoft.Payment.Model;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Web;

    public abstract class NotifyQuery
    {
        private NotifyEventHandler NotifyVerifyFaild;
        private NotifyEventHandler PaidToIntermediary;
        private NotifyEventHandler PaidToMerchant;

        public event NotifyEventHandler NotifyVerifyFaild
        {
            add
            {
                NotifyEventHandler handler2;
                NotifyEventHandler notifyVerifyFaild = this.NotifyVerifyFaild;
                do
                {
                    handler2 = notifyVerifyFaild;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Combine(handler2, value);
                    notifyVerifyFaild = Interlocked.CompareExchange<NotifyEventHandler>(ref this.NotifyVerifyFaild, handler3, handler2);
                }
                while (notifyVerifyFaild != handler2);
            }
            remove
            {
                NotifyEventHandler handler2;
                NotifyEventHandler notifyVerifyFaild = this.NotifyVerifyFaild;
                do
                {
                    handler2 = notifyVerifyFaild;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Remove(handler2, value);
                    notifyVerifyFaild = Interlocked.CompareExchange<NotifyEventHandler>(ref this.NotifyVerifyFaild, handler3, handler2);
                }
                while (notifyVerifyFaild != handler2);
            }
        }

        public event NotifyEventHandler PaidToIntermediary
        {
            add
            {
                NotifyEventHandler handler2;
                NotifyEventHandler paidToIntermediary = this.PaidToIntermediary;
                do
                {
                    handler2 = paidToIntermediary;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Combine(handler2, value);
                    paidToIntermediary = Interlocked.CompareExchange<NotifyEventHandler>(ref this.PaidToIntermediary, handler3, handler2);
                }
                while (paidToIntermediary != handler2);
            }
            remove
            {
                NotifyEventHandler handler2;
                NotifyEventHandler paidToIntermediary = this.PaidToIntermediary;
                do
                {
                    handler2 = paidToIntermediary;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Remove(handler2, value);
                    paidToIntermediary = Interlocked.CompareExchange<NotifyEventHandler>(ref this.PaidToIntermediary, handler3, handler2);
                }
                while (paidToIntermediary != handler2);
            }
        }

        public event NotifyEventHandler PaidToMerchant
        {
            add
            {
                NotifyEventHandler handler2;
                NotifyEventHandler paidToMerchant = this.PaidToMerchant;
                do
                {
                    handler2 = paidToMerchant;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Combine(handler2, value);
                    paidToMerchant = Interlocked.CompareExchange<NotifyEventHandler>(ref this.PaidToMerchant, handler3, handler2);
                }
                while (paidToMerchant != handler2);
            }
            remove
            {
                NotifyEventHandler handler2;
                NotifyEventHandler paidToMerchant = this.PaidToMerchant;
                do
                {
                    handler2 = paidToMerchant;
                    NotifyEventHandler handler3 = (NotifyEventHandler) Delegate.Remove(handler2, value);
                    paidToMerchant = Interlocked.CompareExchange<NotifyEventHandler>(ref this.PaidToMerchant, handler3, handler2);
                }
                while (paidToMerchant != handler2);
            }
        }

        protected NotifyQuery()
        {
        }

        public virtual string GetGatewayOrderId()
        {
            return string.Empty;
        }

        public abstract decimal GetOrderAmount();
        public abstract string GetOrderId();
        public virtual string GetRemark1()
        {
            return string.Empty;
        }

        public virtual string GetRemark2()
        {
            return string.Empty;
        }

        protected virtual string GetResponse(string url, int timeout)
        {
            string str;
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                request.Timeout = timeout;
                Stream responseStream = ((HttpWebResponse) request.GetResponse()).GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (-1 != reader.Peek())
                {
                    builder.Append(reader.ReadLine());
                }
                str = builder.ToString();
                reader.Close();
                responseStream.Close();
            }
            catch (Exception exception)
            {
                str = "Error:" + exception.Message;
            }
            return str;
        }

        public static NotifyQuery Instance(string notifyType, NameValueCollection parameters)
        {
            if (string.IsNullOrEmpty(notifyType))
            {
                return null;
            }
            object[] args = new object[] { parameters };
            return (Activator.CreateInstance(Type.GetType(notifyType), args) as NotifyQuery);
        }

        protected virtual void OnNotifyVerifyFaild()
        {
            if (this.NotifyVerifyFaild != null)
            {
                this.NotifyVerifyFaild(this);
            }
        }

        protected virtual void OnPaidToIntermediary()
        {
            if (this.PaidToIntermediary != null)
            {
                this.PaidToIntermediary(this);
            }
        }

        protected virtual void OnPaidToMerchant()
        {
            if (this.PaidToMerchant != null)
            {
                this.PaidToMerchant(this);
            }
        }

        public abstract void VerifyNotify(int timeout, PayeeInfo payee);
        public abstract void WriteBack(HttpContext context, bool success);

        public string ReturnUrl { get; set; }
    }
}

