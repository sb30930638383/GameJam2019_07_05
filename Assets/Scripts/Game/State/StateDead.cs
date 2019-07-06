using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateDead : StateWithEventMap<PawnBase>
    {
        public StateDead(PawnBase owner) : base(owner)
        {
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                if (mOwner is EnemyEntity)
                {
                    MessageManager.Inst.SendEnemyDie(mOwner.Id);
                }
                else if (mOwner is PlayerEntity)
                {
                    mOwner.Post("State.ChangeState.Idle");
                }
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
            AddStateEvent("State.ChangeState.Idle", "StateIdle");
        }

        protected override void ConstructActionEvent()
        {

        }
    }
}