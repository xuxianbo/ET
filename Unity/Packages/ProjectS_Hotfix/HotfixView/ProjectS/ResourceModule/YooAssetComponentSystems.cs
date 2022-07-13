// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月13日, 星期三
// --------------------------

using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using YooAsset;

namespace ET
{
    public class YooAssetComponentDestroySystem : DestroySystem<YooAssetComponent>
    {
        public override void Destroy(YooAssetComponent self)
        {
            foreach (var assetsOperation in self.AssetsOperationHandles)
            {
                assetsOperation.Release();
            }

            self.RawFileOperationHandles.Clear();
        }
    }

    public static class YooAssetComponentSystems
    {
        public static async UniTask<T> LoadAssetAsync<T>(this YooAssetComponent self, string path)
            where T : UnityEngine.Object
        {
            AssetOperationHandle assetOperationHandle = await YooAssetProxy.LoadAssetAsync<T>(path);

            self.AssetsOperationHandles.Add(assetOperationHandle);

            return assetOperationHandle.GetAssetObject<T>();
        }

        public static async UniTask<T> LoadSubAssetsAsync<T>(this YooAssetComponent self,
            string mainAssetPath, string subAssetName)
            where T : UnityEngine.Object
        {
            SubAssetsOperationHandle subAssetsOperationHandle =
                await YooAssetProxy.LoadSubAssetsAsync<T>(mainAssetPath, subAssetName);

            self.SubAssetsOperationHandles.Add(subAssetsOperationHandle);

            return subAssetsOperationHandle.GetSubAssetObject<T>(subAssetName);
        }

        public static async UniTask<UnityEngine.SceneManagement.Scene> LoadSceneAsync(this YooAssetComponent self,
            string scenePath,
            LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            SceneOperationHandle sceneOperationHandle = await YooAssetProxy.LoadSceneAsync(scenePath, loadSceneMode);

            self.SceneOperationHandles.Add(sceneOperationHandle);

            return sceneOperationHandle.SceneObject;
        }

        public static async UniTask<string> GetRawFileAsync_Txt(this YooAssetComponent self, string path)
        {
            RawFileOperation rawFileOperation = await YooAssetProxy.GetRawFileAsync(path);

            self.RawFileOperationHandles.Add(rawFileOperation);

            return rawFileOperation.GetRawString();
        }
        
        public static async UniTask<byte[]> GetRawFileAsync_Bytes(this YooAssetComponent self, string path)
        {
            RawFileOperation rawFileOperation = await YooAssetProxy.GetRawFileAsync(path);

            self.RawFileOperationHandles.Add(rawFileOperation);

            return rawFileOperation.GetRawBytes();
        }
    }
}