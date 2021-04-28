using ET;

namespace ETHotfix
{
    public class EnterMapFinish_RemoveLobbyUI: AEvent<HotfixEventType.EnterMapFinish>
    {
        protected override async ETTask Run(HotfixEventType.EnterMapFinish args)
        {
            // 加载场景资源
            await ResourcesComponent.Instance.LoadBundleAsync("map.unity3d");
            // 切换到map场景
            SceneChangeComponent sceneChangeComponent = Game.Scene.AddComponent<SceneChangeComponent>();

            await sceneChangeComponent.ChangeSceneAsync("Map");
            
            sceneChangeComponent.Dispose();
            args.ZoneScene.AddComponent<OperaComponent>();
            await UIHelper.Remove(args.ZoneScene, UIType.UILobby);
        }
    }
}