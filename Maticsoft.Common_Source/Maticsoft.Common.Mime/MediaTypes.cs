namespace Maticsoft.Common.Mime
{
    using System;

    public static class MediaTypes
    {
        public static readonly string Alternative = "alternative";
        public static readonly string Application;
        public static readonly string Message = "message";
        public static readonly string MessageRfc822 = (Message + "/" + Rfc822);
        public static readonly string Mixed = "mixed";
        public static readonly string Multipart = "multipart";
        public static readonly string MultipartAlternative = (Multipart + "/" + Alternative);
        public static readonly string MultipartMixed = (Multipart + "/" + Mixed);
        public static readonly string Rfc822 = "rfc822";
        public static readonly string TextHtml = "text/html";
        public static readonly string TextPlain = "text/plain";
        public static readonly string TextRich = "text/richtext";
        public static readonly string TextXml = "text/xml";
    }
}

