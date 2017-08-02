namespace xofz.Framework.Transformation
{
    using System;
    using System.Collections.Generic;

    public class Translator<T, Y>
    {
        public Translator(Func<Y> yFactory)
        {
            this.yFactory = yFactory;
        }

        public virtual Y Translate(T source, Action<T, Y> transform)
        {
            Y y;
            var s = this.source;
            if (s == null)
            {
                y = this.yFactory();
            }
            else
            {
                if (!s.MoveNext())
                {
                    this.setSource(null);
                    y = this.yFactory();
                }
                else
                {
                    y = s.Current;
                }
            }

            transform(source, y);
            return y;
        }

        public virtual void ApplySource(IEnumerable<Y> source)
        {
            this.setSource(
                source.GetEnumerator());
        }

        private void setSource(IEnumerator<Y> source)
        {
            this.source = source;
        }

        private IEnumerator<Y> source;
        private readonly Func<Y> yFactory;
    }
}
