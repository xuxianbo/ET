// --------------------------
// 作者：烟雨迷离半世殇
// 邮箱：1778139321@qq.com
// 日期：2022年7月16日, 星期六
// --------------------------

using Cysharp.Threading.Tasks;
using ET.Client;
using ET.EventType;

namespace ET
{
    [Event(SceneType.SingleGame)]
    public class EnterMapFinish_CreateUnit: AEvent<Scene, EventType.EnterGameMapFinish>
    {
        protected override UniTask Run(Scene entity, EnterGameMapFinish a)
        {
            UnitFactory.Create(entity, new UnitInfo()
            {
                X = -10,
                Y = 0,
                Z = 0,
                ConfigId = 10001,
            });
            
            return UniTask.CompletedTask;
        }
    }
}