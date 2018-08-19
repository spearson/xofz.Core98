namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class EnumerableRotator
    {
        public virtual ICollection<T> Rotate<T>(
            IEnumerable<T> source, 
            int cycles, 
            bool goRight = true)
        {
            var ll = new LinkedList<T>();
            if (source == null)
            {
                return ll;
            }
            
            if (goRight)
            {
                for (var i = 0; i < cycles; ++i)
                {
                    var node = ll.Last;
                    ll.RemoveLast();
                    ll.AddFirst(node);
                }

                return ll;
            }

            for (var i = 0; i < cycles; ++i)
            {
                var node = ll.First;
                ll.RemoveFirst();
                ll.AddLast(node);
            }

            return ll;
        }
    }
}
