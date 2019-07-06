using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EntityManager
    {
        public static EntityManager Inst { get { return inst; } }
        private static EntityManager inst = new EntityManager();

        private Dictionary<int, EntityBase> entityDict = new Dictionary<int, EntityBase>();

        public T CreateEnemy<T>(Vector2 pos, Vector2 fwd) where T : EnemyEntity
        {
            T t = EntityBase.FactoryCreate<T>();
            t.Init(pos, fwd);
            entityDict.Add(t.Id, t);
            return t;
        }

        public PlayerEntity CreatePlayer(Vector2 pos, Vector2 fwd)
        {
            PlayerEntity entity = EntityBase.FactoryCreate<PlayerEntity>();
            entity.Init(pos, fwd);
            entityDict.Add(entity.Id, entity);
            Global.Player = entity;
            return entity;
        }
    }
}