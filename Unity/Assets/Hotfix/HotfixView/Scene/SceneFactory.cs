using ET;

namespace ETHotfix
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> CreateZoneScene(int zone, string name)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateId(), zone, SceneType.Zone, name, Game.Scene);
            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent>();
            zoneScene.AddComponent<UnitComponent>();
            //TODO Fill while Config Module has fixed
            zoneScene.AddComponent<AIComponent, int>(1);
            
            // UI层的初始化
            await Game.EventSystem.Publish(new HotfixEventType.AfterCreateZoneScene() {ZoneScene = zoneScene});
            return zoneScene;
        }
    }
}