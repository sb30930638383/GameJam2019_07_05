using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    /// <summary>
    /// UI Base.
    /// </summary>
    public abstract class UIPanelBase : MonoBehaviour
    {
        public bool IsShow { get { return isShow; } }
        protected bool isShow;

        protected bool initialized = false;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (initialized) return;
            Hide();
            initialized = true;
        }

        public virtual void Show()
        {
            isShow = true;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            isShow = false;
            gameObject.SetActive(false);
        }
    }
}