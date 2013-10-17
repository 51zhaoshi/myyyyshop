namespace Maticsoft.Common
{
    using System;
    using System.Collections.Generic;

    public static class ComparerFactroy<T>
    {
        public static IComparer<T> Create<V1>(Func<T, V1> keySelector1)
        {
            return new CommonComparer<T, V1, object, object, object>(keySelector1, Comparer<V1>.Default, null, null, null, null, null, null);
        }

        public static IComparer<T> Create<V1, V2>(Func<T, V1> keySelector1, Func<T, V2> keySelector2)
        {
            return new CommonComparer<T, V1, V2, object, object>(keySelector1, Comparer<V1>.Default, keySelector2, Comparer<V2>.Default, null, null, null, null);
        }

        public static IComparer<T> Create<V1, V2, V3>(Func<T, V1> keySelector1, Func<T, V2> keySelector2, Func<T, V3> keySelector3)
        {
            return new CommonComparer<T, V1, V2, V3, object>(keySelector1, Comparer<V1>.Default, keySelector2, Comparer<V2>.Default, keySelector3, Comparer<V3>.Default, null, null);
        }

        public static IComparer<T> Create<V1, V2, V3, V4>(Func<T, V1> keySelector1, Func<T, V2> keySelector2, Func<T, V3> keySelector3, Func<T, V4> keySelector4)
        {
            return new CommonComparer<T, V1, V2, V3, V4>(keySelector1, Comparer<V1>.Default, keySelector2, Comparer<V2>.Default, keySelector3, Comparer<V3>.Default, keySelector4, Comparer<V4>.Default);
        }

        private class CommonComparer<V1, V2, V3, V4> : IComparer<T>
        {
            private IComparer<V1> comparer1;
            private IComparer<V2> comparer2;
            private IComparer<V3> comparer3;
            private IComparer<V4> comparer4;
            private Func<T, V1> keySelector1;
            private Func<T, V2> keySelector2;
            private Func<T, V3> keySelector3;
            private Func<T, V4> keySelector4;

            public CommonComparer(Func<T, V1> keySelector1, IComparer<V1> compare1, Func<T, V2> keySelector2, IComparer<V2> compare2, Func<T, V3> keySelector3, IComparer<V3> compare3, Func<T, V4> keySelector4, IComparer<V4> compare4)
            {
                this.keySelector1 = keySelector1;
                this.keySelector2 = keySelector2;
                this.keySelector3 = keySelector3;
                this.keySelector4 = keySelector4;
                this.comparer1 = compare1;
                this.comparer2 = compare2;
                this.comparer3 = compare3;
                this.comparer4 = compare4;
            }

            public int Compare(T x, T y)
            {
                int num = 0;
                if (this.keySelector1 != null)
                {
                    num = this.comparer1.Compare(this.keySelector1(x), this.keySelector1(y));
                }
                if ((this.keySelector2 != null) && (num == 0))
                {
                    num = this.comparer2.Compare(this.keySelector2(x), this.keySelector2(y));
                }
                if ((this.keySelector3 != null) && (num == 0))
                {
                    num = this.comparer3.Compare(this.keySelector3(x), this.keySelector3(y));
                }
                if ((this.keySelector4 != null) && (num == 0))
                {
                    num = this.comparer4.Compare(this.keySelector4(x), this.keySelector4(y));
                }
                return num;
            }
        }
    }
}

