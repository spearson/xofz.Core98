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
            var holderType = eventHolder?.GetType();
            if (holderType == null)
            {
                return;
            }

            const BindingFlags fieldFlags = BindingFlags.Instance
                                            | BindingFlags.NonPublic;
            var @event = (Delegate)holderType
                .GetField(
                    eventName,
                    fieldFlags)
                ?.GetValue(eventHolder);
            if (@event == null)
            {
                Type baseType = null;
                for (byte i = 0; i < 0xFF; ++i)
                {
                    if (baseType == null)
                    {
                        baseType = holderType.BaseType;
                        if (baseType == null)
                        {
                            break;
                        }

                        goto tryGetEvent;
                    }

                    baseType = baseType.BaseType;
                    if (baseType == null)
                    {
                        break;
                    }

                    tryGetEvent:
                    @event = (Delegate)baseType
                        .GetField(eventName,
                            fieldFlags)
                        ?.GetValue(eventHolder);
                    if (@event != null)
                    {
                        goto raise;
                    }
                }

                return;
            }

            raise:
            @event.DynamicInvoke(args);
        }
    }
}