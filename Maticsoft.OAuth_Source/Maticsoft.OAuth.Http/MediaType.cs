namespace Maticsoft.OAuth.Http
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    [Serializable]
    public class MediaType : IEquatable<MediaType>, IComparable<MediaType>
    {
        public static readonly MediaType ALL = new MediaType("*", "*");
        public static readonly MediaType APPLICATION_ATOM_XML = new MediaType("application", "atom+xml");
        public static readonly MediaType APPLICATION_FORM_URLENCODED = new MediaType("application", "x-www-form-urlencoded");
        public static readonly MediaType APPLICATION_JSON = new MediaType("application", "json");
        public static readonly MediaType APPLICATION_OCTET_STREAM = new MediaType("application", "octet-stream");
        public static readonly MediaType APPLICATION_XHTML_XML = new MediaType("application", "xhtml+xml");
        public static readonly MediaType APPLICATION_XML = new MediaType("application", "xml");
        public static readonly MediaType IMAGE_GIF = new MediaType("image", "gif");
        public static readonly MediaType IMAGE_JPEG = new MediaType("image", "jpeg");
        public static readonly MediaType IMAGE_PNG = new MediaType("image", "png");
        public static readonly MediaType MULTIPART_FORM_DATA = new MediaType("multipart", "form-data");
        private const string PARAM_CHARSET = "charset";
        private const string PARAM_QUALITY_FACTOR = "q";
        private IDictionary<string, string> parameters;
        public static IComparer<MediaType> QUALITY_VALUE_COMPARER = new QualityValueComparer();
        private int SortIndex;
        public static IComparer<MediaType> SPECIFICITY_COMPARER = new SpecificityComparer();
        private string subtype;
        public static readonly MediaType TEXT_HTML = new MediaType("text", "html");
        public static readonly MediaType TEXT_PLAIN = new MediaType("text", "plain");
        public static readonly MediaType TEXT_XML = new MediaType("text", "xml");
        private string type;
        private const string WILDCARD_TYPE = "*";

        public MediaType(string type) : this(type, "*")
        {
        }

        public MediaType(MediaType otherMediaType, IDictionary<string, string> parameters) : this(otherMediaType.Type, otherMediaType.Subtype, parameters)
        {
        }

        public MediaType(string type, string subtype) : this(type, subtype, new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase))
        {
        }

        public MediaType(string type, string subtype, IDictionary<string, string> parameters)
        {
            ArgumentUtils.AssertHasText(type, "type");
            ArgumentUtils.AssertHasText(subtype, "subtype");
            this.type = type.ToLower(CultureInfo.InvariantCulture);
            this.subtype = subtype.ToLower(CultureInfo.InvariantCulture);
            this.parameters = new Dictionary<string, string>(parameters, StringComparer.OrdinalIgnoreCase);
        }

        public MediaType(string type, string subtype, double qualityValue) : this(type, subtype)
        {
            this.parameters.Add("q", qualityValue.ToString(CultureInfo.InvariantCulture));
        }

        public MediaType(string type, string subtype, string charSet) : this(type, subtype)
        {
            this.parameters.Add("charset", charSet);
        }

        public MediaType(string type, string subtype, Encoding charSet) : this(type, subtype)
        {
            this.parameters.Add("charset", charSet.WebName);
        }

        public int CompareTo(MediaType other)
        {
            int num = this.type.CompareTo(other.type);
            if (num != 0)
            {
                return num;
            }
            num = this.subtype.CompareTo(other.subtype);
            if (num != 0)
            {
                return num;
            }
            num = this.parameters.Count - other.parameters.Count;
            if (num != 0)
            {
                return num;
            }
            string[] array = new string[this.parameters.Keys.Count];
            this.parameters.Keys.CopyTo(array, 0);
            Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
            string[] strArray2 = new string[other.parameters.Keys.Count];
            other.parameters.Keys.CopyTo(strArray2, 0);
            Array.Sort<string>(strArray2, StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < this.parameters.Count; i++)
            {
                string strA = array[i];
                string strB = strArray2[i];
                num = string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase);
                if (num != 0)
                {
                    return num;
                }
                num = string.Compare(this.parameters[strA], other.parameters[strB]);
                if (num != 0)
                {
                    return num;
                }
            }
            return this.SortIndex.CompareTo(other.SortIndex);
        }

        public bool Equals(MediaType other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (!object.ReferenceEquals(this, other))
            {
                if ((!(this.type == other.type) || !(this.subtype == other.subtype)) || (other.parameters.Count != this.parameters.Count))
                {
                    return false;
                }
                foreach (string str in this.parameters.Keys)
                {
                    if (!other.parameters.ContainsKey(str) || !string.Equals(other.parameters[str], this.parameters[str], StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MediaType);
        }

        public override int GetHashCode()
        {
            return this.ToString().ToUpper(CultureInfo.InvariantCulture).GetHashCode();
        }

        public string GetParameter(string name)
        {
            return this.parameters[name];
        }

        public bool Includes(MediaType otherMediaType)
        {
            if (otherMediaType != null)
            {
                if (this.IsWildcardType)
                {
                    return true;
                }
                if (this.type == otherMediaType.type)
                {
                    if ((this.subtype == otherMediaType.subtype) || this.IsWildcardSubtype)
                    {
                        return true;
                    }
                    int index = this.subtype.IndexOf('+');
                    int num2 = otherMediaType.subtype.IndexOf('+');
                    if ((index != -1) && (num2 != -1))
                    {
                        string str = this.subtype.Substring(0, index);
                        string str2 = this.subtype.Substring(index + 1);
                        string str3 = otherMediaType.subtype.Substring(num2 + 1);
                        if ((str2 == str3) && ("*" == str))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool IsCompatibleWith(MediaType otherMediaType)
        {
            if (otherMediaType != null)
            {
                if (this.IsWildcardType || otherMediaType.IsWildcardType)
                {
                    return true;
                }
                if (this.type == otherMediaType.type)
                {
                    if (((this.subtype == otherMediaType.subtype) || this.IsWildcardSubtype) || otherMediaType.IsWildcardSubtype)
                    {
                        return true;
                    }
                    int index = this.subtype.IndexOf('+');
                    int length = otherMediaType.subtype.IndexOf('+');
                    if ((index != -1) && (length != -1))
                    {
                        string str = this.subtype.Substring(0, index);
                        string str2 = otherMediaType.subtype.Substring(0, length);
                        string str3 = this.subtype.Substring(index + 1);
                        string str4 = otherMediaType.subtype.Substring(length + 1);
                        if ((str3 == str4) && (("*" == str) || ("*" == str2)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool operator ==(MediaType mediaType1, MediaType mediaType2)
        {
            if (object.ReferenceEquals(mediaType1, null))
            {
                return object.ReferenceEquals(mediaType2, null);
            }
            return mediaType1.Equals(mediaType2);
        }

        public static bool operator !=(MediaType mediaType1, MediaType mediaType2)
        {
            return !(mediaType1 == mediaType2);
        }

        public static MediaType Parse(string mediaType)
        {
            if (!StringUtils.HasText(mediaType))
            {
                return null;
            }
            string[] strArray = mediaType.Split(new char[] { ';' });
            string str = strArray[0].Trim();
            if (str == "*")
            {
                str = "*/*";
            }
            int index = str.IndexOf('/');
            if (index == -1)
            {
                throw new ArgumentException(string.Format("'{0}' does not contain '/'", mediaType), "mediaType");
            }
            if (index == (str.Length - 1))
            {
                throw new ArgumentException(string.Format("'{0}' does not contain subtype after '/'", mediaType), "mediaType");
            }
            string type = str.Substring(0, index);
            string subtype = str.Substring(index + 1);
            IDictionary<string, string> parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (strArray.Length > 1)
            {
                for (int i = 1; i < strArray.Length; i++)
                {
                    string str4 = strArray[i].Trim();
                    int length = str4.IndexOf('=');
                    if (length != -1)
                    {
                        string key = str4.Substring(0, length);
                        string str6 = str4.Substring(length + 1);
                        parameters.Add(key, str6);
                    }
                }
            }
            return new MediaType(type, subtype, parameters);
        }

        public static void SortByQualityValue(List<MediaType> mediaTypes)
        {
            ArgumentUtils.AssertNotNull(mediaTypes, "mediaTypes");
            if (mediaTypes.Count > 1)
            {
                for (int i = 0; i < mediaTypes.Count; i++)
                {
                    mediaTypes[i].SortIndex = i;
                }
                mediaTypes.Sort(QUALITY_VALUE_COMPARER);
            }
        }

        public static void SortBySpecificity(List<MediaType> mediaTypes)
        {
            ArgumentUtils.AssertNotNull(mediaTypes, "mediaTypes");
            if (mediaTypes.Count > 1)
            {
                for (int i = 0; i < mediaTypes.Count; i++)
                {
                    mediaTypes[i].SortIndex = i;
                }
                mediaTypes.Sort(SPECIFICITY_COMPARER);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.type);
            builder.Append('/');
            builder.Append(this.subtype);
            foreach (string str in this.parameters.Keys)
            {
                builder.Append(';');
                builder.Append(str);
                builder.Append('=');
                builder.Append(this.parameters[str]);
            }
            return builder.ToString();
        }

        public static string ToString(IEnumerable<MediaType> mediaTypes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (MediaType type in mediaTypes)
            {
                if (builder.Length > 0)
                {
                    builder.Append(',');
                }
                builder.Append(type);
            }
            return builder.ToString();
        }

        public Encoding CharSet
        {
            get
            {
                string str = null;
                this.parameters.TryGetValue("charset", out str);
                if (str == null)
                {
                    return null;
                }
                return Encoding.GetEncoding(str);
            }
        }

        public bool IsWildcardSubtype
        {
            get
            {
                return ("*" == this.subtype);
            }
        }

        public bool IsWildcardType
        {
            get
            {
                return ("*" == this.type);
            }
        }

        public double QualityValue
        {
            get
            {
                string str = null;
                if (!this.parameters.TryGetValue("q", out str))
                {
                    return 1.0;
                }
                return double.Parse(str, CultureInfo.InvariantCulture);
            }
        }

        public string Subtype
        {
            get
            {
                return this.subtype;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
        }

        private class QualityValueComparer : IComparer<MediaType>
        {
            public int Compare(MediaType x, MediaType y)
            {
                double qualityValue = x.QualityValue;
                int num3 = y.QualityValue.CompareTo(qualityValue);
                if (num3 != 0)
                {
                    return num3;
                }
                if (x.IsWildcardType && !y.IsWildcardType)
                {
                    return 1;
                }
                if (y.IsWildcardType && !x.IsWildcardType)
                {
                    return -1;
                }
                if (x.type == y.type)
                {
                    if (x.IsWildcardSubtype && !y.IsWildcardSubtype)
                    {
                        return 1;
                    }
                    if (y.IsWildcardSubtype && !x.IsWildcardSubtype)
                    {
                        return -1;
                    }
                    if (x.subtype != y.subtype)
                    {
                        return x.SortIndex.CompareTo(y.SortIndex);
                    }
                    int count = x.parameters.Count;
                    int num5 = y.parameters.Count;
                    int num6 = (num5 < count) ? -1 : ((num5 == count) ? 0 : 1);
                    if (num6 != 0)
                    {
                        return num6;
                    }
                }
                return x.SortIndex.CompareTo(y.SortIndex);
            }
        }

        private class SpecificityComparer : IComparer<MediaType>
        {
            public int Compare(MediaType x, MediaType y)
            {
                if (x.IsWildcardType && !y.IsWildcardType)
                {
                    return 1;
                }
                if (y.IsWildcardType && !x.IsWildcardType)
                {
                    return -1;
                }
                if (x.type == y.type)
                {
                    if (x.IsWildcardSubtype && !y.IsWildcardSubtype)
                    {
                        return 1;
                    }
                    if (y.IsWildcardSubtype && !x.IsWildcardSubtype)
                    {
                        return -1;
                    }
                    if (x.subtype != y.subtype)
                    {
                        return x.SortIndex.CompareTo(y.SortIndex);
                    }
                    double qualityValue = x.QualityValue;
                    int num3 = y.QualityValue.CompareTo(qualityValue);
                    if (num3 != 0)
                    {
                        return num3;
                    }
                    int count = x.parameters.Count;
                    int num5 = y.parameters.Count;
                    int num6 = (num5 < count) ? -1 : ((num5 == count) ? 0 : 1);
                    if (num6 != 0)
                    {
                        return num6;
                    }
                }
                return x.SortIndex.CompareTo(y.SortIndex);
            }
        }
    }
}

