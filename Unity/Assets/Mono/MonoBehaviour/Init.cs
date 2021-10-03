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

        [Tooltip("验证服地址")]
        public string LoginAddress = "127.0.0.1:10002";
        
        [Tooltip("如果开启，将直连本地的服务端并且以编辑器模式加载资源")]
        public bool DevelopMode;
        
        private XAssetUpdater m_XAssetUpdater;

        private void Awake()
        {
            InternalAwake().Coroutine();
        }

        private async ETVoid InternalAwake()
        {
            try
            {
                // 设置全局模式
                GloabDefine.ILRuntimeMode = this.ILRuntimeMode;
                GloabDefine.DevelopMode = this.DevelopMode;
                GloabDefine.SetLoginAddress(LoginAddress);
                
                // 限制帧率，尽量避免手机发烫
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;

                SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

                DontDestroyOnLoad(gameObject);

                // 初始化FGUI系统
                FUIEntry.Init();
                
                Updater updater = this.GetComponent<Updater>();
                m_XAssetUpdater = new XAssetUpdater(updater);

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