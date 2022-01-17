#pragma warning disable 659
namespace xofz
{
    using System.Threading;

    public class XTuple<T, U>
    {
        public XTuple(
            T item1,
            U item2)
        {
            this.one = item1;
            this.two = item2;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        protected const long oneL = 1;
        protected const long zeroL = 0;

        public virtual void AtomicGet(
            out T item1,
            out U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected long settingOrGettingIf1;
    }

    public class XTuple<T, U, V>
    {
        public XTuple(
            T item1,
            U item2,
            V item3)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;

            protected set => this.three = value;
        }

        protected const long oneL = 1;
        protected const long zeroL = 0;

        public virtual void AtomicGet(
            out T item1,
            out U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out U item2,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            U item2,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out U item2,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected long settingOrGettingIf1;
    }

    public class XTuple<T, U, V, W>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
            this.four = item4;
        }

        public virtual T Item1
        {
            get => this.one;
            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;
            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;
            protected set => this.three = value;
        }

        public virtual W Item4
        {
            get => this.four;
            protected set => this.four = value;
        }

        protected const long oneL = 1;
        protected const long zeroL = 0;

        public virtual void AtomicGet(
            out T item1,
            out U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicGet(
            out U item2,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            U item2,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicGet(
            out U item2,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item2 = this.Item2;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            U item2,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out V item3,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            V item3,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out U item2,
            out V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2,
            V item3)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out U item2,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out V item3,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            V item3,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicGet(
            out U item2,
            out V item3,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            U item2,
            V item3,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1,
                zeroL);
        }

        public virtual void AtomicGet(
            out T item1,
            out U item2,
            out V item3,
            out W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public virtual void AtomicSet(
            T item1,
            U item2,
            V item3,
            W item4)
        {
            while (Interlocked.Exchange(
                       ref this.settingOrGettingIf1,
                       oneL) == oneL)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.Exchange(
                ref this.settingOrGettingIf1, 
                zeroL);
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V, W>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                goto checkItem4;
            }

            return falsity;

            checkItem4:
            var ti4 = this.Item4;
            var oi4 = otherTuple.Item4;
            if (ti4?.Equals(oi4) ?? oi4 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected W four;
        protected long settingOrGettingIf1;
    }

    public class XTuple<T, U, V, W, X>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
            this.four = item4;
            this.five = item5;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;

            protected set => this.three = value;
        }

        public virtual W Item4
        {
            get => this.four;

            protected set => this.four = value;
        }

        public virtual X Item5
        {
            get => this.five;

            protected set => this.five = value;
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V, W, X>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                goto checkItem4;
            }

            return falsity;

            checkItem4:
            var ti4 = this.Item4;
            var oi4 = otherTuple.Item4;
            if (ti4?.Equals(oi4) ?? oi4 == null)
            {
                goto checkItem5;
            }

            return falsity;

            checkItem5:
            var ti5 = this.Item5;
            var oi5 = otherTuple.Item5;
            if (ti5?.Equals(oi5) ?? oi5 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected W four;
        protected X five;
    }

    public class XTuple<T, U, V, W, X, Y>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
            this.four = item4;
            this.five = item5;
            this.six = item6;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;
            protected set => this.three = value;
        }

        public virtual W Item4
        {
            get => this.four;
            protected set => this.four = value;
        }

        public virtual X Item5
        {
            get => this.five;

            protected set => this.five = value;
        }

        public virtual Y Item6
        {
            get => this.six;

            protected set => this.six = value;
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V, W, X, Y>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                goto checkItem4;
            }

            return falsity;

            checkItem4:
            var ti4 = this.Item4;
            var oi4 = otherTuple.Item4;
            if (ti4?.Equals(oi4) ?? oi4 == null)
            {
                goto checkItem5;
            }

            return falsity;

            checkItem5:
            var ti5 = this.Item5;
            var oi5 = otherTuple.Item5;
            if (ti5?.Equals(oi5) ?? oi5 == null)
            {
                goto checkItem6;
            }

            return falsity;

            checkItem6:
            var ti6 = this.Item6;
            var oi6 = otherTuple.Item6;
            if (ti6?.Equals(oi6) ?? oi6 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected W four;
        protected X five;
        protected Y six;
    }

