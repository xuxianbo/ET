using UnityEngine;

namespace ET.Client
{
    public static class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        unit.AddComponent<NumericComponent>();
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
