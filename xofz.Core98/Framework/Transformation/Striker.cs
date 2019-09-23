namespace xofz.Framework.Transformation
{
    using System;

    public class Striker<T, Y>
    {
        public Striker(
            Translator<T, Y> translator,
            Gen<T> tFactory)
        {
            this.translator = translator;
            this.tFactory = tFactory;
        }

        public virtual Y Strike(
            Action<T> tAction, 
            Do<T, Y> transform,
            Action<Y> yAction)
        {
            var tf = this.tFactory;
            T t;
            if (tf == null)
            {
                t = default;
                goto actT;
            }

            t = tf();

            actT:
            tAction?.Invoke(t);

            Y y;
            var tr = this.translator;
            if (tr == null)
            {
                y = default;
                goto actY;
            }

            y = tr.Translate(
                t, 
                transform);

            actY:
            yAction?.Invoke(y);

            return y;
        }

        protected readonly Translator<T, Y> translator;
        protected readonly Gen<T> tFactory;
    }
}
