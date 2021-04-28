using System;
using ET;

namespace ETHotfix
{
    public interface IEvent
    {
        Type GetEventType();

        Func<object, ETTask> GetEventTask();
    }

    [Event]
    public abstract class AEvent<A>: IEvent
    {
        public Type GetEventType()
        {
            return typeof (A);
        }

        public virtual Func<object, ETTask> GetEventTask()
        {
            throw new NotImplementedException();
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