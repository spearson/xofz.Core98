namespace xofz.Framework
{
    using System;

    public class EventSubscriber
    {
        public virtual void Subscribe(
            object publisher,
            string eventName,
            Delegate handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe(
            object publisher,
            string eventName,
            EventHandler handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<TEventArgs>(
            object publisher,
            string eventName,
            EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe(
            object publisher,
            string eventName,
            Do handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T>(
            object publisher,
            string eventName,
            Do<T> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U>(
            object publisher,
            string eventName,
            Do<T, U> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V>(
            object publisher,
            string eventName,
            Do<T, U, V> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Subscribe<T, U, V, W>(
            object publisher,
            string eventName,
            Do<T, U, V, W> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.AddEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            Delegate handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            EventHandler handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<TEventArgs>(
            object publisher,
            string eventName,
            EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe(
            object publisher,
            string eventName,
            Do handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T>(
            object publisher,
            string eventName,
            Do<T> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U>(
            object publisher,
            string eventName,
            Do<T, U> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V>(
            object publisher,
            string eventName,
            Do<T, U, V> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }

        public virtual void Unsubscribe<T, U, V, W>(
            object publisher,
            string eventName,
            Do<T, U, V, W> handler)
        {
            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }
    }
}
