using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateMove : StateWithEventMap<PawnBase>
    {
        public StateMove(PawnBase owner) : base(owner)
        {
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("move"), true);
            });
        }

        protected override void ConstructOver()
        {

        }

        protected override void ConstructStateEvent()
        {
            AddStateEvent("State.ChangeState.Idle", "StateIdle");
            AddStateEvent("State.ChangeState.Attack", "StateAttack");
        }

        protected override void ConstructActionEvent()
        {
            AddActionEvent("Action.Move", DoMove);
            AddActionEvent("Action.StopMove", DoStopMove);
        }

        private void DoMove(HFSMEvent evt)
        {
            Vector2 dir = (Vector2)evt.obj0;
            const float tempMoveSpeed = 0.1f;
            mOwner.Forward = dir;
            mOwner.Move(dir * tempMoveSpeed);
        }

        private void DoStopMove()
        {
            mOwner.Post("State.ChangeState.Idle");
        }
    }
}