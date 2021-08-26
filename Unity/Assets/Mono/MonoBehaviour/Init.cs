using System;
using System.Reflection;
using System.Threading;
using FairyGUI;
using libx;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace ET
{
    public class Init : MonoBehaviour
    {
        public bool ILRuntimeMode = false;

        private XAssetUpdater m_XAssetUpdater;

        private void Awake()
        {
            InternalAwake().Coroutine();
        }

        private async ETVoid InternalAwake()
        {
            try
            {
                GloabDefine.ILRuntimeMode = this.ILRuntimeMode;

                SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

                DontDestroyOnLoad(gameObject);

                // 初始化FGUI系统
                FUIEntry.Init();

                m_XAssetUpdater = new XAssetUpdater(this.GetComponent<Updater>());
                
                FUI_CheckForResUpdateComponent.Init(m_XAssetUpdater.Updater);
                
                await m_XAssetUpdater.StartUpdate();
                
                byte[] dllByte = XAssetLoader.LoadAsset<TextAsset>(XAssetPathUtilities.GetHotfixDllPath("Hotfix"))
                    .bytes;
                byte[] pdbByte = XAssetLoader.LoadAsset<TextAsset>(XAssetPathUtilities.GetHotfixPdbPath("Hotfix"))
                    .bytes;
                
                HotfixHelper.GoToHotfix(dllByte, pdbByte);
                
                GloabLifeCycle.StartAction?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private void Update()
        {
            ThreadSynchronizationContext.Instance.Update();
            GloabLifeCycle.UpdateAction?.Invoke();
        }

        private void LateUpdate()
        {
            GloabLifeCycle.LateUpdateAction?.Invoke();
        }

        private void OnApplicationQuit()
        {
            GloabLifeCycle.OnApplicationQuitAction?.Invoke();
        }
    }
}