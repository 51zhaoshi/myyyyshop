namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability
{
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("userenv.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern IntPtr EnterCriticalPolicySection([MarshalAs(UnmanagedType.Bool)] bool bMachine);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("userenv.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool LeaveCriticalPolicySection(IntPtr handle);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("userenv.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool RegisterGPNotification(SafeWaitHandle hEvent, [MarshalAs(UnmanagedType.Bool)] bool bMachine);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("userenv.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern bool UnregisterGPNotification(SafeWaitHandle hEvent);
    }
}

