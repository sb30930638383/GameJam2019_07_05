using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PlayerEntity : PawnBase
    {
        private Vector2 moveCache;

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            RefreshModel("PlayerModel");
            base.Init(pos, fwd);
        }

        private void Start()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.AddState("StateAttack", new StateAttack(this));
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