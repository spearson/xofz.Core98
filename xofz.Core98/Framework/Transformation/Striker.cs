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

        public Y Strike(
            Action<T> tAction, 
            Do<T, Y> transform,
            Action<Y> yAction)
        {
            var t = this.tFactory();
            tAction(t);

            var y = this.translator.Translate(t, transform);
            yAction(y);

            return y;
        }

        private readonly Translator<T, Y> translator;
        private readonly Gen<T> tFactory;
    }
}
