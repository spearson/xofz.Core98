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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }
            
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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

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
            if (handler == null)
            {
                return;
            }

            publisher
                ?.GetType()
                ?.GetEvent(eventName)
                ?.RemoveEventHandler(
                    publisher,
                    handler);
        }
    }
}
