using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace GameJam2019
{
    [ExecuteInEditMode]
    public class MusicConfigTools : MonoBehaviour
    {
        public float _Time = 0;
        public MusicImpactEnum _Impact = MusicImpactEnum.EnemyEntity;
        public Vector2 _Position = Vector2.zero;

        private List<MusicData> dataList = new List<MusicData>();

        [Button("添加", ButtonSizes.Gigantic)]
        public void AddOnce()
        {
            MusicData data = new MusicData();
            data.Time = _Time;
            data.MusicImpact = _Impact;
            data.Position = _Position.ToStr();
            dataList.Add(data);
            Debug.LogError("已添加， Time:" + Time.time);
        }
        
        [Button("保存json文件", ButtonSizes.Medium)]
        public void DoSave()
        {
            if (dataList == null)
                return;
            string jsonData = JsonConvert.SerializeObject(dataList);
            string filePath = string.Format("{0}/MusicData_Name_{1}", Application.dataPath, Time.unscaledTime);
            Debug.LogError(filePath);
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);
            file.Close();
            file.Dispose();
            File.WriteAllText(filePath, jsonData);
            Debug.LogErrorFormat("已保存文件到{0}.", filePath);
        }
        
        [Button("清空所有的", ButtonSizes.Medium)]
        public void ClearData()
        {
            dataList.Clear();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}