using ET;
using UnityEngine;

namespace ETHotfix
{
    public class ChangePosition_SyncGameObjectPos: AEvent<HotfixEventType.ChangePosition>
    {
        protected override async ETTask Run(HotfixEventType.ChangePosition args)
        {
            GameObjectComponent gameObjectComponent = args.Unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }
            Transform transform = gameObjectComponent.GameObject.transform;
            transform.position = args.Unit.Position;
            await ETTask.CompletedTask;
        }
    }
}