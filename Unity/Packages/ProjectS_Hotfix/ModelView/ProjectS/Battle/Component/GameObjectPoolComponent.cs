using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ET.Client;
using UnityEngine;

namespace ET
{
    public class GameObjectPoolComponentAwakeSystem : AwakeSystem<GameObjectPoolComponent>
    {
        public override void Awake(GameObjectPoolComponent self)
        {
            GameObjectPoolComponent.Instance = self;
        }
    }
    
    public class GameObjectPoolComponent : Entity, IAwake
    {
        public static GameObjectPoolComponent Instance { get; set; }

        /// <summary>
        /// 所有Prefab的缓存
        /// </summary>
        public Dictionary<string, GameObject> AllPrefabs = new Dictionary<string, GameObject>();

        public Dictionary<string, Queue<GameObject>> AllCachedGos = new Dictionary<string, Queue<GameObject>>();


        public async UniTask<GameObject> FetchGameObject(string resName, YooAssetProxy.YooAssetResType gameObjectType)
        {
            GameObject gameObject;
            if (AllCachedGos.TryGetValue(resName, out var gameObjects))
            {
                if (gameObjects.Count > 0)
                {
                    gameObject = gameObjects.Dequeue();
                }
                else
                {
                    gameObject =
                        UnityEngine.Object.Instantiate(AllPrefabs[resName], GlobalComponent.Instance.Unit, true);
                }
            }
            else
            {
                GameObject targetprefab;
                if (AllPrefabs.TryGetValue(resName, out var prefab))
                {
                    targetprefab = prefab;
                }
                else
                {
                    YooAssetComponent yooAssetComponent = this.DomainScene().GetComponent<YooAssetComponent>();

                    targetprefab = await yooAssetComponent.LoadAssetAsync<GameObject>(
                        YooAssetProxy.GetYooAssetFormatResPath(resName, gameObjectType));

                    if (targetprefab == null)
                    {
                        return null;
                    }

                    AllPrefabs[resName] = targetprefab;
                    AllCachedGos[resName] = new Queue<GameObject>();
                }

                gameObject = UnityEngine.Object.Instantiate(targetprefab, GlobalComponent.Instance.Unit, true);
                gameObject.transform.position = targetprefab.transform.position;
                gameObject.name = "Need To Be Renamed";
            }

            gameObject.SetActive(true);
            return gameObject;
        }

        public void RecycleGameObject(string resName, GameObject gameObject)
        {
            gameObject.SetActive(false);
            if (string.IsNullOrEmpty(resName))
            {
                return;
            }

            AllCachedGos[resName].Enqueue(gameObject);
        }
    }
}