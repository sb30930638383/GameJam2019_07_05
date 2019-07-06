using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace GameJam2019
{
    public class EntityBase : ObjectBase
    {
        public Vector2 Position { get { return transform.position; } set { transform.position = value; } }
        public Vector2 Forward { get { return forward; } set { forward = value.normalized; RefreshForward(); } }
        public ArmatureController ArmatureControl { get { return armatureControl; } }

        protected Vector2 forward;
        protected GameObject model;
        protected ArmatureController armatureControl;

        public EntityBase()
        {
            armatureControl = new ArmatureController();
        }

        public virtual void Init(Vector2 pos, Vector2 fwd)
        {
            Position = pos;
            Forward = fwd;
        }

        protected virtual void Update()
        {
            if (armatureControl != null)
                armatureControl.Update();
        }

        protected virtual void RefreshForward()
        {
            armatureControl.SetForward(forward);
        }

        /// <summary>
        /// 刷新模型.
        /// </summary>
        public virtual void RefreshModel(string resName)
        {
            DesModel();
            CreateModel(resName);
        }

        protected void CreateModel(string resName)
        {
            if (model == null)
            {
                model = ResManager.Inst.Load<GameObject>(resName, ResTypeEnum.Model);
                model = Instantiate(model, transform);
                armatureControl.Init(model.GetComponent<SkeletonAnimation>());
                armatureControl.SetFlipRatio(0.4f);
            }
        }

        protected void DesModel()
        {
            if (model != null)
            {
                Destroy(model);
                model = null;
            }
        }

        /// <summary>
        /// 死亡时.
        /// </summary>
        public virtual void OnDie()
        {
            DestroySelf();
        }

        /// <summary>
        /// 被强行销毁.
        /// </summary>
        public virtual void DestroySelf()
        {
            CancelInvoke();
            Destroy(gameObject);
        }
    }
}