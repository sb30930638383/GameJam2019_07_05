using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class AudioManager
    {
        public static AudioManager Inst { get { return inst; } }
        private static AudioManager inst = new AudioManager();

        private GameObject audioObject;
        private AudioSource bgmCmp;

        public void Init()
        {
            audioObject = new GameObject("Audio Manager");
            bgmCmp = audioObject.AddComponent<AudioSource>();
        }

        public void PlayBgm(AudioClip clip)
        {
            bgmCmp.clip = clip;
            bgmCmp.loop = true;
            bgmCmp.Play();
        }
    }
}