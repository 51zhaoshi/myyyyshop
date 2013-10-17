namespace Microsoft.Web.Infrastructure
{
    using System;
    using System.Security;
    using System.Threading;

    internal static class ModuleInitializer
    {
        [SecuritySafeCritical]
        public static void Initialize()
        {
            CriticalInitializer.Initialize();
        }

        [SecurityCritical]
        private static class CriticalInitializer
        {
            private static int _initializeCalled;

            private static void CheckKillBit()
            {
                using (KillBitHelper helper = new KillBitHelper())
                {
                    helper.ThrowIfKillBitIsSet();
                }
            }

            public static void Initialize()
            {
                if (Interlocked.Exchange(ref _initializeCalled, 1) == 0)
                {
                    CheckKillBit();
                }
            }
        }
    }
}

