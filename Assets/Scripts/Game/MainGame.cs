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
            Global.Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            UIManager.Inst.Init();
        }
    }
}