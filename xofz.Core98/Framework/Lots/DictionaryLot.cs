namespace xofz.Framework.Lots
{
    using System.Collections;
    using System.Collections.Generic;

    public class DictionaryLot<TKey, TValue>
        : Lot<KeyValuePair<TKey, TValue>>
    {
        public DictionaryLot()
            : this(new Dictionary<TKey, TValue>())
        {
        }

        public DictionaryLot(
            IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<TKey, TValue>();
            }

            this.dictionary = dictionary;
        }

        public virtual long Count => this.dictionary.Count;

        public virtual TValue this[TKey key] => this.dictionary[key];

        public virtual ICollection<TKey> Keys => this.dictionary.Keys;

        public virtual ICollection<TValue> Values => this.dictionary.Values;

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool ContainsKey(
            TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public virtual bool TryGetValue(
            TKey key,
            out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public virtual bool Remove(
            TKey key)
        {
            return this.dictionary.Remove(key);
        }

        public virtual void Add(
            TKey key,
            TValue value)
        {
            this.dictionary.Add(key, value);
        }

        protected readonly IDictionary<TKey, TValue> dictionary;
    }
}
