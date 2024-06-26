﻿namespace xofz.Framework.Transformation
{
    using System.Collections.Generic;

    public class Translator<T, Y>
        : System.IDisposable
    {
        public Translator(
            Gen<Y> yFactory)
        {
            this.yFactory = yFactory;
        }

        public virtual Y Translate(
            T item,
            Do<T, Y> transform)
        {
            Y y;
            var s = this.sourceEnumerator;
            if (s == null)
            {
                y = this.yFactory();
                goto translate;
            }

            if (!s.MoveNext())
            {
                this.Dispose();
                y = this.yFactory();
                goto translate;
            }

            y = s.Current;

            translate:
            transform(item, y);
            return y;
        }

        public virtual void ApplySource(
            IEnumerable<Y> source)
        {
            if (source == null)
            {
                this.setSourceEnumerator(
                    EnumerableHelpers
                        .Empty<Y>()
                        .GetEnumerator());
                return;
            }

            this.setSourceEnumerator(
                source.GetEnumerator());
        }

        public virtual void Dispose()
        {
            this.sourceEnumerator?.Dispose();

            this.setSourceEnumerator(null);
        }

        protected virtual void setSourceEnumerator(
            IEnumerator<Y> sourceEnumerator)
        {
            this.sourceEnumerator = sourceEnumerator;
        }

        protected IEnumerator<Y> sourceEnumerator;
        protected readonly Gen<Y> yFactory;
    }
}