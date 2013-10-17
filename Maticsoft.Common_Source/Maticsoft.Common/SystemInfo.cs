namespace Maticsoft.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;

    public class SystemInfo
    {
        private void Assign(DataTable table, string name, string value)
        {
            DataRow row = table.NewRow();
            row["Name"] = name;
            row["Value"] = value;
            table.Rows.Add(row);
        }

        private string FormatNumber(ulong value)
        {
            if (value < 0x1000L)
            {
                return string.Format("{0} Bytes", value);
            }
            if (value < 0x400000L)
            {
                double num = ((double) value) / 1024.0;
                return string.Format("{0} KB", num.ToString("N"));
            }
            if (value < 0x100000000L)
            {
                double num2 = ((double) value) / 1048576.0;
                return string.Format("{0} MB", num2.ToString("N"));
            }
            if (value < 0x40000000000L)
            {
                double num3 = ((double) value) / 1073741824.0;
                return string.Format("{0} GB", num3.ToString("N"));
            }
            double num4 = ((double) value) / 1099511627776;
            return string.Format("{0} TB", num4.ToString("N"));
        }

        private DataTable GenerateDataTable(string name)
        {
            DataTable table = new DataTable(name);
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Value", typeof(string));
            return table;
        }

        private DataTable GetEnvironmentVariables()
        {
            DataTable table = this.GenerateDataTable("Environment Variables");
            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                this.Assign(table, entry.Key.ToString(), entry.Value.ToString());
            }
            return table;
        }

        private DataTable GetGraphicsObjectInfo()
        {
            DataTable table = this.GenerateDataTable("Graphics COM Component Information");
            this.Assign(table, "SoftArtisans.ImageGen", this.TestObject("SoftArtisans.ImageGen").ToString());
            this.Assign(table, "W3Image.Image", this.TestObject("W3Image.Image").ToString());
            this.Assign(table, "Persits.Jpeg", this.TestObject("Persits.Jpeg").ToString());
            this.Assign(table, "XY.Graphics", this.TestObject("XY.Graphics").ToString());
            this.Assign(table, "Ironsoft.DrawPic", this.TestObject("Ironsoft.DrawPic").ToString());
            this.Assign(table, "Ironsoft.FlashCapture", this.TestObject("Ironsoft.FlashCapture").ToString());
            return table;
        }

        private DataTable GetMailObjectInfo()
        {
            DataTable table = this.GenerateDataTable("Mail COM Component Information");
            this.Assign(table, "JMail.SMTPMail", this.TestObject("JMail.SMTPMail").ToString());
            this.Assign(table, "JMail.Message", this.TestObject("JMail.Message").ToString());
            this.Assign(table, "CDONTS.NewMail", this.TestObject("CDONTS.NewMail").ToString());
            this.Assign(table, "CDO.Message", this.TestObject("CDO.Message").ToString());
            this.Assign(table, "Persits.MailSender", this.TestObject("Persits.MailSender").ToString());
            this.Assign(table, "SMTPsvg.Mailer", this.TestObject("SMTPsvg.Mailer").ToString());
            this.Assign(table, "DkQmail.Qmail", this.TestObject("DkQmail.Qmail").ToString());
            this.Assign(table, "SmtpMail.SmtpMail.1", this.TestObject("SmtpMail.SmtpMail.1").ToString());
            this.Assign(table, "Geocel.Mailer.1", this.TestObject("Geocel.Mailer.1").ToString());
            return table;
        }

        private DataTable GetOtherObjectInfo()
        {
            DataTable table = this.GenerateDataTable("Other COM Component Information");
            this.Assign(table, "dyy.zipsvr", this.TestObject("dyy.zipsvr").ToString());
            this.Assign(table, "hin2.com_iis", this.TestObject("hin2.com_iis").ToString());
            this.Assign(table, "Socket.TCP", this.TestObject("Socket.TCP").ToString());
            return table;
        }

        private DataTable GetRequestHeaderInfo()
        {
            DataTable table = this.GenerateDataTable("Request Headers");
            foreach (string str in HttpContext.Current.Request.Headers.AllKeys)
            {
                this.Assign(table, str, HttpContext.Current.Request.Headers[str]);
            }
            return table;
        }

        private DataTable GetServerVariables()
        {
            DataTable table = this.GenerateDataTable("Server Variables");
            foreach (string str in HttpContext.Current.Request.ServerVariables.AllKeys)
            {
                this.Assign(table, str, HttpContext.Current.Request.ServerVariables[str]);
            }
            return table;
        }

        private DataTable GetSessionInfo()
        {
            DataTable table = this.GenerateDataTable("Session Information");
            this.Assign(table, "Session Count", HttpContext.Current.Session.Contents.Count.ToString());
            this.Assign(table, "Application Count", HttpContext.Current.Application.Contents.Count.ToString());
            return table;
        }

        private DataTable GetSystemInfo()
        {
            DataTable table = this.GenerateDataTable("System Information");
            this.Assign(table, "Server Name", HttpContext.Current.Server.MachineName);
            this.Assign(table, "Server IP", HttpContext.Current.Request.ServerVariables["LOCAl_ADDR"]);
            this.Assign(table, "Server Domain", HttpContext.Current.Request.ServerVariables["Server_Name"]);
            this.Assign(table, "Server Port", HttpContext.Current.Request.ServerVariables["Server_Port"]);
            this.Assign(table, "Web Server Version", HttpContext.Current.Request.ServerVariables["Server_SoftWare"]);
            this.Assign(table, "Virtual Request Path", HttpContext.Current.Request.FilePath);
            this.Assign(table, "Physical Request Path", HttpContext.Current.Request.PhysicalPath);
            this.Assign(table, "Virtual Application Root Path", HttpContext.Current.Request.ApplicationPath);
            this.Assign(table, "Physical Application Root Path", HttpContext.Current.Request.PhysicalApplicationPath);
            this.Assign(table, "Operating System", OperatingSystemOSVersion);
            this.Assign(table, "Operating System Installation Directory", Environment.SystemDirectory);
            this.Assign(table, ".Net Version", DotNetVersion.ToString());
            this.Assign(table, ".Net Language", DotNetLanguage);
            this.Assign(table, "Server Current Time", DateTime.Now.ToString());
            this.Assign(table, "System Uptime", SystemUptime.ToString());
            this.Assign(table, "Script Timeout", ScriptTimeout.ToString());
            return table;
        }

        private DataTable GetSystemMemoryInfo()
        {
            DataTable table = this.GenerateDataTable("Memory Information");
            this.Assign(table, "Current Working Set", this.FormatNumber((ulong) Environment.WorkingSet));
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SystemInfoImport.MemoryInfo memory = SystemInfoImport.Memory;
                    this.Assign(table, "Physical Memory Size", this.FormatNumber((ulong) memory.dwTotalPhys));
                    this.Assign(table, "Physical Free Memory Size", this.FormatNumber((ulong) memory.dwAvailPhys));
                    this.Assign(table, "PageFile Size", this.FormatNumber((ulong) memory.dwTotalPageFile));
                    this.Assign(table, "Available PageFile Size", this.FormatNumber((ulong) memory.dwAvailPageFile));
                    this.Assign(table, "Virtual Memory Size", this.FormatNumber((ulong) memory.dwTotalVirtual));
                    this.Assign(table, "Available Memory Size", this.FormatNumber((ulong) memory.dwAvailVirtual));
                    this.Assign(table, "Memory Load", string.Format("{0} %", memory.dwMemoryLoad.ToString("N")));
                    return table;
                }
                if (Environment.OSVersion.Platform > PlatformID.WinCE)
                {
                    this.GetSystemMemoryInfo_proc(table);
                }
            }
            catch (Exception)
            {
            }
            return table;
        }

        private void GetSystemMemoryInfo_proc(DataTable table)
        {
            string path = "/proc/meminfo";
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path, Encoding.ASCII))
                {
                    Hashtable hashtable = new Hashtable();
                    string str2 = string.Empty;
                    while ((str2 = reader.ReadLine()) != null)
                    {
                        string[] strArray = str2.Split(":".ToCharArray());
                        if (strArray.Length == 2)
                        {
                            string key = strArray[0].Trim();
                            string str4 = strArray[1].Trim();
                            hashtable.Add(key, str4);
                        }
                    }
                    this.Assign(table, "Physical Memory Size", string.Format("{0}", hashtable["MemTotal"]));
                    this.Assign(table, "Physical Free Memory Size", string.Format("{0}", hashtable["MemFree"]));
                    this.Assign(table, "Swap Total Size", string.Format("{0}", hashtable["SwapTotal"]));
                    this.Assign(table, "Swap Free Size", string.Format("{0}", hashtable["SwapFree"]));
                }
            }
        }

        private DataTable GetSystemObjectInfo()
        {
            DataTable table = this.GenerateDataTable("System COM Component Information");
            this.Assign(table, "Adodb.Connection", this.TestObject("Adodb.Connection").ToString());
            this.Assign(table, "Adodb.RecordSet", this.TestObject("Adodb.RecordSet").ToString());
            this.Assign(table, "Adodb.Stream", this.TestObject("Adodb.Stream").ToString());
            this.Assign(table, "Scripting.FileSystemObject", this.TestObject("Scripting.FileSystemObject").ToString());
            this.Assign(table, "Microsoft.XMLHTTP", this.TestObject("Microsoft.XMLHTTP").ToString());
            this.Assign(table, "WScript.Shell", this.TestObject("WScript.Shell").ToString());
            this.Assign(table, "MSWC.AdRotator", this.TestObject("MSWC.AdRotator").ToString());
            this.Assign(table, "MSWC.BrowserType", this.TestObject("MSWC.BrowserType").ToString());
            this.Assign(table, "MSWC.NextLink", this.TestObject("MSWC.NextLink").ToString());
            this.Assign(table, "MSWC.Tools", this.TestObject("MSWC.Tools").ToString());
            this.Assign(table, "MSWC.Status", this.TestObject("MSWC.Status").ToString());
            this.Assign(table, "MSWC.Counters", this.TestObject("MSWC.Counters").ToString());
            this.Assign(table, "IISSample.ContentRotator", this.TestObject("IISSample.ContentRotator").ToString());
            this.Assign(table, "IISSample.PageCounter", this.TestObject("IISSample.PageCounter").ToString());
            this.Assign(table, "MSWC.PermissionChecker", this.TestObject("MSWC.PermissionChecker").ToString());
            return table;
        }

        private DataTable GetSystemProcessorInfo()
        {
            DataTable table = this.GenerateDataTable("Processor Information");
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    this.Assign(table, "Number of Processors", Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
                    this.Assign(table, "Processor Id", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
                    SystemInfoImport.CpuInfo cpu = SystemInfoImport.Cpu;
                    this.Assign(table, "Processor Type", cpu.dwProcessorType.ToString());
                    this.Assign(table, "Processor Level", cpu.dwProcessorLevel.ToString());
                    this.Assign(table, "Processor OEM Id", cpu.dwOemId.ToString());
                    this.Assign(table, "Page Size", cpu.dwPageSize.ToString());
                    this.GetSystemProcessorInfo_WMI(table);
                    return table;
                }
                if (Environment.OSVersion.Platform > PlatformID.WinCE)
                {
                    this.GetSystemProcessorInfo_proc(table);
                }
            }
            catch (Exception)
            {
            }
            return table;
        }

        private void GetSystemProcessorInfo_proc(DataTable table)
        {
            string path = "/proc/cpuinfo";
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path, Encoding.ASCII))
                {
                    ArrayList list = new ArrayList();
                    Hashtable hashtable = new Hashtable();
                    string str2 = string.Empty;
                    while ((str2 = reader.ReadLine()) != null)
                    {
                        if (str2.Trim().Length == 0)
                        {
                            list.Add(hashtable);
                            hashtable = new Hashtable();
                        }
                        string[] strArray = str2.Split(":".ToCharArray());
                        if (strArray.Length == 2)
                        {
                            string key = strArray[0].Trim();
                            string str4 = strArray[1].Trim();
                            hashtable.Add(key, str4);
                        }
                    }
                    foreach (Hashtable hashtable2 in list)
                    {
                        string name = string.Format("Processor {0}", hashtable2["processor"]);
                        string str6 = string.Format("{0}{1}", hashtable2["model name"], (hashtable2["cpu MHz"] != null) ? string.Format(" - {0} MHz", hashtable2["cpu MHz"]) : string.Empty);
                        this.Assign(table, name, str6);
                    }
                }
            }
        }

        private void GetSystemProcessorInfo_WMI(DataTable table)
        {
            Assembly assembly = Assembly.Load("System.Management, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A");
            if (assembly != null)
            {
                Type type = assembly.GetType("System.Management.ManagementObjectSearcher");
                if (assembly != null)
                {
                    MethodInfo method = type.GetMethod("Get", new Type[0]);
                    object obj2 = type.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { "Select * From Win32_Processor" });
                    if (assembly != null)
                    {
                        object obj3 = method.Invoke(obj2, null);
                        Type type2 = assembly.GetType("System.Management.ManagementObject");
                        foreach (object obj4 in (IEnumerable) obj3)
                        {
                            try
                            {
                                try
                                {
                                    PropertyInfo property = type2.GetProperty("Item", new Type[] { typeof(string) });
                                    StringBuilder builder = new StringBuilder();
                                    string str = (string) property.GetValue(obj4, new object[] { "Name" });
                                    builder.Append(str);
                                    uint num = (uint) property.GetValue(obj4, new object[] { "CurrentClockSpeed" });
                                    uint num2 = (uint) property.GetValue(obj4, new object[] { "MaxClockSpeed" });
                                    builder.AppendFormat(" - {0} MHz / {1} MHz", num, num2);
                                    ushort num3 = (ushort) property.GetValue(obj4, new object[] { "CurrentVoltage" });
                                    double num4 = 0.0;
                                    if ((num3 & 0x80) == 0)
                                    {
                                        num4 = ((double) (num3 & 0x7f)) / 10.0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            uint num5 = (uint) property.GetValue(obj4, new object[] { "VoltageCaps" });
                                            switch ((num5 & 15))
                                            {
                                                case 1:
                                                    num4 = 5.0;
                                                    goto Label_0227;

                                                case 2:
                                                    num4 = 3.3;
                                                    goto Label_0227;

                                                case 3:
                                                    num4 = 2.9;
                                                    goto Label_0227;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                Label_0227:
                                    if (num4 > 0.0)
                                    {
                                        builder.AppendFormat(" - {0}v", num4);
                                    }
                                    ushort num6 = (ushort) property.GetValue(obj4, new object[] { "LoadPercentage" });
                                    builder.AppendFormat(" - Load = {0} %", num6);
                                    this.Assign(table, "Processor", builder.ToString());
                                }
                                catch (Exception exception)
                                {
                                    this.Assign(table, "Exception Occurs", exception.ToString());
                                }
                                continue;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        private DataTable GetSystemStorageInfo()
        {
            DataTable table = this.GenerateDataTable("Storage Information");
            try
            {
                this.Assign(table, "Logical Driver Information", string.Join(", ", Directory.GetLogicalDrives()));
            }
            catch (Exception)
            {
            }
            if (Environment.Version.Major >= 2)
            {
                this.GetSystemStorageInfo_DriveInfo(table);
                return table;
            }
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                this.GetSystemStorageInfo_WMI(table);
            }
            return table;
        }

        private void GetSystemStorageInfo_DriveInfo(DataTable table)
        {
            try
            {
                Type type = Type.GetType("System.IO.DriveInfo");
                foreach (object obj3 in (IEnumerable) type.GetMethod("GetDrives").Invoke(null, null))
                {
                    try
                    {
                        PropertyInfo[] properties = type.GetProperties();
                        bool flag = (bool) type.GetProperty("IsReady").GetValue(obj3, null);
                        string str = string.Empty;
                        string str2 = string.Empty;
                        string str3 = string.Empty;
                        string str4 = string.Empty;
                        ulong num = 0L;
                        ulong num2 = 0L;
                        foreach (PropertyInfo info2 in properties)
                        {
                            string str7 = info2.Name;
                            if (str7 != null)
                            {
                                if (!(str7 == "Name"))
                                {
                                    if (str7 == "VolumeLabel")
                                    {
                                        goto Label_0109;
                                    }
                                    if (str7 == "DriveFormat")
                                    {
                                        goto Label_011F;
                                    }
                                    if (str7 == "DriveType")
                                    {
                                        goto Label_0135;
                                    }
                                    if (str7 == "TotalFreeSpace")
                                    {
                                        goto Label_0147;
                                    }
                                    if (str7 == "TotalSize")
                                    {
                                        goto Label_015D;
                                    }
                                }
                                else
                                {
                                    str = (string) info2.GetValue(obj3, null);
                                }
                            }
                            goto Label_0171;
                        Label_0109:
                            if (flag)
                            {
                                str2 = (string) info2.GetValue(obj3, null);
                            }
                            goto Label_0171;
                        Label_011F:
                            if (flag)
                            {
                                str3 = (string) info2.GetValue(obj3, null);
                            }
                            goto Label_0171;
                        Label_0135:
                            str4 = info2.GetValue(obj3, null).ToString();
                            goto Label_0171;
                        Label_0147:
                            if (flag)
                            {
                                num = (ulong) ((long) info2.GetValue(obj3, null));
                            }
                            goto Label_0171;
                        Label_015D:
                            if (flag)
                            {
                                num2 = (ulong) ((long) info2.GetValue(obj3, null));
                            }
                        Label_0171:;
                        }
                        string name = string.Empty;
                        string str6 = string.Empty;
                        if (flag)
                        {
                            name = string.Format("{0} - <{1}> [{2}] - {3,-10}", new object[] { str, str2, str3, str4 });
                            if (((num2 > 0L) && (num2 != ulong.MaxValue)) && (num2 != 0x7fffffffL))
                            {
                                str6 = string.Format("Free {0} / Total {1}", this.FormatNumber(num), this.FormatNumber(num2));
                            }
                        }
                        else
                        {
                            name = string.Format("{0} {1,-10}", str, str4);
                        }
                        this.Assign(table, name, str6);
                        continue;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void GetSystemStorageInfo_WMI(DataTable table)
        {
            try
            {
                Assembly assembly = Assembly.Load("System.Management, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=B03F5F7F11D50A3A");
                if (assembly != null)
                {
                    Type type = assembly.GetType("System.Management.ManagementObjectSearcher");
                    if (assembly != null)
                    {
                        MethodInfo method = type.GetMethod("Get", new Type[0]);
                        object obj2 = type.GetConstructor(new Type[] { typeof(string) }).Invoke(new object[] { "Select * From Win32_LogicalDisk" });
                        if (assembly != null)
                        {
                            object obj3 = method.Invoke(obj2, null);
                            Type type2 = assembly.GetType("System.Management.ManagementObject");
                            foreach (object obj4 in (IEnumerable) obj3)
                            {
                                try
                                {
                                    PropertyInfo property = type2.GetProperty("Item", new Type[] { typeof(string) });
                                    uint num = (uint) property.GetValue(obj4, new object[] { "DriveType" });
                                    string str = string.Empty;
                                    switch (num)
                                    {
                                        case 1:
                                            str = "No Root Directory";
                                            break;

                                        case 2:
                                            str = "Removable Disk";
                                            break;

                                        case 3:
                                            str = "Local Disk";
                                            break;

                                        case 4:
                                            str = "Network Drive";
                                            break;

                                        case 5:
                                            str = "Compact Disc";
                                            break;

                                        case 6:
                                            str = "RAM Disk";
                                            break;

                                        default:
                                            str = "Unknown";
                                            break;
                                    }
                                    string str2 = property.GetValue(obj4, new object[] { "Name" }) as string;
                                    string str3 = property.GetValue(obj4, new object[] { "VolumeName" }) as string;
                                    string str4 = property.GetValue(obj4, new object[] { "FileSystem" }) as string;
                                    string str5 = string.Empty;
                                    try
                                    {
                                        str5 = this.FormatNumber((ulong) property.GetValue(obj4, new object[] { "FreeSpace" }));
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    string str6 = string.Empty;
                                    try
                                    {
                                        str6 = this.FormatNumber((ulong) property.GetValue(obj4, new object[] { "Size" }));
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    string name = string.Format("{0} - <{1}> [{2}] - {3,-10}", new object[] { str2, str3, str4, str });
                                    string str8 = ((str5 == null) || (str5 == "")) ? string.Empty : string.Format("Free {0} / Total {1}", str5, str6);
                                    this.Assign(table, name, str8);
                                    continue;
                                }
                                catch (Exception exception)
                                {
                                    this.Assign(table, "Exception Occurs", exception.ToString());
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                this.Assign(table, "Exception Occurs", exception2.ToString());
            }
        }

        private DataTable GetUploadObjectInfo()
        {
            DataTable table = this.GenerateDataTable("Upload COM Component Information");
            this.Assign(table, "LyfUpload.UploadFile", this.TestObject("LyfUpload.UploadFile").ToString());
            this.Assign(table, "Persits.Upload", this.TestObject("Persits.Upload").ToString());
            this.Assign(table, "Ironsoft.UpLoad", this.TestObject("Ironsoft.UpLoad").ToString());
            this.Assign(table, "aspcn.Upload", this.TestObject("aspcn.Upload").ToString());
            this.Assign(table, "SoftArtisans.FileUp", this.TestObject("SoftArtisans.FileUp").ToString());
            this.Assign(table, "SoftArtisans.FileManager", this.TestObject("SoftArtisans.FileManager").ToString());
            this.Assign(table, "Dundas.Upload", this.TestObject("Dundas.Upload").ToString());
            this.Assign(table, "w3.upload", this.TestObject("w3.upload").ToString());
            return table;
        }

        private bool TestObject(string progID)
        {
            try
            {
                HttpContext.Current.Server.CreateObject(progID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string DotNetLanguage
        {
            get
            {
                return CultureInfo.InstalledUICulture.EnglishName;
            }
        }

        public static Version DotNetVersion
        {
            get
            {
                return Environment.Version;
            }
        }

        public static string OperatingSystemFull
        {
            get
            {
                return string.Format("{0} -- {1}", OperatingSystemOSVersion, OperatingSystemSimple);
            }
        }

        public static string OperatingSystemOSVersion
        {
            get
            {
                return Environment.OSVersion.ToString();
            }
        }

        public static string OperatingSystemSimple
        {
            get
            {
                OperatingSystem oSVersion = Environment.OSVersion;
                string str = string.Empty;
                switch (oSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                        switch (oSVersion.Version.Minor)
                        {
                            case 0:
                                return "Microsoft Windows 95";

                            case 10:
                                return "Microsoft Windows 98";

                            case 90:
                                return "Microsoft Windows Millennium Edition";
                        }
                        return "Microsoft Windows 95 or later";

                    case PlatformID.Win32NT:
                        switch (oSVersion.Version.Major)
                        {
                            case 3:
                                return "Microsoft Windows NT 3.51";

                            case 4:
                                return "Microsoft Windows NT 4.0";

                            case 5:
                                switch (oSVersion.Version.Minor)
                                {
                                    case 0:
                                        return "Microsoft Windows 2000";

                                    case 1:
                                        return "Microsoft Windows XP";

                                    case 2:
                                        return "Microsoft Windows 2003";
                                }
                                return "Microsoft NT 5.x";

                            case 6:
                                switch (oSVersion.Version.Minor)
                                {
                                    case 0:
                                        return "Microsoft Windows Vista or Server 2008";

                                    case 1:
                                        return "Microsoft Windows 7 or Server 2008 R2";

                                    case 2:
                                        return "Microsoft Windows 8 or Server 2012";
                                }
                                return "Microsoft NT 6.x";
                        }
                        return str;
                }
                if (oSVersion.Platform <= PlatformID.WinCE)
                {
                    return str;
                }
                string path = "/proc/version";
                if (!File.Exists(path))
                {
                    return str;
                }
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
        }

        public static TimeSpan ScriptTimeout
        {
            get
            {
                return TimeSpan.FromSeconds((double) HttpContext.Current.Server.ScriptTimeout);
            }
        }

        public static string ServerDomain
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["Server_Name"];
            }
        }

        public static string ServerIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["LOCAl_ADDR"];
            }
        }

        public static string ServerName
        {
            get
            {
                return HttpContext.Current.Server.MachineName;
            }
        }

        public static string ServerPort
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["Server_Port"];
            }
        }

        public static TimeSpan SystemUptime
        {
            get
            {
                return TimeSpan.FromMilliseconds((double) Environment.TickCount);
            }
        }

        public static string WebServerVersion
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["Server_SoftWare"];
            }
        }
    }
}

