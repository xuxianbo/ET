// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月11日, 星期一
// --------------------------

using ET.EventType;

namespace ET
{
    [Event(SceneType.Client)]
    public class BeginLoad_ShowLoadingUI : AEvent<Scene, LoadingBegin>
    {
        protected override async ETTask Run(Scene entity, LoadingBegin a)
        {
            var scene = entity;

            // 别忘了自己处理依赖关系包
            await scene.GetComponent<FUIPackageManagerComponent>().AddPackageAsync(FUIPackage.Shared);
            await scene.GetComponent<FUIPackageManagerComponent>().AddPackageAsync(FUIPackage.Loading);
            
            FUI_Loading fuiLoading = await FUI_Loading.CreateInstanceAsync(scene);
            
            fuiLoading.MakeFullScreen();
            
            FUI_LoadingComponent fuiLoadingComponent = fuiLoading.AddChild<FUI_LoadingComponent, FUI_Loading>(fuiLoading);
            
            FUIManagerComponent fuiManagerComponent = scene.GetComponent<FUIManagerComponent>();
            
            fuiManagerComponent.Add(FUIPackage.Loading, fuiLoading, fuiLoadingComponent);
        }
    }
}