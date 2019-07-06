using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PawnBase : EntityBase, IStateable
    {
        protected readonly HFSM hfsm = new HFSM();
        protected readonly PropertyPool propertyPool = new PropertyPool();
        protected PropertyNode propertyMoveSpeed;
        protected PropertyNode propertyMaxHp;
        protected PropertyNode propertyCurHp;
        protected PropertyNode propertyAttackDamage;
        private Dictionary<ActionFlagEnum, int> actionFlagDict = new Dictionary<ActionFlagEnum, int>();

        public override void Init(Vector2 pos, Vector2 fwd)
        {
            InitActionFlag(ActionFlagEnum.Move);
            InitActionFlag(ActionFlagEnum.Rotate);
            InitPropertyPool();
            InitStatus();
            base.Init(pos, fwd);
        }

        protected override void Update()
        {
            hfsm.Update();
            base.Update();
        }

        protected virtual void InitPropertyPool()
        {

        }

        protected virtual void InitStatus()
        {

        }

        public PropertyNode GetProperty(PropertyEnum propertyType)
        {
            return propertyPool.GetProperty(propertyType);
        }

        protected override void RefreshForward()
        {
            if (!GetActionFlag(ActionFlagEnum.Rotate))
                return;
            base.RefreshForward();
        }

        public void Move(Vector2 dir)
        {
            Position += dir;
        }

        public void Attack(AttackDirEnum atkDir)
        {
            Post(new HFSMEvent("State.ChangeState.Attack", atkDir));
        }

        public void InitActionFlag(ActionFlagEnum actionFlag)
        {
            if (!actionFlagDict.ContainsKey(actionFlag))
                actionFlagDict.Add(actionFlag, 0);
        }

        public void ModiflyAction(ActionFlagEnum actionFlag, bool active)
        {
            System.Diagnostics.Debug.Assert(!actionFlagDict.ContainsKey(actionFlag),
                string.Format("error: action flag {0} not initialized.", actionFlag.ToString()));
            actionFlagDict[actionFlag] += active ? 1 : -1;
        }

        public bool GetActionFlag(ActionFlagEnum actionFlag)
        {
            System.Diagnostics.Debug.Assert(!actionFlagDict.ContainsKey(actionFlag),
                string.Format("error: action flag {0} not initialized.", actionFlag.ToString()));
            return actionFlagDict[actionFlag] >= 0;
        }

        public virtual void OnDamage(float damage)
        {
            propertyCurHp.AddValueAdd(-damage);
            if (propertyCurHp.ValueFixed <= 0)
            {
                Post("State.ChangeState.Dead");
            }
        }

        public bool Post(string msg)
        {
            return hfsm.Post(msg);
        }

        public bool Post(HFSMEvent evt)
        {
            return hfsm.Post(evt);
        }

        public virtual string GetAnimNameByState(string nameBase)
        {
            return nameBase;
        }
    }
}