using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace GameJam2019
{
    public class MusicOrCreate : MonoBehaviour
    {
        private Action onUpdate;
        private float startTime;
        private List<MusicData> dataList = new List<MusicData>();

        public void StartPlay(string soundName, string configData)
        {
            FormatConfig(configData);
            StartCreateEvent();

            if (!string.IsNullOrEmpty(soundName))
            {
                AudioClip clip = ResManager.Inst.Load<AudioClip>(soundName, ResTypeEnum.Sound);
                AudioManager.Inst.PlayBgm(clip);
            }
        }

        private void StartCreateEvent()
        {
            startTime = Time.unscaledTime;
            onUpdate += OnUpdate;
        }

        private void FormatConfig(string configData)
        {
            dataList.Clear();
            dataList = JsonConvert.DeserializeObject<List<MusicData>>(configData);
        }

        private void OnUpdate()
        {
            if (dataList.Count == 0)
            {
                onUpdate -= OnUpdate;
                return;
            }
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[0].Time <= Time.unscaledTime - startTime)
                {
                    ApplyImpact(dataList[0]);
                    dataList.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }

        private void ApplyImpact(MusicData data)
        {
            if (data.MusicImpact == MusicImpactEnum.EnemyEntity)
            {
                EntityManager.Inst.CreateEnemy<EnemyEntity>(data.Position.ToV2(), Vector2.right);
            }
        }

        public void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
    }

    public class MusicData
    {
        public float Time { get; set; }
        public MusicImpactEnum MusicImpact { get; set; }
        public string Position { get; set; }
    }
}