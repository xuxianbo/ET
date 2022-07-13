using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Cysharp.Threading.Tasks;
using Huatuo;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Sirenix.Serialization;

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using YooAsset;
using Debug = UnityEngine.Debug;

namespace ET
{
    // 1 mono模式 2 ILRuntime模式 3 mono热重载模式
    public enum CodeMode
    {
        Mono = 1,
        Reload = 3,
    }

#if UNITY_EDITOR
    // 注册Editor下的Log
    [InitializeOnLoad]
    public class EditorRegisteLog
    {
        static EditorRegisteLog()
        {
            Game.ILog = new UnityLogger();
        }
    }
#endif

    public partial class Init : MonoBehaviour
    {
        public CodeMode CodeMode = CodeMode.Mono;
        public YooAssets.EPlayMode PlayMode = YooAssets.EPlayMode.EditorSimulateMode;
        
        private void Awake()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

            SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

            DontDestroyOnLoad(gameObject);

            ETTask.ExceptionHandler += Log.Error;

            Game.ILog = new UnityLogger();

            CodeLoader.Instance.CodeMode = this.CodeMode;
            Options.Instance.Develop = 1;
            Options.Instance.LogLevel = 0;

            FUIEntry.Init();

            if (PlayMode == YooAssets.EPlayMode.HostPlayMode)
            {
                FUI_CheckForResUpdateComponent.Init();
            }

            LoadAssetsAndHotfix().Forget();
        }

        private async UniTaskVoid LoadAssetsAndHotfix()
        {
            // 启动YooAsset引擎，并在初始化完毕后进行热更代码加载
            await YooAssetProxy.StartYooAssetEngine(PlayMode);

            // Shader Warm Up
            ShaderVariantCollection shaderVariantCollection = (await YooAssetProxy.LoadAssetAsync<ShaderVariantCollection>("Shader_ProjectSShaderVariant")).GetAssetObject<ShaderVariantCollection>();
            
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            Log.Info($"开始Shader Warm Up, shaderCount: {shaderVariantCollection.shaderCount} variantCount: {shaderVariantCollection.variantCount}");
            
            shaderVariantCollection.WarmUp();
            
            stopwatch.Stop();
            
            Log.Info($"Shader Warm Up完成, 耗时: {stopwatch.ElapsedMilliseconds}ms");

            await LoadCode();
            
            if (PlayMode == YooAssets.EPlayMode.HostPlayMode)
            {
                FUI_CheckForResUpdateComponent.Release();
            }
        }

        private void Update()
        {
            CodeLoader.Instance.Update?.Invoke();
        }
        
        private void FixedUpdate()
        {
            CodeLoader.Instance.FixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            CodeLoader.Instance.LateUpdate?.Invoke();
        }

        private void OnApplicationQuit()
        {
            CodeLoader.Instance.OnApplicationQuit();
            CodeLoader.Instance.Dispose();
        }
    }
}