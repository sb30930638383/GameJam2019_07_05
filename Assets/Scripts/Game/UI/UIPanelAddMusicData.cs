using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

namespace GameJam2019
{
    public class UIPanelAddMusicData : UIPanelBase
    {
        public Dropdown _Dropdown;
        public InputField _TimeInput;
        public Button _AddButton;
        private Vector2 worldPos;
        private List<MusicData> dataList = new List<MusicData>();

        protected override void Initialize()
        {
            _AddButton.onClick.AddListener(OnButtonDown);
            base.Initialize();
        }

        public override void Show()
        {
            worldPos = CameraController.Inst.MousePosition;
            transform.position = Input.mousePosition;
            base.Show();
            _TimeInput.ActivateInputField();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Hide();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //DoSave();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                OnButtonDown();
            }
        }

        public void OnButtonDown()
        {
            MusicImpactEnum impact = (MusicImpactEnum)_Dropdown.value;
            float time;
            if (!float.TryParse(_TimeInput.text, out time))
            {
                Debug.LogError("时间填写错误");
                return;
            }
            MusicData data = new MusicData();
            data.MusicImpact = impact;
            data.Time = time;
            data.Position = worldPos.ToStr();
            dataList.Add(data);
            DoSave();
            Hide();
        }

        public void DoSave()
        {
            if (dataList == null)
                return;
            string jsonData = JsonConvert.SerializeObject(dataList);
            string filePath = string.Format("{0}/Resources/Config/MusicData_Test", Application.dataPath);
            //Debug.LogError(filePath);
            FileStream file = new FileStream(filePath, FileMode.OpenOrCreate);
            file.Close();
            file.Dispose();
            File.WriteAllText(filePath, jsonData);
            Debug.LogErrorFormat("已保存文件到{0}.", filePath);
        }
    }
}