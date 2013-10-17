namespace Maticsoft.OAuth.Json
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public class JsonValue
    {
        protected JsonValueType type;
        protected object value;

        public JsonValue()
        {
            this.type = JsonValueType.Null;
            this.value = null;
        }

        public JsonValue(bool value)
        {
            this.type = JsonValueType.Boolean;
            this.value = value;
        }

        public JsonValue(byte value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(decimal value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(double value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(short value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(int value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(long value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(float value)
        {
            this.type = JsonValueType.Number;
            this.value = value;
        }

        public JsonValue(string value)
        {
            ArgumentUtils.AssertNotNull(value, "value");
            this.type = JsonValueType.String;
            this.value = value;
        }

        protected JsonValue(JsonValueType type, object value)
        {
            this.type = type;
            this.value = value;
        }

        public virtual bool ContainsName(string name)
        {
            throw new JsonException("The value held by this instance is not a JSON object structure.");
        }

        public virtual ICollection<string> GetNames()
        {
            throw new JsonException("The value held by this instance is not a JSON object structure.");
        }

        public T GetValue<T>()
        {
            T local;
            if (this.value == null)
            {
                return default(T);
            }
            if (this.value is T)
            {
                return (T) this.value;
            }
            Type nullableType = typeof(T);
            if (nullableType.IsGenericType && (nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                nullableType = Nullable.GetUnderlyingType(nullableType);
            }
            try
            {
                local = (T) Convert.ChangeType(this.value, nullableType, CultureInfo.InvariantCulture);
            }
            catch (Exception exception)
            {
                throw new JsonException(string.Format("Could not cast JSON {0} value to type '{1}'.", this.type.ToString().ToLower(), nullableType), exception);
            }
            return local;
        }

        public T GetValue<T>(int index)
        {
            JsonValue value2 = this.GetValue(index);
            if (value2 == null)
            {
                throw new JsonException(string.Format("The JSON array structure does not have an entry at index '{0}'.", index));
            }
            return value2.GetValue<T>();
        }

        public virtual JsonValue GetValue(int index)
        {
            throw new JsonException("The value held by this instance is not a JSON array structure.");
        }

        public virtual JsonValue GetValue(string name)
        {
            throw new JsonException("The value held by this instance is not a JSON object structure.");
        }

        public T GetValue<T>(string name)
        {
            JsonValue value2 = this.GetValue(name);
            if (value2 == null)
            {
                throw new JsonException(string.Format("The JSON object structure does not have an entry named '{0}'.", name));
            }
            return value2.GetValue<T>();
        }

        public T GetValueOrDefault<T>(int index)
        {
            return this.GetValueOrDefault<T>(index, default(T));
        }

        public T GetValueOrDefault<T>(string name)
        {
            return this.GetValueOrDefault<T>(name, default(T));
        }

        public T GetValueOrDefault<T>(int index, T defaultValue)
        {
            JsonValue value2 = this.GetValue(index);
            if (value2 != null)
            {
                return value2.GetValue<T>();
            }
            return defaultValue;
        }

        public T GetValueOrDefault<T>(string name, T defaultValue)
        {
            JsonValue value2 = this.GetValue(name);
            if (value2 != null)
            {
                return value2.GetValue<T>();
            }
            return defaultValue;
        }

        public virtual ICollection<JsonValue> GetValues()
        {
            throw new JsonException("The value held by this instance is not a JSON object or array structure.");
        }

        public ICollection<JsonValue> GetValues(int index)
        {
            JsonValue value2 = this.GetValue(index);
            if (value2 == null)
            {
                throw new JsonException(string.Format("The JSON array structure does not have an entry at index '{0}'.", index));
            }
            return value2.GetValues();
        }

        public ICollection<JsonValue> GetValues(string name)
        {
            JsonValue value2 = this.GetValue(name);
            if (value2 == null)
            {
                throw new JsonException(string.Format("The JSON object structure does not have an entry named '{0}'.", name));
            }
            return value2.GetValues();
        }

        public static JsonValue Parse(string json)
        {
            JsonValue value2;
            if (!TryParse(json, out value2))
            {
                throw new JsonException(string.Format("Could not parse JSON string '{0}'.", json));
            }
            return value2;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x7d0);
            JsonGenerator.GenerateValue(this, builder);
            return builder.ToString();
        }

        public static bool TryParse(string json, out JsonValue result)
        {
            result = null;
            if (json == null)
            {
                return true;
            }
            char[] chArray = json.ToCharArray();
            int index = 0;
            bool success = true;
            JsonValue value2 = JsonParser.ParseValue(chArray, ref index, ref success);
            if (success)
            {
                result = value2;
                return true;
            }
            return false;
        }

        public bool IsArray
        {
            get
            {
                return (this.type == JsonValueType.Array);
            }
        }

        public bool IsBoolean
        {
            get
            {
                return (this.type == JsonValueType.Boolean);
            }
        }

        public bool IsNull
        {
            get
            {
                return (this.type == JsonValueType.Null);
            }
        }

        public bool IsNumber
        {
            get
            {
                return (this.type == JsonValueType.Number);
            }
        }

        public bool IsObject
        {
            get
            {
                return (this.type == JsonValueType.Object);
            }
        }

        public bool IsString
        {
            get
            {
                return (this.type == JsonValueType.String);
            }
        }

        private static class JsonGenerator
        {
            private static void GenerateArray(IList<JsonValue> jsonArray, StringBuilder builder)
            {
                builder.Append("[");
                bool flag = true;
                foreach (JsonValue value2 in jsonArray)
                {
                    if (!flag)
                    {
                        builder.Append(",");
                    }
                    GenerateValue(value2, builder);
                    flag = false;
                }
                builder.Append("]");
            }

            private static void GenerateNumber(object jsonNumber, StringBuilder builder)
            {
                builder.Append(Convert.ToString(jsonNumber, CultureInfo.InvariantCulture));
            }

            private static void GenerateObject(IDictionary<string, JsonValue> jsonObject, StringBuilder builder)
            {
                builder.Append("{");
                bool flag = true;
                foreach (KeyValuePair<string, JsonValue> pair in jsonObject)
                {
                    if (!flag)
                    {
                        builder.Append(",");
                    }
                    GenerateString(pair.Key, builder);
                    builder.Append(":");
                    GenerateValue(pair.Value, builder);
                    flag = false;
                }
                builder.Append("}");
            }

            private static void GenerateString(string jsonString, StringBuilder builder)
            {
                builder.Append("\"");
                foreach (char ch in jsonString.ToCharArray())
                {
                    switch (ch)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;

                        case '\\':
                            builder.Append(@"\\");
                            break;

                        case '\b':
                            builder.Append(@"\b");
                            break;

                        case '\f':
                            builder.Append(@"\f");
                            break;

                        case '\n':
                            builder.Append(@"\n");
                            break;

                        case '\r':
                            builder.Append(@"\r");
                            break;

                        case '\t':
                            builder.Append(@"\t");
                            break;

                        default:
                            builder.Append(ch);
                            break;
                    }
                }
                builder.Append("\"");
            }

            public static void GenerateValue(JsonValue jsonValue, StringBuilder builder)
            {
                switch (jsonValue.type)
                {
                    case JsonValue.JsonValueType.String:
                        GenerateString((string) jsonValue.value, builder);
                        return;

                    case JsonValue.JsonValueType.Number:
                        GenerateNumber(jsonValue.value, builder);
                        return;

                    case JsonValue.JsonValueType.Object:
                        GenerateObject((IDictionary<string, JsonValue>) jsonValue.value, builder);
                        return;

                    case JsonValue.JsonValueType.Array:
                        GenerateArray((IList<JsonValue>) jsonValue.value, builder);
                        return;

                    case JsonValue.JsonValueType.Boolean:
                        builder.Append(((bool) jsonValue.value) ? "true" : "false");
                        return;

                    case JsonValue.JsonValueType.Null:
                        builder.Append("null");
                        return;
                }
            }
        }

        private static class JsonParser
        {
            private const int TOKEN_COLON = 5;
            private const int TOKEN_COMMA = 6;
            private const int TOKEN_CURLY_CLOSE = 2;
            private const int TOKEN_CURLY_OPEN = 1;
            private const int TOKEN_FALSE = 10;
            private const int TOKEN_NONE = 0;
            private const int TOKEN_NULL = 11;
            private const int TOKEN_NUMBER = 8;
            private const int TOKEN_SQUARED_CLOSE = 4;
            private const int TOKEN_SQUARED_OPEN = 3;
            private const int TOKEN_STRING = 7;
            private const int TOKEN_TRUE = 9;

            private static void EatWhitespace(char[] json, ref int index)
            {
                while (index < json.Length)
                {
                    if (" \t\n\r\b\f".IndexOf(json[index]) == -1)
                    {
                        return;
                    }
                    index++;
                }
            }

            private static int GetLastIndexOfNumber(char[] json, int index)
            {
                int num = index;
                while (num < json.Length)
                {
                    if ("0123456789+-.eE".IndexOf(json[num]) == -1)
                    {
                        break;
                    }
                    num++;
                }
                return (num - 1);
            }

            private static int LookAhead(char[] json, int index)
            {
                int num = index;
                return NextToken(json, ref num);
            }

            private static int NextToken(char[] json, ref int index)
            {
                EatWhitespace(json, ref index);
                if (index != json.Length)
                {
                    char ch = json[index];
                    index++;
                    switch (ch)
                    {
                        case '"':
                            return 7;

                        case ',':
                            return 6;

                        case '-':
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            return 8;

                        case ':':
                            return 5;

                        case '[':
                            return 3;

                        case ']':
                            return 4;

                        case '{':
                            return 1;

                        case '}':
                            return 2;
                    }
                    index--;
                    int num = json.Length - index;
                    if ((((num >= 5) && (json[index] == 'f')) && ((json[index + 1] == 'a') && (json[index + 2] == 'l'))) && ((json[index + 3] == 's') && (json[index + 4] == 'e')))
                    {
                        index += 5;
                        return 10;
                    }
                    if ((((num >= 4) && (json[index] == 't')) && ((json[index + 1] == 'r') && (json[index + 2] == 'u'))) && (json[index + 3] == 'e'))
                    {
                        index += 4;
                        return 9;
                    }
                    if ((((num >= 4) && (json[index] == 'n')) && ((json[index + 1] == 'u') && (json[index + 2] == 'l'))) && (json[index + 3] == 'l'))
                    {
                        index += 4;
                        return 11;
                    }
                }
                return 0;
            }

            private static JsonArray ParseArray(char[] json, ref int index, ref bool success)
            {
                JsonArray array = new JsonArray(new JsonValue[0]);
                NextToken(json, ref index);
                bool flag = false;
                while (!flag)
                {
                    int num = LookAhead(json, index);
                    if (num == 0)
                    {
                        success = false;
                        return null;
                    }
                    if (num == 6)
                    {
                        NextToken(json, ref index);
                    }
                    else
                    {
                        if (num == 4)
                        {
                            NextToken(json, ref index);
                            return array;
                        }
                        JsonValue value2 = ParseValue(json, ref index, ref success);
                        if (!success)
                        {
                            return null;
                        }
                        array.AddValue(value2);
                    }
                }
                return array;
            }

            private static object ParseNumber(char[] json, ref int index, ref bool success)
            {
                EatWhitespace(json, ref index);
                int lastIndexOfNumber = GetLastIndexOfNumber(json, index);
                int length = (lastIndexOfNumber - index) + 1;
                string str = new string(json, index, length);
                index = lastIndexOfNumber + 1;
                return str;
            }

            private static JsonObject ParseObject(char[] json, ref int index, ref bool success)
            {
                JsonObject obj2 = new JsonObject();
                NextToken(json, ref index);
                bool flag = false;
                while (!flag)
                {
                    switch (LookAhead(json, index))
                    {
                        case 0:
                            success = false;
                            return null;

                        case 6:
                        {
                            NextToken(json, ref index);
                            continue;
                        }
                        case 2:
                            NextToken(json, ref index);
                            return obj2;
                    }
                    string name = ParseString(json, ref index, ref success);
                    if (!success)
                    {
                        success = false;
                        return null;
                    }
                    if (NextToken(json, ref index) != 5)
                    {
                        success = false;
                        return null;
                    }
                    JsonValue value2 = ParseValue(json, ref index, ref success);
                    if (!success)
                    {
                        success = false;
                        return null;
                    }
                    obj2.AddValue(name, value2);
                }
                return obj2;
            }

            private static string ParseString(char[] json, ref int index, ref bool success)
            {
                StringBuilder builder = new StringBuilder();
                EatWhitespace(json, ref index);
                char ch = json[index++];
                bool flag = false;
                while (!flag)
                {
                    if (index == json.Length)
                    {
                        break;
                    }
                    ch = json[index++];
                    if (ch == '"')
                    {
                        flag = true;
                        break;
                    }
                    if (ch == '\\')
                    {
                        if (index == json.Length)
                        {
                            break;
                        }
                        ch = json[index++];
                        if (ch == '"')
                        {
                            builder.Append('"');
                        }
                        else
                        {
                            if (ch == '\\')
                            {
                                builder.Append('\\');
                                continue;
                            }
                            if (ch == '/')
                            {
                                builder.Append('/');
                                continue;
                            }
                            if (ch == 'b')
                            {
                                builder.Append('\b');
                                continue;
                            }
                            if (ch == 'f')
                            {
                                builder.Append('\f');
                                continue;
                            }
                            if (ch == 'n')
                            {
                                builder.Append('\n');
                                continue;
                            }
                            if (ch == 'r')
                            {
                                builder.Append('\r');
                                continue;
                            }
                            if (ch == 't')
                            {
                                builder.Append('\t');
                            }
                            else if (ch == 'u')
                            {
                                uint num2;
                                int num = json.Length - index;
                                if (num < 4)
                                {
                                    break;
                                }
                                if (!(success = TryParseUInt32(new string(json, index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2)))
                                {
                                    return "";
                                }
                                if ((0xd800 <= num2) && (num2 <= 0xdbff))
                                {
                                    uint num3;
                                    index += 4;
                                    num = json.Length - index;
                                    if ((((num < 6) || (new string(json, index, 2) != @"\u")) || (!TryParseUInt32(new string(json, index + 2, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3) || (0xdc00 > num3))) || (num3 > 0xdfff))
                                    {
                                        success = false;
                                        return "";
                                    }
                                    builder.Append((char) num2);
                                    builder.Append((char) num3);
                                    index += 6;
                                }
                                else
                                {
                                    builder.Append(char.ConvertFromUtf32((int) num2));
                                    index += 4;
                                }
                            }
                        }
                    }
                    else
                    {
                        builder.Append(ch);
                    }
                }
                if (!flag)
                {
                    success = false;
                    return null;
                }
                return builder.ToString();
            }

            public static JsonValue ParseValue(char[] json, ref int index, ref bool success)
            {
                switch (LookAhead(json, index))
                {
                    case 1:
                        return ParseObject(json, ref index, ref success);

                    case 3:
                        return ParseArray(json, ref index, ref success);

                    case 7:
                        return new JsonValue(ParseString(json, ref index, ref success));

                    case 8:
                        return new JsonValue(JsonValue.JsonValueType.Number, ParseNumber(json, ref index, ref success));

                    case 9:
                        NextToken(json, ref index);
                        return new JsonValue(true);

                    case 10:
                        NextToken(json, ref index);
                        return new JsonValue(false);

                    case 11:
                        NextToken(json, ref index);
                        return new JsonValue();
                }
                success = false;
                return null;
            }

            private static bool TryParseUInt32(string s, NumberStyles style, IFormatProvider provider, out uint result)
            {
                return uint.TryParse(s, style, provider, out result);
            }
        }

        protected enum JsonValueType
        {
            String,
            Number,
            Object,
            Array,
            Boolean,
            Null
        }
    }
}

