﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam2019;
using System;

namespace GameJam2019
{
    public class Weapon : MonoBehaviour
    {
        private List<Collider2D> ignoreList = new List<Collider2D>();
        private Action<Vector2> onHit;
        private float damage;
        private string targetTag;

        public void Init(string targetTag)
        {
            this.targetTag = targetTag;
        }

        public void SetOnHit(Action<Vector2> onHit)
        {
            this.onHit = onHit;
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Apply(col);
        }

        private void Apply(Collider2D col)
        {
            if (string.IsNullOrEmpty(targetTag) || ignoreList.Contains(col))
                return;
            if (col.CompareTag(targetTag))
            {
                var enemy = EntityManager.Inst.GetEntity<EnemyEntity>(col.gameObject);
                if (enemy != null)
                    MessageManager.Inst.SendDamagePawn(enemy.Id, damage);
                ignoreList.Add(col);

                if (onHit != null) onHit(col.transform.position);
            }
        }

        public void ClearCoolingTime()
        {
            ignoreList.Clear();
        }
    }
}


