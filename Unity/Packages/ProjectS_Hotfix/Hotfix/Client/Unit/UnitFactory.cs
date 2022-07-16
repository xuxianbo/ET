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

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.SetValueWithoutBroadCast(NumericType.Speed, 700);
            numericComponent.SetValueWithoutBroadCast(NumericType.SpeedBase, 700);

            unit.AddComponent<MoveComponent>();
            unit.AddComponent<StackFsmComponent>();

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