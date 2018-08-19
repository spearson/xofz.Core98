namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableSplitter
    {
        public virtual ICollection<T>[] Split<T>(
            IEnumerable<T> source,
            int splits)
        {
            if (source == null)
            {
                return new ICollection<T>[0];
            }

            if (splits < 2)
            {
                return new ICollection<T>[]
                {
                    new LinkedList<T>(source)
                };
            }

            var array = new ICollection<T>[splits];
            for (var i = 0; i < splits; ++i)
            {
                array[i] = new LinkedList<T>();
            }

            var enumerator = source.GetEnumerator();
            if (enumerator == null)
            {
                return new ICollection<T>[0];
            }

            if (enumerator.MoveNext())
            {
                array[0].Add(enumerator.Current);
            }
            
            var zeroFilled = true;
            while (enumerator.MoveNext())
            {
                loop:
                for (var i = 0; i < splits; ++i)
                {
                    if (zeroFilled && i == 0)
                    {
                        continue;
                    }

                    zeroFilled = false;
                    array[i].Add(enumerator.Current);
                    if (i < splits - 1)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                    }
                }

                zeroFilled = false;
                if (enumerator.MoveNext())
                {
                    goto loop;
                }
            }

            enumerator.Dispose();

            return array;
        }
    }
}
