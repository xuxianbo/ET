// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月17日, 星期日
// --------------------------

using ET.Client;

namespace ET
{
    public class
        B2S_ColliderComponent_System_AwakeSystem0 : AwakeSystem<B2S_ColliderComponent, UnitFactory.CreateColliderArgs>
    {
        public override void Awake(B2S_ColliderComponent self, UnitFactory.CreateColliderArgs a)
        {
            self.MonoBridge = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject
                .GetComponent<MonoBridge>();

            self.MonoBridge.OnCollisionExit_Callback = self.OnCollisionExit;
            self.MonoBridge.OnCollisionStay_Callback = self.OnCollisionStay;
            self.MonoBridge.OnTriggerExit_Callback = self.OnTriggerExit;
            self.MonoBridge.OnTriggerStay_Callback = self.OnCollisionStay;
        }
    }

    public class B2S_ColliderComponent_System_DestroySystem0 : DestroySystem<B2S_ColliderComponent>
    {
        public override void Destroy(B2S_ColliderComponent self)
        {
            self.MonoBridge.OnCollisionExit_Callback = null;
            self.MonoBridge.OnCollisionStay_Callback = null;
            self.MonoBridge.OnTriggerExit_Callback = null;
            self.MonoBridge.OnTriggerStay_Callback = null;
        }
    }


    public static class B2S_ColliderComponent_System_Utilities
    {
        public static void OnCollisionExit(this B2S_ColliderComponent self, MonoBridge other)
        {
            if ( self.GetParent<Unit>().GetComponent<NP_RuntimeTreeManager>() is {} npRuntimeTreeManager)
            {
                foreach (var runtimeTree in npRuntimeTreeManager.RuntimeTrees)
                {
                    //runtimeTree.Value.GetBlackboard().Set();
                }
            }
        }

        public static void OnCollisionStay(this B2S_ColliderComponent self, MonoBridge other)
        {
        }

        public static void OnTriggerExit(this B2S_ColliderComponent self, MonoBridge other)
        {
        }

        public static void OnTriggerStay(this B2S_ColliderComponent self, MonoBridge other)
        {
        }
    }
}