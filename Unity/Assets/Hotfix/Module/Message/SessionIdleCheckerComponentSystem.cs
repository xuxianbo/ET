using ET;

namespace ETHotfix
{
    [ObjectSystem]
    public class SessionIdleCheckerComponentAwakeSystem: AwakeSystem<SessionIdleCheckerComponent, int>
    {
        public override void Awake(SessionIdleCheckerComponent self, int checkInteral)
        {
            self.RepeatedTimer = TimerComponent.Instance.NewRepeatedTimer(checkInteral, self.Check);
        }
    }

    [ObjectSystem]
    public class SessionIdleCheckerComponentDestroySystem: DestroySystem<SessionIdleCheckerComponent>
    {
        public override void Destroy(SessionIdleCheckerComponent self)
        {
            TimerComponent.Instance.Remove(ref self.RepeatedTimer);
        }
    }
}