using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class GamePlayer
    {
        public static GamePlayer Inst { get { return inst; } }
        private static GamePlayer inst = new GamePlayer();

        /// <summary>
        /// 游戏数据管理器.
        /// </summary>
        public GameDataManager GameDataMgr { get; private set; }

        public void Init()
        {
            GameDataMgr = new GameDataManager();
            GameDataMgr.Init(this);
        }

        ~GamePlayer()
        {
            GameDataMgr = null;
        }
    }
}