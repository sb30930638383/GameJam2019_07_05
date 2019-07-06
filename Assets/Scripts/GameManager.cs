using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class GameManager : MonoSingleton<GameManager>
    {

        public Player player;

        public int waveCount;

        private void Awake()
        {
            player = FindObjectOfType<Player>();
        }
    }
}
