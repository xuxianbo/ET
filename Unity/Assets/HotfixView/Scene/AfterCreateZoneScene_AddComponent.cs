using ET;

namespace ETHotfix
{
    public class AfterCreateZoneScene_AddComponent: AEvent<HotfixEventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(HotfixEventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<UIComponent>();
            await ETTask.CompletedTask;
        }
    }
}