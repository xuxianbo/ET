//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月29日 10:34:57
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using ET.cfg.SceneConfig;
using ET.Client;
using Sirenix.Serialization;
using UnityEngine;

namespace ET
{
    public static class FUI_LoginUtilities
    {
        // 选择进入游戏，则单局游戏Scene创建，为ClientScene的子物体，所有资源OperationHandle都挂在这个单局游戏的Scene上面，Scene销毁时统一进行释放
        public static async UniTaskVoid OnLogin(FUI_LoginComponent self)
        {
            Scene singleGameScene =
                SceneFactory.CreateSingleGameScene(++GlobalDefine.SingleGameSceneIndex, "SingleGameScene", self.Domain);

            SceneBaseConfig sceneBaseConfig = ConfigComponent.Instance.AllConfigTables.TbSceneBase[10001];

            await Game.EventSystem.PublishAsync(singleGameScene, new EventType.LoadingBegin()
            {
                SceneName = YooAssetProxy.GetYooAssetFormatResPath(sceneBaseConfig.SceneName,
                    YooAssetProxy.YooAssetResType.Scene),
                ResList = new List<string>()
                {
                    // 测试用，这个资源列表只应该是场景中的动态物件以及一些逻辑配置
                    YooAssetProxy.GetYooAssetFormatResPath(sceneBaseConfig.SceneRecastNavDataFileName,
                        YooAssetProxy.YooAssetResType.PathFind)
                }
            });

            ClientSceneManagerComponent.Instance.Get(1).GetComponent<FUIManagerComponent>().Remove(FUIPackage.Login);

            await Game.EventSystem.PublishAsync(singleGameScene, new EventType.LoadingFinish());

            await singleGameScene.GetComponent<PathFindComponent>()
                .LoadRecastGraphData(sceneBaseConfig.SceneRecastNavDataFileName);

            await Game.EventSystem.PublishAsync(singleGameScene, new EventType.EnterGameMapFinish());
        }

        private static async UniTask TestHotfixSkillDes()
        {
            var SkillDataSupportor_Client_Des = SerializationUtility.DeserializeValue<NP_DataSupportor>(
                (await YooAssetProxy.LoadAssetAsync<TextAsset>("Config_Darius_Q")).GetAssetObject<TextAsset>().bytes,
                DataFormat.Binary);

            Log.Info($"反序列化 {SkillDataSupportor_Client_Des.BuffNodeDataDic.Count} 成功");

            Log.Info(Application.dataPath);

            using (FileStream file = File.Create($"{Application.dataPath}/TestSeri.bytes"))
            {
                byte[] bytes = SerializationUtility.SerializeValue(SkillDataSupportor_Client_Des, DataFormat.Binary);
                file.Write(bytes, 0, bytes.Length);
            }

            Log.Info($"序列化 {SkillDataSupportor_Client_Des.BuffNodeDataDic.Count} 成功");

            var SkillDataSupportor_Client_Des2 = SerializationUtility.DeserializeValue<NP_DataSupportor>(
                File.ReadAllBytes($"{Application.dataPath}/TestSeri.bytes"),
                DataFormat.Binary);

            foreach (var buff in SkillDataSupportor_Client_Des.BuffNodeDataDic)
            {
                Log.Info($"Buff: {buff.Value.NodeId}");
            }

            foreach (var np in SkillDataSupportor_Client_Des.NpDataSupportorBase.NP_DataSupportorDic)
            {
                Log.Info($"Node: {np.Key} {np.Value.NodeDes?.ToString()}");
            }

            foreach (var np in SkillDataSupportor_Client_Des.NpDataSupportorBase.NP_BBValueManager)
            {
                Log.Info($"BB: {np.Key} {np.Value.NP_BBValueType.ToString()}");
            }

            Log.Info($"反序列化 {SkillDataSupportor_Client_Des2.BuffNodeDataDic.Count} 成功");
        }

        public static void OnRegister(FUI_LoginComponent self)
        {
            // FUI_Login fuiLogin = self.FuiUIPanelLogin;
            // RegisteHelper.Register(self, GlobalDefine.GetLoginAddress(), fuiLogin.m_accountText.text,
            //     fuiLogin.m_passwordText.text).Forget();
        }
    }
}