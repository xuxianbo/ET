// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月16日, 星期六
// --------------------------

using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class PathFindComponentSystems : AwakeSystem<PathFindComponent>
    {
        public override void Awake(PathFindComponent self)
        {
            self.DoAwake().Forget();
        }
    }

    public class PathFindComponentDestroySystems : DestroySystem<PathFindComponent>
    {
        public override void Destroy(PathFindComponent self)
        {
            self.DoDestroy();
        }
    }

    public static class PathFindUtitlies
    {
        public static async UniTask LoadRecastGraphData(this PathFindComponent self, string graphName)
        {
            TextAsset textAsset = await self.DomainScene().GetComponent<YooAssetComponent>()
                .LoadAssetAsync<TextAsset>(
                    YooAssetProxy.GetYooAssetFormatResPath(graphName, YooAssetProxy.YooAssetResType.PathFind));
            
            self.PathFindInstance.data.DeserializeGraphs(textAsset.bytes);
            Log.Info($"加载RecastNav数据图：{graphName}完成");
        }
    }
}