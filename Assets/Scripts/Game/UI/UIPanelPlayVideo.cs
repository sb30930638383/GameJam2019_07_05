using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GameJam2019
{
    public class UIPanelPlayVideo : UIPanelBase
    {
        public RawImage _RawImage;
        public VideoPlayer _Video;

        private Action onFinish;

        public void Show(string resName, Action onFinish)
        {
            this.onFinish = onFinish;
            VideoClip clip = ResManager.Inst.Load<VideoClip>(resName, ResTypeEnum.Video);
            _Video.clip = clip;
            _Video.isLooping = false;
            StartCoroutine(IWaitOver((float)clip.length));
        }

        private IEnumerator IWaitOver(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (onFinish != null)
            {
                onFinish();
                onFinish = null;
            }
        }
    }
}