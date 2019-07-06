using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam2019
{
    public class SkillEntity01 : SkillEntityBase
    {
        private const float cDamage = 999;
        private const float cMoveSpeed = 7f;
        private const float cRadius = 0.8f;

        private List<Collider2D> ignoreList = new List<Collider2D>();
        private EnemyEntity tempEnemy;

        public override void Init(PawnBase owner, Vector2 pos, Vector2 fwd)
        {
            RefreshModel("Skill01Model");
            RefreshCollider(cRadius);
            collider.isTrigger = true;
            base.Init(owner, pos, fwd);
        }

        protected override void Update()
        {
            Move(Forward * cMoveSpeed * Time.deltaTime);
            base.Update();
        }

        protected override void OnTriggerStay2D(Collider2D col)
        {
            if (!ignoreList.Contains(col))
            {
                tempEnemy = EntityManager.Inst.GetEntity<EnemyEntity>(col.gameObject);
                if (tempEnemy != null) MessageManager.Inst.SendDamagePawn(tempEnemy.Id, cDamage);
                ignoreList.Add(col);
            }
            base.OnTriggerStay2D(col);
        }
    }
}