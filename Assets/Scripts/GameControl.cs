using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class GameControl : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {
            PlayerMove();
            SkillCast();
        }

        private void PlayerMove()
        {
            float hMove = Input.GetAxis("Horizontal") * GameManager.instance.player.speed;
            float vMove = Input.GetAxis("Vertical") * GameManager.instance.player.speed;
            Vector3 translation = new Vector2(hMove, vMove) * Time.deltaTime;
            transform.Translate(translation);
        }

        private void SkillCast()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.instance.player.sp >= 20)
            {

                GameManager.instance.player.SkillBladeWind();
                GameManager.instance.player.sp -= 20;

            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && GameManager.instance.player.sp >= 40)
            {
                GameManager.instance.player.SkillSpeedUp();
                GameManager.instance.player.sp -= 40;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && GameManager.instance.player.sp >= 60)
            {
                GameManager.instance.player.SkillHealing();
                GameManager.instance.player.sp -= 60;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && GameManager.instance.player.sp >= 100)
            {
                GameManager.instance.player.SkillAllAttack();
                GameManager.instance.player.sp -= 100;
            }
        }
    }
}
