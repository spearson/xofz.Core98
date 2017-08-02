namespace xofz
{
    public class Tuple<T, U>
    {
        public Tuple(T item1, U item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public virtual T Item1 { get; private set; }

        public virtual U Item2 { get; private set; }

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

        public virtual T Item1 { get; }

        public virtual U Item2 { get; }

        public virtual V Item3 { get; }

        public virtual W Item4 { get; }
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
