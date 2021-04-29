using ET;
using UnityEngine;

namespace ETHotfix
{
    [Event]    
    public class LoadingBeginEvent_CreateLoadingUI : AEvent<HotfixEventType.LoadingBegin>
    {
        protected override async ETTask Run(HotfixEventType.LoadingBegin args)
        {
            await UIHelper.Create(args.Scene, UIType.UILoading);
        }
    }
}
