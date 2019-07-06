using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJam2019
{
    public static class GameUtil
    {
        public static List<EnemyEntity> GetEnemyListByDistance()
        {
            var list = EntityManager.Inst.EnemyDict.Values.ToList();
            if (Global.Player == null) return list;
            EnemyEntity tempEnemy;
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int o = 0; o < list.Count - 1 - i; o++)
                {
                    if (Vector2.Distance(Global.Player.Position, list[o].Position) >
                    Vector2.Distance(Global.Player.Position, list[o + 1].Position))
                    {
                        tempEnemy = list[o];
                        list[o] = list[o + 1];
                        list[o + 1] = tempEnemy;
                    }
                }
            }
            return list;
        }
    }
}