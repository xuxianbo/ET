// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月11日, 星期一
// --------------------------
namespace ET
{
    public class FUI_LoadingComponentSystems : AwakeSystem<FUI_LoadingComponent,FUI_Loading>
    {
        public override void Awake(FUI_LoadingComponent self, FUI_Loading a)
        {
            a.MakeFullScreen();
        }
    }
}