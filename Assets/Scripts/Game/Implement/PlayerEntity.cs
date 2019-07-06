using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PlayerEntity : PawnBase
    {
        private const float cRadius = 0.85f;
        private Vector2 cOffset = new Vector2(0, 1.2f);

        private Vector2 moveCache;

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            RefreshModel("PlayerModel");
            Tag = TagsUtil.Player;
            RefreshCollider(cRadius);
            collider.offset = cOffset;
            base.Init(pos, fwd);
        }

        protected override void InitStatus()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.AddState("StateAttack", new StateAttack(this));
            hfsm.AddState("StateDead", new StateDead(this));
            hfsm.Init("StateIdle");
        }

        protected override void Update()
        {
            moveCache.x = Input.GetAxis("Horizontal");
            moveCache.y = Input.GetAxis("Vertical");

            if (moveCache.magnitude > 0)
            {
                TempMove(moveCache);
            }
            else TempStopMove();

            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack(AttackDirEnum.Right);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                Attack(AttackDirEnum.Up);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Attack(AttackDirEnum.Down);
            }

            base.Update();
        }

        protected override void InitPropertyPool()
        {
            propertyMoveSpeed = propertyPool.CreateProperty(PropertyEnum.MoveSpeed, 5f);
            propertyMaxHp = propertyPool.CreateProperty(PropertyEnum.MaxHp, 4);
            propertyCurHp = propertyPool.CreateProperty(PropertyEnum.CurHp, 4);
            propertyAttackDamage = propertyPool.CreateProperty(PropertyEnum.AttackDamage, 1);
        }

        protected void TempMove(Vector2 dir)
        {
            Post("State.ChangeState.Move");
            Post(new HFSMEvent("Action.Move", dir));
        }

        protected void TempStopMove()
        {
            Post("Action.StopMove");
        }

        public override string GetAnimNameByState(string nameBase)
        {
            switch (nameBase)
            {
                case "idle": return "idle";
                case "move": return "walk";
                case "attack_Up": return "attackup";
                case "attack_Down": return "attackdown";
                case "attack_Left": return "attackright";
                case "attack_Right": return "attackright";
                default:
                    return null;
            }
        }
    }
}