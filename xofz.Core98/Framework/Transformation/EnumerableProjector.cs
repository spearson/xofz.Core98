namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableProjector
    {
        public virtual T[][] Project2<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[zero][];
            }

            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(System.Math.Sqrt(queue.Count) + one);
            var jagged2 = new T[magnitude][];
            for (int i = zero; i < magnitude; ++i)
            {
                jagged2[i] = new T[magnitude];
            }

            int counter1 = zero, counter2 = zero;
            T item;
            while (queue.Count > zero)
            {
                item = queue.Dequeue();
                jagged2[counter1][counter2] = item;
                ++counter2;
                if (counter2 < jagged2[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = zero;
            }

            return jagged2;
        }

        public virtual T[][][] Project3<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[zero][][];
            }

            const byte three = 3;
            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(System.Math.Pow(queue.Count, one / (double)three) + one);
            var jagged3 = new T[magnitude][][];
            for (int i = zero; i < magnitude; ++i)
            {
                jagged3[i] = new T[magnitude][];
                for (int j = zero; j < magnitude; ++j)
                {
                    jagged3[i][j] = new T[magnitude];
                }
            }

            int counter1 = zero, counter2 = zero, counter3 = zero;
            T item;
            while (queue.Count > zero)
            {
                item = queue.Dequeue();
                jagged3[counter1][counter2][counter3] = item;
                ++counter3;
                if (counter3 < jagged3[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = zero;

                if (counter2 < jagged3[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = zero;
            }

            return jagged3;
        }

        public virtual T[][][][] Project4<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[zero][][][];
            }

            const byte four = 4;
            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(System.Math.Pow(queue.Count, one / (double)four) + one);
            var jagged4 = new T[magnitude][][][];
            for (int i = zero; i < magnitude; ++i)
            {
                jagged4[i] = new T[magnitude][][];
                for (int j = zero; j < magnitude; ++j)
                {
                    jagged4[i][j] = new T[magnitude][];
                    for (int k = zero; k < magnitude; ++k)
                    {
                        jagged4[i][j][k] = new T[magnitude];
                    }
                }
            }

            int counter1 = zero,
                counter2 = zero,
                counter3 = zero,
                counter4 = zero;
            T item;
            while (queue.Count > zero)
            {
                item = queue.Dequeue();
                jagged4[counter1][counter2][counter3][counter4] = item;
                ++counter4;
                if (counter4 < jagged4[counter1][counter2][counter3].Length)
                {
                    continue;
                }

                ++counter3;
                counter4 = zero;

                if (counter3 < jagged4[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = zero;

                if (counter2 < jagged4[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = zero;
            }

            return jagged4;
        }

        protected const byte
            zero = 0,
            one = 1;
    }
}