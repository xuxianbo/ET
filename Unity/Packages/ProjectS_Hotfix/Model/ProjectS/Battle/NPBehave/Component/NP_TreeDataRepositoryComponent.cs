//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年8月23日 15:44:40
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Sirenix.Serialization;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 行为树数据仓库组件
    /// </summary>
    public class NP_TreeDataRepositoryComponent: Entity, IAwake
    {
        public const string NPDataServerPath = "../Config/SkillConfigs/";

        /// <summary>
        /// 运行时的行为树仓库，注意，一定不能对这些数据做修改
        /// </summary>
        public Dictionary<long, NP_DataSupportor> NpRuntimeTreesDatas = new Dictionary<long, NP_DataSupportor>();

        public async UniTask LoadSkillCanvas()
        {
            YooAssetComponent yooAssetComponent = this.DomainScene().GetComponent<YooAssetComponent>();
            
            foreach (var skillCanvasConfig in YooAssetProxy.GetAssetPathsByTag("SkillConfig"))
            {
                TextAsset textAsset =
                    await yooAssetComponent.LoadAssetAsync<TextAsset>(skillCanvasConfig);
            
                if (textAsset.bytes.Length == 0) Log.Info("没有读取到文件");
                try
                {
                    NP_DataSupportor MnNpDataSupportor = SerializationUtility.DeserializeValue<NP_DataSupportor>(textAsset.bytes, DataFormat.Binary);
            
                    Log.Info($"反序列化行为树:{skillCanvasConfig}完成");
            
                    this.NpRuntimeTreesDatas.Add(MnNpDataSupportor.NpDataSupportorBase.NPBehaveTreeDataId, MnNpDataSupportor);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    throw;
                }
            }
        }
    }
}