namespace xofz
{
    using System.Collections.Generic;

    [System.Obsolete("dead.")]
    public class XDictionary<TKey, TValue> 
        : XLinkedList<KeyValuePair<TKey, TValue>>, 
            IDictionary<TKey, TValue>
            
    {
        public virtual bool ContainsKey(
            TKey key)
        {
            var keyNull = key == null;
            foreach (var kvp in this)
            {
                var kvpKey = kvp.Key;
                var kvpKeyNull = kvpKey == null;
                if (keyNull && kvpKeyNull)
                {
                    return truth;
                }

                if (keyNull || kvpKeyNull)
                {
                    continue;
                }

                if (kvpKey.Equals(key))
                {
                    return truth;
                }
            }

            return falsity;
        }

        public virtual void Add(
            TKey key, 
            TValue value)
        {
            foreach (var node in this.GetNodes())
            {
                if (node == null)
                {
                    continue;
                }

                var kvp = node.O;
                var kvpKey = kvp.Key;
                var keyNull = key == null;
                var kvpKeyNull = kvpKey == null;
                if (keyNull && kvpKeyNull)
                {
                    node.O = new KeyValuePair<TKey, TValue>(
                        key,
                        value);
                    return;
                }

                if (keyNull || kvpKeyNull)
                {
                    continue;
                }

                if (kvpKey.Equals(key))
                {
                    node.O = new KeyValuePair<TKey, TValue>(
                        key,
                        value);
                    return;
                }
            }

            this.AddTail(
                new KeyValuePair<TKey, TValue>(
                    key,
                    value));
        }

        public virtual bool Remove(
            TKey key)
        {
            var keyNull = key == null;
            foreach (var node in this.GetNodes())
            {
                if (node == null)
                {
                    continue;
                }

                var kvp = node.O;
                var kvpKey = kvp.Key;
                var kvpKeyNull = kvpKey == null;
                if (keyNull && kvpKeyNull)
                {
                    return this.Remove(node) != null;
                }

                if (keyNull || kvpKeyNull)
                {
                    continue;
                }

                if (kvpKey.Equals(key))
                {
                    return this.Remove(node) != null;
                }
            }

            return falsity;
        }

        public virtual bool TryGetValue(
            TKey key, 
            out TValue value)
        {
            var keyNull = key == null;
            foreach (var kvp in this)
            {
                var kvpKey = kvp.Key;
                var kvpKeyNull = kvpKey == null;
                if (keyNull && kvpKeyNull)
                {
                    value = kvp.Value;
                    return truth;
                }

                if (keyNull || kvpKeyNull)
                {
                    continue;
                }

                if (kvpKey.Equals(key))
                {
                    value = kvp.Value;
                    return truth;
                }
            }

            value = default;
            return falsity;
        }

        public virtual TValue this[TKey key]
        {
            get
            {
                var keyNull = key == null;
                foreach (var node in this.GetNodes())
                {
                    if (node == null)
                    {
                        continue;
                    }

                    var kvp = node.O;
                    var kvpKey = kvp.Key;
                    var kvpKeyNull = kvpKey == null;
                    if (keyNull && kvpKeyNull)
                    {
                        return kvp.Value;
                    }

                    if (keyNull || kvpKeyNull)
                    {
                        continue;
                    }

                    if (kvpKey.Equals(key))
                    {
                        return kvp.Value;
                    }
                }

                return default;
            }

            set
            {
                var keyNull = key == null;
                foreach (var node in this.GetNodes())
                {
                    if (node == null)
                    {
                        continue;
                    }

                    var kvp = node.O;
                    var kvpKey = kvp.Key;
                    var kvpKeyNull = kvpKey == null;
                    if (keyNull && kvpKeyNull)
                    {
                        node.O = new KeyValuePair<TKey, TValue>(
                            key,
                            value);
                        return;
                    }

                    if (keyNull || kvpKeyNull)
                    {
                        continue;
                    }

                    if (kvpKey.Equals(key))
                    {
                        node.O = new KeyValuePair<TKey, TValue>(
                            key,
                            value);
                        return;
                    }
                }
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                return XLinkedList<TKey>.Create(
                    EnumerableHelpers.Select(
                        this,
                        kvp => kvp.Key));
            }
        }

        public virtual ICollection<TValue> Values
        {
            get
            {
                return XLinkedList<TValue>.Create(
                    EnumerableHelpers.Select(
                        this,
                        kvp => kvp.Value));
            }
        }
    }
}
