namespace Maticsoft.ZipLib
{
    using Maticsoft.ZipLib.Checksums;
    using Maticsoft.ZipLib.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;

    public static class ZipHelper
    {
        public static bool ExistsZipFileDirectory(string[] directories, string zipedFile, bool isExactlyMatch = true)
        {
            List<bool> list = new List<bool>();
            using (FileStream stream = File.Create(zipedFile))
            {
                using (new ZipOutputStream(stream))
                {
                    for (int i = 0; i < directories.Length; i++)
                    {
                        if (directories[i][directories[i].Length - 1] != Path.DirectorySeparatorChar)
                        {
                            string[] strArray2;
                            IntPtr ptr;
                            (strArray2 = directories)[(int) (ptr = (IntPtr) i)] = strArray2[(int) ptr] + Path.DirectorySeparatorChar;
                        }
                        string[] strArray = Directory.GetDirectories(directories[i]);
                        foreach (string str in strArray)
                        {
                            list.Add(str == directories[i]);
                        }
                    }
                }
            }
            if (isExactlyMatch)
            {
                List<bool> list2 = list.FindAll(xx => xx);
                return ((list2 != null) && (list2.Count == directories.Length));
            }
            return (list.Count == directories.Length);
        }

        public static void UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
        {
            if (strDirectory == "")
            {
                strDirectory = Directory.GetCurrentDirectory();
            }
            if (!strDirectory.EndsWith(@"\"))
            {
                strDirectory = strDirectory + @"\";
            }
            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(zipedFile)))
            {
                ZipEntry entry;
                stream.Password = password;
                while ((entry = stream.GetNextEntry()) != null)
                {
                    string str = "";
                    string path = "";
                    path = entry.Name;
                    if (path != "")
                    {
                        str = Path.GetDirectoryName(path) + @"\";
                    }
                    string fileName = Path.GetFileName(path);
                    Directory.CreateDirectory(strDirectory + str);
                    if ((fileName != "") && ((File.Exists(strDirectory + str + fileName) && overWrite) || !File.Exists(strDirectory + str + fileName)))
                    {
                        using (FileStream stream2 = File.Create(strDirectory + str + fileName))
                        {
                            bool flag;
                            int count = 0x800;
                            byte[] buffer = new byte[0x800];
                            goto Label_013C;
                        Label_010C:
                            count = stream.Read(buffer, 0, buffer.Length);
                            if (count <= 0)
                            {
                                goto Label_0141;
                            }
                            stream2.Write(buffer, 0, count);
                        Label_013C:
                            flag = true;
                            goto Label_010C;
                        Label_0141:
                            stream2.Close();
                        }
                    }
                }
                stream.Close();
            }
        }

        public static void ZipFile(string fileToZip, string zipedFile)
        {
            if (!File.Exists(fileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
            }
            using (FileStream stream = File.OpenRead(fileToZip))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                using (FileStream stream2 = File.Create(zipedFile))
                {
                    using (ZipOutputStream stream3 = new ZipOutputStream(stream2))
                    {
                        ZipEntry entry = new ZipEntry(fileToZip.Substring(fileToZip.LastIndexOf(@"\") + 1));
                        stream3.PutNextEntry(entry);
                        stream3.SetLevel(5);
                        stream3.Write(buffer, 0, buffer.Length);
                        stream3.Finish();
                        stream3.Close();
                    }
                }
            }
        }

        public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
        {
            if (!File.Exists(fileToZip))
            {
                throw new FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");
            }
            using (FileStream stream = File.Create(zipedFile))
            {
                using (ZipOutputStream stream2 = new ZipOutputStream(stream))
                {
                    using (FileStream stream3 = new FileStream(fileToZip, FileMode.Open, FileAccess.Read))
                    {
                        ZipEntry entry = new ZipEntry(fileToZip.Substring(fileToZip.LastIndexOf(@"\") + 1));
                        stream2.PutNextEntry(entry);
                        stream2.SetLevel(compressionLevel);
                        byte[] buffer = new byte[blockSize];
                        int count = 0;
                        try
                        {
                            do
                            {
                                count = stream3.Read(buffer, 0, buffer.Length);
                                stream2.Write(buffer, 0, count);
                            }
                            while (count > 0);
                        }
                        catch (Exception exception)
                        {
                            throw exception;
                        }
                        stream3.Close();
                    }
                    stream2.Finish();
                    stream2.Close();
                }
                stream.Close();
            }
        }

        public static void ZipFileDirectory(string strDirectory, string zipedFile)
        {
            using (FileStream stream = File.Create(zipedFile))
            {
                using (ZipOutputStream stream2 = new ZipOutputStream(stream))
                {
                    ZipSetp(strDirectory, stream2, "");
                }
            }
        }

        private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
        {
            if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
            {
                strDirectory = strDirectory + Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();
            string[] fileSystemEntries = Directory.GetFileSystemEntries(strDirectory);
            foreach (string str in fileSystemEntries)
            {
                if (Directory.Exists(str))
                {
                    string str2 = parentPath;
                    str2 = str2 + str.Substring(str.LastIndexOf(@"\") + 1) + @"\";
                    ZipSetp(str, s, str2);
                }
                else
                {
                    using (FileStream stream = File.OpenRead(str))
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        ZipEntry entry = new ZipEntry(parentPath + str.Substring(str.LastIndexOf(@"\") + 1)) {
                            DateTime = DateTime.Now,
                            Size = stream.Length
                        };
                        stream.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);
                        s.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}

