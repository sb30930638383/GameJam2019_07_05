using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class UIPanelBlackLerp : UIPanelBase
    {
        private const float cShowAnimDuration = 1f;
        private const float cHideAnimDuration = 1f;

        public Animator _Animator;
        private Action onFinish;
        private Coroutine waitCor;

        public void Show(Action onFinish)
        {
            if (waitCor != null) return;
            this.onFinish = onFinish;
            _Animator.Play("Show");
            waitCor = 
                StartCoroutine(IWaitAct(cShowAnimDuration));
        }

        public void Hide(Action onFinish)
        {
            if (waitCor != null) return;
            waitCor = StartCoroutine(IWaitAct(cHideAnimDuration));
            _Animator.Play("Hide");
        }

        private IEnumerator IWaitAct(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (onFinish != null)
            {
                onFinish();
                onFinish = null;
            }
            waitCor = null;
        }
    }
}