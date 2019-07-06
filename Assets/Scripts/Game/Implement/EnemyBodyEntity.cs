using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EnemyBodyEntity : EntityBase
    {
        public override void Init(Vector2 pos, Vector2 fwd)
        {
            RefreshModel("EnemyBodyModel");
            armatureControl.SortingLayerName = SortingLayerUtil.Ground;
            base.Init(pos, fwd);
            string animName = string.Format("dead0{0}", Random.Range(1, 5));
            PlayAnimation(0, animName, false, OnComplete);
        }

        private void OnComplete(Spine.TrackEntry trackEntry)
        {
            OnDie();
        }
    }
}