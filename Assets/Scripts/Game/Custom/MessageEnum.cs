namespace GameJam2019
{
    public enum MessageEnum
    {
        None = -1,

        /// <summary>
        /// 打印log.
        /// </summary>
        Log = 101,

        /// <summary>
        /// 增加游戏分数.
        /// </summary>
        AddGameScore = 102,

        /// <summary>
        /// 伤害某个活体.
        /// </summary>
        DamagePawn = 103,

        /// <summary>
        /// 怪物死亡.
        /// </summary>
        EnemyDie = 104,

        /// <summary>
        /// 清空当前所有怪物.
        /// </summary>
        ClearEnemy = 105,

        /// <summary>
        /// 开始播放音乐.
        /// </summary>
        PlayMusic = 106,

        /// <summary>
        /// 玩家死亡.
        /// </summary>
        PlayerDie = 107,

        /// <summary>
        /// 重置当前关卡.
        /// </summary>
        ResetCurrentLevel = 108,
    }
}