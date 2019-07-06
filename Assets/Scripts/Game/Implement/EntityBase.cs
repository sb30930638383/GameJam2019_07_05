using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

namespace GameJam2019
{
    public class EntityBase : ObjectBase
    {
        public Vector2 Position { get { return transform.position; } set { transform.position = value; RefreshDeepZ(); } }
        public Vector2 Forward { get { return forward; } set { forward = value.normalized; RefreshForward(); } }
        public ArmatureController ArmatureControl { get { return armatureControl; } }
        public GameObject Model { get { return model; } }
        public string Tag { get { return tag; } set { transform.SetTagWithChild(value); } }

        protected Vector3 tempDeep;
        protected GameObject model;
        protected new CircleCollider2D collider;
        protected new Rigidbody2D rigidbody;
        protected ArmatureController armatureControl;
        private Vector2 forward;

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

        public void Move(Vector2 dir)
        {
            Position += dir;
        }

        /// <summary>
        /// 刷新模型.
        /// </summary>
        public virtual void RefreshModel(string resName)
        {
            DesModel();
            CreateModel(resName);
        }

        public virtual void RefreshDeepZ()
        {
            tempDeep = Position;
            tempDeep.z = tempDeep.y;
            transform.position = tempDeep;
        }

        protected void CreateModel(string resName)
        {
            if (model == null)
            {
                model = ResManager.Inst.Load<GameObject>(resName, ResTypeEnum.Model);
                model = Instantiate(model, transform);
                armatureControl.Init(model.GetComponent<SkeletonAnimation>());
                armatureControl.SetFlipRatio(0.4f);
                armatureControl.SortingLayerName = SortingLayerUtil.Entity;
            }
        }

        public void RefreshCollider(float radius)
        {
            if (collider == null)
            {
                collider = gameObject.AddComponent<CircleCollider2D>();
            }
            collider.radius = radius;
            if (rigidbody == null)
            {
                rigidbody = gameObject.AddComponent<Rigidbody2D>();
                rigidbody.mass = 1000;
                rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rigidbody.freezeRotation = false;
                rigidbody.gravityScale = 0;
                rigidbody.drag = 1000;
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

        public TrackEntry PlayAnimation(int trackIndex, string animName, bool loop, Spine.AnimationState.TrackEntryDelegate onFinish = null)
        {
            return armatureControl.Play(trackIndex, animName, loop, onFinish);
        }

        protected virtual void OnCollisionEnter2D(Collision2D col) { }

        protected virtual void OnTriggerEnter2D(Collider2D col) { }

        protected virtual void OnTriggerStay2D(Collider2D col) { }

        /// <summary>
        /// 死亡时.
        /// </summary>
        public virtual void OnDie()
        {
            EntityManager.Inst.RemoveEntity(this);
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