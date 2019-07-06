using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class GameUtil : MonoBehaviour
    {
        public static void CameraShake()
        {
            //IShake
        }

        private static IEnumerator IShake(Vector2 outShakeVec, float outEulerShakeFloat, float atten = 1f, float duration = 0.6f, float speed = 1f, float eulerAtten = 0.5f)
        {
            float radiusCoef = 0.3f * atten;    //抖动幅度 相关系数
            float shakeSpeed = 10 * speed;      //抖动速度
            Vector2 forPos = Vector2.zero;
            Vector2 curPos = Vector2.zero;
            outShakeVec = forPos;
            Vector2 dir;
            float partDuration = 0;
            while (duration > 0)
            {
                forPos = curPos;
                curPos = UnityEngine.Random.insideUnitCircle * duration * radiusCoef;
                outShakeVec = forPos;
                dir = (curPos - forPos).normalized;
                partDuration = (curPos - forPos).magnitude / shakeSpeed;
                while (partDuration > 0)
                {
                    outShakeVec = forPos + dir * Time.deltaTime;
                    outEulerShakeFloat = outShakeVec.x * eulerAtten;
                    partDuration -= Time.deltaTime;
                    duration -= Time.deltaTime;
                    if (duration <= 0) break;
                    yield return null;
                }
                while (Time.timeScale <= 0)
                {
                    outShakeVec = Vector2.zero;
                    outEulerShakeFloat = 0;
                    yield return null;
                }
            }
            outShakeVec = Vector2.zero;
            outEulerShakeFloat = 0;
        }
    }
}