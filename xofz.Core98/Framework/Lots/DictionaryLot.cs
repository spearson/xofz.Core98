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
            IEnumerable<KeyValuePair<TKey, TValue>> finiteSource)
        {
            if (finiteSource == null)
            {
                this.dictionary = new Dictionary<TKey, TValue>();
                return;
            }

            if (finiteSource is IDictionary<TKey, TValue> d)
            {
                this.dictionary = d;
                return;
            }

            if (finiteSource is DictionaryLot<TKey, TValue> lot)
            {
                this.dictionary = lot.dictionary
                                  ?? new Dictionary<TKey, TValue>();
                return;
            }

            var dict = new Dictionary<TKey, TValue>();
            foreach (var item in finiteSource)
            {
                dict.Add(
                    item.Key,
                    item.Value);
            }

            this.dictionary = dict;
        }

        public DictionaryLot(
            IEnumerator<KeyValuePair<TKey, TValue>> finiteEnumerator)
        {
            var d = new Dictionary<TKey, TValue>();

            if (finiteEnumerator == null)
            {
                this.dictionary = d;
                return;
            }

            while (finiteEnumerator.MoveNext())
            {
                var c = finiteEnumerator.Current;
                d?.Add(
                    c.Key,
                    c.Value);
            }

            this.dictionary = d;
        }

        public virtual long Count => this.dictionary?.Count ?? nOne;

        public virtual TValue this[TKey key]
        {
            get
            {
                var d = this.dictionary;
                if (d == null)
                {
                    return default;
                }

                return d[key];
            }
        }

        public virtual ICollection<TKey> Keys => 
            this.dictionary?.Keys ?? new XLinkedList<TKey>();

        public virtual ICollection<TValue> Values => 
            this.dictionary?.Values ?? new XLinkedList<TValue>();

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.dictionary?.GetEnumerator()
                   ?? EnumerableHelpers
                       .Empty<KeyValuePair<TKey, TValue>>()
                       .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual bool ContainsKey(
            TKey key)
        {
            return this.dictionary?.ContainsKey(key)
                ?? falsity;
        }

        public virtual bool TryGetValue(
            TKey key,
            out TValue value)
        {
            var d = this.dictionary;
            if (d == null)
            {
                value = default;
                return falsity;
            }

            return d.TryGetValue(
                key, 
                out value);
        }

        public virtual void Add(
            TKey key,
            TValue value)
        {
            this.dictionary?.Add(
                key,
                value);
        }

        public virtual bool Remove(
            TKey key)
        {
            return this.dictionary?.Remove(key)
                ?? falsity;
        }

        protected readonly IDictionary<TKey, TValue> dictionary;
        protected const short nOne = -1;
        protected const bool falsity = false;
    }
}
