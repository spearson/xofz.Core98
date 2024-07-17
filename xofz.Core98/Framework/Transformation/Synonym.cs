namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class Synonym
    {
        public virtual IEnumerable<K> Nominate<T, K>(
            IEnumerable<T> source,
            Gen<T, K> kFactory)
        {
            if (source == null || kFactory == null)
            {
                yield break;
            }

            foreach (var item in source)
            {
                yield return kFactory(item);
            }
        }
    }
}
