using ET;
using UnityEngine;

namespace ETHotfix
{

    [Event]    public class ChangeRotation_SyncGameObjectRotation: AEvent<HotfixEventType.ChangeRotation>
    {
        protected override async ETTask Run(HotfixEventType.ChangeRotation args)
        {
            Transform transform = args.Unit.GetComponent<GameObjectComponent>().GameObject.transform;
            transform.rotation = args.Unit.Rotation;
            await ETTask.CompletedTask;
        }
    }
}