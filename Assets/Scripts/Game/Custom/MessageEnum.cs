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

    }
}