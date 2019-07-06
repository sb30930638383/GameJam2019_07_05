using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameJam2019
{
    public class MainGame : MonoBehaviour
    {
        [LabelText("是否开始测试")]
        public bool IsTestGame;

        [ShowIf("IsTestGame")]
        [LabelText("Bgm名称")]
        public string SoundName;

        void Start()
        {
            InitGame();
            StartGame();
        }

        void Update()
        {
            TestCreateMonster();
        }

        private void InitGame()
        {
            Global.Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            Global.MainCamera = Camera.main;
            DoCoroutine.Mono = this;
            GamePlayer.Inst.Init();
            UIManager.Inst.Init();
            MessageHandler.Inst.Init();

            MessageManager.Inst.SendLog("============打印我叭===========");
            MessageManager.Inst.SendAddGameScore(233);

            var camCtrl = Global.MainCamera.gameObject.AddComponent<CameraController>();
            camCtrl.Init();
        }

        private void StartGame()
        {
            if (IsTestGame)
            {
                EntityManager.Inst.CreatePlayer(Vector2.zero, Vector2.right);
                var cmp = gameObject.AddComponent<MusicOrCreate>();
                string path = string.Format("{0}/Resources/Config/MusicData_Test", Application.dataPath);
                string configData = File.ReadAllText(path);
                cmp.StartPlay(SoundName, configData);
            }
            else
            {

            }
        }

        private void TestCreateMonster()
        {
            if (!IsTestGame)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var ui = UIManager.Inst.GetUIPanel<UIPanelAddMusicData>();
                    if (ui == null || !ui.IsShow)
                        UIManager.Inst.OpenUIPanel<UIPanelAddMusicData>();
                }
            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Vector2 worldPos = CameraController.Inst.MousePosition;
            //    EntityManager.Inst.CreateEnemy<EnemyEntity>(worldPos, Vector2.right);
            //}
        }
    }
}