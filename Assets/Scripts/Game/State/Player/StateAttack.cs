﻿using Spine;
using System;
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
        private List<Weapon> weaponList = new List<Weapon>();
        private Action onHitOn;
        private PropertyNode propertyAttackDamage;
        private bool ignoreMoveAnim;


        public StateAttack(PawnBase owner) : base(owner)
        {
            propertyAttackDamage = mOwner.GetProperty(PropertyEnum.AttackDamage);

            weaponList.Add(owner.Model.transform.Find("HitCollider_Up").GetComponent<Weapon>());
            weaponList.Add(owner.Model.transform.Find("HitCollider_Down").GetComponent<Weapon>());
            weaponList.Add(owner.Model.transform.Find("HitCollider_Right").GetComponent<Weapon>());
            weaponList.Add(weaponList[2]);

            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].Init(TagsUtil.Enemy);
                weaponList[i].SetOnHit(OnHit);
            }
            RefreshColliderActive(-1);
        }

        protected override void ConstructStart()
        {
            AddActionOnStart(objs =>
            {
                mOwner.ModiflyAction(ActionFlagEnum.Rotate, false);
                atkDir = (AttackDirEnum)objs[0];
                onHitOn = objs[1] as Action;
                RefreshAttackDamage();
                animName = mOwner.GetAnimNameByState(string.Format("attack_{0}", atkDir.ToString()));
                trackEntry = mOwner.PlayAnimation(1, animName, false, OnComplete);
                if (trackEntry != null)
                {
                    trackEntry.Event += AnimEvent;
                }
            });

        }

        private void AnimEvent(TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == "hit_on")
            {
                RefreshColliderActive((int)atkDir);
                if (onHitOn != null)
                {
                    onHitOn();
                    onHitOn = null;
                }
            }
            else if (e.Data.Name == "hit_off")
            {
                RefreshColliderActive(-1);
            }
        }

        protected override void ConstructOver()
        {
            AddActionOnOver(() =>
            {
                RefreshColliderActive(-1);
                ClearCoolingTime();
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
            AddStateEvent("State.ChangeState.Dead", "StateDead");
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
            if (!ignoreMoveAnim)
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("move"), true);
                mOwner.PlayAnimation(2, mOwner.GetAnimNameByState("move_tui"), true);
            }
            ignoreMoveAnim = true;
        }

        private void DoStopMove()
        {
            if (ignoreMoveAnim)
            {
                mOwner.PlayAnimation(0, mOwner.GetAnimNameByState("idle"), true);
                mOwner.PlayAnimation(2, mOwner.GetAnimNameByState("idle_tui"), true);
            }
            ignoreMoveAnim = false;
        }

        private void RefreshColliderActive(int index)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetActive(index == i);
            }
        }

        private void RefreshAttackDamage()
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].SetDamage(propertyAttackDamage.ValueFixed);
            }
        }

        private void ClearCoolingTime()
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                weaponList[i].ClearCoolingTime();
            }
        }

        private void OnHit(Vector2 point)
        {
            EntityManager.Inst.CreateSpineEffect(point, "HitEffectModel", "kanji");
            CameraController.Inst.Shake(4f, 0.3f, 3f, 0);
        }
    }
}