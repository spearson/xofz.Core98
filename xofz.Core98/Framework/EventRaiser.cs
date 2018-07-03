namespace xofz.Framework
{
    using System;
    using System.Reflection;

    public class EventRaiser
    {
        public virtual void Raise(
            object eventHolder,
            string eventName,
            params object[] args)
        {
            var d = (Delegate)eventHolder
                        ?.GetType()
                        .GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic)
                        ?.GetValue(eventHolder);
            if (d != default(Delegate))
            {
                d.DynamicInvoke(args);
                return;
            }
            
            this.internalRaiseBase(
                eventHolder,
                eventName,
                0xFF,
                args);
        }

        private void internalRaiseBase(
            object eventHolder,
            string eventName,
            int depth,
            params object[] args)
        {
            if (depth < 1)
            {
                return;
            }

            if (eventHolder == null)
            {
                return;
            }

            Type baseType = null;
            var holderType = eventHolder.GetType();
            for (var i = 0; i < depth; ++i)
            {
                baseType = baseType?.BaseType ?? holderType.BaseType;
                var d = (Delegate)baseType
                    ?.GetField(
                        eventName,
                        BindingFlags.Instance |
                        BindingFlags.NonPublic)
                    ?.GetValue(eventHolder);
                if (d != default(Delegate))
                {
                    d.DynamicInvoke(args);
                    return;
                }
            }
        }
    }
}
