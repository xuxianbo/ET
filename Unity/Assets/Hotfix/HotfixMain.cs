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
            ProtobufHelper.Init();
            Assembly[] assemblys = { Assembly.Load("Unity.Hotfix"), Assembly.Load("Unity.HotfixView") };

            foreach (Assembly assembly in assemblys)
            {
                Game.EventSystem.Add(assembly);
            }

            Debug.Log("Hotfix初始化完成");
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