using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EnemyEntity : PawnBase
    {
        public float speed;
        public float hp;
        public float hpmax;


        public override void Init(Vector2 pos, Vector2 fwd)
        {
            base.Init(pos, fwd);
           // Post("State.ChangeState.Move");
        }

        protected virtual void Start()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));

            hfsm.Init("StateIdle");

        }

        public void Damage(int atk)
        {
            hp -= atk;

            if (hp<=0)
                OnDie();
            
        }

    }
}