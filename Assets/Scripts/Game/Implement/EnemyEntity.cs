using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EnemyEntity : PawnBase
    {
        public override void Init(Vector2 pos, Vector2 fwd)
        {
            base.Init(pos, fwd);
        }

        protected virtual void Start()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.Init("StateIdle");
        }
    }
}