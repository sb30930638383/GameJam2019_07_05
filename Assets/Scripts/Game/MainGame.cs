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
        }

        void Update()
        {

        }

        private void InitGame()
        {
            GamePlayer.Inst.Init();
            Global.Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            UIManager.Inst.Init();
            MessageHandler.Inst.Init();

            MessageManager.Inst.SendLog("============打印我叭===========");
            MessageManager.Inst.SendAddGameScore(233);
        }
    }
}