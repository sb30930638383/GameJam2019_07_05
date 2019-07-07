using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using System;

namespace GameJam2019
{
    public class ArmatureController
    {
        public string SortingLayerName { get { return meshRender.sortingLayerName; } set { meshRender.sortingLayerName = value; } }
        public int SortingOrder { get { return meshRender.sortingOrder; } set { meshRender.sortingOrder = value; } }
        public float FlipX { get { return armature.Skeleton.ScaleX; } set { armature.Skeleton.ScaleX = value; } }

        private SkeletonAnimation armature;
        private MeshRenderer meshRender;
        private TrackEntry tempTrackInfo;
        private Color tempColor = Color.white;
        private bool curFlipX;
        private float flipRatio;

        public void Init(SkeletonAnimation armature)
        {
            System.Diagnostics.Debug.Assert(armature == null, "spine animation component is null.");
            this.armature = armature;
            meshRender = armature.GetComponent<MeshRenderer>();
            curFlipX = false;
        }

        public void Update()
        {
            if (flipRatio > 0)
            {
                FlipX = Mathf.Lerp(FlipX, curFlipX ? 1 : -1, flipRatio);
            }
        }

        public TrackEntry Play(int trackIndex, string animName, bool loop, Spine.AnimationState.TrackEntryDelegate onFinish)
        {
            tempTrackInfo = armature.AnimationState.SetAnimation(trackIndex, animName, loop);
            if (tempTrackInfo != null)
            {
                tempTrackInfo.Complete += onFinish;
            }
            else
            {
                if (onFinish != null)
                    onFinish(null);
            }
            return tempTrackInfo;
        }

        public void SetAlpha(float alpha)
        {
            tempColor.a = alpha;
            armature.Skeleton.SetColor(tempColor);
        }

        public void SetSkin(string skinName)
        {
            armature.Skeleton.SetSkin(skinName);
            armature.Skeleton.SetSlotAttachmentsToSetupPose();
        }

        public void SetEmptyAnim(int trackIndex, float mixDuration = 0.1f)
        {
            armature.AnimationState.SetEmptyAnimation(trackIndex, mixDuration);
        }

        public void SetFlipRatio(float v)
        {
            flipRatio = v;
        }

        public void SetForward(Vector2 fwd)
        {
            if (fwd.x != 0)
            {
                if (flipRatio <= 0)
                    FlipX = fwd.x;
                else curFlipX = fwd.x < 0;
            }
        }
    }
}