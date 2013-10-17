namespace Maticsoft.Common.DEncrypt
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Security;

    public class DESEncrypt
    {
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "MATICSOFT");
        }

        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int num = Text.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num3 = Convert.ToInt32(Text.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num3;
            }
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.Default.GetString(stream.ToArray());
        }

        public static string Decrypt(string Text, string key, string iv)
        {
            byte[] bytes = new UnicodeEncoding().GetBytes(key);
            byte[] buffer2 = new UnicodeEncoding().GetBytes(iv);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int num = Text.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num3 = Convert.ToInt32(Text.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num3;
            }
            provider.Key = bytes;
            provider.IV = buffer2;
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Encoding.Default.GetString(stream.ToArray());
        }

        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "MATICSOFT");
        }

        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(Text);
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        public static string Encrypt(string Text, out string key, out string iv)
        {
            key = "";
            iv = "";
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(Text);
            provider.GenerateIV();
            provider.GenerateKey();
            iv = new UnicodeEncoding().GetString(provider.IV);
            key = new UnicodeEncoding().GetString(provider.Key);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }
    }
}

