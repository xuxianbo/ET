namespace ET
{
    public class AppStart_Init: AEvent<EventType.AppStart>
    {
        protected override async ETTask Run(EventType.AppStart args)
        {
            Log.Info("APP Start回调");
            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();

            Game.Scene.AddComponent<ConfigComponent>();
            await ConfigComponent.Instance.LoadAsync();
            
            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            
            Game.Scene.AddComponent<NetThreadComponent>();
            
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            
            Game.Scene.AddComponent<GlobalComponent>();
            
            Game.Scene.AddComponent<AIDispatcherComponent>();

            Scene zoneScene = await SceneFactory.CreateZoneScene(1, "Game", Game.Scene);
            
            await Game.EventSystem.Publish(new EventType.AppStartInitFinish() { ZoneScene = zoneScene });

        }
    }
}
