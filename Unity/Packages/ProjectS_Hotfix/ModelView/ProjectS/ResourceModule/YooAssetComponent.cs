// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月13日, 星期三
// --------------------------

using System.Collections.Generic;
using YooAsset;

namespace ET
{
    /// <summary>
    /// 热更层特有的YooAsset封装，用于加载资源，并进行统一管理
    /// 所有需要进行资源加载的Scene都要挂载这个组件
    /// </summary>
    public class YooAssetComponent : Entity, IAwake, IDestroy
    {
        public List<SceneOperationHandle> SceneOperationHandles = new List<SceneOperationHandle>();
        public List<AssetOperationHandle> AssetsOperationHandles = new List<AssetOperationHandle>();
        public List<SubAssetsOperationHandle> SubAssetsOperationHandles = new List<SubAssetsOperationHandle>();
        public List<RawFileOperation> RawFileOperationHandles = new List<RawFileOperation>();
    }
}