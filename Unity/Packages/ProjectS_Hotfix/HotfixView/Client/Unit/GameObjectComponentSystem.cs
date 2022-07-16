using System;

namespace ET.Client
{
    public static class GameObjectComponentSystem
    {
        [ObjectSystem]
        public class DestroySystem : DestroySystem<GameObjectComponent>
        {
            public override void Destroy(GameObjectComponent self)
            {
                GameObjectPoolComponent.Instance.RecycleGameObject(self.ResName, self.GameObject);
            }
        }
    }
}