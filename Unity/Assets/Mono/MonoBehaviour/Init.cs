using System;
using System.Reflection;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace ET
{
	public class Init: MonoBehaviour
	{
		public bool ILRuntimeMode = false;
		
		private void Awake()
		{
			GloabDefine.ILRuntimeMode = this.ILRuntimeMode;
			
			SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);
			
			DontDestroyOnLoad(gameObject);

			// TODO 使用XAsset进行加载
			byte[] dllByte = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Res/Code/Code.prefab").GetComponent<ReferenceCollector>().Get<TextAsset>("Hotfix.dll").bytes;
			byte[] pdbByte = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Res/Code/Code.prefab").GetComponent<ReferenceCollector>().Get<TextAsset>("Hotfix.pdb").bytes;
			
			HotfixHelper.GoToHotfix(dllByte, pdbByte);
		}

		private void Start()
		{
			GloabLifeCycle.StartAction?.Invoke();
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