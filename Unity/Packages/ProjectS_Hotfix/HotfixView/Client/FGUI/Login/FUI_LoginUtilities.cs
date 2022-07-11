//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月29日 10:34:57
//------------------------------------------------------------

using System.IO;
using ET.Client;
using Sirenix.Serialization;
using UnityEngine;

namespace ET
{
    public static class FUI_LoginUtilities
    {
        public static void OnLogin(FUI_LoginComponent self)
        {
            TestHotfixSkillDes().Coroutine();
            
            Game.EventSystem.Publish(self.DomainScene(), new EventType.LoadingBegin());
        }

        private static async ETTask TestHotfixSkillDes()
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
            //     fuiLogin.m_passwordText.text).Coroutine();
        }
    }
}