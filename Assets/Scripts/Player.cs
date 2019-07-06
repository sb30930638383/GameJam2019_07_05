using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam2019;


namespace GameJam2019
{
    public class Player : MonoBehaviour
    {

        public int sp = 0;
        public int spMax = 100;

        public int hp = 7;
        public int hpMax = 7;

        public int atk = 1;

        public float speed = 4;

        public Weapon weapon;

        public List<GameObject> enemyProtectList;

        private void Awake()
        {
            weapon = GetComponentInChildren<Weapon>();
        }

        public void ClearEnemyList()
        {
            enemyProtectList.Clear();
        }

       public void Attack()
       {
       
       }
       
       public void SkillBladeWind()
       {
       
       }
       
       public void SkillSpeedUp()
       {
       
       }
       
       public void SkillHealing()
       {
       
       }
       
       public void SkillAllAttack()
       {
       
       }
       private void OnTriggerEnter2D(Collider2D collision)
       {
           if (collision.tag == "Enemy")
           {
               collision.GetComponent<Enemy>().Death();
           }
       }


    }
}

