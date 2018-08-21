namespace xofz.Framework
{
    public class EventSubscriber
    {
        public virtual void Subscribe(
            object publisher,
            string eventName,
            System.Delegate handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe(
            object publisher,
            string eventName,
            System.EventHandler handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<TEventArgs>(
            object publisher,
            string eventName,
            System.EventHandler<TEventArgs> handler)
            where TEventArgs : System.EventArgs
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe(
            object publisher,
            string eventName,
            Action handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T>(
            object publisher,
            string eventName,
            Action<T> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U>(
            object publisher,
            string eventName,
            Action<T, U> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V>(
            object publisher,
            string eventName,
            Action<T, U, V> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            System.Delegate handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            System.EventHandler handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<TEventArgs>(
            object publisher,
            string eventName,
            System.EventHandler<TEventArgs> handler)
            where TEventArgs : System.EventArgs
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            Action handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T>(
            object publisher,
            string eventName,
            Action<T> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U>(
            object publisher,
            string eventName,
            Action<T, U> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V>(
            object publisher,
            string eventName,
            Action<T, U, V> handler)
        {
            publisher
                ?.GetType()
                .GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }
    }
}
