using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PawnBase : EntityBase, IStateable
    {
        protected readonly HFSM hfsm = new HFSM();

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            base.Init(pos, fwd);
        }

        public void Move(Vector2 dir)
        {
            Position += dir;
        }

        public bool Post(string msg)
        {
            return hfsm.Post(msg);
        }

        public bool Post(HFSMEvent evt)
        {
            return hfsm.Post(evt);
        }

        public void PlayAnimation(int trackIndex, string animName, bool isLoop, Action onFinish = null)
        {

        }

        public string GetAnimNameByState(string nameBase)
        {
            return nameBase;
        }
    }
}