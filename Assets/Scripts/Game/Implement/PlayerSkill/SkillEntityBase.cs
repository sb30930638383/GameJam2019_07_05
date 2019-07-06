using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class SkillEntityBase : EntityBase
    {
        protected PawnBase owner;

        public virtual void Init(PawnBase owner, Vector2 pos, Vector2 fwd)
        {
            this.owner = owner;
            Init(pos, fwd);
        }
    }
}