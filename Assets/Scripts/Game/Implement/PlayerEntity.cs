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
            hfsm.Init("StateIdle");
        }

        private void Update()
        {
            moveCache.x = Input.GetAxis("Horizontal");
            moveCache.y = Input.GetAxis("Vertical");

            if (moveCache.magnitude > 0)
            {
                TempMove(moveCache);
            }
            else TempStopMove();
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
    }
}