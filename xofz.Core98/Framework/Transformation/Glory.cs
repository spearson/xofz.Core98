namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class Glory<T>
    {
        public virtual T O { get; set; }

        public virtual IEnumerable<T> Glorify(
            Do<T> act)
        {
            const bool truth = true;
            if (act == null)
            {
                while (truth)
                {
                    yield return this.O;
                }
            }
            
            while (truth)
            {
                var o = this.O;
                act(o);
                yield return o;
            }
        }
    }
}
