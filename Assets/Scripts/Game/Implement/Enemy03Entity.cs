using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class Enemy03Entity : EnemyEntity
    {
        private const float cRadius = 1.1f;
        private Vector2 cOffset = new Vector2(0f, 1f);
        private const float cMoveSpeed = 0.8f;
        private float cMaxHp = 1;
        private float cDamage = 1;

        private Vector2 tempDir;

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            cModelName = "Enemy03Model";
            RefreshCollider(cRadius);
            collider.offset = cOffset;
            base.Init(pos, fwd);
        }

        protected override void InitStatus()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.AddState("StateDead", new StateDead(this));
            hfsm.AddState("StateBorn", new StateBorn(this));
            hfsm.Init("StateBorn");
        }

        protected override void Update()
        {
            if (Global.IsPerform) return;
            base.Update();
        }

        protected override void InitPropertyPool()
        {
            propertyMoveSpeed = propertyPool.CreateProperty(PropertyEnum.MoveSpeed, cMoveSpeed);
            propertyMaxHp = propertyPool.CreateProperty(PropertyEnum.MaxHp, cMaxHp);
            propertyCurHp = propertyPool.CreateProperty(PropertyEnum.CurHp, cMaxHp);
            propertyAttackDamage = propertyPool.CreateProperty(PropertyEnum.AttackDamage, cDamage);
        }

        protected override void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag(TagsUtil.Player))
            {
                PlayerEntity player = EntityManager.Inst.GetEntity<PlayerEntity>(col.gameObject);
                if (player != null)
                    MessageManager.Inst.SendDamagePawn(player.Id, propertyAttackDamage.ValueFixed);
            }
            base.OnCollisionEnter2D(col);
        }

        private void TempMoveToTargetPos()
        {
            if (Global.Player == null)
            {
                Post("Action.StopMove");
                return;
            }
            tempDir = (Global.Player.Position - Position).normalized;
            Post("State.ChangeState.Move");
            Post(new HFSMEvent("Action.Move", tempDir));
        }

        public override void OnDie()
        {
            EntityManager.Inst.CreateEnemyBody(Position);
            base.OnDie();
        }

        public override string GetAnimNameByState(string nameBase)
        {
            switch (nameBase.ToLower())
            {
                case "idle": return "idle";
                case "move": return "walk";
                case "show": return "show";
                default: return nameBase;
            }
        }
    }
}