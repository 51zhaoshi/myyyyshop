namespace Maticsoft.Common
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class INIFile
    {
        public string path;

        public INIFile(string INIPath)
        {
            this.path = INIPath;
        }

        public void ClearAllSection()
        {
            this.IniWriteValue(null, null, null);
        }

        public void ClearSection(string Section)
        {
            this.IniWriteValue(Section, null, null);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder retVal = new StringBuilder(0xff);
            GetPrivateProfileString(Section, Key, "", retVal, 0xff, this.path);
            return retVal.ToString();
        }

        public byte[] IniReadValues(string section, string key)
        {
            byte[] retVal = new byte[0xff];
            GetPrivateProfileString(section, key, "", retVal, 0xff, this.path);
            return retVal;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}

