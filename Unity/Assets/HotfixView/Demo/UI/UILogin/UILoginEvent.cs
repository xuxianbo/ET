using System;
using UnityEngine;

namespace ET
{
    [UIEvent(UIType.UILogin)]
    public class UILoginEvent: AUIEvent
    {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent)
        {
            GameObject bundleGameObject =
                await XAssetLoader.LoadAssetAsync<GameObject>(XAssetPathUtilities.GetUGUIPath(UIType.UILogin));
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

            UI ui = EntityFactory.CreateWithParent<UI, string, GameObject>(uiComponent, UIType.UILogin, gameObject);

            ui.AddComponent<UILoginComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
            XAssetLoader.UnLoadAsset(XAssetPathUtilities.GetUGUIPath(UIType.UILogin));
        }
    }
}