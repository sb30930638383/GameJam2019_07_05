using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam2019;


namespace GameJam2019
{
    public class Weapon : MonoBehaviour
    {

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (GameManager.instance.player.enemyProtectList.Contains(collision.gameObject))
            {
                return;
            }

            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyEntity>().Damage(GameManager.instance.player.atk);
                GameManager.instance.player.enemyProtectList.Add(collision.gameObject);
            }

        }
    }
}


