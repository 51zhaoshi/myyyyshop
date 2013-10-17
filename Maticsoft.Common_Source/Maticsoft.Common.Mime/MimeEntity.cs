namespace Maticsoft.Common.Mime
{
    using Maticsoft.Common.Mail;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Text;

    public class MimeEntity
    {
        private List<MimeEntity> _children;
        private MemoryStream _content;
        private string _contentDescription;
        private System.Net.Mime.ContentDisposition _contentDisposition;
        private string _contentId;
        private System.Net.Mime.TransferEncoding _contentTransferEncoding;
        private System.Net.Mime.ContentType _contentType;
        private StringBuilder _encodedMessage;
        private NameValueCollection _headers;
        private string _mediaMainType;
        private string _mediaSubType;
        private string _mimeVersion;
        private MimeEntity _parent;
        private string _startBoundary;
        private string _transferEncoding;

        public MimeEntity()
        {
            this._children = new List<MimeEntity>();
            this._headers = new NameValueCollection();
            this._contentType = MimeReader.GetContentType(string.Empty);
            this._parent = null;
            this._encodedMessage = new StringBuilder();
        }

        public MimeEntity(MimeEntity parent) : this()
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this._parent = parent;
            this._startBoundary = parent.StartBoundary;
        }

        private void BuildMultiPartMessage(MimeEntity entity, MailMessageEx message)
        {
            foreach (MimeEntity entity2 in entity.Children)
            {
                if (entity2 == null)
                {
                    continue;
                }
                if (string.Equals(entity2.ContentType.MediaType, MediaTypes.MultipartAlternative, StringComparison.InvariantCultureIgnoreCase) || string.Equals(entity2.ContentType.MediaType, MediaTypes.MultipartMixed, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.BuildMultiPartMessage(entity2, message);
                    continue;
                }
                if (!IsAttachment(entity2) && (string.Equals(entity2.ContentType.MediaType, MediaTypes.TextPlain) || string.Equals(entity2.ContentType.MediaType, MediaTypes.TextHtml)))
                {
                    message.AlternateViews.Add(this.CreateAlternateView(entity2));
                    this.SetMessageBody(message, entity2);
                    continue;
                }
                if (string.Equals(entity2.ContentType.MediaType, MediaTypes.MessageRfc822, StringComparison.InvariantCultureIgnoreCase) && string.Equals(entity2.ContentDisposition.DispositionType, "attachment", StringComparison.InvariantCultureIgnoreCase))
                {
                    message.Children.Add(this.ToMailMessageEx(entity2));
                }
                else if (IsAttachment(entity2))
                {
                    message.Attachments.Add(this.CreateAttachment(entity2));
                }
            }
        }

        private void BuildSinglePartMessage(MimeEntity entity, MailMessageEx message)
        {
            this.SetMessageBody(message, entity);
        }

        private AlternateView CreateAlternateView(MimeEntity view)
        {
            return new AlternateView(view.Content, view.ContentType) { TransferEncoding = view.ContentTransferEncoding, ContentId = TrimBrackets(view.ContentId) };
        }

        private Attachment CreateAttachment(MimeEntity entity)
        {
            Attachment attachment = new Attachment(entity.Content, entity.ContentType);
            if (entity.ContentDisposition != null)
            {
                attachment.ContentDisposition.Parameters.Clear();
                foreach (string str in entity.ContentDisposition.Parameters.Keys)
                {
                    attachment.ContentDisposition.Parameters.Add(str, entity.ContentDisposition.Parameters[str]);
                }
                attachment.ContentDisposition.CreationDate = entity.ContentDisposition.CreationDate;
                attachment.ContentDisposition.DispositionType = entity.ContentDisposition.DispositionType;
                attachment.ContentDisposition.FileName = entity.ContentDisposition.FileName;
                attachment.ContentDisposition.Inline = entity.ContentDisposition.Inline;
                attachment.ContentDisposition.ModificationDate = entity.ContentDisposition.ModificationDate;
                attachment.ContentDisposition.ReadDate = entity.ContentDisposition.ReadDate;
                attachment.ContentDisposition.Size = entity.ContentDisposition.Size;
            }
            if (!string.IsNullOrEmpty(entity.ContentId))
            {
                attachment.ContentId = TrimBrackets(entity.ContentId);
            }
            attachment.TransferEncoding = entity.ContentTransferEncoding;
            return attachment;
        }

        private string DecodeBytes(byte[] buffer, Encoding encoding)
        {
            if (buffer == null)
            {
                return null;
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF7;
            }
            return encoding.GetString(buffer);
        }

        public Encoding GetEncoding()
        {
            if (string.IsNullOrEmpty(this.ContentType.CharSet))
            {
                return Encoding.ASCII;
            }
            try
            {
                return Encoding.GetEncoding(this.ContentType.CharSet);
            }
            catch (ArgumentException)
            {
                return Encoding.ASCII;
            }
        }

        private static bool IsAttachment(MimeEntity child)
        {
            return ((child.ContentDisposition != null) && string.Equals(child.ContentDisposition.DispositionType, "attachment", StringComparison.InvariantCultureIgnoreCase));
        }

        internal void SetContentType(System.Net.Mime.ContentType contentType)
        {
            this._contentType = contentType;
            this._contentType.MediaType = MimeReader.GetMediaType(contentType.MediaType);
            this._mediaMainType = MimeReader.GetMediaMainType(contentType.MediaType);
            this._mediaSubType = MimeReader.GetMediaSubType(contentType.MediaType);
        }

        private void SetMessageBody(MailMessageEx message, MimeEntity child)
        {
            Encoding encoding = child.GetEncoding();
            message.Body = this.DecodeBytes(child.Content.ToArray(), encoding);
            message.BodyEncoding = encoding;
            message.IsBodyHtml = string.Equals(MediaTypes.TextHtml, child.ContentType.MediaType, StringComparison.InvariantCultureIgnoreCase);
        }

        public MailMessageEx ToMailMessageEx()
        {
            return this.ToMailMessageEx(this);
        }

        private MailMessageEx ToMailMessageEx(MimeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            MailMessageEx message = MailMessageEx.CreateMailMessageFromEntity(entity);
            if (!string.IsNullOrEmpty(entity.ContentType.Boundary))
            {
                message = MailMessageEx.CreateMailMessageFromEntity(entity);
                this.BuildMultiPartMessage(entity, message);
                return message;
            }
            if (string.Equals(entity.ContentType.MediaType, MediaTypes.MessageRfc822, StringComparison.InvariantCultureIgnoreCase))
            {
                if (entity.Children.Count < 0)
                {
                    throw new Pop3Exception("Invalid child count on message/rfc822 entity.");
                }
                message = MailMessageEx.CreateMailMessageFromEntity(entity.Children[0]);
                this.BuildMultiPartMessage(entity, message);
                return message;
            }
            message = MailMessageEx.CreateMailMessageFromEntity(entity);
            this.BuildSinglePartMessage(entity, message);
            return message;
        }

        public static string TrimBrackets(string value)
        {
            if ((value != null) && (value.StartsWith("<") && value.EndsWith(">")))
            {
                return value.Trim(new char[] { '<', '>' });
            }
            return value;
        }

        public List<MimeEntity> Children
        {
            get
            {
                return this._children;
            }
        }

        public MemoryStream Content
        {
            get
            {
                return this._content;
            }
            internal set
            {
                this._content = value;
            }
        }

        public string ContentDescription
        {
            get
            {
                return this._contentDescription;
            }
            set
            {
                this._contentDescription = value;
            }
        }

        public System.Net.Mime.ContentDisposition ContentDisposition
        {
            get
            {
                return this._contentDisposition;
            }
            set
            {
                this._contentDisposition = value;
            }
        }

        public string ContentId
        {
            get
            {
                return this._contentId;
            }
            set
            {
                this._contentId = value;
            }
        }

        public System.Net.Mime.TransferEncoding ContentTransferEncoding
        {
            get
            {
                return this._contentTransferEncoding;
            }
            set
            {
                this._contentTransferEncoding = value;
            }
        }

        public System.Net.Mime.ContentType ContentType
        {
            get
            {
                return this._contentType;
            }
        }

        public StringBuilder EncodedMessage
        {
            get
            {
                return this._encodedMessage;
            }
        }

        public string EndBoundary
        {
            get
            {
                return (this.StartBoundary + "--");
            }
        }

        internal bool HasBoundary
        {
            get
            {
                if (string.IsNullOrEmpty(this._contentType.Boundary))
                {
                    return !string.IsNullOrEmpty(this._startBoundary);
                }
                return true;
            }
        }

        public NameValueCollection Headers
        {
            get
            {
                return this._headers;
            }
        }

        public string MediaMainType
        {
            get
            {
                return this._mediaMainType;
            }
        }

        public string MediaSubType
        {
            get
            {
                return this._mediaSubType;
            }
        }

        public string MimeVersion
        {
            get
            {
                return this._mimeVersion;
            }
            set
            {
                this._mimeVersion = value;
            }
        }

        public MimeEntity Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }

        public string StartBoundary
        {
            get
            {
                if (!string.IsNullOrEmpty(this._startBoundary) && string.IsNullOrEmpty(this._contentType.Boundary))
                {
                    return this._startBoundary;
                }
                return ("--" + this._contentType.Boundary);
            }
        }

        public string TransferEncoding
        {
            get
            {
                return this._transferEncoding;
            }
            set
            {
                this._transferEncoding = value;
            }
        }
    }
}

