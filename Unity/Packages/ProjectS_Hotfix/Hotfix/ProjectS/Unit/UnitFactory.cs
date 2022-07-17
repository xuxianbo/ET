using UnityEngine;

namespace ET.Client
{
    public static class UnitFactory
    {
        public static Unit CreatePlayerHero(Scene currentScene, UnitInfo unitInfo)
        {
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
            unitComponent.Add(unit);

            unit.AddComponent<NumericComponent>();
            unit.AddComponent<MoveComponent>();
            unit.AddComponent<StackFsmComponent>();

            unit.AddComponent<NP_SyncComponent>();
            unit.AddComponent<NP_RuntimeTreeManager>();
            
            //增加Buff管理组件
            unit.AddComponent<BuffManagerComponent>();
            unit.AddComponent<SkillCanvasManagerComponent>();
            unit.AddComponent<B2S_RoleCastComponent, RoleCamp, RoleTag>(unitInfo.RoleCamp, RoleTag.Hero);
            
            unit.AddComponent<CommonAttackComponent_Logic>();
            unit.AddComponent<CastDamageComponent>();
            unit.AddComponent<ReceiveDamageComponent>();

            unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
            unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);

            Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate_Logic()
            {
                UnitConfigId = unitInfo.ConfigId,
                UnitName = "还是我的Darius"
            });

            return unit;
        }
    }
}