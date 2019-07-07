using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 怪物死亡.
        /// </summary>
        public void SendEnemyDie(int id)
        {
            MessageHandler.Inst.SendMsg(MessageEnum.EnemyDie, id);
        }
        [MessageHandler(MessageEnum.EnemyDie)]
        public void ReceiveEnemyDie(GamePlayer gamePlayer, params object[] objs)
        {
            int id = (int)objs[0];
            EnemyEntity enemy = EntityManager.Inst.GetEntity<EnemyEntity>(id);
            if (enemy != null)
                enemy.OnDie();
        }

        /// <summary>
        /// 伤害某个实体.
        /// </summary>
        public void SendDamagePawn(int id, float damage)
        {
            MessageHandler.Inst.SendMsg(MessageEnum.DamagePawn, id, damage);
        }
        [MessageHandler(MessageEnum.DamagePawn)]
        public void ReceiveDamagePawn(GamePlayer gamePlayer, params object[] objs)
        {
            int id = (int)objs[0];
            float damage = (float)objs[1];
            PawnBase pawn = EntityManager.Inst.GetEntity<PawnBase>(id);
            if (pawn != null)
                pawn.OnDamage(damage);
        }

        /// <summary>
        /// 清空当前所有怪物.
        /// </summary>
        public void SendClearEnemy()
        {
            MessageHandler.Inst.SendMsg(MessageEnum.ClearEnemy);
        }
        [MessageHandler(MessageEnum.ClearEnemy)]
        public void ReceiveClearEnemy(GamePlayer gamePlayer, params object[] objs)
        {
            DoCoroutine.StartCoroutine(IDelayDesAllEnemy());
        }
        private IEnumerator IDelayDesAllEnemy()
        {
            int maxCount = 1;
            float interval = 0.02f;
            int curCount = 0;
            var list = GameUtil.GetEnemyListByDistance();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].OnDamage(99999);
                CameraController.Inst.Shake(2f, 0.3f, 3f, 0);
                if (++curCount >= maxCount)
                {
                    yield return new WaitForSeconds(interval);
                }
            }
        }

        /// <summary>
        /// 开始播放音乐.
        /// </summary>
        public void SendPlayMusic(string soundName, string jsonData)
        {
            MessageHandler.Inst.SendMsg(MessageEnum.PlayMusic, soundName, jsonData);
        }
        [MessageHandler(MessageEnum.PlayMusic)]
        public void ReceivePlayMusic(GamePlayer gamePlayer, params object[] objs)
        {
            if (Global.Player == null)
                EntityManager.Inst.CreatePlayer(Vector2.zero, Vector2.right);
            MusicOrCreate.Inst.StartPlay((string)objs[0], (string)objs[1]);
        }

        /// <summary>
        /// 玩家死亡.
        /// </summary>
        public void SendPlayerDie()
        {

        }
        [MessageHandler(MessageEnum.PlayerDie)]
        public void ReceivePlayerDie()
        {

        }
    }
}