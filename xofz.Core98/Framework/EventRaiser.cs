namespace xofz.Framework
{
    using System;
    using System.Reflection;

    public class EventRaiser
    {
        public virtual void Raise(object eventHolder, string eventName, params object[] args)
        {
            ((Delegate)eventHolder
                .GetType()
                .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(eventHolder))?
                .DynamicInvoke(args);
        }
    }
}
