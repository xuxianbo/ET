// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月17日, 星期日
// --------------------------

using System.Collections.Generic;
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

            self.MonoBridge.OnTriggerEnter_Callback = self.OnTriggerEnter;
            self.MonoBridge.OnTriggerStay_Callback = self.OnTriggerStay;
            self.MonoBridge.OnTriggerExit_Callback = self.OnTriggerExit;

            self.TargetNP_RuntimeTreeManager = self.GetParent<Unit>().GetComponent<NP_RuntimeTreeManager>();

            if (self.TargetNP_RuntimeTreeManager == null)
            {
                self.TargetNP_RuntimeTreeManager =
                    self.CreateColliderArgs.BelontToUnit.GetComponent<NP_RuntimeTreeManager>();
            }

            if (self.TargetNP_RuntimeTreeManager == null)
            {
                Log.Error("既没在碰撞体Unit身上找到NP_RuntimeTreeManager，也没在监护者BelongToUnit身上找到NP_RuntimeTreeManager，逻辑有误");
                return;
            }
        }
    }

    public class B2S_ColliderComponent_System_DestroySystem0 : DestroySystem<B2S_ColliderComponent>
    {
        public override void Destroy(B2S_ColliderComponent self)
        {
            self.MonoBridge.OnTriggerEnter_Callback = null;
            self.MonoBridge.OnTriggerExit_Callback = null;
            self.MonoBridge.OnTriggerStay_Callback = null;

            self.CreateColliderArgs = null;
            ReferencePool.Release(self.CreateColliderArgs);
        }
    }


    public static class B2S_ColliderComponent_System_Utilities
    {
        public static void OnTriggerEnter(this B2S_ColliderComponent self, MonoBridge other)
        {
            if (self.TargetNP_RuntimeTreeManager == null)
            {
                Log.Error("既没在碰撞体Unit身上找到NP_RuntimeTreeManager，也没在监护者BelongToUnit身上找到NP_RuntimeTreeManager，逻辑有误");
                return;
            }

            Unit collisionUnit = self.DomainScene().GetComponent<UnitComponent>().Get(other.BelongToUnitId);
            B2S_RoleCastComponent b2SRoleCastComponent = collisionUnit.GetComponent<B2S_RoleCastComponent>();

            if ((b2SRoleCastComponent.RoleCamp | self.CreateColliderArgs.TargetCollsionRoleCamp) ==
                b2SRoleCastComponent.RoleCamp &&
                (b2SRoleCastComponent.RoleTag | self.CreateColliderArgs.TargetCollsionRoleTag) ==
                b2SRoleCastComponent.RoleTag &&
                b2SRoleCastComponent.GetRoleCastToTarget(self.GetParent<Unit>()) ==
                self.CreateColliderArgs.TargetCollsionRoleCast) ;
            {
                foreach (var runtimeTree in self.TargetNP_RuntimeTreeManager.RuntimeTrees)
                {
                    runtimeTree.Value.GetBlackboard().Get<List<long>>(self.CreateColliderArgs.OnTriggerEnter)
                        ?.Add(other.BelongToUnitId);
                }
            }
        }

        public static void OnTriggerStay(this B2S_ColliderComponent self, MonoBridge other)
        {
            if (self.TargetNP_RuntimeTreeManager == null)
            {
                Log.Error("既没在碰撞体Unit身上找到NP_RuntimeTreeManager，也没在监护者BelongToUnit身上找到NP_RuntimeTreeManager，逻辑有误");
                return;
            }
            
            Unit collisionUnit = self.DomainScene().GetComponent<UnitComponent>().Get(other.BelongToUnitId);
            B2S_RoleCastComponent b2SRoleCastComponent = collisionUnit.GetComponent<B2S_RoleCastComponent>();

            if ((b2SRoleCastComponent.RoleCamp | self.CreateColliderArgs.TargetCollsionRoleCamp) ==
                b2SRoleCastComponent.RoleCamp &&
                (b2SRoleCastComponent.RoleTag | self.CreateColliderArgs.TargetCollsionRoleTag) ==
                b2SRoleCastComponent.RoleTag &&
                b2SRoleCastComponent.GetRoleCastToTarget(self.GetParent<Unit>()) ==
                self.CreateColliderArgs.TargetCollsionRoleCast) ;
            {
                foreach (var runtimeTree in self.TargetNP_RuntimeTreeManager.RuntimeTrees)
                {
                    runtimeTree.Value.GetBlackboard().Get<List<long>>(self.CreateColliderArgs.OnTriggerStay)
                        ?.Add(other.BelongToUnitId);
                }
            }
        }

        public static void OnTriggerExit(this B2S_ColliderComponent self, MonoBridge other)
        {
            if (self.TargetNP_RuntimeTreeManager == null)
            {
                Log.Error("既没在碰撞体Unit身上找到NP_RuntimeTreeManager，也没在监护者BelongToUnit身上找到NP_RuntimeTreeManager，逻辑有误");
                return;
            }
            
            Unit collisionUnit = self.DomainScene().GetComponent<UnitComponent>().Get(other.BelongToUnitId);
            B2S_RoleCastComponent b2SRoleCastComponent = collisionUnit.GetComponent<B2S_RoleCastComponent>();

            if ((b2SRoleCastComponent.RoleCamp | self.CreateColliderArgs.TargetCollsionRoleCamp) ==
                b2SRoleCastComponent.RoleCamp &&
                (b2SRoleCastComponent.RoleTag | self.CreateColliderArgs.TargetCollsionRoleTag) ==
                b2SRoleCastComponent.RoleTag &&
                b2SRoleCastComponent.GetRoleCastToTarget(self.GetParent<Unit>()) ==
                self.CreateColliderArgs.TargetCollsionRoleCast) ;
            {
                foreach (var runtimeTree in self.TargetNP_RuntimeTreeManager.RuntimeTrees)
                {
                    runtimeTree.Value.GetBlackboard().Get<List<long>>(self.CreateColliderArgs.OnTriggerExit)
                        ?.Add(other.BelongToUnitId);
                }
            }
        }
    }
}