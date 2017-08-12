namespace xofz
{
    using System.Threading;

    public class Tuple<T, U>
    {
        public Tuple(T item1, U item2)
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

        private int settingOrGettingIf1;
    }

    public class Tuple<T, U, V>
    {
        public Tuple(
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

        private int settingOrGettingIf1;
    }

    public class Tuple<T, U, V, W>
    {
        public Tuple(
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

        private int settingOrGettingIf1;
    }

    public static class Tuple
    {
        public static Tuple<T, U> Create<T, U>(
            T item1,
            U item2)
        {
            return new Tuple<T, U>(
                item1,
                item2);
        }


        public static Tuple<T, U, V> Create<T, U, V>(
            T item1,
            U item2,
            V item3)
        {
            return new Tuple<T, U, V>(
                item1,
                item2,
                item3);
        }

        public static Tuple<T, U, V, W> Create<T, U, V, W>(
            T item1,
            U item2,
            V item3,
            W item4)
        {
            return new Tuple<T, U, V, W>(
                item1,
                item2,
                item3,
                item4);
        }
    }
}
