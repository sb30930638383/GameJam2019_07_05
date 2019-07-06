using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class StateAttack : StateWithEventMap<PawnBase>
    {
        private TrackEntry trackEntry;
        private AttackDirEnum atkDir;
        private string animName;

        public StateAttack(PawnBase owner) : base(owner)
        {
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(objs =>
            {
                mOwner.ModiflyAction(ActionFlagEnum.Rotate, false);
                atkDir = (AttackDirEnum)objs[0];
                animName = mOwner.GetAnimNameByState(string.Format("attack_{0}", atkDir.ToString()));
                trackEntry = mOwner.PlayAnimation(1, animName, false, OnComplete);
                if(trackEntry != null)
                    trackEntry.Event += AnimEvent;
            });
        }

        private void AnimEvent(TrackEntry trackEntry, Spine.Event e)
        {
            Debug.LogError(e.Data.Name);
        }

        protected override void ConstructOver()
        {
            AddActionOnOver(() =>
            {
                mOwner.ModiflyAction(ActionFlagEnum.Rotate, true);
                mOwner.ArmatureControl.SetEmptyAnim(1);
            });
        }

        private void OnComplete(Spine.TrackEntry trackEntry)
        {
            mOwner.Post("State.ChangeState.Idle");
        }

        protected override void ConstructStateEvent()
        {
            AddStateEvent("State.ChangeState.Idle", "StateIdle");
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
            mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("move"), true);
        }

        private void DoStopMove()
        {
            mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("idle"), true);
        }
    }
}