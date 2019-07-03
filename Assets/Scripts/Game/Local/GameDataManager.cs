using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class GameDataManager
    {
        private GameData gameData;
        private GamePlayer owner;

        public void Init(GamePlayer owner)
        {
            this.owner = owner;

            //暂时应该不需要读取已经保存的数据.
            gameData = new GameData();
        }

        /// <summary>
        /// 获取当前游戏分数.
        /// </summary>
        public int GetGameScore()
        {
            return gameData.Score;
        }

        /// <summary>
        /// 增加游戏分数.
        /// </summary>
        public void AddGameScore(int val)
        {
            gameData.Score += val;
        }
    }
}