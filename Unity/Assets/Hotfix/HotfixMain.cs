//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月28日 10:20:07
//------------------------------------------------------------

using System;
using System.Reflection;
using ET;
using ET.EventType;
using UnityEngine;

namespace ETHotfix
{
    public class HotfixMain
    {
        public static void Init()
        {
            Debug.Log("HotfixMain Init！");
            ET.Game.Hotfix.HotfixUpdate = () => { Update(); };
            ET.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
            
            LitJsonHelper.Init();
            ProtobufHelper.Init();

            //Game.EventSystem.Add(ET.Game.Hotfix.GetHotfixViewTypes());
            Game.EventSystem.Add(ET.Game.Hotfix.GetHotfixTypes());

            Debug.Log("Hotfix初始化完成");
        }

        private static void LateUpdate()
        {
            try
            {
                Game.EventSystem.LateUpdate();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Update()
        {
            try
            {
                Game.EventSystem.Update();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}