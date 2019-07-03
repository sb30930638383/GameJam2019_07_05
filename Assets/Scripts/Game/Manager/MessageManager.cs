using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class MessageManager
    {
        public static MessageManager Inst { get { return inst; } }
        private static MessageManager inst = new MessageManager();

        /// <summary>
        /// 输出信息到控制台.
        /// </summary>
        public void SendLog(object obj)
        {
            MessageHandler.Inst.SendMsg(MessageEnum.Log, obj);
        }
        [MessageHandler(MessageEnum.Log)]
        public void ReceiveLog(GamePlayer gamePlayer, params object[] objs)
        {
            Debug.LogError(objs[0]);
        }

        /// <summary>
        /// 增加游戏分数.
        /// </summary>
        public void SendAddGameScore(int val)
        {
            MessageHandler.Inst.SendMsg(MessageEnum.AddGameScore, val);
        }
        [MessageHandler(MessageEnum.AddGameScore)]
        public void ReceiveAddGameScore(GamePlayer gamePlayer, params object[] objs)
        {
            int val = (int)objs[0];
            gamePlayer.GameDataMgr.AddGameScore(val);
            SendLog(string.Format("增加游戏分数成功, 当前分数{0}.", gamePlayer.GameDataMgr.GetGameScore()));
        }
    }
}