using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam2019
{
    public class StateAttack : StateWithEventMap<PawnBase>
    {
        public StateAttack(PawnBase owner) : base(owner)
        {
        }
        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("Attack"), true);
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
            AddStateEvent("State.ChangeState.Attack", "StateAttack");
        }
        protected override void ConstructActionEvent()
        {
            
        }
    }
}