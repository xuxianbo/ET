using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FairyGUI;
using UnityEngine;
using YooAsset;

namespace ET
{
    /// <summary>
    /// 管理所有UI Package
    /// </summary>
    public class FUIPackageManagerComponent : Entity, IAwake, IDestroy
    {
        public YooAssetComponent UsedYooAssetComponent;
        
        public async UniTask AddPackageAsync(string type)
        {
            // 先加载UI描述文件
            TextAsset desTextAsset = await UsedYooAssetComponent.LoadAssetAsync<TextAsset>($"FGUI_{type}_fui");

            // 再加载UI图集文件
            UIPackage.AddPackage(desTextAsset.bytes, type, LoadPackageInternalAsync);
        }

        /// <summary>
        /// 加载资源的异步委托
        /// </summary>
        /// <param name="name">注意，这个name是FGUI内部组装的纹理全名，例如FUILogin_atlas0</param>
        /// <param name="extension"></param>
        /// <param name="type"></param>
        /// <param name="item"></param>
        private async void LoadPackageInternalAsync(string name, string extension, System.Type type, PackageItem item)
        {
            item.owner.SetItemAsset(item, await UsedYooAssetComponent.LoadAssetAsync<Texture>($"FGUI_{name}"), DestroyMethod.Unload);
        }
    }
}