    public class XTuple<T, U, V, W, X, Y, Z>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6,
            Z item7)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
            this.four = item4;
            this.five = item5;
            this.six = item6;
            this.seven = item7;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;
            protected set => this.three = value;
        }

        public virtual W Item4
        {
            get => this.four;
            protected set => this.four = value;
        }

        public virtual X Item5
        {
            get => this.five;

            protected set => this.five = value;
        }

        public virtual Y Item6
        {
            get => this.six;

            protected set => this.six = value;
        }

        public virtual Z Item7
        {
            get => this.seven;

            protected set => this.seven = value;
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V, W, X, Y, Z>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                goto checkItem4;
            }

            return falsity;

            checkItem4:
            var ti4 = this.Item4;
            var oi4 = otherTuple.Item4;
            if (ti4?.Equals(oi4) ?? oi4 == null)
            {
                goto checkItem5;
            }

            return falsity;

            checkItem5:
            var ti5 = this.Item5;
            var oi5 = otherTuple.Item5;
            if (ti5?.Equals(oi5) ?? oi5 == null)
            {
                goto checkItem6;
            }

            return falsity;

            checkItem6:
            var ti6 = this.Item6;
            var oi6 = otherTuple.Item6;
            if (ti6?.Equals(oi6) ?? oi6 == null)
            {
                goto checkItem7;
            }

            return falsity;

            checkItem7:
            var ti7 = this.Item7;
            var oi7 = otherTuple.Item7;
            if (ti7?.Equals(oi7) ?? oi7 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected W four;
        protected X five;
        protected Y six;
        protected Z seven;
    }

    public class XTuple<T, U, V, W, X, Y, Z, AA>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6,
            Z item7,
            AA item8)
        {
            this.one = item1;
            this.two = item2;
            this.three = item3;
            this.four = item4;
            this.five = item5;
            this.six = item6;
            this.seven = item7;
            this.eight = item8;
        }

        public virtual T Item1
        {
            get => this.one;

            protected set => this.one = value;
        }

        public virtual U Item2
        {
            get => this.two;

            protected set => this.two = value;
        }

        public virtual V Item3
        {
            get => this.three;
            protected set => this.three = value;
        }

        public virtual W Item4
        {
            get => this.four;
            protected set => this.four = value;
        }

        public virtual X Item5
        {
            get => this.five;

            protected set => this.five = value;
        }

        public virtual Y Item6
        {
            get => this.six;

            protected set => this.six = value;
        }

        public virtual Z Item7
        {
            get => this.seven;

            protected set => this.seven = value;
        }

        public virtual AA Item8
        {
            get => this.eight;

            protected set => this.eight = value;
        }

        public override bool Equals(
            object obj)
        {
            const bool
                truth = true,
                falsity = false;
            var otherTuple = obj as XTuple<T, U, V, W, X, Y, Z, AA>;
            if (otherTuple == null)
            {
                return falsity;
            }

            var ti1 = this.Item1;
            var oi1 = otherTuple.Item1;
            if (ti1?.Equals(oi1) ?? oi1 == null)
            {
                goto checkItem2;
            }

            return falsity;

            checkItem2:
            var ti2 = this.Item2;
            var oi2 = otherTuple.Item2;
            if (ti2?.Equals(oi2) ?? oi2 == null)
            {
                goto checkItem3;
            }

            return falsity;

            checkItem3:
            var ti3 = this.Item3;
            var oi3 = otherTuple.Item3;
            if (ti3?.Equals(oi3) ?? oi3 == null)
            {
                goto checkItem4;
            }

            return falsity;

            checkItem4:
            var ti4 = this.Item4;
            var oi4 = otherTuple.Item4;
            if (ti4?.Equals(oi4) ?? oi4 == null)
            {
                goto checkItem5;
            }

            return falsity;

            checkItem5:
            var ti5 = this.Item5;
            var oi5 = otherTuple.Item5;
            if (ti5?.Equals(oi5) ?? oi5 == null)
            {
                goto checkItem6;
            }

            return falsity;

            checkItem6:
            var ti6 = this.Item6;
            var oi6 = otherTuple.Item6;
            if (ti6?.Equals(oi6) ?? oi6 == null)
            {
                goto checkItem7;
            }

            return falsity;

            checkItem7:
            var ti7 = this.Item7;
            var oi7 = otherTuple.Item7;
            if (ti7?.Equals(oi7) ?? oi7 == null)
            {
                goto checkItem8;
            }

            return falsity;

            checkItem8:
            var ti8 = this.Item8;
            var oi8 = otherTuple.Item8;
            if (ti8?.Equals(oi8) ?? oi8 == null)
            {
                return truth;
            }

            return falsity;
        }

        protected T one;
        protected U two;
        protected V three;
        protected W four;
        protected X five;
        protected Y six;
        protected Z seven;
        protected AA eight;
    }

    public static class XTuple
    {
        public static XTuple<T, U> Create<T, U>(
            T item1,
            U item2)
        {
            return new XTuple<T, U>(
                item1,
                item2);
        }

        public static XTuple<T, U, V> Create<T, U, V>(
            T item1,
            U item2,
            V item3)
        {
            return new XTuple<T, U, V>(
                item1,
                item2,
                item3);
        }

        public static XTuple<T, U, V, W> Create<T, U, V, W>(
            T item1,
            U item2,
            V item3,
            W item4)
        {
            return new XTuple<T, U, V, W>(
                item1,
                item2,
                item3,
                item4);
        }

        public static XTuple<T, U, V, W, X> Create<T, U, V, W, X>(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5)
        {
            return new XTuple<T, U, V, W, X>(
                item1,
                item2,
                item3,
                item4,
                item5);
        }

        public static XTuple<T, U, V, W, X, Y> Create<T, U, V, W, X, Y>(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6)
        {
            return new XTuple<T, U, V, W, X, Y>(
                item1,
                item2,
                item3,
                item4,
                item5,
                item6);
        }

        public static XTuple<T, U, V, W, X, Y, Z> Create<T, U, V, W, X, Y, Z>(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6,
            Z item7)
        {
            return new XTuple<T, U, V, W, X, Y, Z>(
                item1,
                item2,
                item3,
                item4,
                item5,
                item6,
                item7);
        }

        public static XTuple<T, U, V, W, X, Y, Z, AA> Create<T, U, V, W, X, Y,
            Z, AA>(
            T item1,
            U item2,
            V item3,
            W item4,
            X item5,
            Y item6,
            Z item7,
            AA item8)
        {
            return new XTuple<T, U, V, W, X, Y, Z, AA>(
                item1,
                item2,
                item3,
                item4,
                item5,
                item6,
                item7,
                item8);
        }
    }
}
