using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PlayerEntity : PawnBase
    {
        private const float cDamageProtectBlinkDuration = 2;
        private const float cDamageProtectBlinkInterval = 0.08f;
        private const float cRadius = 0.85f;
        private Vector2 cOffset = new Vector2(0, 1.2f);

        private Vector2 moveCache;

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            RefreshModel("PlayerModel");
            Tag = TagsUtil.Player;
            RefreshCollider(cRadius);
            collider.offset = cOffset;
            rigidbody.mass = 5000;
            base.Init(pos, fwd);
        }

        protected override void InitStatus()
        {
            hfsm.AddState("StateIdle", new StateIdle(this));
            hfsm.AddState("StateMove", new StateMove(this));
            hfsm.AddState("StateAttack", new StateAttack(this));
            hfsm.AddState("StateDead", new StateDead(this));
            hfsm.Init("StateIdle");
        }

        protected override void Update()
        {
            moveCache.x = Input.GetAxis("Horizontal");
            moveCache.y = Input.GetAxis("Vertical");

            if (moveCache.magnitude > 0)
            {
                TempMove(moveCache);
            }
            else TempStopMove();

            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack(AttackDirEnum.Right);
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                Attack(AttackDirEnum.Up);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Attack(AttackDirEnum.Down);
            }


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                EntityManager.Inst.CreateSkillEntity<SkillEntity01>(this, Position, Forward);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EntityManager.Inst.CreateSkillEntity<SkillEntity02>(this, Position, Forward);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EntityManager.Inst.CreateSkillEntity<SkillEntity03>(this, Position, Forward);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                EntityManager.Inst.CreateSkillEntity<SkillEntity04>(this, Position, Forward);
            }



            base.Update();
        }

        protected override void InitPropertyPool()
        {
            propertyMoveSpeed = propertyPool.CreateProperty(PropertyEnum.MoveSpeed, 5f);
            propertyMaxHp = propertyPool.CreateProperty(PropertyEnum.MaxHp, 4);
            propertyCurHp = propertyPool.CreateProperty(PropertyEnum.CurHp, 4, OnHpValueUpdate);
            propertyAttackDamage = propertyPool.CreateProperty(PropertyEnum.AttackDamage, 1);
            propertySp = propertyPool.CreateProperty(PropertyEnum.SpecialPoint, 200);
        }

        public override void OnDamage(float damage)
        {
            if (!GetActionFlag(ActionFlagEnum.ReceiveDamage))
                return;
            base.OnDamage(damage);
            AddDamageProtect(cDamageProtectBlinkDuration, cDamageProtectBlinkInterval);
        }

        private void OnHpValueUpdate(float curV)
        {
            RefreshSkin((int)curV);
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

        private void RefreshSkin(int curV)
        {
            string skilName = "face01";
            if (curV <= 0) skilName = "kuai04";
            else if (curV <= 1) skilName = "kuai03";
            else if (curV <= 2) skilName = "kuai02";
            else if (curV <= 3) skilName = "kuai01";
            armatureControl.SetSkin(skilName);
        }

        public override string GetAnimNameByState(string nameBase)
        {
            switch (nameBase)
            {
                case "idle": return "idle";
                case "idle_tui": return "idle_tui";
                case "move": return "walk";
                case "move_tui": return "walk_tui";
                case "attack_Up": return "attackup";
                case "attack_Down": return "attackdown";
                case "attack_Left": return "attackright";
                case "attack_Right": return "attackright";
                default:
                    return null;
            }
        }
    }
}