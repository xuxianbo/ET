//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年4月28日 12:02:49
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ProtoBuf;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace ET
{
    public static class ILHelper
    {
        public static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
            //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
            appdomain.UnityMainThreadID = Thread.CurrentThread.ManagedThreadId;
            appdomain.DebugService.StartDebugService(56000);
#endif

            // 注册跨域继承适配器
            RegisterCrossBindingAdaptor(appdomain);
            // 注册重定向函数
            RegisterILRuntimeCLRRedirection(appdomain);
            // 注册委托
            RegisterMethodDelegate(appdomain);
            RegisterFunctionDelegate(appdomain);
            RegisterDelegateConvertor(appdomain);
            // 注册值类型绑定
            RegisterValueTypeBinder(appdomain);

            ////////////////////////////////////
            // CLR绑定的注册，一定要记得将CLR绑定的注册写在CLR重定向的注册后面，因为同一个方法只能被重定向一次，只有先注册的那个才能生效
            ////////////////////////////////////
            Type t = Type.GetType("ILRuntime.Runtime.Generated.CLRBindings");
            if (t != null)
            {
                t.GetMethod("Initialize")?.Invoke(null, new object[] { appdomain });
            }
        }

        /// <summary>
        /// 注册跨域继承适配器
        /// </summary>
        /// <param name="appdomain"></param>
        static void RegisterCrossBindingAdaptor(AppDomain appdomain)
        {
            //自动注册一波，无需再手动添加了，如果想要性能也可以手动自己加
            Assembly assembly = typeof (ILHelper).Assembly;
            foreach (Type type in assembly.GetTypes().ToList().FindAll(t => t.IsSubclassOf(typeof (CrossBindingAdaptor))))
            {
                object obj = Activator.CreateInstance(type);
                CrossBindingAdaptor adaptor = obj as CrossBindingAdaptor;
                if (adaptor == null)
                {
                    continue;
                }

                appdomain.RegisterCrossBindingAdaptor(adaptor);
            }
        }

        /// <summary>
        /// 注册CLR重定向
        /// </summary>
        /// <param name="appdomain"></param>
        static void RegisterILRuntimeCLRRedirection(AppDomain appdomain)
        {
            //LitJson适配
            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(appdomain);
            //Protobuf适配
            PType.RegisterILRuntimeCLRRedirection(appdomain);
        }

        /// <summary>
        /// 注册委托（不带返回值）
        /// </summary>
        static void RegisterMethodDelegate(AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterMethodDelegate<List<object>>();
            appdomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
        }

        /// <summary>
        /// 注册委托（带返回值）
        /// </summary>
        static void RegisterFunctionDelegate(AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Object, ET.ETTask>();
        }

        /// <summary>
        /// 注册委托转换器
        /// </summary>
        static void RegisterDelegateConvertor(AppDomain appdomain)
        {
        }

        /// <summary>
        /// 注册值类型绑定
        /// </summary>
        static void RegisterValueTypeBinder(AppDomain appdomain)
        {
            // appdomain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
            // appdomain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
            // appdomain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        }
    }
}