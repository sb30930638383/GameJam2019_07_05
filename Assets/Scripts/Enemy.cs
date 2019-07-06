using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class Enemy : MonoBehaviour
    {

        public float speed = 3;
        public float hp;
        public float hpmax;

        void Start()
        {
            hpmax = hp;
        }

        void Update()
        {
            MoveAtk();
        }

        public void MoveAtk()
        {
            if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) <= 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed * 1.25f * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed * Time.deltaTime);
            }

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Player")
            {
                Death();
            }
        }

        public void Death()
        {
            GameObjectPool.instance.CollectObject(gameObject);
            hp = hpmax;
        }
    }

}