namespace Maticsoft.Json
{
    using System;

    internal sealed class BooleanObject
    {
        public static readonly object False = false;
        public static readonly object True = true;

        private BooleanObject()
        {
            throw new NotSupportedException();
        }

        public static object Box(bool value)
        {
            if (!value)
            {
                return False;
            }
            return True;
        }
    }
}

