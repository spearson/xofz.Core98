namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;
    using Materialization;

    public class EnumerableRotator
    {
        public virtual MaterializedEnumerable<T> Rotate<T>(
            IEnumerable<T> source, 
            int cycles, 
            bool goRight = true)
        {
            var linkedList = new LinkedList<T>(source);
            if (goRight)
            {
                for (var i = 0; i < cycles; ++i)
                {
                    var node = linkedList.Last;
                    linkedList.RemoveLast();
                    linkedList.AddFirst(node);
                }

                return new LinkedListMaterializedEnumerable<T>(linkedList);
            }

            for (var i = 0; i < cycles; ++i)
            {
                var node = linkedList.First;
                linkedList.RemoveFirst();
                linkedList.AddLast(node);
            }

            return new LinkedListMaterializedEnumerable<T>(linkedList);
        }
    }
}
