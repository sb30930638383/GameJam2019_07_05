using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateMove : StateWithEventMap<PawnBase>
    {
        private PropertyNode propertyMoveSpeed;

        public StateMove(PawnBase owner) : base(owner)
        {
            propertyMoveSpeed = owner.GetProperty(PropertyEnum.MoveSpeed);
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("move"), true);
                if (mOwner.CompareTag(TagsUtil.Player))
                    mOwner.PlayAnimation(2, mOwner.GetAnimNameByState("move_tui"), true);
            });
        }

        protected override void ConstructOver()
        {

        }

        protected override void ConstructStateEvent()
        {
            AddStateEvent("State.ChangeState.Idle", "StateIdle");
            AddStateEvent("State.ChangeState.Attack", "StateAttack");
            AddStateEvent("State.ChangeState.Dead", "StateDead");
        }

        protected override void ConstructActionEvent()
        {
            AddActionEvent("Action.Move", DoMove);
            AddActionEvent("Action.StopMove", DoStopMove);
        }

        private void DoMove(HFSMEvent evt)
        {
            Vector2 dir = (Vector2)evt.obj0;
            mOwner.Forward = dir;
            mOwner.Move(dir * propertyMoveSpeed.ValueFixed * Time.deltaTime);
        }

        private void DoStopMove()
        {
            mOwner.Post("State.ChangeState.Idle");
        }
    }
}