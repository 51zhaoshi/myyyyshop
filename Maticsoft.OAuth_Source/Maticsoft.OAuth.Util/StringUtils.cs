namespace Maticsoft.OAuth.Util
{
    using System;

    internal sealed class StringUtils
    {
        internal static bool HasLength(string target)
        {
            return ((target != null) && (target.Length > 0));
        }

        internal static bool HasText(string target)
        {
            if (target == null)
            {
                return false;
            }
            return HasLength(target.Trim());
        }
    }
}

