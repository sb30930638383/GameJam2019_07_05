using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam2019
{
    public class SkillEntity01 : SkillEntityBase
    {
        private const float cDamage = 999;
        private const float cMoveSpeed = 9f;
        private const float cRadius = 0.8f;
        private const float cLifeDuration = 10;

        private List<Collider2D> ignoreList = new List<Collider2D>();
        private EnemyEntity tempEnemy;
        private float duration;

        public override void Init(PawnBase owner, Vector2 pos, Vector2 fwd)
        {
            RefreshModel("Skill01Model");
            RefreshCollider(cRadius);
            collider.isTrigger = true;
            base.Init(owner, pos, fwd);
        }

        protected override void Update()
        {
            if ((duration += Time.deltaTime) >= cLifeDuration)
            {
                OnDie();
                return;
            }
            Move(Forward * cMoveSpeed * Time.deltaTime);
            base.Update();
        }

        protected override void OnTriggerStay2D(Collider2D col)
        {
            if (!ignoreList.Contains(col))
            {
                tempEnemy = EntityManager.Inst.GetEntity<EnemyEntity>(col.gameObject);
                if (tempEnemy != null)
                {
                    MessageManager.Inst.SendDamagePawn(tempEnemy.Id, cDamage);
                    CameraController.Inst.Shake(4f, 0.3f, 3f, 0);
                }
                ignoreList.Add(col);
            }
            base.OnTriggerStay2D(col);
        }
    }
}