using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace ET
{
    public class Init: MonoBehaviour
    {
        private void Start()
        {
            try
            {
                SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

                DontDestroyOnLoad(gameObject);

                string[] assemblyNames = { "Unity.Model.dll", "Unity.ModelView.dll" };

                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    string assemblyName = assembly.ManifestModule.Name;
                    if (!assemblyNames.Contains(assemblyName))
                    {
                        continue;
                    }

                    Game.EventSystem.Add(assembly);
                }

                Game.Options = new Options();

                Game.Scene.AddComponent<TimerComponent>();
                Game.Scene.AddComponent<CoroutineLockComponent>();
                Game.Scene.AddComponent<GlobalComponent>();
                // 加载配置
                Game.Scene.AddComponent<ResourcesComponent>();

                Debug.Log("下载热更包，其中包含热更用的dll文件");

                Game.Hotfix.LoadHotfixAssembly();
                Debug.Log("进入热更初始化");
                Game.Hotfix.GotoHotfix();

                Debug.Log("向热更层发出事件");
                Game.EventSystem.Publish(new EventType.AppStart() { i = 10});
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void Update()
        {
            ThreadSynchronizationContext.Instance.Update();
            Game.Hotfix.HotfixUpdate?.Invoke();
            Game.EventSystem.Update();
        }

        private void LateUpdate()
        {
            Game.EventSystem.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            Game.Close();
        }
    }
}