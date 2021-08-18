//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月26日 18:21:50
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

namespace ET
{
    [InitializeOnLoad]
    public static class HotfixCodeCopyHelper
    {
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies/";
        private const string FinalHotfixAssembliesDir = "Temp/MyAssembly/";
        private const string CodeDir = "Assets/Res/Code/";
        private const string HotfixDll = "Unity.Hotfix.dll";
        private const string HotfixViewDll = "Unity.HotfixView.dll";

        private static void CopyDllAndPdb(string FileName)
        {
            File.Copy(Path.Combine(FinalHotfixAssembliesDir, FileName + ".dll"), Path.Combine(CodeDir, FileName + ".dll.bytes"), true);
            File.Copy(Path.Combine(FinalHotfixAssembliesDir, FileName + ".pdb"), Path.Combine(CodeDir, FileName + ".pdb.bytes"), true);
        }

        /// <summary>
        /// 将两个热更程序集构建成一个Hotfix.dll
        /// Code From：https://github.com/mister91jiao/ET_ILRuntime/blob/HEAD/Unity/Assets/Editor/BuildEditor/BuildAssemblieEditor.cs#L13
        /// </summary>
        [MenuItem("Tools/构建热更程序集")]
        public static void BuildFinalHotfixDll()
        {
            BuildMuteAssembly("Hotfix", new[] {"Assets/Hotfix/", "Assets/HotfixView/" });
        }

        public static void BuildMuteAssembly(string Name, string[] CodeDirectorys)
        {
            List<string> Scripts = new List<string>();
            for (int i = 0; i < CodeDirectorys.Length; i++)
            {
                DirectoryInfo dti = new DirectoryInfo(CodeDirectorys[i]);
                FileInfo[] fileInfos = dti.GetFiles("*.cs", System.IO.SearchOption.AllDirectories);
                for (int j = 0; j < fileInfos.Length; j++)
                {
                    Scripts.Add(fileInfos[j].FullName);
                }
            }

            string outputAssembly = "Temp/MyAssembly/" + Name + ".dll";

            Directory.CreateDirectory("Temp/MyAssembly");

            AssemblyBuilder assemblyBuilder = new AssemblyBuilder(outputAssembly, Scripts.ToArray());

            //启用UnSafe
            assemblyBuilder.compilerOptions.AllowUnsafeCode = true;

            assemblyBuilder.compilerOptions.ApiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6;

            assemblyBuilder.compilerOptions.CodeOptimization = CodeOptimization.Release;

            assemblyBuilder.flags = AssemblyBuilderFlags.EditorAssembly;
            //AssemblyBuilderFlags.None                 正常发布
            //AssemblyBuilderFlags.DevelopmentBuild     开发模式打包
            //AssemblyBuilderFlags.EditorAssembly       编辑器状态

            assemblyBuilder.referencesOptions = ReferencesOptions.UseEngineModules;

            assemblyBuilder.buildTarget = EditorUserBuildSettings.activeBuildTarget;

            assemblyBuilder.buildTargetGroup = BuildTargetGroup.Standalone;

            //添加额外的宏定义
            // assemblyBuilder.additionalDefines = new[]
            // {
            //     ""
            // };

            //需要排除自身的引用
            assemblyBuilder.excludeReferences = new[] { $"{ScriptAssembliesDir}{HotfixDll}", $"{ScriptAssembliesDir}{HotfixViewDll}", };

            assemblyBuilder.buildStarted += delegate(string assemblyPath) { Debug.LogFormat("程序集开始构建：" + assemblyPath); };

            assemblyBuilder.buildFinished += delegate(string assemblyPath, CompilerMessage[] compilerMessages)
            {
                int errorCount = compilerMessages.Count(m => m.type == CompilerMessageType.Error);
                int warningCount = compilerMessages.Count(m => m.type == CompilerMessageType.Warning);

                Debug.LogFormat("程序集构建完成：" + assemblyPath);
                Debug.LogFormat("Warnings: {0} - Errors: {1}", warningCount, errorCount);

                if (warningCount > 0)
                {
                    Debug.LogFormat("有{0}个Warning!!!", warningCount);
                }

                if (errorCount > 0)
                {
                    for (int i = 0; i < compilerMessages.Length; i++)
                    {
                        if (compilerMessages[i].type == CompilerMessageType.Error)
                        {
                            Debug.LogError(compilerMessages[i].message);
                        }
                    }
                }
                else
                {
                    CopyDllAndPdb(Name);
                }
            };

            //开始构建
            if (!assemblyBuilder.Build())
            {
                Debug.LogErrorFormat("构建程序集失败：" + assemblyBuilder.assemblyPath);
                return;
            }
        }
    }
}