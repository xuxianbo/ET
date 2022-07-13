// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月13日, 星期三
// --------------------------
namespace ET
{
    public class FUIPackageManagerSystems : AwakeSystem<FUIPackageManagerComponent>
    {
        public override void Awake(FUIPackageManagerComponent self)
        {
            self.UsedYooAssetComponent = self.Domain.GetComponent<YooAssetComponent>();
        }
    }
    
    public class FUIPackageManagerDestroySystems : DestroySystem<FUIPackageManagerComponent>
    {
        public override void Destroy(FUIPackageManagerComponent self)
        {
            self.UsedYooAssetComponent = null;
        }
    }
}