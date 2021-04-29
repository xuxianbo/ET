//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月28日 11:11:32
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;

namespace ET
{
    public class Hotfix
    {
#if ILRUNTIME
		private ILRuntime.Runtime.Enviorment.AppDomain s_appDomain;
		private MemoryStream s_hotfixDllStream;
		private MemoryStream s_hotfixPdbStream;
		private MemoryStream s_hotfixViewDllStream;
		private MemoryStream s_hotfixViewPdbStream;
#else
        private Assembly s_hotfixAssembly;
        private Assembly s_hotfixViewAssembly;
#endif

        private IStaticMethod s_hotfixInit;
        private List<Type> s_hotfixTypes;
        private List<Type> s_hotfixViewTypes;

        public Action HotfixUpdate;
        public Action LateUpdate;

        public void GotoHotfix()
        {
#if ILRUNTIME
			ILHelper.InitILRuntime(s_appDomain);
#endif
            s_hotfixInit.Run();
        }

        public List<Type> GetHotfixTypes()
        {
            return s_hotfixTypes;
        }

        public List<Type> GetHotfixViewTypes()
        {
            return s_hotfixViewTypes;
        }

        public void LoadHotfixAssembly()
        {
            Game.Scene.GetComponent<ResourcesComponent>().LoadBundle($"code.unity3d");
            GameObject code = (GameObject) Game.Scene.GetComponent<ResourcesComponent>().GetAsset("code.unity3d", "Code");

            byte[] hotfixAssBytes = code.GetComponent<ReferenceCollector>().Get<TextAsset>("Hotfix.dll").bytes;
            byte[] hotfixPdbBytes = code.GetComponent<ReferenceCollector>().Get<TextAsset>("Hotfix.pdb").bytes;
            byte[] hotfixViewAssBytes = code.GetComponent<ReferenceCollector>().Get<TextAsset>("HotfixView.dll").bytes;
            byte[] hotfixViewPdbBytes = code.GetComponent<ReferenceCollector>().Get<TextAsset>("HotfixView.pdb").bytes;

#if ILRUNTIME
			Log.Debug($"当前使用的是ILRuntime模式");
			s_appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

			s_hotfixDllStream = new MemoryStream(hotfixAssBytes);
			s_hotfixPdbStream = new MemoryStream(hotfixPdbBytes);
			
			s_hotfixViewDllStream = new MemoryStream(hotfixViewAssBytes);
			s_hotfixViewPdbStream = new MemoryStream(hotfixViewPdbBytes);
			
			s_appDomain.LoadAssembly(s_hotfixDllStream, s_hotfixPdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
			s_appDomain.LoadAssembly(s_hotfixViewDllStream, s_hotfixViewPdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

			s_hotfixInit = new ILStaticMethod(s_appDomain, "ETHotfix.HotfixMain", "Init", 0);
			
			s_hotfixTypes = s_appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();
			s_hotfixViewTypes = s_appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();

#else
            Log.Debug($"当前使用的是Mono模式");

            s_hotfixAssembly = Assembly.Load(hotfixAssBytes, hotfixPdbBytes);
            s_hotfixViewAssembly = Assembly.Load(hotfixViewAssBytes, hotfixViewPdbBytes);
            
            Type hotfixInit = s_hotfixAssembly.GetType("ETHotfix.HotfixMain");
            
            s_hotfixInit = new MonoStaticMethod(hotfixInit, "Init");
            
            s_hotfixTypes = s_hotfixAssembly.GetTypes().ToList();
            s_hotfixViewTypes = s_hotfixViewAssembly.GetTypes().ToList();
#endif

            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"code.unity3d");
        }
    }
}