namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class Effluviant
    {
        public virtual IEnumerable<T> Coalesce<T>(
            Effluvia effluvia,
            Gen<T> generate,
            IEnumerable<bool> decisions)
        {
            if (effluvia == null || decisions == null)
            {
                yield break;
            }

            var e = decisions.GetEnumerator();
            foreach (var item in effluvia.Generate(
                         generate))
            {
                if (e.MoveNext())
                {
                    if (e.Current)
                    {
                        yield return item;
                    }

                    continue;
                }

                break;
            }

            e.Dispose();
        }
    }
}