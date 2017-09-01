namespace xofz
{
    public class Beholder<T>
    {
        public virtual void Receive(T state)
        {
            this.currentState = state;
        }

        public virtual void Swap(T newIn, out T previous)
        {
            previous = this.currentState;
            this.currentState = newIn;
        }

        private T currentState;
    }
}
