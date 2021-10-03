using System.Collections.Generic;
using ET;
using FairyGUI;
using UnityEngine;

namespace ET
{
    public class FUIManagerComponentAwakeSystem : AwakeSystem<FUIManagerComponent>
    {
        public override void Awake(FUIManagerComponent self)
        {
            GRoot.inst.SetContentScaleFactor(1280, 720, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            self.Root = Entity.Create<FUI, GObject>(self.Domain, GRoot.inst);
        }
    }

    /// <summary>
    /// 管理所有顶层UI, 顶层UI都是GRoot的孩子
    /// </summary>
    public class FUIManagerComponent : Entity
    {
        public FUI Root;

        private Dictionary<string, FUI> m_AllHotfixFuis = new Dictionary<string, FUI>();

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.m_AllHotfixFuis.Clear();
            Root.Dispose();
            Root = null;
        }

        public void Add(string name, FUI ui, bool asChildGObject = true)
        {
            if (m_AllHotfixFuis.TryGetValue(name, out var fui))
            {
                Log.Error($"已有名为：{name} 的FUI，请勿重复添加！");
                fui.Dispose();
                return;
            }
            else
            {
                m_AllHotfixFuis[name] = ui;
                Root?.Add(ui, asChildGObject);
            }
        }

        public void Remove(string name)
        {
            if (m_AllHotfixFuis.TryGetValue(name, out var fui))
            {
                m_AllHotfixFuis.Remove(name);
                Root?.Remove(fui.Name);
            }
            else
            {
                Log.Error($"不存在名为：{name} 的FUI，请勿检查逻辑！");
                return;
            }
        }

        /// <summary>
        /// 通过名字获得FUI
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FUI Get(string name)
        {
            return Root?.Get(name);
        }

        /// <summary>
        /// 通过ID获得FUI
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FUI Get(long id)
        {
            return Root?.Get(id.ToString());
        }

        public FUI[] GetAll()
        {
            return Root?.GetAll();
        }

        public void Clear()
        {
            var childrens = GetAll();
            if (childrens != null)
            {
                foreach (var fui in childrens)
                {
                    Remove(fui.Name);
                }
            }
        }
    }
}