using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class EntityBase : ObjectBase
    {
        protected GameObject model;

        public virtual void Init(Vector2 pos, Vector2 fwd)
        {

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