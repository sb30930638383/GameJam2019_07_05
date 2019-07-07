using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class SpineEffectEntity : EntityBase
    {
        public void Init(Vector2 pos, string modelRes, string animName)
        {
            RefreshModel(modelRes);
            base.Init(pos, Vector2.right);
            PlayAnimation(0, animName, false, OnComplete);
        }

        private void OnComplete(Spine.TrackEntry trackEntry)
        {
            OnDie();
        }
    }
}