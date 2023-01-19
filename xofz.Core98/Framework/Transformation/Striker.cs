namespace xofz.Framework.Transformation
{
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
            System.Action<T> tAction, 
            Do<T, Y> transform,
            System.Action<Y> yAction)
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
