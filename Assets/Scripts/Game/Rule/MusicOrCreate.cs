using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace GameJam2019
{
    public class MusicOrCreate
    {
        public static MusicOrCreate Inst { get { return inst; } }
        private static MusicOrCreate inst = new MusicOrCreate();

        private Action onUpdate;
        private float startTime;
        private float maxTime;
        private List<MusicData> dataList = new List<MusicData>();

        public void StartPlay(string soundName, string configData)
        {
            FormatConfig(configData);
            StartCreateEvent();

            if (!string.IsNullOrEmpty(soundName))
            {
                AudioClip clip = ResManager.Inst.Load<AudioClip>(soundName, ResTypeEnum.Sound);
                maxTime = clip.length + startTime;
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
            switch (data.MusicImpact)
            {
                case MusicImpactEnum.EnemyEntity:
                EntityManager.Inst.CreateEnemy<EnemyEntity>(data.Position.ToV2(), Vector2.right);
                    break;
                case MusicImpactEnum.Enemy01Entity:
                EntityManager.Inst.CreateEnemy<Enemy01Entity>(data.Position.ToV2(), Vector2.right);
                    break;
                case MusicImpactEnum.Enemy02Entity:
                EntityManager.Inst.CreateEnemy<Enemy02Entity>(data.Position.ToV2(), Vector2.right);
                    break;
                case MusicImpactEnum.Enemy03Eneity:
                EntityManager.Inst.CreateEnemy<Enemy03Entity>(data.Position.ToV2(), Vector2.right);
                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }

        public static string GetJson()
        {
            string path = string.Format("{0}/Resources/Config/MusicData_Test", Application.dataPath);
            return File.ReadAllText(path);
        }
    }

    public class MusicData
    {
        public float Time { get; set; }
        public MusicImpactEnum MusicImpact { get; set; }
        public string Position { get; set; }
    }
}