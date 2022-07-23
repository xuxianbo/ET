// Administrator20222311:502022

using Cysharp.Threading.Tasks;
using UnityEditor;
using YooAsset.Editor;

namespace ET
{
    public struct CIHelper
    {
        public static void BuildAB()
        {
            AssetBundleBuilder assetBundleBuilder = new AssetBundleBuilder();

            BuildParameters buildParameters = new BuildParameters();
            buildParameters.BuildMode = EBuildMode.IncrementalBuild;
            buildParameters.BuildPipeline = EBuildPipeline.ScriptableBuildPipeline;
            buildParameters.BuildTarget = BuildTarget.StandaloneWindows64;
            buildParameters.BuildVersion = ++Init.Instance.Version;
            buildParameters.EnableAddressable = true;
            buildParameters.CompressOption = ECompressOption.LZ4;
            buildParameters.OutputRoot = AssetBundleBuilderHelper.GetDefaultOutputRoot();
            buildParameters.VerifyBuildingResult = true;

            assetBundleBuilder.Run(buildParameters);
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
            }
        }
    }
}