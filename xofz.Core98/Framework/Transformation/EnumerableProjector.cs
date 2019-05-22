namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class EnumerableProjector
    {
        public virtual T[][] Project2<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[0][];
            }

            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(Math.Sqrt(queue.Count) + 1);
            var jagged2 = new T[magnitude][];
            for (var i = 0; i < magnitude; ++i)
            {
                jagged2[i] = new T[magnitude];
            }

            int counter1 = 0, counter2 = 0;
            T item;
            while (queue.Count > 0)
            {
                item = queue.Dequeue();
                jagged2[counter1][counter2] = item;
                ++counter2;
                if (counter2 != jagged2[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return jagged2;
        }

        public virtual T[][][] Project3<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[0][][];
            }

            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(Math.Pow(queue.Count, 1 / (double)3) + 1);
            var jagged3 = new T[magnitude][][];
            for (var i = 0; i < magnitude; ++i)
            {
                jagged3[i] = new T[magnitude][];
                for (var j = 0; j < magnitude; ++j)
                {
                    jagged3[i][j] = new T[magnitude];
                }
            }

            int counter1 = 0, counter2 = 0, counter3 = 0;
            T item;
            while (queue.Count > 0)
            {
                item = queue.Dequeue();
                jagged3[counter1][counter2][counter3] = item;
                ++counter3;
                if (counter3 != jagged3[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = 0;

                if (counter2 != jagged3[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return jagged3;
        }

        public virtual T[][][][] Project4<T>(
            IEnumerable<T> finiteSource)
        {
            if (finiteSource == null)
            {
                return new T[0][][][];
            }

            var queue = new Queue<T>(finiteSource);
            var magnitude = (int)(Math.Pow(queue.Count, 1 / (double)4) + 1);
            var jagged4 = new T[magnitude][][][];
            for (var i = 0; i < magnitude; ++i)
            {
                jagged4[i] = new T[magnitude][][];
                for (var j = 0; j < magnitude; ++j)
                {
                    jagged4[i][j] = new T[magnitude][];
                    for (var k = 0; k < magnitude; ++k)
                    {
                        jagged4[i][j][k] = new T[magnitude];
                    }
                }
            }

            int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0;
            T item;
            while (queue.Count > 0)
            {
                item = queue.Dequeue();
                jagged4[counter1][counter2][counter3][counter4] = item;
                ++counter4;
                if (counter4 != jagged4[counter1][counter2][counter3].Length)
                {
                    continue;
                }

                ++counter3;
                counter4 = 0;

                if (counter3 != jagged4[counter1][counter2].Length)
                {
                    continue;
                }

                ++counter2;
                counter3 = 0;

                if (counter2 != jagged4[counter1].Length)
                {
                    continue;
                }

                ++counter1;
                counter2 = 0;
            }

            return jagged4;
        }
    }
}
