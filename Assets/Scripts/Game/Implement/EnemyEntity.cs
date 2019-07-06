using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EnemyEntity : PawnBase
    {
        private const float cRadius = 0.85f;
        private Vector2 cOffset = new Vector2(0, 1.2f);

        public float MoveSpeed = 1.5f;
        public float MaxHp = 1;
        public float Damage = 1;
        private Vector2 tempDir;

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            RefreshModel("EnemyModel");
            Tag = TagsUtil.Enemy;
            RefreshCollider(cRadius);
            collider.offset = cOffset;
            base.Init(pos, fwd);
        }

        protected override void InitStatus()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.AddState("StateDead", new StateDead(this));
            hfsm.Init("StateIdle");
        }

        protected override void Update()
        {
            TempMoveToTargetPos();
            base.Update();
        }

        protected override void InitPropertyPool()
        {
            propertyMoveSpeed = propertyPool.CreateProperty(PropertyEnum.MoveSpeed, MoveSpeed);
            propertyMaxHp = propertyPool.CreateProperty(PropertyEnum.MaxHp, MaxHp);
            propertyCurHp = propertyPool.CreateProperty(PropertyEnum.CurHp, MaxHp);
            propertyAttackDamage = propertyPool.CreateProperty(PropertyEnum.AttackDamage, Damage);
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

        public override string GetAnimNameByState(string nameBase)
        {
            switch (nameBase.ToLower())
            {
                case "idle": return "idle";
                case "move": return "walk";
                default: return nameBase;
            }
        }
    }
}