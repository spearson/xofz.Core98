namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class Translator<T, Y> : IDisposable
    {
        public Translator(Gen<Y> yFactory)
        {
            this.yFactory = yFactory;
        }

        public virtual Y Translate(
            T item, 
            Do<T, Y> transform)
        {
            Y y;
            var s = this.source;
            if (s == null)
            {
                y = this.yFactory();
                goto translate;
            }

            if (!s.MoveNext())
            {
                this.setSource(null);
                y = this.yFactory();
                goto translate;
            }

            y = s.Current;

            translate:
            transform(item, y);
            return y;
        }

        public virtual void ApplySource(IEnumerable<Y> source)
        {
            this.setSource(
                source.GetEnumerator());
        }

        public void Dispose()
        {
            this.source?.Dispose();
        }

        private void setSource(IEnumerator<Y> source)
        {
            this.source = source;
        }

        private IEnumerator<Y> source;
        private readonly Gen<Y> yFactory;
    }
}
