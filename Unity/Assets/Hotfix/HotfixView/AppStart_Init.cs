using System;
using System.Reflection;
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
            ConfigComponent.Instance.Load();
            ResourcesComponent.Instance.UnloadBundle("config.unity3d");

            // Log.Info("配置表加载完成！");
            //
            // foreach (var configs in ConfigComponent.Instance.AllConfig)
            // {
            //     //object config = configs;
            //     //Log.Info(configs.Value.ToString());
            //     MethodInfo methodInfo = configs.Value.GetType().GetMethod("AfterDeserialization", BindingFlags.Public);
            //     methodInfo.Invoke(configs.Value,null);
            //     //((ProtoObject)(configs.Value)).AfterDeserialization();
            // }
            //
            // Log.Info("配置表序列化完成！");

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
            return (eventParam) => Game.EventSystem.Publish((ET.EventType.AppStart) eventParam);
        }
    }
}