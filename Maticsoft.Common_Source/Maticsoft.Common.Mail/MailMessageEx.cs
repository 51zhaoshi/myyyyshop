namespace Maticsoft.Common.Mail
{
    using Maticsoft.Common.Mime;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class MailMessageEx : MailMessage
    {
        private List<MailMessageEx> _children = new List<MailMessageEx>();
        private int _messageNumber;
        private long _octets;
        private static readonly char[] AddressDelimiters = new char[] { ',', ';' };
        public const string EmailRegexPattern = "(['\"]{1,}.+['\"]{1,}\\s+)?<?[\\w\\.\\-]+@[^\\.][\\w\\.\\-]+\\.[a-z]{2,}>?";

        public static MailAddress CreateMailAddress(string address)
        {
            try
            {
                return new MailAddress(address.Trim(new char[] { '\t' }));
            }
            catch
            {
                return new MailAddress(address + "@mail.error");
            }
        }

        public static MailMessageEx CreateMailMessageFromEntity(MimeEntity entity)
        {
            MailMessageEx ex = new MailMessageEx();
            foreach (string str2 in entity.Headers.AllKeys)
            {
                string str = entity.Headers[str2];
                if (str.Equals(string.Empty))
                {
                    str = " ";
                }
                ex.Headers.Add(str2.ToLowerInvariant(), str);
                string str3 = str2.ToLowerInvariant();
                if (str3 != null)
                {
                    if (!(str3 == "bcc"))
                    {
                        if (str3 == "cc")
                        {
                            goto Label_00C3;
                        }
                        if (str3 == "from")
                        {
                            goto Label_00D1;
                        }
                        if (str3 == "reply-to")
                        {
                            goto Label_00DF;
                        }
                        if (str3 == "subject")
                        {
                            goto Label_00ED;
                        }
                        if (str3 == "to")
                        {
                            goto Label_00F6;
                        }
                    }
                    else
                    {
                        PopulateAddressList(str, ex.Bcc);
                    }
                }
                goto Label_0102;
            Label_00C3:
                PopulateAddressList(str, ex.CC);
                goto Label_0102;
            Label_00D1:
                ex.From = CreateMailAddress(str);
                goto Label_0102;
            Label_00DF:
                ex.ReplyTo = CreateMailAddress(str);
                goto Label_0102;
            Label_00ED:
                ex.Subject = str;
                goto Label_0102;
            Label_00F6:
                PopulateAddressList(str, ex.To);
            Label_0102:;
            }
            return ex;
        }

        private string GetHeader(string header)
        {
            return this.GetHeader(header, false);
        }

        private string GetHeader(string header, bool stripBrackets)
        {
            if (stripBrackets)
            {
                return MimeEntity.TrimBrackets(base.Headers[header]);
            }
            return base.Headers[header];
        }

        private static IEnumerable<MailAddress> GetMailAddresses(string addressList)
        {
            Regex iteratorVariable0 = new Regex("(['\"]{1,}.+['\"]{1,}\\s+)?<?[\\w\\.\\-]+@[^\\.][\\w\\.\\-]+\\.[a-z]{2,}>?");
            IEnumerator enumerator = iteratorVariable0.Matches(addressList).GetEnumerator();
            while (enumerator.MoveNext())
            {
                Match current = (Match) enumerator.Current;
                yield return CreateMailAddress(current.Value);
            }
        }

        public static void PopulateAddressList(string addressList, MailAddressCollection recipients)
        {
            foreach (MailAddress address in GetMailAddresses(addressList))
            {
                recipients.Add(address);
            }
        }

        public List<MailMessageEx> Children
        {
            get
            {
                return this._children;
            }
        }

        public string ContentDescription
        {
            get
            {
                return this.GetHeader("content-description");
            }
        }

        public System.Net.Mime.ContentDisposition ContentDisposition
        {
            get
            {
                string header = this.GetHeader("content-disposition");
                if (string.IsNullOrEmpty(header))
                {
                    return null;
                }
                return new System.Net.Mime.ContentDisposition(header);
            }
        }

        public string ContentId
        {
            get
            {
                return this.GetHeader("content-id");
            }
        }

        public System.Net.Mime.ContentType ContentType
        {
            get
            {
                string header = this.GetHeader("content-type");
                if (string.IsNullOrEmpty(header))
                {
                    return null;
                }
                return MimeReader.GetContentType(header);
            }
        }

        public DateTime DeliveryDate
        {
            get
            {
                string header = this.GetHeader("date");
                if (string.IsNullOrEmpty(header))
                {
                    return DateTime.MinValue;
                }
                if ((header.IndexOf("(EST)") > 1) && (header.IndexOf("-") > 1))
                {
                    header = header.Substring(0, header.IndexOf("-"));
                }
                return Convert.ToDateTime(header);
            }
        }

        public string MessageId
        {
            get
            {
                return this.GetHeader("message-id");
            }
        }

        public int MessageNumber
        {
            get
            {
                return this._messageNumber;
            }
            internal set
            {
                this._messageNumber = value;
            }
        }

        public string MimeVersion
        {
            get
            {
                return this.GetHeader("mime-version");
            }
        }

        public long Octets
        {
            get
            {
                return this._octets;
            }
            set
            {
                this._octets = value;
            }
        }

        public string ReplyToMessageId
        {
            get
            {
                return this.GetHeader("in-reply-to", true);
            }
        }

        public MailAddress ReturnAddress
        {
            get
            {
                string header = this.GetHeader("reply-to");
                if (string.IsNullOrEmpty(header))
                {
                    return null;
                }
                return CreateMailAddress(header);
            }
        }

        public string Routing
        {
            get
            {
                return this.GetHeader("received");
            }
        }

        [CompilerGenerated]
        private sealed class <GetMailAddresses>d__0 : IEnumerable<MailAddress>, IEnumerable, IEnumerator<MailAddress>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private MailAddress <>2__current;
            public string <>3__addressList;
            public IEnumerator <>7__wrap3;
            public IDisposable <>7__wrap4;
            private int <>l__initialThreadId;
            public Regex <email>5__1;
            public Match <match>5__2;
            public string addressList;

            [DebuggerHidden]
            public <GetMailAddresses>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
            }

            private void <>m__Finally5()
            {
                this.<>1__state = -1;
                this.<>7__wrap4 = this.<>7__wrap3 as IDisposable;
                if (this.<>7__wrap4 != null)
                {
                    this.<>7__wrap4.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<email>5__1 = new Regex("(['\"]{1,}.+['\"]{1,}\\s+)?<?[\\w\\.\\-]+@[^\\.][\\w\\.\\-]+\\.[a-z]{2,}>?");
                            this.<>7__wrap3 = this.<email>5__1.Matches(this.addressList).GetEnumerator();
                            this.<>1__state = 1;
                            goto Label_0098;

                        case 2:
                            this.<>1__state = 1;
                            goto Label_0098;

                        default:
                            goto Label_00AB;
                    }
                Label_005A:
                    this.<match>5__2 = (Match) this.<>7__wrap3.Current;
                    this.<>2__current = MailMessageEx.CreateMailAddress(this.<match>5__2.Value);
                    this.<>1__state = 2;
                    return true;
                Label_0098:
                    if (this.<>7__wrap3.MoveNext())
                    {
                        goto Label_005A;
                    }
                    this.<>m__Finally5();
                Label_00AB:
                    flag = false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<MailAddress> IEnumerable<MailAddress>.GetEnumerator()
            {
                MailMessageEx.<GetMailAddresses>d__0 d__;
                if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new MailMessageEx.<GetMailAddresses>d__0(0);
                }
                d__.addressList = this.<>3__addressList;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.System.Collections.Generic.IEnumerable<System.Net.Mail.MailAddress>.GetEnumerator();
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.<>1__state)
                {
                    case 1:
                    case 2:
                        try
                        {
                        }
                        finally
                        {
                            this.<>m__Finally5();
                        }
                        return;
                }
            }

            MailAddress IEnumerator<MailAddress>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }
    }
}

