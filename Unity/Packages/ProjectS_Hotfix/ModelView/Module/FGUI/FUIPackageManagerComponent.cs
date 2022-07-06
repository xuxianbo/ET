using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using YooAsset;

namespace ET
{
    /// <summary>
    /// 管理所有UI Package
    /// </summary>
    public class FUIPackageManagerComponent : Entity, IAwake
    {
        private Dictionary<string, List<AssetOperationHandle>> s_Packages =
            new Dictionary<string, List<AssetOperationHandle>>();

        public async ETTask AddPackageAsync(string type)
        {
            if (s_Packages.ContainsKey(type))
            {
                return;
            }

            s_Packages[type] = new List<AssetOperationHandle>();

            // 先加载UI描述文件
            AssetOperationHandle defineAssetOperationHandle =
                await YooAssetProxy.LoadAssetAsync<TextAsset>($"FGUI_{type}_fui");
            TextAsset desTextAsset = defineAssetOperationHandle.GetAssetObject<TextAsset>();

            // 再加载UI图集文件
            UIPackage.AddPackage(desTextAsset.bytes, type, LoadPackageInternalAsync);

            s_Packages[type].Add(defineAssetOperationHandle);
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
            AssetOperationHandle altasAssetOperationHandle =
                await YooAssetProxy.LoadAssetAsync<Texture>($"FGUI_{name}");
            item.owner.SetItemAsset(item, altasAssetOperationHandle.GetAssetObject<Texture>(), DestroyMethod.Unload);
            
            s_Packages[name.Replace($"_{item.id}", String.Empty)].Add(altasAssetOperationHandle);
        }

        /// <summary>
        /// 移除一个包，并清理其asset
        /// </summary>
        /// <param name="type"></param>
        public void RemovePackage(string type)
        {
            if (s_Packages.TryGetValue(type, out var operationList))
            {
                var p = UIPackage.GetByName(type);
                
                if (p != null)
                {
                    UIPackage.RemovePackage(p.name);

                    foreach (var operationHandle in operationList)
                    {
                        operationHandle.Release();
                    }
                }

                s_Packages.Remove(type);
            }
        }
    }
}