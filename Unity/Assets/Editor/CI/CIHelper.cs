// Administrator20222311:502022

using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace ET
{
    public struct CIHelper
    {
        public static void BuildAB()
        {
            AssetBundleBuilder assetBundleBuilder = new AssetBundleBuilder();

            BuildParameters buildParameters = new BuildParameters();
            buildParameters.BuildMode = EBuildMode.ForceRebuild;
            buildParameters.BuildPipeline = EBuildPipeline.BuiltinBuildPipeline;
            buildParameters.BuildTarget = BuildTarget.StandaloneWindows64;
            buildParameters.BuildVersion = 2;
            buildParameters.EnableAddressable = true;
            buildParameters.CompressOption = ECompressOption.LZ4;
            buildParameters.OutputRoot = AssetBundleBuilderHelper.GetDefaultOutputRoot();
            buildParameters.VerifyBuildingResult = true;

            assetBundleBuilder.Run(buildParameters);

            EditorApplication.Exit(0);
        }

        /// <summary>
        /// 目前如果只BuildEXE而不Build Dll的话，有时会导致元数据对应不上，所以额外构建一次Dll
        /// </summary>
        public static UniTask BuildEXE()
        {
            return internalBuildEXE();

            async UniTask internalBuildEXE()
            {
                await BuildAssemblieEditor.BuildCodeRelease();

                BuildStandalonePlayer.Build();
            }
        }

        /// <summary>
        /// 构建EXE和AB
        /// </summary>
        public static void BuildAll()
        {
            internalBuildAll().Forget();

            async UniTaskVoid internalBuildAll()
            {
                await BuildEXE();
                BuildAB();

                // 需要注意的是，一旦使用了UniTask，在batchmode需要自己处理Exit
                EditorApplication.Exit(0);
            }
        }

        public static void CollectSVC()
        {
            // 先删除再保存，否则ShaderVariantCollection内容将无法及时刷新
            AssetDatabase.DeleteAsset(ShaderVariantCollectorSettingData.Setting.SavePath);

            ShaderVariantCollector.OnCompletedCallback = () =>
            {
                ShaderVariantCollection collection =
                    AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(ShaderVariantCollectorSettingData.Setting
                        .SavePath);

                if (collection != null)
                {
                    Debug.Log($"ShaderCount : {collection.shaderCount}");
                    Debug.Log($"VariantCount : {collection.variantCount}");
                }
                
                EditorTools.CloseUnityGameWindow();
                EditorApplication.Exit(0);
            };

            ShaderVariantCollector.Run(ShaderVariantCollectorSettingData.Setting.SavePath);
        }
    }
}