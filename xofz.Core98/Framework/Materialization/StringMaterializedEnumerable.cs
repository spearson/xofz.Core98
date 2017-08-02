namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;

    public sealed class StringMaterializedEnumerable : MaterializedEnumerable<char>
    {
        public StringMaterializedEnumerable(string s)
        {
            this.s = s;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return this.s.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public long Count => this.s.Length;

        public void CopyTo(char[] array)
        {
            this.s.CopyTo(0, array, 0, (int)this.Count);
        }

        public bool Contains(char item)
        {
            return this.s.Contains(new string(item, 1));
        }

        private readonly string s;
    }
}
