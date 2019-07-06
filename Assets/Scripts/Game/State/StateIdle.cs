using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateIdle : StateWithEventMap<PawnBase>
    {
        public StateIdle(PawnBase owner) : base(owner)
        {
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("idle"), true);
                if (mOwner.CompareTag(TagsUtil.Player))
                    mOwner.PlayAnimation(2, mOwner.GetAnimNameByState("idle_tui"), true);
            });
        }

        protected override void ConstructOver()
        {
            AddActionOnOver(() =>
            {

            });
        }

        protected override void ConstructStateEvent()
        {
            AddStateEvent("State.ChangeState.Move", "StateMove");
            AddStateEvent("State.ChangeState.Attack", "StateAttack");
            AddStateEvent("State.ChangeState.Dead", "StateDead");
        }

        protected override void ConstructActionEvent()
        {

        }
    }
}