using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UILobby)]
    public class UILobbyEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent)
        {
            await ETTask.CompletedTask;
            GameObject bundleGameObject = await XAssetLoader.LoadAssetAsync<GameObject>(XAssetPathUtilities.GetUGUIPath(UIType.UILobby));
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
            UI ui = EntityFactory.CreateWithParent<UI, string, GameObject>(uiComponent, UIType.UILobby, gameObject);

            ui.AddComponent<UILobbyComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            XAssetLoader.UnLoadAsset(XAssetPathUtilities.GetUGUIPath(UIType.UILobby));
        }
    }
}