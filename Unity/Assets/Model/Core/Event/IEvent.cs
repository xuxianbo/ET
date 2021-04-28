using System;

namespace ET
{
    public interface IEvent
    {
        Type GetEventType();
    }

    public class EventProxy: IEvent
    {
        private Func<object, ETTask> m_Action;
        private Type m_EventType;

        public EventProxy(Type eventType, Func<object, ETTask> action)
        {
            this.m_EventType = eventType;
            this.m_Action = action;
        }

        public Type GetEventType()
        {
            return m_EventType;
        }

        public async ETTask Handle<T>(T actionParam)
        {
            try
            {
                await m_Action.Invoke(actionParam);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }

    [Event]
    public abstract class AEvent<A>: IEvent where A : struct
    {
        public Type GetEventType()
        {
            return typeof (A);
        }

        protected abstract ETTask Run(A a);

        public async ETTask Handle(A a)
        {
            try
            {
                await Run(a);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}