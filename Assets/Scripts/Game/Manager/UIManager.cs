using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class UIManager
    {
        public static UIManager Inst { get { return inst; } }
        private static UIManager inst = new UIManager();

        public Transform UIParent { get { return uiParent; } }
        private Transform uiParent;

        private Dictionary<string, UIPanelBase> uiPanelDict = new Dictionary<string, UIPanelBase>();
        private string pName;

        public void Init()
        {
            uiParent = new GameObject("UI Parent").transform;
            uiParent.SetParent(Global.Canvas.transform);
        }

        public T OpenUIPanel<T>() where T : UIPanelBase
        {
            pName = typeof(T).Name;
            UIPanelBase panelBase;
            if (!uiPanelDict.TryGetValue(pName, out panelBase))
            {
                panelBase = InstantiatePanel(pName);
                uiPanelDict.Add(pName, panelBase);
            }
            panelBase.Show();
            return panelBase as T;
        }

        public void HideUIPanel<T>()
        {
            pName = typeof(T).Name;
            UIPanelBase panelBase;
            if (uiPanelDict.TryGetValue(pName, out panelBase))
            {
                panelBase.Hide();
            }
        }

        private UIPanelBase InstantiatePanel(string className)
        {
            var prefab = ResManager.Inst.Load<GameObject>(className, ResTypeEnum.GUI);
            prefab = GameObject.Instantiate(prefab);
            prefab.transform.SetParentWithNormal(uiParent);
            var ui = prefab.GetComponent<UIPanelBase>();
            System.Diagnostics.Debug.Assert(ReferenceEquals(ui, null), "error: ui prefab can't find component " + className);
            return ui;
        }
    }
}