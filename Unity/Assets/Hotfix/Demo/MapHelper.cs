using System;
using ET;
using UnityEngine;

namespace ETHotfix
{
    public static class MapHelper
    {
        public static async ETVoid EnterMapAsync(Scene zoneScene, string sceneName)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMap()) as G2C_EnterMap;

                UnitComponent unitComponent = zoneScene.GetComponent<UnitComponent>();
                unitComponent.MyUnit = unitComponent.Get(g2CEnterMap.UnitId);
                Game.EventSystem.Publish(new HotfixEventType.EnterMapFinish() {ZoneScene = zoneScene}).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}