using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :MonoSingleton<GameManager> {

   public Player player;

   public int waveCount;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
}
