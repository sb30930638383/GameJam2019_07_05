using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EntityManager
    {
        public static EntityManager Inst { get { return inst; } }
        private static EntityManager inst = new EntityManager();

        public Dictionary<int, EnemyEntity> EnemyDict { get { return enemyDict; } }

        private Dictionary<int, EntityBase> entityDict = new Dictionary<int, EntityBase>();
        private Dictionary<int, EnemyEntity> enemyDict = new Dictionary<int, EnemyEntity>();
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
            enemyDict.Add(t.Id, t);
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

        public void CreateEnemyBody(Vector2 pos)
        {
            EnemyBodyEntity entity = EntityBase.FactoryCreate<EnemyBodyEntity>();
            entity.Init(pos, Vector2.right);
            entityDict.Add(entity.Id, entity);
            entityObjectDict.Add(entity.gameObject, entity);
        }

        public void CreateSkillEntity<T>(PawnBase owner, Vector2 pos, Vector2 fwd) where T : SkillEntityBase
        {
            SkillEntityBase entity = EntityBase.FactoryCreate<T>();
            entity.Init(owner, pos, fwd);
        }

        public void CreateSpineEffect(Vector2 pos, string modelRes, string animName)
        {
            SpineEffectEntity entity = EntityBase.FactoryCreate<SpineEffectEntity>();
            entity.Init(pos, modelRes, animName);
        }

        public void RemoveEntity(EntityBase entity)
        {
            if (entity == null) return;
            if (entityDict.ContainsKey(entity.Id))
                entityDict.Remove(entity.Id);
            if (entityObjectDict.ContainsKey(entity.gameObject))
                entityObjectDict.Remove(entity.gameObject);
            if (entity.CompareTag(TagsUtil.Enemy))
                enemyDict.Remove(entity.Id);
        }
    }
}