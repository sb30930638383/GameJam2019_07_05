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
        private Dictionary<GameObject, EntityBase> entityObjectDict = new Dictionary<GameObject, EntityBase>();

        public T GetEntity<T>(int id) where T : EntityBase
        {
            EntityBase entity;
            entityDict.TryGetValue(id, out entity);
            return entity as T;
        }

        public T GetEntity<T>(GameObject obj) where T : EntityBase
        {
            EntityBase entity;
            entityObjectDict.TryGetValue(obj, out entity);
            return entity as T;
        }

        public T CreateEnemy<T>(Vector2 pos, Vector2 fwd) where T : EnemyEntity
        {
            T t = EntityBase.FactoryCreate<T>();
            t.Init(pos, fwd);
            entityDict.Add(t.Id, t);
            entityObjectDict.Add(t.gameObject, t);
            return t;
        }

        public PlayerEntity CreatePlayer(Vector2 pos, Vector2 fwd)
        {
            PlayerEntity entity = EntityBase.FactoryCreate<PlayerEntity>();
            entity.Init(pos, fwd);
            entityDict.Add(entity.Id, entity);
            entityObjectDict.Add(entity.gameObject, entity);
            Global.Player = entity;
            return entity;
        }

        public void RemoveEntity(EntityBase entity)
        {
            if (entity == null) return;
            if (entityDict.ContainsKey(entity.Id))
                entityDict.Remove(entity.Id);
            if (entityObjectDict.ContainsKey(entity.gameObject))
                entityObjectDict.Remove(entity.gameObject);
        }
    }
}