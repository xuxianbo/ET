using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    public class RecastNavMeshManagerComponentAwakeSystem : AwakeSystem<RecastNavMeshManagerComponent>
    {
        public override void Awake(RecastNavMeshManagerComponent self)
        {
            RecastNavMeshManagerComponent.Instance = self;
            
            // TODO
            // foreach (var recastNavDataConfig in RecastNavDataConfigCategory.Instance.GetAll())
            // {
            //     byte[] result = XAssetLoader.LoadAsset<TextAsset>(
            //         XAssetPathUtilities.GetRecastNavDataConfigPath(recastNavDataConfig.Value.ConfigName)).bytes;
            //     self.AllNavData[recastNavDataConfig.Value.ConfigName] = result;
            // }
        }
    }

    public static class RecastNavMeshManagerComponentSystem
    {
        public static IntPtr Get(this RecastNavMeshManagerComponent self, string name)
        {
            IntPtr ptr = IntPtr.Zero;
            if (self.Navmeshs.TryGetValue(name, out ptr))
            {
                return ptr;
            }

            if (self.AllNavData.TryGetValue(name, out var navdata))
            {
                if (navdata.Length == 0)
                {
                    throw new Exception($"no nav data: {name}");
                }

                // TODO
                //ptr = RecastInterface.RecastLoad(name.GetHashCode(), navdata, navdata.Length);
                self.Navmeshs[name] = ptr;
            }

            return ptr;
        }
    }
}