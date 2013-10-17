namespace Maticsoft.Common
{
    using System;
    using System.Runtime.InteropServices;

    public class SystemInfoImport
    {
        [DllImport("kernel32", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern void GetSystemInfo(ref CpuInfo cpuinfo);
        [DllImport("kernel32", CharSet=CharSet.Auto, SetLastError=true)]
        internal static extern void GlobalMemoryStatus(ref MemoryInfo meminfo);

        public static CpuInfo Cpu
        {
            get
            {
                CpuInfo cpuinfo = new CpuInfo();
                GetSystemInfo(ref cpuinfo);
                return cpuinfo;
            }
        }

        public static MemoryInfo Memory
        {
            get
            {
                MemoryInfo meminfo = new MemoryInfo();
                GlobalMemoryStatus(ref meminfo);
                return meminfo;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CpuInfo
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryInfo
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
    }
}

