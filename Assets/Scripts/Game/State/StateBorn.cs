using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateBorn : StateWithEventMap<PawnBase>
    {
        public StateBorn(PawnBase owner) : base(owner)
        {
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(() =>
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("show"), false, OnComplete);
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

        private void OnComplete(Spine.TrackEntry trackEntry)
        {
            mOwner.Post("State.ChangeState.Idle");
        }
    }
}