namespace ET
{
    public class DeadComponentAwakeSystem : AwakeSystem<DeadComponent, long>
    {
        public override void Awake(DeadComponent self, long resurrectionTime)
        {
            self.ResurrectionTime = resurrectionTime;

            Unit unit = self.GetParent<Unit>();
            
            // 休眠刚体，不再会产生碰撞
            unit.GetComponent<B2S_ColliderComponent>().Body.gameObject.SetActive(false);
            //
            // self.DeadTimerId = TimerComponent.Instance.NewOnceTimer(TimeHelper.ClientNow() + self.ResurrectionTime,
            //     () => { self.GetParent<Unit>()?.RemoveComponent<DeadComponent>(); });
        }
    }

    public class DeadComponentDestroySystem : DestroySystem<DeadComponent>
    {
        public override void Destroy(DeadComponent self)
        {
            Unit unit = self.GetParent<Unit>();

            if (!(unit.GetComponent<B2S_ColliderComponent>().Body is null))
            {
                unit.GetComponent<B2S_ColliderComponent>().Body.gameObject.SetActive(true);
            }

            TimerComponent.Instance.Remove(ref self.DeadTimerId);
        }
    }
}