using System;
using ET;
using UnityEngine;

namespace ETHotfix
{
    [Event]
    public class AppStart_Init: AEvent<ET.EventType.AppStart>
    {
        protected override async ETTask Run(ET.EventType.AppStart args)
        {
            Debug.Log($"Hotfix receive a event from Model!---------------{args.i}");
            ResourcesComponent.Instance.LoadBundle("config.unity3d");
            Game.Scene.AddComponent<ConfigComponent>();
            ConfigComponent.GetAllConfigBytes = LoadConfigHelper.LoadAllConfigBytes;
            await ConfigComponent.Instance.LoadAsync();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");

            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            Game.Scene.AddComponent<NetThreadComponent>();
            Game.Scene.AddComponent<ZoneSceneManagerComponent>();
            Game.Scene.AddComponent<AIDispatcherComponent>();

            ResourcesComponent.Instance.LoadBundle("unit.unity3d");

            Scene zoneScene = await SceneFactory.CreateZoneScene(1, "Hotfix Zone [1]");

            await Game.EventSystem.Publish(new HotfixEventType.AppStartInitFinish() { ZoneScene = zoneScene });
        }

        public override Func<object, ETTask> GetEventTask()
        {
            return (eventParam) => { return Game.EventSystem.Publish((ET.EventType.AppStart)eventParam); };
        }
    }
}