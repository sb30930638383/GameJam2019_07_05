using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class SkillEntity02 : SkillEntityBase
    {
        public override void Init(PawnBase owner, Vector2 pos, Vector2 fwd)
        {
            RefreshModel("Skill02Model");
            base.Init(owner, pos, fwd);
            PlayAnimation(0, "skill02", false, OnComplete);
        }

        protected override void Update()
        {
            if (owner != null)
                Position = owner.Position;
            base.Update();
        }

        private void OnComplete(Spine.TrackEntry trackEntry)
        {
            OnDie();
        }
    }
}