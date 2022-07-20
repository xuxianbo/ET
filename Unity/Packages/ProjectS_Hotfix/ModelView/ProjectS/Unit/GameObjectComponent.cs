using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class GameObjectComponent : Entity, IAwake<YooAssetProxy.YooAssetResType, string>, IDestroy
    {
        public YooAssetProxy.YooAssetResType YooAssetResType;
        public string ResName;
        public GameObject GameObject { get; set; }

        public async UniTask<GameObject> CreateGameObjectInternal()
        {
            GameObject gameObject =
                await GameObjectPoolComponent.Instance.FetchGameObject(this.ResName, this.YooAssetResType);
            gameObject.GetComponent<MonoBridge>().BelongToUnitId = this.GetParent<Unit>().Id;
            this.GameObject = gameObject;
            return gameObject;
        }
    }
}