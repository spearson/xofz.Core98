namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class Effluvia
    {
        public virtual IEnumerable<T> Generate<T>(
            Gen<T> instantiate)
        {
            if (instantiate == null)
            {
                yield break;
            }

            while (truth)
            {
                yield return instantiate();
            }
        }

        public virtual IEnumerable<T> Generate<T>(
            Gen<T> instantiate,
            Gen<T, bool> decider)
        {
            if (decider == null)
            {
                yield break;
            }

            foreach (var item in this.Generate(
                         instantiate))
            {
                if (decider(item))
                {
                    yield return item;
                }
            }
        }

        public virtual IEnumerable<T> Generate<T>(
            Gen<T> instantiate,
            Gen<T, bool> decider,
            Do<T> prepare)
        {
            foreach (var item in this.Generate(
                         instantiate, 
                         decider))
            {
                prepare?.Invoke(item);
                yield return item;
            }
        }

        protected const bool truth = true;
    }
}
