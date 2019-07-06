using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class MainGame : MonoBehaviour
    {
        void Start()
        {
            InitGame();
            StartGame();
        }

        void Update()
        {

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
            EntityManager.Inst.CreatePlayer(Vector2.zero, Vector2.right);
        }
    }
}