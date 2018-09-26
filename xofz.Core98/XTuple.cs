#pragma warning disable 659
namespace xofz
{
    using System.Threading;

    public class XTuple<T, U>
    {
        public XTuple(T item1, U item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public virtual T Item1 { get; private set; }

        public virtual U Item2 { get; private set; }

        public virtual void AtomicGet(out T item1, out U item2)
        {
            while (Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2)
        {
            while (Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public override bool Equals(object obj)
        {
            var otherTuple = obj as XTuple<T, U>;
            if (otherTuple == null)
            {
                return false;
            }

            var item1Equals = this.Item1?.Equals(otherTuple.Item1)
                              ?? otherTuple.Item1 == null;
            if (!item1Equals)
            {
                return false;
            }

            var item2Equals = this.Item2?.Equals(otherTuple.Item2)
                              ?? otherTuple.Item2 == null;
            if (!item2Equals)
            {
                return false;
            }

            return true;
        }

        private int settingOrGettingIf1;
    }

    public class XTuple<T, U, V>
    {
        public XTuple(
            T item1,
            U item2,
            V item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public virtual T Item1 { get; private set; }

        public virtual U Item2 { get; private set; }

        public virtual V Item3 { get; private set; }

        public virtual void AtomicGet(out T item1, out U item2)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, V item3)
        {
            while (Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out U item2, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(U item2, V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out U item2, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2, V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public override bool Equals(object obj)
        {
            var otherTuple = obj as XTuple<T, U, V>;
            if (otherTuple == null)
            {
                return false;
            }

            var item1Equals = this.Item1?.Equals(otherTuple.Item1)
                              ?? otherTuple.Item1 == null;
            if (!item1Equals)
            {
                return false;
            }

            var item2Equals = this.Item2?.Equals(otherTuple.Item2)
                              ?? otherTuple.Item2 == null;
            if (!item2Equals)
            {
                return false;
            }
            
            var item3Equals = this.Item3?.Equals(otherTuple.Item3)
                              ?? otherTuple.Item3 == null;
            if (!item3Equals)
            {
                return false;
            }

            return true;
        }

        private int settingOrGettingIf1;
    }

    public class XTuple<T, U, V, W>
    {
        public XTuple(
            T item1,
            U item2,
            V item3,
            W item4)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }

        public virtual T Item1 { get; private set; }

        public virtual U Item2 { get; private set; }

        public virtual V Item3 { get; private set; }

        public virtual W Item4 { get; private set; }

        public virtual void AtomicGet(out T item1, out U item2)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out U item2, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(U item2, V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out U item2, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item2 = this.Item2;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(U item2, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out V item3, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(V item3, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out U item2, out V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2, V item3)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out U item2, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, U item2, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out T item1, out V item3, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(T item1, V item3, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(out U item2, out V item3, out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item2 = this.Item2;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(U item2, V item3, W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicGet(
            out T item1, 
            out U item2, 
            out V item3, 
            out W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            item1 = this.Item1;
            item2 = this.Item2;
            item3 = this.Item3;
            item4 = this.Item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public virtual void AtomicSet(
            T item1, 
            U item2, 
            V item3, 
            W item4)
        {
            while (Interlocked.CompareExchange(
                       ref this.settingOrGettingIf1, 1, 0) == 1)
            {
                continue;
            }

            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            Interlocked.CompareExchange(
                ref this.settingOrGettingIf1, 0, 1);
        }

        public override bool Equals(object obj)
        {
            var otherTuple = obj as XTuple<T, U, V, W>;
            if (otherTuple == null)
            {
                return false;
            }

            var item1Equals = this.Item1?.Equals(otherTuple.Item1)
                              ?? otherTuple.Item1 == null;
            if (!item1Equals)
            {
                return false;
            }

            var item2Equals = this.Item2?.Equals(otherTuple.Item2)
                              ?? otherTuple.Item2 == null;
            if (!item2Equals)
            {
                return false;
            }

            var item3Equals = this.Item3?.Equals(otherTuple.Item3)
                              ?? otherTuple.Item3 == null;
            if (!item3Equals)
            {
                return false;
            }

            var item4Equals = this.Item4?.Equals(otherTuple.Item4)
                              ?? otherTuple.Item4 == null;
            if (!item4Equals)
            {
                return false;
            }

            return true;
        }

        private int settingOrGettingIf1;
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
    }
}
